using AppData.Abstract;
using AppData.Entities;
using AppData.FakesRepositoryes;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Infrastructure
{
    public class RepositoryesFactory
    {
        private static IKernel ninjectKernel = null;
        private static RepositoryesFactory _instance;

        public static RepositoryesFactory GetInstance()
        {
            if(_instance == null)
            {
                _instance = new RepositoryesFactory();
                _instance.Init();
            }

            return _instance;
        }

        private void Init()
        {
            ninjectKernel = new StandardKernel();
            AddBinds();
        }

        public T Get<T>()
        {
            if (!ninjectKernel.CanResolve<T>())
                throw new ArgumentException("Has`t type!");

            return (T)ninjectKernel.Get<T>();
        }

        private void AddBinds()
        {
            #region Создание Mock объектов
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

            #endregion

            #region Добавление зависимостей в Ninject
            ninjectKernel.Bind<IPersoneRepository>().To< FakePersonesRepository>();
            ninjectKernel.Bind<IPersonePositionRepository>().To<FakePersonsPositionRepository>();
            ninjectKernel.Bind<IPersoneAddressRepository>().To<FakePersonesAddressRepository>();

            ninjectKernel.Bind<IPhonesRepository>().To<FakePhonesRepository>();

            ninjectKernel.Bind<IDocumentRepository>().To<FakeDocumentRespository>();
            ninjectKernel.Bind<IDocumentTypeRepository>().To<FakeDocumentTypeRepository>();

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
