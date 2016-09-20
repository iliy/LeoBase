using AppPresentators.Presentators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Views;
using AppPresentators.VModels;
using AppPresentators.Infrastructure;
using AppPresentators.Services;
using AppPresentators.Components.MainMenu;
using AppPresentators.VModels.MainMenu;

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

        public void AddMenu()
        {
            var user = _mainView.Manager;

            if (user == null)
            {
                Login();
                return;
            }

            var perService = _appFactory.GetService<IPermissonsService>();

            var menuItems = perService.GetMenuItems(user.Role);

            var mainMenu = _appFactory.GetComponent<IMainMenu>();

            mainMenu.MenuItemSelected += MainMenu_MenuItemSelected;

            mainMenu.AddMenuItems(menuItems);

            _mainView.SetMenu(mainMenu);
        }

        private void MainMenu_MenuItemSelected(MenuCommand command)
        {
            // Add to center, clear old center and stack
            _mainView.ClearCenter();
            _mainView.SetComponent(_appFactory.GetComponent(command).GetControl());
        }

        public void Login()
        {
            _mainView.Manager = null;
            var loginPresentator = _appFactory.GetPresentator<ILoginPresentator, ILoginView, ILoginService>(_mainView);
            loginPresentator.LoginComplete += (m) => LoginComplete(m);
            loginPresentator.Run();
        }

        public void LoginComplete(VManager manager)
        {
            _mainView.Manager = manager;
            AddMenu();
        }

        public void Run()
        {
            _mainView.Show();
        }
    }
}
