using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppData.Abstract;
using Moq;
using AppPresentators.Services;
using AppData.Entities;
using AppData.FakesRepositoryes;
using System.Collections.Generic;
using System.Linq;
using AppPresentators.VModels.Persons;


namespace Tests.Services
{
    //[TestClass]
    public class TestPersonesService
    {
        private IPersoneRepository _userRepository;
        private IPersonePositionRepository _positionRepository;
        private IPersoneAddressRepository _userAdddresRepository;
        private IPhonesRepository _phoneRepository;
        private IDocumentRepository _documentsRepository;
        private IDocumentTypeRepository _documentsTypeRepository;

        private PersonesService _target;
        
        [TestInitialize]
        public void Initialize()
        {
            _userRepository = new FakePersonesRepository();
            _positionRepository = new FakePersonsPositionRepository();
            _userAdddresRepository = new FakePersonesAddressRepository();
            _phoneRepository = new FakePhonesRepository();
            _documentsRepository = new FakeDocumentRespository();
            _documentsTypeRepository = new FakeDocumentTypeRepository();
            
            _target = new PersonesService(_userRepository, _positionRepository, _userAdddresRepository, _phoneRepository, _documentsRepository, _documentsTypeRepository);
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            int count = _userRepository.Count;

            _target.PageModel = new AppPresentators.VModels.PageModel
            {
                ItemsOnPage = -1
            };

            var users = _target.GetPersons();

            Assert.AreEqual(users.Count(), count);
        }

        [TestMethod]
        public void TestGetFirstPageItemsOnPage2()
        {
            int id1 = _userRepository.Persons.ToList()[0].UserID;
            int id2 = _userRepository.Persons.ToList()[1].UserID;

            _target.PageModel = new AppPresentators.VModels.PageModel
            {
                ItemsOnPage = 2
            };

            var persons = _target.GetPersons();

            Assert.AreEqual(persons.Count, 2);

            Assert.AreEqual(persons[0].UserID, id1);

            Assert.AreEqual(persons[1].UserID, id2);
        }

        [TestMethod]
        public void TestDeleteByIDPersone()
        {
            var personeForDelete = _userRepository.Persons.ToList()[0];

            int secondUserID = _userRepository.Persons.ToList()[1].UserID;

            int count = _userRepository.Count;

            Assert.AreEqual(_target.GetPersons().Count(), count);

            _target.Remove(_userRepository.Persons.ToList()[0].UserID);

            Assert.AreEqual(_target.GetPersons().Count(), count - 1);

            Assert.AreEqual(_target.GetPersons()[0].UserID, secondUserID);
        }

        [TestMethod]
        public void TestDeleteByModel()
        {
            PersoneViewModel pModel = new PersoneViewModel
            {
                UserID = 1
            };

            int count = _target.GetPersons().Count - 1;
        
            //_target.Remove(pModel);

            Assert.AreEqual(_target.GetPersons().Count, count);
        }

        [TestMethod]
        public void TestGetPersoneByID()
        {
            var persone = _target.GetPerson(1);

            Assert.AreEqual(persone.FirstName, _userRepository.Persons.ToList()[0].FirstName);

            Assert.AreEqual(persone.SecondName, _userRepository.Persons.ToList()[0].SecondName);

            Assert.AreEqual(persone.MiddleName, _userRepository.Persons.ToList()[0].MiddleName);
        }

        [TestMethod]
        public void TestAddNewPerson()
        {
            PersoneViewModel person = new PersoneViewModel
            {
                FirstName = "Test first name 1",
                SecondName = "Test second name 1",
                MiddleName = "Test middle name 1",
                DateBirthday = new DateTime(1990, 12, 2, 0, 0, 0, DateTimeKind.Utc),
                Addresses = new List<PersonAddressModelView>
                {
                    new PersonAddressModelView
                    {
                        City = "пгт. Славянка",
                        Street = "Героев-Хасана",
                        HomeNumber = "2",
                        Flat = "3",
                        Note = "Проживает"
                    }
                },
                Phones = new List<PhoneViewModel>
                {
                    new PhoneViewModel
                    {
                        PhoneID = 1,
                        PhoneNumber = "2434"
                    }
                },
                PositionID = 1,
                IsEmploeyr = true
            };

            int maxId = _userRepository.Persons.Max(x => x.UserID);

            int personsCount = _userRepository.Count;
            int addressCount = _userAdddresRepository.Addresses.Count();
            int phoneCount = _phoneRepository.Phones.Count();

            int id = 1;// _target.AddNewPersone(person);

            var lastAddress = _userAdddresRepository.Addresses.Last();

            Assert.AreEqual(id, maxId + 1);

            Assert.AreEqual(personsCount + 1, _userRepository.Count);

            Assert.AreEqual(addressCount + 1, _userAdddresRepository.Addresses.Count());

            Assert.AreEqual(phoneCount + 2, _phoneRepository.Phones.Count());

            Assert.AreEqual(lastAddress.City, person.Addresses[0].City);

            Assert.AreEqual(lastAddress.Street, person.Addresses[0].Street);

            Assert.AreEqual(lastAddress.HomeNumber, person.Addresses[0].HomeNumber);

            Assert.AreEqual(lastAddress.Flat, person.Addresses[0].Flat);
        }


