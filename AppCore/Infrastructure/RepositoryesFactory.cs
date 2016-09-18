using AppData.Abstract;
using AppData.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Infrastructure
{
    public static class RepositoryesFactory
    {
        private static IKernel ninjectKernel = null; 
        public static void Init()
        {
            ninjectKernel = new StandardKernel();
            AddBinds();
        }

        public static T Get<T>()
        {
            if (!ninjectKernel.CanResolve<T>())
                throw new ArgumentException("Has`t type!");

            return (T)ninjectKernel.Get<T>();
        }

        private static void AddBinds()
        {
            #region Создание Mock объектов
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IUserTypeRepository> mockUserTypeRepository = new Mock<IUserTypeRepository>();
            Mock<IPositionRepository> mockPositionRepository = new Mock<IPositionRepository>();
            Mock<IUserAddressRepository> mockAddressRepository = new Mock<IUserAddressRepository>();
            Mock<IAddressTypeRepository> mockAddressTypeRepository = new Mock<IAddressTypeRepository>();
            Mock<IPhonesRepository> mockPhonesRepository = new Mock<IPhonesRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IDocumentTypeRepository> mockDocumentTypeRepository = new Mock<IDocumentTypeRepository>();
            Mock<IViolationRepository> mockViolationRepository = new Mock<IViolationRepository>();
            Mock<IViolationTypeRepository> mockViolationTypeRepository = new Mock<IViolationTypeRepository>();
            Mock<IViolatorRepository> mockViolatorsRepository = new Mock<IViolatorRepository>();
            Mock<IEmployerRepository> mockEmploerRepository = new Mock<IEmployerRepository>();
            Mock<IManagersRepository> mockManagersRepository = new Mock<IManagersRepository>();
            #endregion

            #region Установка Mock объектов
            mockManagersRepository.Setup(m => m.Managers).Returns(new List<Manager>
            {
                new Manager
                {
                    Login = "admin",
                    Password = "45166",
                    ManagerID = 1,
                    Role = "admin"
                },
                new Manager
                {
                    Login = "manager",
                    Password = "45166",
                    ManagerID = 2,
                    Role = "manager"
                },
                new Manager
                {
                    Login = "user",
                    Password = "45166",
                    ManagerID = 3,
                    Role = "user"
                }
            }.AsQueryable());

            mockEmploerRepository.Setup(e => e.Employers).Returns(new List<Employer>
            {
                new Employer
                {
                    EmploerID = 1,
                    ViolationID = 1,
                    UserID = 4
                },
                new Employer
                {
                    EmploerID = 2,
                    ViolationID = 2,
                    UserID = 5
                },
                new Employer
                {
                    EmploerID = 3,
                    ViolationID = 3,
                    UserID = 4
                },
                new Employer
                {
                    EmploerID = 4,
                    ViolationID = 3,
                    UserID = 5
                },
                new Employer
                {
                    EmploerID = 5,
                    ViolationID = 4,
                    UserID = 4
                },
                new Employer
                {
                    EmploerID = 6,
                    ViolationID = 5,
                    UserID = 5
                }
            }.AsQueryable());

            mockViolatorsRepository.Setup(v => v.Violators).Returns(new List<Violator>
            {
                new Violator
                {
                    ViolatorID = 1,
                    ViolationID = 1,
                    UserID = 1
                },
                new Violator
                {
                    ViolatorID = 2,
                    ViolationID = 1,
                    UserID = 1
                },
                new Violator
                {
                    ViolatorID = 3,
                    ViolationID = 2,
                    UserID = 2
                },
                new Violator
                {
                    ViolatorID = 4,
                    ViolationID = 3,
                    UserID = 3
                },
                new Violator
                {
                    ViolatorID = 4,
                    ViolationID = 4,
                    UserID = 2
                },
                new Violator
                {
                    ViolatorID = 5,
                    ViolationID = 3,
                    UserID = 3
                }
            }.AsQueryable());

            mockViolationTypeRepository.Setup(vt => vt.ViolationTypes).Returns(new List<ViolationType>
            {
                new ViolationType
                {
                    ViolationTypeID = 1,
                    Name = "Административное правонарушение"
                },
                new ViolationType
                {
                    ViolationTypeID = 2,
                    Name = "Уголовное правонарушение"
                }
            }.AsQueryable());

            mockViolationRepository.Setup(v => v.Violations).Returns(new List<Violation>
            {
                new Violation
                {
                    ViolationID = 1,
                    ViolationTypeID = 1,
                    Date = "4.09.2016",
                    N = 43.01233,
                    E = 123.01231,
                    Description = "Описание первого нарушения А"
                },
                new Violation
                {
                    ViolationID = 2,
                    ViolationTypeID = 2,
                    Date = "10.09.2016",
                    N = 43.21312,
                    E = 123.1123,
                    Description = "Описание второго нарушения У"
                },
                new Violation
                {
                    ViolationID  = 3,
                    ViolationTypeID = 1,
                    Date = "14.09.2015",
                    N = 44.321313,
                    E = 111.321313,
                    Description = "Описание третьего нарушения А"
                },
                new Violation
                {
                    ViolationID  = 4,
                    ViolationTypeID = 1,
                    Date = "17.08.2016",
                    N = 41.321313,
                    E = 121.321313,
                    Description = "Описание четвертого нарушения А"
                },
                new Violation
                {
                    ViolationID  = 5,
                    ViolationTypeID = 2,
                    Date = "13.12.2014",
                    N = 21.321313,
                    E = 161.321313,
                    Description = "Описание пятого нарушения У"
                }
            }.AsQueryable()); 

            mockDocumentTypeRepository.Setup(dt => dt.DocumentTypes).Returns(new List<DocumentType>
            {
                new DocumentType
                {
                    DocumentTypeID = 1,
                    Name = "Паспорт"
                },
                new DocumentType
                {
                    DocumentTypeID = 2,
                    Name = "Водительские права"
                }
            }.AsQueryable());

            mockDocumentRepository.Setup(d => d.Documents).Returns(new List<Document>
            {
                new Document
                {
                    DocumentID = 1,
                    DocumentTypeID = 1,
                    UserID = 1,
                    Serial = "111",
                    Number = "123",
                    IssuedBy = "Выдан 1",
                    WhenIssued = "11.11.2016",
                    CodeDevision = "234"
                },
                new Document
                {
                    DocumentID = 2,
                    DocumentTypeID = 1,
                    UserID = 2,
                    Serial = "212",
                    Number = "126",
                    IssuedBy = "Выдан 2",
                    WhenIssued = "11.11.2015"
                },
                new Document
                {
                    DocumentID = 3,
                    DocumentTypeID = 2,
                    UserID = 2,
                    Serial = "322",
                    Number = "125",
                    IssuedBy = "Выдан 3",
                    WhenIssued = "11.12.2015"
                },
                new Document
                {
                    DocumentID = 4,
                    DocumentTypeID = 1,
                    UserID = 3,
                    Serial = "413",
                    Number = "127",
                    IssuedBy = "Выдан 2",
                    WhenIssued = "5.11.2016",
                    CodeDevision = "2341"
                },
                new Document
                {
                    DocumentID = 5,
                    DocumentTypeID = 2,
                    UserID = 4,
                    Serial = "524",
                    Number = "223",
                    IssuedBy = "Выдан 3",
                    WhenIssued = "11.11.2012"
                },
                new Document
                {
                    DocumentID = 6,
                    DocumentTypeID = 1,
                    UserID = 5,
                    Serial = "615",
                    Number = "12312",
                    IssuedBy = "Выдан 8",
                    WhenIssued = "09.7.2011"
                }
            }.AsQueryable());

            mockPhonesRepository.Setup(p => p.Phones).Returns(new List<Phone>
            {
                new Phone
                {
                    PhoneID = 1,
                    UserID = 1,
                    PhoneNumber = "1100000"
                },
                new Phone
                {
                    PhoneID = 2,
                    UserID = 2,
                    PhoneNumber = "2200000"
                },
                new Phone
                {
                    PhoneID = 3,
                    UserID = 2,
                    PhoneNumber = "3200000"
                },
                new Phone
                {
                    PhoneID = 4,
                    UserID = 3,
                    PhoneNumber = "4300000"
                },
                new Phone
                {
                    PhoneID = 5,
                    UserID = 4,
                    PhoneNumber = "5400000"
                },
                new Phone
                {
                    PhoneID = 6,
                    UserID = 5,
                    PhoneNumber = "6500000"
                },
                new Phone
                {
                    PhoneID = 7,
                    UserID = 5,
                    PhoneNumber = "7500000"
                }
            }.AsQueryable());

            mockAddressTypeRepository.Setup(at => at.AddressTypes).Returns(new List<AddressType>
            {
                new AddressType
                {
                    AddressTypeID = 1,
                    Name = "Прописка"
                },
                new AddressType
                {
                    AddressTypeID = 2,
                    Name = "Проживает"
                },
                new AddressType
                {
                    AddressTypeID = 3,
                    Name = "Проживает и прописка"
                }
            }.AsQueryable());

            mockAddressRepository.Setup(a => a.UserAddresses).Returns(new List<UserAddress>
            {
                new UserAddress
                {
                    AddressID = 1,
                    UserID = 1,
                    Address = "Приморский край, Хасанский район, пгт.Славянка, ул.Героев-Хасана 25, кв 27",
                    AddressTypeID = 1
                },
                new UserAddress
                {
                    AddressID = 2,
                    UserID = 1,
                    Address = "Приморский край, Хасанский район, пгт.Славянка, ул.Лазо 26, кв 53",
                    AddressTypeID = 2
                },


                new UserAddress
                {
                    AddressID = 3,
                    UserID = 2,
                    Address = "Приморский край, Хасанский район, с.Барабаш, ул.Хасанская 2",
                    AddressTypeID = 1
                },
                new UserAddress
                {
                    AddressID = 4,
                    UserID = 2,
                    Address = "Приморский край, Хасанский район, с.Барабаш, ул.Космонавтов 3",
                    AddressTypeID = 2
                },
                new UserAddress
                {
                    AddressID = 5,
                    UserID = 3,
                    Address = "Приморский край, Хасанский район, с.Филиповка, ул.Гагарина 7, кв 15",
                    AddressTypeID = 3
                },
                new UserAddress
                {
                    AddressID = 6,
                    UserID = 4,
                    Address = "Приморский край, Хасанский район, д.Овчиновка, ул.Кирова, д.7",
                    AddressTypeID = 3
                },
                new UserAddress
                {
                    AddressID = 7,
                    UserID = 5,
                    Address = "Приморский край, Хасанский район, с.Безверхово, ул.Моложежная 7, д. 6",
                    AddressTypeID = 3
                }

            }.AsQueryable());

            mockPositionRepository.Setup(p => p.Positions).Returns(new List<Position>
            {
                new Position
                {
                    PositionID = 1,
                    Name = "Госинспектор"
                },
                new Position
                {
                    PositionID = 2,
                    Name = "Участковый госинспектор"
                },
                new Position
                {
                    PositionID = 3,
                    Name = "Старший госинпектор"
                }
            }.AsQueryable());

            mockUserTypeRepository.Setup(ut => ut.UserTypes).Returns(new List<UserType>
            {
                new UserType
                {
                    UserTypeID = 1,
                    TypeName = "Нарушитель"
                },
                new UserType
                {
                    UserTypeID = 2,
                    TypeName = "Сотрудник"
                }
            }.AsQueryable());

            mockUserRepository.Setup(u => u.Users).Returns(new List<User>{
                    new User
                    {
                        UserID = 1,
                        UserTypeID = 1,
                        FirstName = "Иванов",
                        SecondName = "Иван",
                        MiddleName = "Васильевич"
                    },
                    new User
                    {
                        UserID = 2,
                        UserTypeID = 1,
                        FirstName = "Сидоров",
                        SecondName = "Евгений",
                        MiddleName = "Эдуардович"
                    },
                    new User
                    {
                        UserID = 3,
                        UserTypeID = 1,
                        FirstName = "Кирилов",
                        SecondName = "Дмитрий",
                        MiddleName = "Петрововия"
                    },
                    new User
                    {
                        UserID = 4,
                        UserTypeID = 2,
                        FirstName = "Сидоренко",
                        SecondName = "Илья",
                        MiddleName = "Игоревич",
                        PositionID = 1
                    },
                    new User
                    {
                        UserID = 5,
                        UserTypeID = 2,
                        FirstName = "Гагарин",
                        SecondName = "Юрий",
                        MiddleName = "Семенович",
                        PositionID = 2
                    },
            }.AsQueryable());

            #endregion

            #region Добавление зависимостей в Ninject
            ninjectKernel.Bind<IUserRepository>().ToConstant(mockUserRepository.Object);
            ninjectKernel.Bind<IUserTypeRepository>().ToConstant(mockUserTypeRepository.Object);
            ninjectKernel.Bind<IPositionRepository>().ToConstant(mockPositionRepository.Object);
            ninjectKernel.Bind<IUserAddressRepository>().ToConstant(mockAddressRepository.Object);
            ninjectKernel.Bind<IAddressTypeRepository>().ToConstant(mockAddressTypeRepository.Object);
            ninjectKernel.Bind<IPhonesRepository>().ToConstant(mockPhonesRepository.Object);
            ninjectKernel.Bind<IDocumentRepository>().ToConstant(mockDocumentRepository.Object);
            ninjectKernel.Bind<IDocumentTypeRepository>().ToConstant(mockDocumentTypeRepository.Object);
            ninjectKernel.Bind<IViolationRepository>().ToConstant(mockViolationRepository.Object);
            ninjectKernel.Bind<IViolationTypeRepository>().ToConstant(mockViolationTypeRepository.Object);
            ninjectKernel.Bind<IViolatorRepository>().ToConstant(mockViolatorsRepository.Object);
            ninjectKernel.Bind<IEmployerRepository>().ToConstant(mockEmploerRepository.Object);
            ninjectKernel.Bind<IManagersRepository>().ToConstant(mockManagersRepository.Object);

            ninjectKernel.Bind<string>().ToConstant("Test message");
            #endregion
        }
    }
}
