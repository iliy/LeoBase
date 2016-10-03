using AppData.Abstract;
using AppData.Entities;
using AppData.Infrastructure;
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Services
{
    public interface IPersonesService
    {
        PageModel PageModel { get; set; }
        PersonsSearchModel SearchModel { get; set; }
        SearchAddressModel AddressSearchModel { get; set; }
        DocumentSearchModel DocumentSearchModel { get; set; }
        PersonsOrderModel OrderModel { get; set; }
        List<PersoneViewModel> GetPersons();
        int AddNewPersone(PersoneViewModel personeModel);
        bool Remove(int id);
        bool Remove(PersoneViewModel model);
        PersoneViewModel GetPerson(int id, bool loadFullModel = false);
        bool UpdatePersone(PersoneViewModel persone);
    }

    public class PersonesService : IPersonesService
    {
        private IPersoneRepository _personRepository;
        private IPersonePositionRepository _personPositionRepository;
        private IPersoneAddressRepository _personAddressRepository;
        private IPhonesRepository _phonesRepository;
        private IDocumentRepository _documentsRepository;
        private IDocumentTypeRepository _documentsTypeRepository;

        private Dictionary<int, string> _documentTypes;
        public SearchAddressModel AddressSearchModel { get; set; }
        public PersonsSearchModel SearchModel { get; set; }
        public DocumentSearchModel DocumentSearchModel { get; set; }
        public PersonsOrderModel OrderModel { get; set; } = new PersonsOrderModel
        {
            OrderType = OrderType.NONE
        };


        public PageModel PageModel { get; set; } = new PageModel
        {
            ItemsOnPage = 30,
            CurentPage = 1
        };

        
        public PersonesService() : this(RepositoryesFactory.GetInstance().Get<IPersoneRepository>(),
                                        RepositoryesFactory.GetInstance().Get<IPersonePositionRepository>(),
                                        RepositoryesFactory.GetInstance().Get<IPersoneAddressRepository>(),
                                        RepositoryesFactory.GetInstance().Get<IPhonesRepository>(),
                                        RepositoryesFactory.GetInstance().Get<IDocumentRepository>(),
                                        RepositoryesFactory.GetInstance().Get<IDocumentTypeRepository>()) { }

        public PersonesService(IPersoneRepository personRepository, 
                               IPersonePositionRepository personPositionRepository, 
                               IPersoneAddressRepository personAddressRepository, 
                               IPhonesRepository phoneRepository,
                               IDocumentRepository documentRepository,
                               IDocumentTypeRepository documentTypeRepository)
        {
            _personRepository = personRepository;
            _personPositionRepository = personPositionRepository;
            _personAddressRepository = personAddressRepository;
            _phonesRepository = phoneRepository;
            _documentsRepository = documentRepository;
            _documentsTypeRepository = documentTypeRepository;

            _documentTypes = new Dictionary<int, string>();

            foreach(var dT in _documentsTypeRepository.DocumentTypes.ToList())
            {
                _documentTypes.Add(dT.DocumentTypeID, dT.Name);
            }
        }

        /// <summary>
        /// Возвращает список представлений для граждан на основе моделей:
        /// SearchModel - общая модель поиска (фамилия, имя и т.д.)
        /// DocumentSearchModel - модель поиска по документу
        /// AddressSearchModel - модель поиска по адресу
        /// Сперва ищеться по документу, потом по адресу и в последнюю очередь по общей модели поиска
        /// </summary>
        /// <returns>Список представлений граждан, без подробной информации</returns>
        public List<PersoneViewModel> GetPersons()
        {
            List<int> usersIds = new List<int>();

            IQueryable<Persone> persones = null;

            #region Search by ID and Return
            if (SearchModel != null && SearchModel.PersoneID != 0)
            {
                var persone = _personRepository.Persons.FirstOrDefault(p => p.UserID == SearchModel.PersoneID);
                if(persone != null)
                {
                    bool isEmployer = persone.IsEmploeyr;
                    EmploeyrPosition position = null;

                    if (isEmployer)
                    {
                        var p = _personPositionRepository.Positions.FirstOrDefault(pos => pos.PositionID == persone.Position_PositionID);
                        if(p != null)
                        {
                            position = p;
                        }
                    }

                    return new List<PersoneViewModel>
                    {
                        new PersoneViewModel
                        {
                            UserID = persone.UserID,
                            FirstName = persone.FirstName,
                            SecondName = persone.SecondName,
                            MiddleName = persone.MiddleName,
                            IsEmploeyr = persone.IsEmploeyr,
                            DateBirthday = persone.DateBirthday,
                            Position = position != null ? position.Name : null,
                            PositionID = position != null ? position.PositionID : -1
                        }
                    };
                }
            }
            #endregion

            #region SearchByDocument
            if(DocumentSearchModel != null && !DocumentSearchModel.IsEmptySearchModel)
            {
                var documents = _documentsRepository.Documents;
                var documentsType = _documentsTypeRepository.DocumentTypes;
                
                if(!string.IsNullOrEmpty(DocumentSearchModel.DocumentTypeName))
                {
                    var docType = documentsType.FirstOrDefault(dt => dt.Name.Equals(DocumentSearchModel.DocumentTypeName));
                    if(docType != null)
                    {
                        documents = documents.Where(d => d.Document_DocumentTypeID == docType.DocumentTypeID);
                    }
                }

                if (!string.IsNullOrEmpty(DocumentSearchModel.Serial))
                {
                    documents = documents.Where(d => d.Serial.Equals(DocumentSearchModel.Serial));
                }

                if (!string.IsNullOrEmpty(DocumentSearchModel.Number))
                {
                    documents = documents.Where(d => d.Number.Equals(DocumentSearchModel.Number));
                }

                if (!string.IsNullOrEmpty(DocumentSearchModel.IssuedBy))
                {
                    if(DocumentSearchModel.CompareIssuedBy == CompareString.EQUAL)
                    {
                        documents = documents.Where(d => d.IssuedBy.Equals(DocumentSearchModel.IssuedBy));
                    }else
                    {
                        documents = documents.Where(d => d.IssuedBy.Contains(DocumentSearchModel.IssuedBy));
                    }
                }

                if (!string.IsNullOrEmpty(DocumentSearchModel.CodeDevision))
                {
                    if (DocumentSearchModel.CompareCodeDevision == CompareString.EQUAL)
                    {
                        documents = documents.Where(d => d.CodeDevision.Equals(DocumentSearchModel.CodeDevision));
                    }
                    else
                    {
                        documents = documents.Where(d => d.CodeDevision.Contains(DocumentSearchModel.CodeDevision));
                    }
                }

                if(DocumentSearchModel.WhenIssued.Year != 1)
                {
                    if(DocumentSearchModel.CompareWhenIssued == CompareValue.MORE)
                    {
                        documents = documents.Where(d => d.WhenIssued >= DocumentSearchModel.WhenIssued);
                    }else if(DocumentSearchModel.CompareWhenIssued == CompareValue.LESS) {
                        documents = documents.Where(d => d.WhenIssued <= DocumentSearchModel.WhenIssued);
                    }else
                    {
                        documents = documents.Where(d => d.WhenIssued == DocumentSearchModel.WhenIssued);
                    }
                }

                string q = documents.ToString();

                usersIds.AddRange(documents.Select(d => d.Persone.UserID).ToList());
            }
            #endregion

            #region Search By Address
            if (AddressSearchModel != null && !AddressSearchModel.IsEmptyAddress)
            {
                var addresses = _personAddressRepository.Addresses;

                if (!string.IsNullOrEmpty(AddressSearchModel.Country))
                {
                    if (AddressSearchModel.CompareCountry == CompareString.EQUAL)
                    {
                        addresses = addresses.Where(a => a.Country.Equals(AddressSearchModel.Country));        
                    }
                    else
                    {
                        addresses = addresses.Where(a => a.Country.Contains(AddressSearchModel.Country));
                    }
                }

                if (!string.IsNullOrEmpty(AddressSearchModel.Subject))
                {
                    if(AddressSearchModel.CompareSubject == CompareString.EQUAL)
                    {
                        addresses = addresses.Where(a => a.Subject.Equals(AddressSearchModel.Subject));
                    }else
                    {
                        addresses = addresses.Where(a => a.Subject.Contains(AddressSearchModel.Subject));
                    }
                }

                if (!string.IsNullOrEmpty(AddressSearchModel.Area))
                {
                    if(AddressSearchModel.CompareArea == CompareString.EQUAL)
                    {
                        addresses = addresses.Where(a => a.Area.Equals(AddressSearchModel.Area));
                    }else
                    {
                        addresses = addresses.Where(a => a.Area.Contains(AddressSearchModel.Area));
                    }
                }

                if (!string.IsNullOrEmpty(AddressSearchModel.City))
                {
                    if(AddressSearchModel.CompareCity == CompareString.EQUAL)
                    {
                        addresses = addresses.Where(a => a.City.Equals(AddressSearchModel.City));
                    }else
                    {
                        addresses = addresses.Where(a => a.City.Contains(AddressSearchModel.City));
                    }
                }

                if (!string.IsNullOrEmpty(AddressSearchModel.Street))
                {
                    if(AddressSearchModel.CompareStreet == CompareString.EQUAL)
                    {
                        addresses = addresses.Where(a => a.Street.Equals(AddressSearchModel.Street));
                    }else
                    {
                        addresses = addresses.Where(a => a.Street.Contains(AddressSearchModel.Street));
                    }
                }

                if (!string.IsNullOrEmpty(AddressSearchModel.HomeNumber))
                {
                    addresses = addresses.Where(a => a.HomeNumber.Equals(AddressSearchModel.HomeNumber));
                }

                if (!string.IsNullOrEmpty(AddressSearchModel.Flat))
                {
                    addresses = addresses.Where(a => a.Flat.Equals(AddressSearchModel.Flat));
                }

                if (!string.IsNullOrEmpty(AddressSearchModel.Note))
                {
                    if(AddressSearchModel.CompareNote == CompareString.EQUAL)
                    {
                        addresses = addresses.Where(a => a.Note.Equals(AddressSearchModel.Note));
                    }else
                    {
                        addresses = addresses.Where(a => a.Note.Contains(AddressSearchModel.Note));
                    }
                }


                if (usersIds.Count == 0)
                {
                    // Если список с ID пуст то просто добавляем новые ID
                    usersIds.AddRange(addresses.Select(a => a.Persone.UserID).ToList());
                }
                else
                {
                    // Иначе делаем пересечение
                    var aaddd = addresses.Select(a => a.Persone.UserID).ToList();
                    usersIds = usersIds.Where(id => aaddd.Contains(id)).Select(id => id).ToList();
                }

            }
            #endregion

            #region Search by Ids
            if (usersIds.Count != 0)
            {
                persones = _personRepository.Persons;

                persones = persones.Where(p => usersIds.Contains(p.UserID));
            }
            #endregion

            #region Search By Name and Others
            if (SearchModel != null && !SearchModel.IsEmptySearch)
            {
                if(persones == null) persones = _personRepository.Persons;

                if (SearchModel.IsEmployer != null)
                {
                    bool isEmployer = (bool)SearchModel.IsEmployer;

                    persones = persones.Where(p => p.IsEmploeyr == isEmployer);

                    if (isEmployer)
                    {
                        if (!string.IsNullOrEmpty(SearchModel.Position))
                        {
                            var position = _personPositionRepository.Positions.FirstOrDefault(p => p.Name.Equals(SearchModel.Position));

                            if(position != null)
                            {
                                persones = persones.Where(p => p.Position_PositionID == position.PositionID);
                            }
                        }
                    }
                }
                
                if (!string.IsNullOrEmpty(SearchModel.FirstName))
                {
                    if(SearchModel.CompareFirstName == CompareString.EQUAL)
                    {
                        persones = persones.Where(p => p.FirstName.Equals(SearchModel.FirstName));
                    }else
                    {
                        persones = persones.Where(p => p.FirstName.Contains(SearchModel.FirstName));
                    }
                }

                if (!string.IsNullOrEmpty(SearchModel.SecondName))
                {
                    if(SearchModel.CompareSecondName == CompareString.EQUAL)
                    {
                        persones = persones.Where(p => p.SecondName.Equals(SearchModel.SecondName));
                    }else
                    {
                        persones = persones.Where(p => p.SecondName.Contains(SearchModel.SecondName));
                    }
                }

                if (!string.IsNullOrEmpty(SearchModel.MiddleName))
                {
                    if (SearchModel.CompareMiddleName == CompareString.EQUAL)
                    {
                        persones = persones.Where(p => p.MiddleName.Equals(SearchModel.MiddleName));
                    }
                    else
                    {
                        persones = persones.Where(p => p.MiddleName.Contains(SearchModel.MiddleName));
                    }
                }

                if (!string.IsNullOrEmpty(SearchModel.PlaceOfBirth))
                {
                    if(SearchModel.ComparePlaceOfBirth == CompareString.EQUAL)
                    {
                        persones = persones.Where(p => p.PlaceOfBirth.Equals(SearchModel.PlaceOfBirth));
                    }else
                    {
                        persones = persones.Where(p => p.PlaceOfBirth.Contains(SearchModel.PlaceOfBirth));
                    }
                }
                
                if(SearchModel.DateBirthday.Year != 1)
                {
                    if(SearchModel.CompareDate == CompareValue.MORE)
                    {
                        persones = persones.Where(p => p.DateBirthday >= SearchModel.DateBirthday);
                    }else if(SearchModel.CompareDate == CompareValue.LESS){

                        persones = persones.Where(p => p.DateBirthday <= SearchModel.DateBirthday);
                    }
                    else
                    {
                        persones = persones.Where(p => p.DateBirthday == SearchModel.DateBirthday);
                    }
                }

                if(SearchModel.Age > 0 && SearchModel.DateBirthday.Year == 1)
                {
                    int year = DateTime.Now.Year - SearchModel.Age;
                    int mounth = DateTime.Now.Month;
                    int day = DateTime.Now.Month;

                    var dateBirth = new DateTime(year, mounth, day);

                    if (SearchModel.CompareAge == CompareValue.MORE)
                    {
                        persones = persones.Where(p => p.DateBirthday <= dateBirth);
                    }else if(SearchModel.CompareAge == CompareValue.LESS)
                    {
                        persones = persones.Where(p => p.DateBirthday >= dateBirth);
                    }
                    else
                    {
                        persones = persones.Where(p => p.DateBirthday.Year == dateBirth.Year
                                                            && (p.DateBirthday.Month < dateBirth.Month
                                                                || p.DateBirthday.Month == dateBirth.Month
                                                                   && p.DateBirthday.Day <= dateBirth.Day));
                    }
                }

                if(SearchModel.WasBeCreated.Year != 1)
                {
                    if(SearchModel.CompareWasBeCreated == CompareValue.MORE)
                    {
                        persones = persones.Where(p => p.WasBeCreated >= SearchModel.WasBeCreated);
                    }
                    else if(SearchModel.CompareWasBeCreated == CompareValue.LESS)
                    {

                        persones = persones.Where(p => p.WasBeCreated <= SearchModel.WasBeCreated);
                    }
                    else
                    {
                        persones = persones.Where(p => p.WasBeCreated == SearchModel.WasBeCreated);
                    }
                }

                if(SearchModel.WasBeUpdated.Year != 1)
                {
                    if (SearchModel.CompareWasBeUpdated == CompareValue.MORE)
                    {
                        persones = persones.Where(p => p.WasBeUpdated >= SearchModel.WasBeUpdated);
                    }
                    else if (SearchModel.CompareWasBeUpdated == CompareValue.LESS)
                    {

                        persones = persones.Where(p => p.WasBeUpdated <= SearchModel.WasBeUpdated);
                    }
                    else
                    {
                        persones = persones.Where(p => p.WasBeUpdated == SearchModel.WasBeUpdated);
                    }
                }

                if (!string.IsNullOrEmpty(SearchModel.PlaceWork))
                {
                    if(SearchModel.ComparePlaceWork == CompareString.EQUAL)
                    {
                        persones = persones.Where(p => p.PlaceWork.Equals(SearchModel.PlaceWork));
                    }else
                    {
                        persones = persones.Where(p => p.PlaceWork.Contains(SearchModel.PlaceWork));
                    }
                }

            }
            #endregion

            #region Orders
            if (OrderModel.OrderType != OrderType.NONE)
            {
                if (persones == null) persones = _personRepository.Persons;

                if (OrderModel.OrderType == OrderType.DESC)
                {
                    switch (OrderModel.OrderProperties)
                    {
                        case PersonsOrderProperties.AGE:
                            persones = persones.OrderBy(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.DATE_BERTHDAY:
                            persones = persones.OrderByDescending(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.FIRST_NAME:
                            persones = persones.OrderByDescending(p => p.FirstName);
                            break;
                        case PersonsOrderProperties.MIDDLE_NAME:
                            persones = persones.OrderByDescending(p => p.MiddleName);
                            break;
                        case PersonsOrderProperties.SECOND_NAME:
                            persones = persones.OrderByDescending(p => p.SecondName);
                            break;
                        case PersonsOrderProperties.WAS_BE_CREATED:
                            persones = persones.OrderByDescending(p => p.WasBeCreated);
                            break;
                        case PersonsOrderProperties.WAS_BE_UPDATED:
                            persones = persones.OrderByDescending(p => p.WasBeUpdated);
                            break;
                    }
                }
                else
                {
                    switch (OrderModel.OrderProperties)
                    {
                        case PersonsOrderProperties.AGE:
                            persones = persones.OrderByDescending(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.DATE_BERTHDAY:
                            persones = persones.OrderBy(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.FIRST_NAME:
                            persones = persones.OrderBy(p => p.FirstName);
                            break;
                        case PersonsOrderProperties.MIDDLE_NAME:
                            persones = persones.OrderBy(p => p.MiddleName);
                            break;
                        case PersonsOrderProperties.SECOND_NAME:
                            persones = persones.OrderBy(p => p.SecondName);
                            break;
                        case PersonsOrderProperties.WAS_BE_CREATED:
                            persones = persones.OrderBy(p => p.WasBeCreated);
                            break;
                        case PersonsOrderProperties.WAS_BE_UPDATED:
                            persones = persones.OrderBy(p => p.WasBeUpdated);
                            break;
                    }
                }
            }

            #endregion

            #region Make total items into PageModel
            if (persones == null) persones = _personRepository.Persons;

            PageModel.TotalItems = persones.Count();
            #endregion

            #region Paginations
            if (PageModel.ItemsOnPage != -1)
            {
                persones = persones.Skip(PageModel.ItemsOnPage * (PageModel.CurentPage - 1)).Take(PageModel.ItemsOnPage);
            }
            #endregion

            return persones
                    .Select(p =>
                        new PersoneViewModel
                        {
                            WasBeCreated = p.WasBeCreated,
                            WasBeUpdated = p.WasBeUpdated,
                            UserID = p.UserID,
                            FirstName = p.FirstName,
                            SecondName = p.SecondName,
                            MiddleName = p.MiddleName,
                            IsEmploeyr = p.IsEmploeyr,
                            DateBirthday = p.DateBirthday,
                            PositionID = p.Position_PositionID,
                            Image = p.Image
                        }
                    ).ToList();
        }

        public int AddNewPersone(PersoneViewModel personeModel)
        {
            var dt = _documentsTypeRepository.DocumentTypes;
            Dictionary<string, int> documentTypes = new Dictionary<string, int>();

            EmploeyrPosition position = null;

            if (personeModel.IsEmploeyr)
            {
                position = _personPositionRepository.Positions.FirstOrDefault(p => p.Name.Equals(personeModel.Position));

                if (position == null) throw new ArgumentException("Неизвестная должность!");
            }

            foreach (var d in dt)
            {
                documentTypes.Add(d.Name, d.DocumentTypeID);
            }

            #region make persone model
            Persone persone = new Persone
            {
                IsEmploeyr = personeModel.IsEmploeyr,
                Position_PositionID = personeModel.IsEmploeyr ? position.PositionID : -1,
                FirstName = personeModel.FirstName,
                SecondName = personeModel.SecondName,
                MiddleName = personeModel.MiddleName,
                DateBirthday = personeModel.DateBirthday,
                WasBeUpdated = DateTime.Now,
                WasBeCreated = DateTime.Now,
                PlaceOfBirth = personeModel.PlaceOfBirth,
                Image = personeModel.Image,
                Address = personeModel.Addresses != null ?
                                        personeModel.Addresses.Select(a =>
                                        new PersoneAddress
                                        {
                                            Country = a.Country,
                                            Subject = a.Subject,
                                            Area = a.Area,
                                            City = a.City,
                                            Street = a.Street,
                                            HomeNumber = a.HomeNumber,
                                            Flat = a.Flat,
                                            Note = a.Note
                                        }
                                        ).ToList()
                                        : new List<PersoneAddress>(),
                Documents = personeModel.Documents != null ?
                                        personeModel.Documents.Select(d =>
                                        new Document
                                        {
                                            Document_DocumentTypeID = documentTypes.ContainsKey(d.DocumentTypeName) ?
                                                                        documentTypes[d.DocumentTypeName] :
                                                                        2,
                                            Serial = d.Serial,
                                            Number = d.Number,
                                            IssuedBy = d.IssuedBy,
                                            WhenIssued = d.WhenIssued,
                                            CodeDevision = d.CodeDevision
                                        }
                                        ).ToList()
                                        : new List<Document>(),
                Phones = personeModel.Phones != null ?
                                        personeModel.Phones.Select(phone =>
                                            new Phone
                                            {
                                                PhoneNumber = phone.PhoneNumber
                                            }
                                        ).ToList() :
                                        new List<Phone>()
            };
            #endregion

            return persone.UserID;
        }

        public bool Remove(int id)
        {
            return _personRepository.Remove(id);
        }

        public bool Remove(PersoneViewModel model)
        {
            return Remove(model.UserID);
        }
        
        public PersoneViewModel GetPerson(int id, bool loadFullModel = false)
        {
            var person = _personRepository.Persons.FirstOrDefault(p => p.UserID == id);
            List<PersonAddressModelView> addresses = null;
            List<PersoneDocumentModelView> documents = null;
            List<PhoneViewModel> phones = null;
            string positionName = null;

            if (person == null) return null;

            if (person.IsEmploeyr)
            {
                var position = _personPositionRepository.Positions
                                                               .FirstOrDefault(p => p.PositionID == person.Position_PositionID);
                
                positionName = position != null ? position.Name : "";
            }

            if (loadFullModel)
            {
                Dictionary<int, string> documentsType = new Dictionary<int, string>();

                foreach(var dt in _documentsTypeRepository.DocumentTypes)
                {
                    documentsType.Add(dt.DocumentTypeID, dt.Name);
                }

                addresses = _personAddressRepository.Addresses
                                                    .Where(a => a.Persone.UserID == id)
                                                    .Select(a => new PersonAddressModelView
                                                    {
                                                        Country = a.Country,
                                                        Subject = a.Subject,
                                                        Area = a.Area,
                                                        City = a.City,
                                                        Street = a.Street,
                                                        HomeNumber = a.HomeNumber,
                                                        AddressID = a.AddressID,
                                                        Flat = a.Flat,
                                                        Note = a.Note
                                                    }).ToList();

                documents = _documentsRepository.Documents
                                                .Where(d => d.Persone.UserID == id)
                                                .Select(d => new PersoneDocumentModelView
                                                {
                                                    DocumentTypeID = d.Document_DocumentTypeID
                                                    //DocumentTypeName = //documentsType.ContainsKey(d.Document_DocumentTypeID)?
                                                    //                    documentsType[d.Document_DocumentTypeID]//:
                                                    //                    //"",
                                                                        ,
                                                    DocumentID = d.DocumentID,
                                                    IssuedBy = d.IssuedBy,
                                                    CodeDevision = d.CodeDevision,
                                                    Number = d.Number,
                                                    Serial = d.Serial,
                                                    WhenIssued = d.WhenIssued
                                                }).ToList();

                phones = _phonesRepository.Phones
                                            .Where(p => p.Persone.UserID == id)
                                            .Select(p => new PhoneViewModel
                                            {
                                                PhoneID = p.PhoneID,
                                                PhoneNumber = p.PhoneNumber
                                            }).ToList();

                foreach(var doc in documents)
                {
                    doc.DocumentTypeName = documentsType.ContainsKey(doc.DocumentTypeID) ?
                                                documentsType[doc.DocumentTypeID] : "";
                }

            }

            if (addresses == null) addresses = new List<PersonAddressModelView>();
            if (documents == null) documents = new List<PersoneDocumentModelView>();
            if (phones == null) phones = new List<PhoneViewModel>();

            PersoneViewModel personeViewModel = new PersoneViewModel
            {
                UserID = person.UserID,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                MiddleName = person.MiddleName,
                IsEmploeyr = person.IsEmploeyr,
                PositionID = person.Position_PositionID,
                PlaceOfBirth = person.PlaceOfBirth,
                DateBirthday = person.DateBirthday,
                Image = person.Image,
                Position = person.IsEmploeyr ? positionName : "",
                Documents = documents,
                Addresses = addresses,
                Phones = phones
            };

            return personeViewModel;
        }

        public bool UpdatePersone(PersoneViewModel personeModel)
        {
            var dt = _documentsTypeRepository.DocumentTypes;
            Dictionary<string, int> documentTypes = new Dictionary<string, int>();

            EmploeyrPosition position = null;

            if (personeModel.IsEmploeyr)
            {
                position = _personPositionRepository.Positions.FirstOrDefault(p => p.Name.Equals(personeModel.Position));

                if (position == null) throw new ArgumentException("Неизвестная должность!");
            }

            foreach (var d in dt)
            {
                documentTypes.Add(d.Name, d.DocumentTypeID);
            }

            #region make persone model
            Persone persone = new Persone
            {
                UserID = personeModel.UserID,
                IsEmploeyr = personeModel.IsEmploeyr,
                Position_PositionID = personeModel.IsEmploeyr ? position.PositionID : -1,
                FirstName = personeModel.FirstName,
                SecondName = personeModel.SecondName,
                MiddleName = personeModel.MiddleName,
                DateBirthday = personeModel.DateBirthday,
                WasBeUpdated = DateTime.Now,
                WasBeCreated = DateTime.Now,
                PlaceOfBirth = personeModel.PlaceOfBirth,
                Image = personeModel.Image,
                Address = personeModel.Addresses != null ?
                                        personeModel.Addresses.Select(a =>
                                        new PersoneAddress
                                        {
                                            AddressID = a.AddressID,
                                            Country = a.Country,
                                            Subject = a.Subject,
                                            Area = a.Area,
                                            City = a.City,
                                            Street = a.Street,
                                            HomeNumber = a.HomeNumber,
                                            Flat = a.Flat,
                                            Note = a.Note
                                        }
                                        ).ToList()
                                        : new List<PersoneAddress>(),
                Documents = personeModel.Documents != null ?
                                        personeModel.Documents.Select(d =>
                                        new Document
                                        {
                                            DocumentID = d.DocumentID,
                                            Document_DocumentTypeID = documentTypes.ContainsKey(d.DocumentTypeName) ?
                                                                        documentTypes[d.DocumentTypeName] :
                                                                        2,
                                            Serial = d.Serial,
                                            Number = d.Number,
                                            IssuedBy = d.IssuedBy,
                                            WhenIssued = d.WhenIssued,
                                            CodeDevision = d.CodeDevision
                                        }
                                        ).ToList()
                                        : new List<Document>(),
                Phones = personeModel.Phones != null ?
                                        personeModel.Phones.Select(phone =>
                                            new Phone
                                            {
                                                PhoneID = phone.PhoneID,
                                                PhoneNumber = phone.PhoneNumber
                                            }
                                        ).ToList() :
                                        new List<Phone>()
            };
            #endregion

            return _personRepository.Update(persone);
        }
    }
}