        #region Тестирование поиска
        [TestMethod]
        public void TestSearchUserByFirstNameContain()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                FirstName = "Сид",
                CompareFirstName = AppPresentators.VModels.CompareString.CONTAINS
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestSearchUserByFirstNameEqual()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                FirstName = "Иванов",
                CompareFirstName = AppPresentators.VModels.CompareString.EQUAL
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }


        [TestMethod]
        public void TestSearchUserByFirstNameNotSearch()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                FirstName = "Ге",
                CompareFirstName = AppPresentators.VModels.CompareString.EQUAL
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void TestSearchUserBySecondName()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                SecondName = "Илья",
                CompareSecondName = AppPresentators.VModels.CompareString.EQUAL
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);

            _target.SearchModel = new PersonsSearchModel
            {
                SecondName = "Евг",
                CompareSecondName = AppPresentators.VModels.CompareString.CONTAINS
            };

            result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);

            Assert.AreEqual(result[0].MiddleName, "Эдуардович");

            _target.SearchModel = new PersonsSearchModel
            {
                MiddleName = "Петро",
                CompareMiddleName = AppPresentators.VModels.CompareString.CONTAINS
            };

            result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }
        
        [TestMethod]
        public void TestSearchUserByDateBirthdayMore()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                DateBirthday = new DateTime(1991, 5, 10, 0, 0, 0),
                CompareDate = AppPresentators.VModels.CompareValue.MORE
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 2) != null);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 5) != null);
        }

        [TestMethod]
        public void TestSearchUserByDateBirthdayLess()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                DateBirthday = new DateTime(1991, 5, 10, 0, 0, 0),
                CompareDate = AppPresentators.VModels.CompareValue.LESS
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 3);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 1) != null);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 3) != null);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 4) != null);
        }

        [TestMethod]
        public void TestSearchUserByAgeLess()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Age = 24,
                CompareAge = AppPresentators.VModels.CompareValue.LESS
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 2) != null);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 5) != null);
        }

        [TestMethod]
        public void TestSearchUserByAgeMore()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Age = 24,
                CompareAge = AppPresentators.VModels.CompareValue.MORE
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 4);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 1) != null);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 2) != null);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 3) != null);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 4) != null);
        }

        [TestMethod]
        public void TestSearchByEmployersFlag()
        {
            _target.SearchModel = new PersonsSearchModel
            {
               IsEmployer = true
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestSearchByEmployersFlagAndPositionName()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                IsEmployer = true,
                Position = "Госинспектор"
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 4) != null);
        }

        [TestMethod]
        public void SearchByAddressCountry()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    Country = "Российская Федерация"
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 5);
        }


        [TestMethod]
        public void SearchByAddressCityEquals()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    City = "с.Барабаш"
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void SearchByAddressCityContains()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    City = "Барабаш",
                    CompareCity = AppPresentators.VModels.CompareString.CONTAINS
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }


        [TestMethod]
        public void SearchByAddressStreetEquals()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    Street = "Гагарина"
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void SearchByAddressStreetContains()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    Street = "Моло",
                    CompareStreet = AppPresentators.VModels.CompareString.CONTAINS
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void SearchByAddressHomeNumberEquals()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    HomeNumber = "7"
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        public void SearchByAddressFlatEquals()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    Flat = "15"
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void SearchByAddressNote()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    Note = "Прописка"
                }
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 5);
        }


        [TestMethod]
        public void SearchAndPageModel()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    Note = "Прописка"
                }
            };

            _target.PageModel = new AppPresentators.VModels.PageModel
            {
                ItemsOnPage = 2,
                CurentPage = 1
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(_target.PageModel.TotalItems, 5);

            Assert.AreEqual(_target.PageModel.TotalPages, 3);
        }

        [TestMethod]
        public void TestSearchByDocumentType()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                //DocumentTypeName = "Водительские права"
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestSearchByDocumentSerial()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                Serial = "524"
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestSearchByDocumentNumber()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                Number = "223"
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestSearchByDocumentIssuedBy()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                IssuedBy = "Выдан 3",
                CompareIssuedBy = AppPresentators.VModels.CompareString.EQUAL
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestSearchByDocumentWhenIssuedEquals()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                WhenIssued = new DateTime(2011, 7, 9, 0, 0, 0),
                CompareWhenIssued = AppPresentators.VModels.CompareValue.EQUAL
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestSearchByDocumentWhenIssuedMore()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                WhenIssued = new DateTime(2011, 7, 10, 0, 0, 0),
                CompareWhenIssued = AppPresentators.VModels.CompareValue.MORE
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 4);
        }

        [TestMethod]
        public void TestSearchByDocumentWhenIssuedLess()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                WhenIssued = new DateTime(2011, 7, 10, 0, 0, 0),
                CompareWhenIssued = AppPresentators.VModels.CompareValue.LESS
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestSearchByDocumentCodeDevissionEquals()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                CodeDevision = "234",
                CompareCodeDevision = AppPresentators.VModels.CompareString.EQUAL
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestSearchByDocumentCodeDevissionCotains()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                CodeDevision = "234",
                CompareCodeDevision = AppPresentators.VModels.CompareString.CONTAINS
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestSearchByDocumentAndFirstName()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                CodeDevision = "234",
                CompareCodeDevision = AppPresentators.VModels.CompareString.CONTAINS
            };
            _target.SearchModel = new PersonsSearchModel
            {
                FirstName = "Кирилов",
                CompareFirstName = AppPresentators.VModels.CompareString.EQUAL
            };
            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);

        }

        [TestMethod]
        public void TestSearchByDocumentSerialAndPersonAge()
        {
            _target.DocumentSearchModel = new DocumentSearchModel
            {
                Serial = "524"
            };

            _target.SearchModel = new PersonsSearchModel
            {
                Age = 23,
                CompareAge = AppPresentators.VModels.CompareValue.LESS
            };
            
            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 1);

            Assert.IsTrue(result.FirstOrDefault(p => p.UserID == 5) != null);
        }
        #endregion

        #region Тестирование сортировки
        [TestMethod]
        public void OrderByFirstName()
        {
            _target.OrderModel = new PersonsOrderModel
            {
                OrderType = AppPresentators.VModels.OrderType.ASC,
                OrderProperties = PersonsOrderProperties.FIRST_NAME
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 5);

            Assert.AreEqual(result[0].UserID, 1);

            Assert.AreEqual(result[1].UserID, 5);
        }

        [TestMethod]
        public void OrderBySecondNameDesc()
        {
            _target.OrderModel = new PersonsOrderModel
            {
                OrderType = AppPresentators.VModels.OrderType.DESC,
                OrderProperties = PersonsOrderProperties.SECOND_NAME
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 5);

            Assert.AreEqual(result[0].UserID, 5);

            Assert.AreEqual(result[1].UserID, 3);
        }

        [TestMethod]
        public void OrderByAgeDesc()
        {
            _target.OrderModel = new PersonsOrderModel
            {
                OrderType = AppPresentators.VModels.OrderType.DESC,
                OrderProperties = PersonsOrderProperties.AGE
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 5);

            Assert.AreEqual(result[0].UserID, 4);

            Assert.AreEqual(result[1].UserID, 3);
        }

        [TestMethod]
        public void OrderByDateBerthdayDesc()
        {
            _target.OrderModel = new PersonsOrderModel
            {
                OrderType = AppPresentators.VModels.OrderType.DESC,
                OrderProperties = PersonsOrderProperties.DATE_BERTHDAY
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 5);

            Assert.AreEqual(result[0].UserID, 5);

            Assert.AreEqual(result[1].UserID, 2);
        }

        [TestMethod]
        public void OrderByMiddleName()
        {
            _target.OrderModel = new PersonsOrderModel
            {
                OrderType = AppPresentators.VModels.OrderType.ASC,
                OrderProperties = PersonsOrderProperties.MIDDLE_NAME
            };

            var result = _target.GetPersons();

            Assert.AreEqual(result.Count, 5);

            Assert.AreEqual(result[0].UserID, 1);

            Assert.AreEqual(result[1].UserID, 4);
        }

        [TestMethod]
        public void TestSearchAndPginationsAndOrder()
        {
            _target.SearchModel = new PersonsSearchModel
            {
                Address = new SearchAddressModel
                {
                    Note = "Прописка"
                }
            };

            _target.PageModel = new AppPresentators.VModels.PageModel
            {
                ItemsOnPage = 2,
                CurentPage = 1
            };

            _target.OrderModel = new PersonsOrderModel
            {
                OrderType = AppPresentators.VModels.OrderType.DESC,
                OrderProperties = PersonsOrderProperties.AGE
            };


            var result = _target.GetPersons();

            Assert.AreEqual(_target.PageModel.TotalItems, 5);

            Assert.AreEqual(_target.PageModel.TotalPages, 3);

            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(result[0].UserID, 4);

            Assert.AreEqual(result[1].UserID, 3);
        }
        #endregion
        // Добавить тесты на выборку только сотрудников, добавить тесты на фильтрацию и сортировку, и последнее - добавить тесты на паджинацию.
    }
}
