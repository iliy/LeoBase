using AppPresentators.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Presentators.Interfaces;
using AppPresentators.Views;
using Ninject;
using AppPresentators.Presentators;
using AppPresentators.Services;
using Ninject.Parameters;
using LeoBase.Forms;
using LeoBase.Components.MainMenu;
using AppPresentators.Components.MainMenu;
using AppPresentators.Components;
using AppPresentators.VModels.MainMenu;
using LeoBase.Components;
using LeoBase.Components.CustomControls;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using LeoBase.Components.CustomControls.SaveComponent;
using LeoBase.Components.CustomControls.DetailsComponent;
using AppPresentators.Components.Protocols;
using LeoBase.Components.CustomControls.Protocols;
using AppPresentators.Presentators.Protocols;
using AppPresentators.Presentators.Interfaces.ProtocolsPresentators;

namespace LeoBase.Infrastructure
{
    public class ApplicationFactory : IApplicationFactory
    {
        private IKernel _ninjectKernel;
        public ApplicationFactory()
        {
            _ninjectKernel = new StandardKernel();
            Init();
        }

        public ApplicationFactory(IKernel kernel)
        {
            _ninjectKernel = kernel;
        }

        public T GetView<T>()
        {
            return _ninjectKernel.Get<T>();
        }

        public T GetService<T>()
        {
            return _ninjectKernel.Get<T>();
        }

        public T GetPresentator<T>()
        {
            ConstructorArgument[] args = new ConstructorArgument[]
            {
                  new ConstructorArgument("appFactory", this)
            };

            return _ninjectKernel.Get<T>(args);
        }
        public T GetPresentator<T, V, S>(IMainView main)
        {
            V view = GetView<V>();
            S service = GetService<S>();

            ConstructorArgument[] args = new ConstructorArgument[]
           {
                 new ConstructorArgument("main", main),
                 new ConstructorArgument("service", service),
                 new ConstructorArgument("view", view)
           };

            return _ninjectKernel.Get<T>(args);
        }

        public T GetPresentator<T>(IMainView main)
        {
            ConstructorArgument[] args = new ConstructorArgument[]
               {
                         new ConstructorArgument("main", main),
                         new ConstructorArgument("appFactory", this)
               };

            return _ninjectKernel.Get<T>(args);
        }

        public IMainPresentator GetMainPresentator()
        {
            var mainPresentator = MainPresentator.GetInstance(null);

            return mainPresentator;
        }

        public IMainPresentator GetMainPresentator(IApplicationFactory appFactory)
        {
            var mainPresentator = MainPresentator.GetInstance(appFactory);

            return mainPresentator;
           // ConstructorArgument[] args = new ConstructorArgument[]
           //{
           //      //new ConstructorArgument("view", main),
           //      new ConstructorArgument("appFactory", appFactory)
           //};

           // return _ninjectKernel.Get<IMainPresentator>(args);
        }

        public IMainView GetMainView()
        {
            return MainView.GetInstance();
        }

