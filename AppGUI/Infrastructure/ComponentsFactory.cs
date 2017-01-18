using AppGUI.Windows;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentators.Factorys;
using WPFPresentators.Services;
using WPFPresentators.Views.Controls;
using WPFPresentators.Views.Windows;

namespace AppGUI.Infrastructure
{
    public class ComponentsFactory : IComponentFactory
    {
        private IUnityContainer _container;
        public ComponentsFactory()
        {
            _container = new UnityContainer();
            Init();
        }

        public T GetControl<T>() where T : IControlView
        {
            throw new NotImplementedException();
        }

        public T GetPresentator<T>()
        {
            throw new NotImplementedException();
        }

        public T GetService<T>()
        {
            if (!_container.IsRegistered<T>()) throw new ArgumentException("Тип не найден");

            return _container.Resolve<T>();
        }

        public T GetWindow<T>() where T : IWindowView
        {
            if (!_container.IsRegistered<T>()) throw new ArgumentException("Тип не найден");

            return _container.Resolve<T>();
        }

        public void Init()
        {
            #region Windows
            _container.RegisterType<IMainView, MainWindow>();
            _container.RegisterType<ILoginView, LoginWindow>();
            #endregion

            #region Services
            _container.RegisterType<ILoginService, LoginService>();
            #endregion

        }
    }
}
