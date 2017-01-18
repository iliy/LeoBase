using BFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentators.Factorys;
using WPFPresentators.Services;
using WPFPresentators.Views.Windows;

namespace WPFPresentators.Presentators
{
    public class MainPresentator:IPresentator
    {
        private IComponentFactory _appFactory;
        private IMainView _mainView;
        public MainPresentator(IComponentFactory appFactory)
        {
            _appFactory = appFactory;
            _mainView = _appFactory.GetWindow<IMainView>();

            _mainView.BackPage += BackPage;
            _mainView.NextPage += NextPage;
            _mainView.Login += Login;

            _mainView.Show();
        }

        private void Login()
        {
            LoginPresentator loginPresentator = new LoginPresentator(_appFactory.GetWindow<ILoginView>(), _appFactory.GetService<ILoginService>());
            loginPresentator.OnLoginComplete += LoginComplete;

            loginPresentator.Show();
        }

        private void LoginComplete(Manager manager)
        {
            AppConfig.CurrentManager = manager;
        }

        private void BackPage()
        {

        }

        private void NextPage()
        {

        }
    }
}