        private void Init()
        {
            #region Presentators
            _ninjectKernel.Bind<IMainPresentator>().To<MainPresentator>();
            _ninjectKernel.Bind<ILoginPresentator>().To<LoginPresentator>();
            _ninjectKernel.Bind<IEmployersPresentator>().To<EmployersPresentator>();
            _ninjectKernel.Bind<ISaveEmployerPresentator>().To<SavePersonPresentator>();
            _ninjectKernel.Bind<IPersoneDetailsPresentator>().To<PersoneDetailsPresentator>();
            _ninjectKernel.Bind<IViolatorTablePresentator>().To<ViolatorTablePresentator>();
            _ninjectKernel.Bind<IViolationTablePresentator>().To<ViolationTablePresentator>();
            //_ninjectKernel.Bind<IAddOrUpdateViolation>().To<AddOrUpdateViolationPresentator>();
            _ninjectKernel.Bind<ISaveAdminViolationPresentatar>().To<SaveAdminViolationPresentator>();
            _ninjectKernel.Bind<IAdminViolationTablePresentator>().To<AdminViolationTablePresentator>();
            _ninjectKernel.Bind<IViolationDetailsPresentator>().To<ViolationDetailsPresentator>();
            _ninjectKernel.Bind<IViolatorDetailsPresentator>().To<ViolatorDetailsPresentator>();
            _ninjectKernel.Bind<IEmployerDetailsPresentator>().To<EmploerDetailsPresentator>();
            _ninjectKernel.Bind<IOptionsPresentator>().To<OptionsPresentator>();
            _ninjectKernel.Bind<IMapPresentator>().To<MapPresentator>();
            _ninjectKernel.Bind<IPassesPresentator>().To<PassesPresentator>();
            _ninjectKernel.Bind<IEditPassPresentator>().To<EditPassPresentator>();

            _ninjectKernel.Bind<IPassDeatailsPresentator>().To<PassDetailsPresentator>();

            #endregion

            #region Services
            _ninjectKernel.Bind<ILoginService>().To<TestLoginService>();
            _ninjectKernel.Bind<IPermissonsService>().To<PermissonsService>();
            _ninjectKernel.Bind<IPersonesService>().To<PersonesService>();
            _ninjectKernel.Bind<IViolationService>().To<ViolationService>();

            _ninjectKernel.Bind<IAdminViolationService>().To<AdminViolationService>();
            #endregion

            #region Views
            _ninjectKernel.Bind<IMainView>().To<MainView>();
            _ninjectKernel.Bind<ILoginView>().To<LoginView>();
            #endregion

            #region Components
            _ninjectKernel.Bind<IMainMenu>().To<MainMenu>();

            _ninjectKernel.Bind<IEmployerDetailsControl>().To<LeoBase.Components.CustomControls.NewControls.EmployerDetailsControl>();

            _ninjectKernel.Bind<IEmployersTableControl>().To<LeoBase.Components.CustomControls.NewControls.EmployerTableControl>();

            _ninjectKernel.Bind<IOptionsControl>().To<LeoBase.Components.CustomControls.NewControls.OptionsPanel.OptionsControl>();

            _ninjectKernel.Bind<ISavePersonControl>().To<Components.CustomControls.NewControls.PersoneSavePanel> ();

            _ninjectKernel.Bind<IViolatorDetailsControl>().To<Components.CustomControls.NewControls.ViolatorDetailsControl>();

            _ninjectKernel.Bind<IPersoneDetailsControl>().To<PersoneDetailsControl>();

            _ninjectKernel.Bind<IViolatorTableControl>().To<Components.CustomControls.NewControls.ViolatorTableControl>();
            _ninjectKernel.Bind<IViolationTableControl>().To<ViolationTableControl>();
            _ninjectKernel.Bind<ILoadedControl>().To<LoadedControl>();
            _ninjectKernel.Bind<ISaveOrUpdateViolationControl>().To<SaveViolationControl>();
            
            _ninjectKernel.Bind<IAdminViolationControl>().To<AdminintrationViolationTableControl>();

            _ninjectKernel.Bind<ISaveAdminViolationControl>().To<Components.CustomControls.NewControls.SaveViolationControl>();

            _ninjectKernel.Bind<IViolationDetailsControl>().To<LeoBase.Components.CustomControls.NewControls.ViolationDetailsControl>();
            _ninjectKernel.Bind<IMapControl>().To<LeoBase.Components.CustomControls.NewControls.MapControl>();
            _ninjectKernel.Bind<IPassesTableControl>().To<Components.CustomControls.NewControls.PassesTableControl>();
            _ninjectKernel.Bind<IEditPassControl>().To<Components.CustomControls.NewControls.SavePassControl>();

            _ninjectKernel.Bind<IPassDetailsControl>().To<Components.CustomControls.NewControls.PassDetailsControl>();
            #endregion

            #region ProtocolsView
            _ninjectKernel.Bind<IProtocolAboutViolationView>().To<ViolationAboutPersoneView>();
            #endregion

            #region ProtocolsPresentators
            _ninjectKernel.Bind<IProtocolAboutViolationPresentator>().To<ProtocolAboutViolationPresentator>();

            #endregion
        }

        public T GetComponent<T>()
        {
            return _ninjectKernel.Get<T>();
        }

        public UIComponent GetComponent(MenuCommand command)
        {
            return null;/* TestComponent
            {
                Title = command.ToString()
            };*/
        }

    }
}
