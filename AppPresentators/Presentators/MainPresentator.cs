using AppPresentators.Presentators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Views;
using AppPresentators.VModels;
using AppPresentators.Infrastructure;

namespace AppPresentators.Presentators
{
    public class MainPresentator:IMainPresentator
    {
        private IMainView _mainView;
        private IApplicationFactory _appFactory;
       
        public MainPresentator(IMainView mainView, IApplicationFactory appFactory)
        {
            _mainView = mainView;
            _appFactory = appFactory;

            _mainView.Login += () => Login();
        }

        public IVManager CurrentManager { get; set; }

        public IMainView MainView
        {
            get
            {
                return _mainView;
            }
        }

        public void Login()
        {
            _mainView.Manager = null;
            var loginPresentator = _appFactory.GetLoginView(_mainView);
            loginPresentator.Run();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
