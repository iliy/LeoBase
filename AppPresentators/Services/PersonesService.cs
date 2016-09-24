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
        DocumentSearchModel DocumentSearchModel { get; set; }
        PersonsOrderModel OrderModel { get; set; }
        List<PersoneViewModel> GetPersons();
        int AddNewPersone(PersoneViewModel personeModel);
        bool Remove(int id);
        bool Remove(PersoneViewModel model);
        PersoneViewModel GetPerson(int id);
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

            foreach(var dT in _documentsTypeRepository.DocumentTypes)
            {
                _documentTypes.Add(dT.DocumentTypeID, dT.Name);
            }
        }

        private bool AddressFilter(PersoneAddress address)
        {
            if (SearchModel.Address == null || SearchModel.Address.IsEmptyAddress) return true;

            SearchAddressModel aS = SearchModel.Address;

            bool result = true;

            if (!string.IsNullOrEmpty(aS.Note))
                result &= string.IsNullOrEmpty(address.Note) || aS.Note.Equals(address.Note); 

            if (!string.IsNullOrEmpty(aS.Country))
            {
                if (aS.CompareCountry == CompareString.EQUAL)
                    result &= aS.Country.Equals(address.Country);
                else
                    result &= address.Country != null && address.Country.Contains(aS.Country); 
            }

            if (!result) return result;

            if (!string.IsNullOrEmpty(aS.Subject))
            {
                if (aS.CompareSubject == CompareString.EQUAL)
                    result &= aS.Subject.Equals(address.Subject);
                else
                    result &= address.Subject != null && address.Subject.Contains(aS.Subject);
            }

            if (!result) return result;

            if (!string.IsNullOrEmpty(aS.Area))
            {
                if (aS.CompareArea == CompareString.EQUAL)
                    result &= aS.Area.Equals(address.Area);
                else
                    result &= address.Area != null && address.Area.Contains(aS.Area);
            }

            if (!result) return result;

            if (!string.IsNullOrEmpty(aS.City))
            {
                if (aS.CompareCity == CompareString.EQUAL)
                    result &= aS.City.Equals(address.City);
                else
                    result &= address.City != null && address.City.Contains(aS.City);
            }

            if (!result) return result;

            if (!string.IsNullOrEmpty(aS.Street))
            {
                if (aS.CompareStreet == CompareString.EQUAL)
                    result &= aS.Street.Equals(address.Street);
                else
                    result &= address.Street != null && address.Street.Contains(aS.Street);
            }

            if (!result) return result;

            if (!string.IsNullOrEmpty(aS.HomeNumber))
                result &= address.HomeNumber != null & address.HomeNumber.Equals(aS.HomeNumber);

            if (!string.IsNullOrEmpty(aS.Flat))
                result &= address.Flat != null && address.Flat.Equals(aS.Flat);

            return result;
        }

        /// <summary>
        /// Фильтрация граждан в соответсвие со свойством SearchModel;
        /// </summary>
        /// <param name="persone"></param>
        /// <returns></returns>
        private bool PersoneFilter(Persone persone)
        {
            if (SearchModel == null || SearchModel.IsEmptySearch) return true;

            bool result = true;
            
            if (!string.IsNullOrEmpty(SearchModel.FirstName))
                if (SearchModel.CompareFirstName == CompareString.EQUAL)
                    result &= persone.FirstName.Equals(SearchModel.FirstName);
                else
                    result &= persone.FirstName.Contains(SearchModel.FirstName);

            if (!result) return result;

            if (!string.IsNullOrEmpty(SearchModel.SecondName))
                if (SearchModel.CompareSecondName == CompareString.EQUAL)
                    result &= persone.SecondName.Equals(SearchModel.SecondName);
                else
                    result &= persone.SecondName.Contains(SearchModel.SecondName);

            if (!result) return result;

            if (!string.IsNullOrEmpty(SearchModel.MiddleName))
                if (SearchModel.CompareMiddleName == CompareString.EQUAL)
                    result &= persone.MiddleName.Equals(SearchModel.MiddleName);
                else
                    result &= persone.MiddleName.Contains(SearchModel.MiddleName);

            if (!result) return result;
            
            if (SearchModel.DateBirthday.Year != 1)
            {
                switch (SearchModel.CompareDate)
                {
                    case CompareValue.EQUAL:
                        result &= persone.DateBirthday == SearchModel.DateBirthday;
                        break;
                    case CompareValue.LESS:
                        result &= persone.DateBirthday <= SearchModel.DateBirthday;
                        break;
                    case CompareValue.MORE:
                        result &= persone.DateBirthday >= SearchModel.DateBirthday;
                        break;
                }
            }else if (SearchModel.Age > 0) {
                int personeAge = (new DateTime() + (DateTime.Now - persone.DateBirthday)).Year;
                switch (SearchModel.CompareAge)
                {
                    case CompareValue.EQUAL:
                        result &=  personeAge == SearchModel.Age;
                        break;
                    case CompareValue.LESS:
                        result &= personeAge <= SearchModel.Age;
                        break;
                    case CompareValue.MORE:
                        result &= personeAge >= SearchModel.Age;
                        break;
                }
            }

            return result;
        }

        private bool FilterDocument(Document doc)
        {
            if (DocumentSearchModel == null || DocumentSearchModel.IsEmptySearchModel) return true;

            bool result = true;

            DocumentSearchModel dsm = DocumentSearchModel;

            if (dsm.DocumentTypeID != 0)
                result &= doc.DocumentTypeID == dsm.DocumentTypeID;

            if (!result) return false;

            if(!string.IsNullOrEmpty(dsm.DocumentTypeName))
            {
                int docTypeId = _documentTypes.FirstOrDefault(d => d.Value.Equals(dsm.DocumentTypeName)).Key;

                result &= docTypeId == doc.DocumentTypeID;
            }

            if (!result) return false;

            if (!string.IsNullOrEmpty(dsm.Number))
                result &= dsm.Number.Equals(doc.Number);

            if (!result) return false;

            if (!string.IsNullOrEmpty(dsm.Serial))
                result &= dsm.Serial.Equals(doc.Serial);

            if (!result) return false;

            if (!string.IsNullOrEmpty(dsm.IssuedBy))
                if (dsm.CompareIssuedBy == CompareString.EQUAL)
                    result &= dsm.IssuedBy.Equals(doc.IssuedBy);
                else
                    result &= !string.IsNullOrEmpty(doc.IssuedBy) && doc.IssuedBy.Contains(dsm.IssuedBy);

            if (!result) return false;

            if(dsm.WhenIssued.Year != 1)
            {
                switch (dsm.CompareWhenIssued)
                {
                    case CompareValue.EQUAL:
                        result &= dsm.WhenIssued == doc.WhenIssued;
                        break;
                    case CompareValue.LESS:
                        result &= doc.WhenIssued <= dsm.WhenIssued;
                        break;
                    case CompareValue.MORE:
                        result &= doc.WhenIssued >= dsm.WhenIssued;
                        break;
                }
            }

            if (!result) return false;

            if (!string.IsNullOrEmpty(dsm.CodeDevision))
                if (dsm.CompareCodeDevision == CompareString.EQUAL)
                    result &= dsm.CodeDevision.Equals(doc.CodeDevision);
                else
                    result &= !string.IsNullOrEmpty(doc.CodeDevision) && doc.CodeDevision.Contains(doc.CodeDevision);

            return result;
        }

        public List<PersoneViewModel> GetPersons()
        {
            List<PersoneViewModel> result;

            IQueryable<Persone> persones = null;

            if(DocumentSearchModel != null && !DocumentSearchModel.IsEmptySearchModel)
            {   // Выборка с фильтрацией по документам
                var userIds = _documentsRepository.Documents.Where(d => FilterDocument(d)).Select(d => d.UserID);

                persones = _personRepository.Persons.Where(p => userIds.Contains(p.UserID));
            }

            if (SearchModel != null && !SearchModel.IsEmptySearch) // Выборка с фильтрацией
            {
                if (persones == null) persones = _personRepository.Persons;

                if(SearchModel.Address != null && !SearchModel.Address.IsEmptyAddress) // Имеется фильрация по адресу
                {
                    var userIds = _personAddressRepository.Addresses.Where(a => AddressFilter(a)).Select(a => a.UserID);

                    persones = persones.Where(p => userIds.Contains(p.UserID));
                }

                if(SearchModel.IsEmployer != null) 
                {
                    if ((bool)SearchModel.IsEmployer) // Если поиск по сотрудникам
                    {
                        persones = persones.Where(p => p.IsEmploeyr == true); // Выбираем только сотрудников

                        if (!string.IsNullOrEmpty(SearchModel.Position)) // Если указана должность то фильтруем и по ней
                        {
                            var pUIds = _personPositionRepository.Positions.Where(p => p.Name.Equals(SearchModel.Position)).Select(p => p.PositionID);

                            persones = persones.Where(p => pUIds.Contains(p.PositionID));
                        }
                    }
                }

                if (persones == null) persones = _personRepository.Persons;

                persones = persones.Where(p => PersoneFilter(p));
            }

            if (persones == null)
                persones = _personRepository.Persons;

            if (OrderModel != null && OrderModel.OrderType != OrderType.NONE)
            {
                if (OrderModel.OrderType == OrderType.DESC)
                {
                    switch (OrderModel.OrderProperties)
                    {
                        case PersonsOrderProperties.DATE_BERTHDAY:
                            persones = persones.OrderByDescending(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.AGE:
                            persones = persones.OrderBy(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.FIRST_NAME:
                            persones = persones.OrderByDescending(p => p.FirstName);
                            break;
                        case PersonsOrderProperties.SECOND_NAME:
                            persones = persones.OrderByDescending(p => p.SecondName);
                            break;
                        case PersonsOrderProperties.MIDDLE_NAME:
                            persones = persones.OrderByDescending(p => p.MiddleName);
                            break;
                    }
                }
                else
                {
                    switch (OrderModel.OrderProperties)
                    {
                        case PersonsOrderProperties.DATE_BERTHDAY:
                            persones = persones.OrderBy(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.AGE:
                            persones = persones.OrderByDescending(p => p.DateBirthday);
                            break;
                        case PersonsOrderProperties.FIRST_NAME:
                            persones = persones.OrderBy(p => p.FirstName);
                            break;
                        case PersonsOrderProperties.SECOND_NAME:
                            persones = persones.OrderBy(p => p.SecondName);
                            break;
                        case PersonsOrderProperties.MIDDLE_NAME:
                            persones = persones.OrderBy(p => p.MiddleName);
                            break;
                    }
                }
            }
            if (PageModel.ItemsOnPage == -1) { 
                result = persones.Select(p =>
                            new PersoneViewModel(_personAddressRepository,
                                             _phonesRepository,
                                             _personPositionRepository,
                                             _documentsRepository,
                                             _documentsTypeRepository)
                            { 
                                UserID = p.UserID,
                                FirstName = p.FirstName,
                                SecondName = p.SecondName,
                                MiddleName = p.MiddleName,
                                IsEmploeyr = p.IsEmploeyr,
                                DateBirthday = p.DateBirthday,
                                PositionID = p.PositionID
                            }
                        ).ToList();
            }else { 
                result = persones.Skip(PageModel.CurentPage - 1).Take(PageModel.ItemsOnPage).Select(p =>
                        new PersoneViewModel(_personAddressRepository, 
                                             _phonesRepository, 
                                             _personPositionRepository,
                                             _documentsRepository,
                                             _documentsTypeRepository)
                        {
                            UserID = p.UserID,
                            FirstName = p.FirstName,
                            SecondName = p.SecondName,
                            MiddleName = p.MiddleName,
                            IsEmploeyr = p.IsEmploeyr,
                            DateBirthday = p.DateBirthday,
                            PositionID = p.PositionID
                        }
                    ).ToList();
            }

            PageModel.TotalItems = persones.Count();

            return result;
        }

        public int AddNewPersone(PersoneViewModel personeModel)
        {
            Persone persone = new Persone
            {
                IsEmploeyr = personeModel.IsEmploeyr,
                FirstName = personeModel.FirstName,
                SecondName = personeModel.SecondName,
                MiddleName = personeModel.MiddleName,
                DateBirthday = personeModel.DateBirthday,
                PositionID = personeModel.IsEmploeyr ? personeModel.PositionID : -1
            };

            int id = _personRepository.AddPersone(persone);

            foreach(var address in personeModel.Addresses)
            {
                PersoneAddress adrEnt = new PersoneAddress
                {
                    UserID = id,
                    Note = address.Note
                };

                if (!string.IsNullOrEmpty(address.Country))
                    adrEnt.Country = address.Country;

                if (!string.IsNullOrEmpty(address.Subject))
                    adrEnt.Subject = address.Subject;

                if (!string.IsNullOrEmpty(address.Area))
                    adrEnt.Area = address.Area;

                adrEnt.City = address.City;
                adrEnt.Street = address.Street;
                adrEnt.HomeNumber = address.HomeNumber;
                adrEnt.Flat = address.Flat;

                int adrId = _personAddressRepository.AddAdress(adrEnt);
            }

            foreach(var phone in personeModel.Phones)
            {
                Phone phoneEnt = new Phone
                {
                    UserID = id,
                    PhoneNumber = phone
                };

                int phoneId = _phonesRepository.AddPhone(phoneEnt);
            }

            return id;
        }

        public bool Remove(int id)
        {
            _personAddressRepository.RemoveUserAddresses(id);
            _phonesRepository.RemoveAllUserPhones(id);
            return _personRepository.Remove(id);
        }

        public bool Remove(PersoneViewModel model)
        {
            return Remove(model.UserID);
        }

        public PersoneViewModel GetPerson(int id)
        {
            var person = _personRepository.Persons.FirstOrDefault(p => p.UserID == id);
            if (person == null) return null;
            return new PersoneViewModel(_personAddressRepository, 
                                        _phonesRepository, 
                                        _personPositionRepository,
                                        _documentsRepository,
                                        _documentsTypeRepository)
            {
                UserID = person.UserID,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                MiddleName = person.MiddleName,
                IsEmploeyr = person.IsEmploeyr,
                PositionID = person.PositionID,
                DateBirthday = person.DateBirthday
            };
        }
    }
}
