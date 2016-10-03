using AppData.Abstract;
using AppData.Abstract.Protocols;
using AppData.Entities;
using AppData.FakesRepositoryes;
using AppData.Repositrys;
using AppData.Repositrys.Violations;
using AppData.Repositrys.Violations.Protocols;
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

        public object GetProtocol(int protocolType)
        {
            return null;
        }

        private void AddBinds()
        {
            ninjectKernel.Bind<IManagersRepository>().To<ManagersRepository>();

            /// Общая информация по гражданам
            ninjectKernel.Bind<IPersoneRepository>().To<PersonesRepository>();
            ninjectKernel.Bind<IPersonePositionRepository>().To<PositionsRepository>();
            ninjectKernel.Bind<IPersoneAddressRepository>().To<AddressRepository>();
            ninjectKernel.Bind<IPhonesRepository>().To<PhonesRepository>();

            /// Репозитории с документами
            ninjectKernel.Bind<IDocumentRepository>().To<DocumentsRepository>();
            ninjectKernel.Bind<IDocumentTypeRepository>().To<DocumentTypesRepository>();

            /// Репозитории с общей информацией о нарушениях
            ninjectKernel.Bind<IViolationRepository>().To<ViolationRepository>();
            ninjectKernel.Bind<IViolationTypeRepository>().To<ViolationTypesRepository>();
            ninjectKernel.Bind<IViolatorRepository>().To<ViolatorsRepository>();
            ninjectKernel.Bind<IEmployerRepository>().To<EmployersRepository>();
            
            /// Протоколы
            ninjectKernel.Bind<IProtocolRepository>().To<ProtocolRepository>();
            ninjectKernel.Bind<IProtocolTypeRepository>().To<ProtocolTypeRepository>();
            ninjectKernel.Bind<IProtocolAboutArrestRepository>().To<ProtocolAboutArrestRepository>();
            ninjectKernel.Bind<IProtocolAboutBringingRepository>().To<ProtocolAboutBringingRepository>();
            ninjectKernel.Bind<IProtocolAboutInspectionAutoRepository>().To<ProtocolAboutInspectionAutoRepository>();
            ninjectKernel.Bind<IProtocolAboutInspectionOrganisationRepository>().To<ProtocolAboutInspectionOrganisationRepository>();
            ninjectKernel.Bind<IProtocolAboutInspectionRepository>().To<ProtocolAboutInspectionRepository>();
            ninjectKernel.Bind<IProtocolAboutViolationOrganisationRepository>().To<ProtocolAboutViolationOrganisationRepository>();
            ninjectKernel.Bind<IProtocolAboutViolationPersoneRepository>().To<ProtocolAboutViolationPersoneRepository>();
            ninjectKernel.Bind<IProtocolAboutWithdrawRepository>().To<ProtocolAboutWithdrawRepository>();
            ninjectKernel.Bind<IRulingForViolationRepository>().To<RulingForViolationRepository>();
            ninjectKernel.Bind<IInjunctionRepository>().To<InjunctionRepository>();
            ninjectKernel.Bind<IInjunctionItemRepository>().To<IInjunctionItemRepository>();
        }
    }
}
