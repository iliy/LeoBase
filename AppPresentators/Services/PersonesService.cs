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
        List<IPersoneViewModel> GetPersons(bool loadDeleted = false);
        int AddNewPersone(IPersoneViewModel personeModel);
        bool Remove(int id);
        bool Remove(IPersoneViewModel model);
        IPersoneViewModel GetPerson(int id, bool loadFullModel = false);
        bool UpdatePersone(IPersoneViewModel persone);
        bool SimpleRemove(int id);
        bool SimpleRemove(IPersoneViewModel model);
        Persone ConverToEnity(IPersoneViewModel model);
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
        private Dictionary<int, string> _positionNames;
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
            _positionNames = new Dictionary<int, string>();

            foreach (var dT in _documentsTypeRepository.DocumentTypes.ToList())
            {
                _documentTypes.Add(dT.DocumentTypeID, dT.Name);
            }


            foreach(var pN in _personPositionRepository.Positions.ToList())
            {
                _positionNames.Add(pN.PositionID, pN.Name);
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
        public List<IPersoneViewModel> GetPersons(bool loadDeleted = false)
        {
            List<int> usersIds = new List<int>();

            IQueryable<Persone> persones = _personRepository.Persons.Where(p => p.Deleted == loadDeleted);
            var aa = persones.ToList();
            #region Search by ID and Return
            if (SearchModel != null && SearchModel.PersoneID != 0)
            {
                var persone = persones.FirstOrDefault(p => p.UserID == SearchModel.PersoneID);
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
                    if (isEmployer)
                    {
                        return new List<IPersoneViewModel>
                        {
                            new EmploeyrViewModel
                            {
                                UserID = persone.UserID,
                                FirstName = persone.FirstName,
                                SecondName = persone.SecondName,
                                MiddleName = persone.MiddleName,
                                IsEmploeyr = persone.IsEmploeyr,
                                DateBirthday = persone.DateBirthday,
                                Position = position != null ? position.Name : null,
                                PositionID = position != null ? position.PositionID.ToString() : "-1"
                            }
                        };
                    }else
                    {

                    }
                    
                }
            }
            #endregion

            bool wasSearched = false;
            #region SearchByDocument
            if (DocumentSearchModel != null && !DocumentSearchModel.IsEmptySearchModel)
            {
                var documents = _documentsRepository.Documents;
                var documentsType = _documentsTypeRepository.DocumentTypes;


                if(!string.IsNullOrEmpty(DocumentSearchModel.DocumentTypeID))
                {
                    int docTypeId = Convert.ToInt32(DocumentSearchModel.DocumentTypeID);
                    if(docTypeId != 0)
                        documents = documents.Where(d => d.Document_DocumentTypeID == docTypeId);
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

                if(DocumentSearchModel.WhenIssued.Year != 1 && (DocumentSearchModel.WhenIssued.Year != DateTime.Now.Year || DocumentSearchModel.WhenIssued.Month != DateTime.Now.Month || DocumentSearchModel.WhenIssued.Day != DateTime.Now.Day))
                {
                    if(DocumentSearchModel.CompareWhenIssued == CompareValue.MORE)
                    {
                        documents = documents.Where(d => d.WhenIssued >= DocumentSearchModel.WhenIssued);
                    }else if(DocumentSearchModel.CompareWhenIssued == CompareValue.LESS) {
                        documents = documents.Where(d => d.WhenIssued <= DocumentSearchModel.WhenIssued);
                    }else if(DocumentSearchModel.CompareWhenIssued == CompareValue.EQUAL)
                    {
                        documents = documents.Where(d => d.WhenIssued == DocumentSearchModel.WhenIssued);
                    }
                }

                //string q = documents.ToString();
                wasSearched = true;
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

                wasSearched = true;

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
            if (wasSearched)
            {
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
                if (SearchModel.DateBirthday.Year != 1 
                    //&& (SearchModel.DateBirthday.Year != DateTime.Now.Year || SearchModel.DateBirthday.Month != DateTime.Now.Month || SearchModel.DateBirthday.Day != DateTime.Now.Day)
                    )
                {
                    if(SearchModel.CompareDate == CompareValue.MORE)
                    {
                        persones = persones.Where(p => p.DateBirthday >= SearchModel.DateBirthday);
                    }else if(SearchModel.CompareDate == CompareValue.LESS){

                        persones = persones.Where(p => p.DateBirthday <= SearchModel.DateBirthday);
                    }
                    else if(SearchModel.CompareDate == CompareValue.EQUAL)
                    {
                        persones = persones.Where(p => p.DateBirthday == SearchModel.DateBirthday);
                    }
                }

                if(SearchModel.Age > 0 && (
                    SearchModel.DateBirthday.Year == 1 || 
                    SearchModel.DateBirthday.Year == DateTime.Now.Year && SearchModel.DateBirthday.Month == DateTime.Now.Month && SearchModel.DateBirthday.Day == DateTime.Now.Day
                    ))
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
                    else if(SearchModel.CompareAge == CompareValue.EQUAL)
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

            if (OrderModel.OrderType == OrderType.NONE) OrderModel.OrderType = OrderType.DESC;

            if (OrderModel.OrderType != OrderType.NONE)
            {
                if (persones == null) persones = _personRepository.Persons;

                if (OrderModel.OrderType == OrderType.ASC)
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
            else
            {
                if (persones == null) persones = _personRepository.Persons;

                persones = persones.OrderBy(p => p.UserID);
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

            List<Persone> result = persones.ToList();

            if (SearchModel.IsEmployer!= null && SearchModel.IsEmployer == true)
            {
                return result
                    .Select(p =>
                        (IPersoneViewModel)
                        new EmploeyrViewModel
                        {
                            WasBeCreated = p.WasBeCreated,
                            WasBeUpdated = p.WasBeUpdated,
                            UserID = p.UserID,
                            FirstName = p.FirstName,
                            SecondName = p.SecondName,
                            MiddleName = p.MiddleName,
                            IsEmploeyr = p.IsEmploeyr,
                            DateBirthday = p.DateBirthday,
                            PlaceOfBirth = p.PlaceOfBirth,
                            PositionID = p.Position_PositionID.ToString(),
                            Position = _positionNames.ContainsKey(p.Position_PositionID) ?
                                          _positionNames[p.Position_PositionID] : ""
                        }
                    ).ToList();
            }
            else
            {
                return result
                    .Select(p =>
                        (IPersoneViewModel)
                        new ViolatorViewModel
                        {
                            WasBeCreated = p.WasBeCreated,
                            WasBeUpdated = p.WasBeUpdated,
                            UserID = p.UserID,
                            FirstName = p.FirstName,
                            SecondName = p.SecondName,
                            MiddleName = p.MiddleName,
                            IsEmploeyr = p.IsEmploeyr,
                            DateBirthday = p.DateBirthday,
                            PlaceOfBirth = p.PlaceOfBirth,
                            PlaceOfWork = p.PlaceWork
                        }
                    ).ToList(); ;
            }

            
        }

        public int AddNewPersone(IPersoneViewModel personeModel)
        {
            var dt = _documentsTypeRepository.DocumentTypes;
            Dictionary<string, int> documentTypes = new Dictionary<string, int>();

            EmploeyrPosition position = null;
            
            if(!personeModel.IsEmploeyr)
            {
                if(personeModel.Addresses.Count == 0 || personeModel.Addresses.First().IsEmptyModel)
                {
                    throw new ArgumentException("Невозможно создать нарушителя.\r\nХотя бы один адрес должен быть заполнен.");
                }

                var document = personeModel.Documents.First();

                if (string.IsNullOrEmpty(document.DocumentTypeName))
                {
                    string docTN = ""; 
                    
                    if(ConfigApp.DocumentTypesList.Count == 0)
                    {
                        throw new ArgumentException("Невозможно создать нарушителя, отсутствуют типы документов. \r\nЧтобы воспользоваться этой функцией, создайте тип документа");
                    }
                    else
                    {
                        docTN = ConfigApp.DocumentTypesList.First().Name;
                    }

                    document.DocumentTypeName = docTN;
                }

                if(string.IsNullOrEmpty(document.IssuedBy) || string.IsNullOrEmpty(document.Number)
                    || string.IsNullOrEmpty(document.Serial) || document.WhenIssued.Year == 1)
                        throw new ArgumentException("У нарушителя должен быть хотя бы один документ!");
            }

            if(personeModel.DateBirthday.Year < 1700)
            {
                throw new ArgumentException("Укажите дату рождения!");
            }

            if (string.IsNullOrEmpty(personeModel.FirstName))
            {
                throw new ArgumentException("Укажите фамилию!");
            }

            if (string.IsNullOrEmpty(personeModel.SecondName))
            {
                throw new ArgumentException("Укажите имя!");
            }

            if (string.IsNullOrEmpty(personeModel.MiddleName))
            {
                throw new ArgumentException("Укажите отчество!");
            }

            if (personeModel.IsEmploeyr && personeModel is EmploeyrViewModel)
            {
                var employer = (EmploeyrViewModel)personeModel;
                
                try
                {

                    int positionID = Convert.ToInt32(employer.PositionID);

                    position = _personPositionRepository.Positions.FirstOrDefault(p => p.PositionID == positionID);
                }
                catch
                {
                    position = _personPositionRepository.Positions.FirstOrDefault(p => p.Name.Equals(employer.Position));
                }
                finally
                {
                    if (position == null) throw new ArgumentException("Неизвестная должность!");
                }

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
                PlaceWork = !personeModel.IsEmploeyr ? ((ViolatorViewModel)personeModel).PlaceOfWork : ""
            };

            persone.Documents = new List<Document>();

            if (!personeModel.IsEmploeyr && personeModel.Documents != null)
            {

                foreach (var doc in personeModel.Documents)
                {
                    if (string.IsNullOrEmpty(doc.DocumentTypeName))
                    {
                        doc.DocumentTypeName = ConfigApp.DocumentTypesList.First().Name;
                    }

                    if (string.IsNullOrEmpty(doc.IssuedBy)
                    || string.IsNullOrEmpty(doc.Number)
                    || string.IsNullOrEmpty(doc.Serial)
                    || doc.WhenIssued.Year == 1) continue;

                    persone.Documents.Add(new Document
                    {
                        DocumentID = doc.DocumentID,
                        Document_DocumentTypeID = ConfigApp.DocumentTypesList.FirstOrDefault(d => d.Name.Equals(doc.DocumentTypeName)).DocumentTypeID,
                        CodeDevision = doc.CodeDevision,
                        IssuedBy = doc.IssuedBy,
                        Number = doc.Number,
                        Serial = doc.Serial,
                        WhenIssued = doc.WhenIssued
                    });
                }
            }

            persone.Address = new List<PersoneAddress>();

            if (personeModel.Addresses != null)
            {
                foreach (var address in personeModel.Addresses)
                {
                    if (!address.IsEmptyModel) {
                        string note = address.Note;

                        if (string.IsNullOrWhiteSpace(note)) note = "Проживает и прописан";

                        persone.Address.Add(new PersoneAddress
                        {
                            Country = address.Country,
                            Subject = address.Subject,
                            Area = address.Area,
                            City = address.City,
                            Street = address.Street,
                            HomeNumber = address.HomeNumber,
                            Flat = address.Flat,
                            Note = address.Note
                        });
                    }
                }
            }

            persone.Phones = new List<Phone>();

            if(personeModel.Phones != null)
            {
                foreach(var phone in personeModel.Phones)
                {
                    if (!string.IsNullOrEmpty(phone.PhoneNumber))
                    {
                        persone.Phones.Add(new Phone
                        {
                            PhoneNumber = phone.PhoneNumber
                        });
                    }
                }
            }

            _personRepository.AddPersone(persone);

            #endregion

            return persone.UserID;
        }

        public bool Remove(int id)
        {
            return _personRepository.Remove(id);
        }

        public bool SimpleRemove(int id)
        {
            return _personRepository.SimpleRemove(id);
        }

        public bool SimpleRemove(IPersoneViewModel model)
        {
            return SimpleRemove(model.UserID);
        }

        public bool Remove(IPersoneViewModel model)
        {
            return Remove(model.UserID);
        }
        
        public IPersoneViewModel GetPerson(int id, bool loadFullModel = false)
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

            if(person.IsEmploeyr != null && person.IsEmploeyr == true)
            {
                return new EmploeyrViewModel
                {
                    UserID = person.UserID,
                    FirstName = person.FirstName,
                    SecondName = person.SecondName,
                    MiddleName = person.MiddleName,
                    IsEmploeyr = person.IsEmploeyr,
                    PositionID = person.Position_PositionID.ToString(),
                    PlaceOfBirth = person.PlaceOfBirth,
                    DateBirthday = person.DateBirthday,
                    Image = person.Image,
                    Position = person.IsEmploeyr ? positionName : "",
                    Documents = documents,
                    Addresses = addresses,
                    Phones = phones,
                    WasBeCreated = person.WasBeCreated,
                    WasBeUpdated = person.WasBeUpdated
                };
            }else
            {
                return new ViolatorViewModel
                {
                    UserID = person.UserID,
                    FirstName = person.FirstName,
                    SecondName = person.SecondName,
                    MiddleName = person.MiddleName,
                    IsEmploeyr = person.IsEmploeyr,
                    PlaceOfBirth = person.PlaceOfBirth,
                    DateBirthday = person.DateBirthday,
                    Image = person.Image,
                    Documents = documents,
                    Addresses = addresses,
                    Phones = phones,
                    PlaceOfWork = person.PlaceWork,
                    WasBeCreated = person.WasBeCreated,
                    WasBeUpdated = person.WasBeUpdated
                };
            }
        }


        public Persone ConverToEnity(IPersoneViewModel model)
        {
            Persone persone = new Persone
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                MiddleName = model.MiddleName,
                IsEmploeyr = model.IsEmploeyr,
                PlaceOfBirth = model.PlaceOfBirth,
                PlaceWork = (!model.IsEmploeyr) ? ((ViolatorViewModel)model).PlaceOfWork : "",
                Image = model.Image,
                Phones = model.Phones.Select(p => new Phone {PhoneNumber = p.PhoneNumber }).ToList(),
                UserID = model.UserID,
                Position_PositionID = model.IsEmploeyr ? Convert.ToInt32(((EmploeyrViewModel)model).PositionID) : 0,
                WasBeCreated = model.WasBeCreated,
                WasBeUpdated = model.WasBeUpdated,
                Address = model.Addresses.Select(a => new PersoneAddress
                {
                    AddressID = a.AddressID,
                    Area = a.Area,
                    City = a.City,
                    Country = a.Country,
                    Flat = a.Flat,
                    HomeNumber = a.HomeNumber,
                    Note = a.Note,
                    Street = a.Street,
                    Subject = a.Subject
                }).ToList(),
                Documents = model.Documents.Select(d => new Document
                {
                    CodeDevision = d.CodeDevision,
                    DocumentID = d.DocumentID,
                    Document_DocumentTypeID = d.DocumentTypeID,
                    IssuedBy = d.IssuedBy,
                    Number = d.Number,
                    Serial = d.Serial,
                    WhenIssued = d.WhenIssued
                }).ToList(),
                DateBirthday = model.DateBirthday,
            };

            return persone;
        }

        public bool UpdatePersone(IPersoneViewModel personeModel)
        {
            var dt = _documentsTypeRepository.DocumentTypes;
            Dictionary<string, int> documentTypes = new Dictionary<string, int>();

            EmploeyrPosition position = null;

            if (personeModel.DateBirthday.Year < 1700)
            {
                throw new ArgumentException("Укажите дату рождения!");
            }

            if (string.IsNullOrEmpty(personeModel.FirstName))
            {
                throw new ArgumentException("Укажите фамилию!");
            }

            if (string.IsNullOrEmpty(personeModel.SecondName))
            {
                throw new ArgumentException("Укажите имя!");
            }

            if (string.IsNullOrEmpty(personeModel.MiddleName))
            {
                throw new ArgumentException("Укажите отчество!");
            }

            if (personeModel.IsEmploeyr && personeModel is EmploeyrViewModel)
            {
                var employer = (EmploeyrViewModel)personeModel;

                try
                {

                    int positionID = Convert.ToInt32(employer.PositionID);

                    position = _personPositionRepository.Positions.FirstOrDefault(p => p.PositionID == positionID);
                }
                catch
                {
                    position = _personPositionRepository.Positions.FirstOrDefault(p => p.Name.Equals(employer.Position));
                }
                finally { 
                    if (position == null) throw new ArgumentException("Неизвестная должность!");
                }
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
                PlaceOfBirth = personeModel.PlaceOfBirth,
                Image = personeModel.Image,
                PlaceWork = !personeModel.IsEmploeyr ? ((ViolatorViewModel)personeModel).PlaceOfWork : "",

            };

            persone.Documents = new List<Document>();

            if (!personeModel.IsEmploeyr && personeModel.Documents != null)
            {

                foreach (var doc in personeModel.Documents)
                {
                    if (doc.WhenIssued.Year == 1) doc.WhenIssued = DateTime.Now;

                    persone.Documents.Add(new Document
                    {
                        DocumentID = doc.DocumentID,
                        Document_DocumentTypeID = ConfigApp.DocumentTypesList.FirstOrDefault(d => d.Name.Equals(doc.DocumentTypeName)).DocumentTypeID,
                        CodeDevision = doc.CodeDevision,
                        IssuedBy = doc.IssuedBy,
                        Number = doc.Number,
                        Serial = doc.Serial,
                        WhenIssued = doc.WhenIssued
                    });
                }
            }

            persone.Address = new List<PersoneAddress>();

            if (personeModel.Addresses != null)
            {
                foreach (var address in personeModel.Addresses)
                {
                    if (!address.IsEmptyModel)
                        persone.Address.Add(new PersoneAddress
                        {
                            AddressID = address.AddressID,
                            Country = address.Country,
                            Subject = address.Subject,
                            Area = address.Area,
                            City = address.City,
                            Street = address.Street,
                            HomeNumber = address.HomeNumber,
                            Flat = address.Flat,
                            Note = address.Note
                        });
                }
            }

            persone.Phones = new List<Phone>();

            if (personeModel.Phones != null)
            {
                foreach (var phone in personeModel.Phones)
                {
                    if (!string.IsNullOrEmpty(phone.PhoneNumber))
                    {
                        persone.Phones.Add(new Phone
                        {
                            PhoneID = phone.PhoneID,
                            PhoneNumber = phone.PhoneNumber
                        });
                    }
                }
            }

            #endregion

            return _personRepository.Update(persone);
        }
    }
}
