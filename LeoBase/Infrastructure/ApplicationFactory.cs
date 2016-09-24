﻿using AppPresentators.Infrastructure;
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

        public IMainPresentator GetMainPresentator(IMainView main, IApplicationFactory appFactory)
        {
            ConstructorArgument[] args = new ConstructorArgument[]
           {
                 new ConstructorArgument("view", main),
                 new ConstructorArgument("appFactory", appFactory)
           };

            return _ninjectKernel.Get<IMainPresentator>(args);
        }

        private void Init()
        {
            #region Presentators
            _ninjectKernel.Bind<IMainPresentator>().To<MainPresentator>();
            _ninjectKernel.Bind<ILoginPresentator>().To<LoginPresentator>();
            _ninjectKernel.Bind<IEmployersPresentator>().To<EmployersPresentator>();
            #endregion

            #region Services
            _ninjectKernel.Bind<ILoginService>().To<TestLoginService>();
            _ninjectKernel.Bind<IPermissonsService>().To<PermissonsService>();
            _ninjectKernel.Bind<IPersonesService>().To<PersonesService>();
            #endregion

            #region Views
            _ninjectKernel.Bind<IMainView>().To<MainView>();
            _ninjectKernel.Bind<ILoginView>().To<LoginView>();
            #endregion

            #region Components
            _ninjectKernel.Bind<IMainMenu>().To<MainMenu>();
            _ninjectKernel.Bind<IEmployersTableControl>().To<EmployerTableControl>();
            #endregion
        }

        public T GetComponent<T>()
        {
            return _ninjectKernel.Get<T>();
        }

        public UIComponent GetComponent(MenuCommand command)
        {
            return new TestComponent
            {
                Title = command.ToString()
            };
        }
    }
}
