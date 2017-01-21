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
using AppPresentators.Components;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System.Threading;
using System.ComponentModel;
using AppData.Contexts;

namespace AppPresentators.Presentators
{
    public class MainPresentator:IMainPresentator
    {
        public object DialogResult { get; set; }
        public IVManager CurrentManager { get; set; }

        private IMainView _mainView;
        private IApplicationFactory _appFactory;
        private static MainPresentator _instance;


        public static MainPresentator GetInstance(IApplicationFactory appFactory)
        {
            if (_instance == null) _instance = new MainPresentator(appFactory);

            return _instance;
        }
        
        public MainPresentator(IApplicationFactory appFactory)
        {
            _mainView = appFactory.GetMainView();
            _appFactory = appFactory;

            _mainView.Login += () => Login();

            _mainView.FastSearchGO += () => FastSearch(_mainView.SearchQuery);

            _instance = this;

            _mainView.GoBackPage += () => GetBackPage();
            _mainView.GoNextPage += () => GetNextPage();
        }
        public void ShowComponentForResult(IComponentPresentator sender, IComponentPresentator executor, ResultTypes resultType)
        {
            executor.ResultType = resultType;

            executor.ShowForResult = true;

            executor.SendResult += (t, d) =>
                {
                    sender.SetResult(t, d);
                    GetBackPage();
                };

            SetNextPage(executor);
        }
        public void SendResult(IComponentPresentator recipient, ResultTypes resultType, object data)
        {

        }
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

            var menuItems = perService.GetMenuItems("admin");

            var mainMenu = _appFactory.GetComponent<IMainMenu>();

            mainMenu.MenuItemSelected += MainMenu_MenuItemSelected;

            mainMenu.AddMenuItems(menuItems);

            _mainView.SetMenu(mainMenu);

            mainMenu.SelecteItem(MenuCommand.Infringement);
        }

        private void FastSearch(string message)
        {
            if(_currentPresentator != null)
            {
                _currentPresentator.FastSearch(message);
            }
        }

        private IComponentPresentator _currentPresentator;
        private List<IComponentPresentator> _pages;
        private int _currentPage;

        public void SetNextPage(IComponentPresentator presentator)
        {
            while (_currentPage + 1 < _pages.Count)
            {
                _pages.RemoveAt(_currentPage + 1);
            }

            if(_currentPage + 1 == _pages.Count)
            {
                _mainView.StartTask();
                _pages.Add(presentator);
                _mainView.ShowFastSearch = presentator.ShowFastSearch;
                _currentPage++;
                _currentPresentator = presentator;
                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(presentator.RenderControl());
                _mainView.EndTask(false);
            }
        }

        public void GetBackPage()
        {
            if(_currentPage >= 1)
            {
                _mainView.StartTask();
                _currentPage--;
                _currentPresentator = _pages[_currentPage];
                _mainView.ShowFastSearch = _currentPresentator.ShowFastSearch;
                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());
                _mainView.EndTask(false);
            }
        }

        public void GetNextPage()
        {
            if(_currentPage + 1 < _pages.Count)
            {
                _mainView.StartTask();
                _currentPage++;
                _currentPresentator = _pages[_currentPage];
                _mainView.ShowFastSearch = _currentPresentator.ShowFastSearch;
                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());
                _mainView.EndTask(false);
            }
        }

        private static bool _taskWork = false;

        private async void MainMenu_MenuItemSelected(MenuCommand command)
        {
            _mainView.StartTask();

            #region Comment
            if (command == MenuCommand.Employees)
            {
                _mainView.ClearCenter();

                _currentPage = 0;

                _pages = new List<IComponentPresentator>();
                var presentator = _appFactory.GetPresentator<IEmployersPresentator>(_mainView);
                _mainView.ShowFastSearch = presentator.ShowFastSearch;
                _currentPresentator = presentator;

                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());

            }
            else if (command == MenuCommand.Violators)
            {
                _mainView.ClearCenter();

                _currentPage = 0;

                _pages = new List<IComponentPresentator>();
                var presentator = _appFactory.GetPresentator<IViolatorTablePresentator>(_mainView);
                _mainView.ShowFastSearch = presentator.ShowFastSearch;
                _currentPresentator = presentator;

                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());

            }
            else if (command == MenuCommand.Infringement)
            {
                _mainView.ClearCenter();

                _currentPage = 0;

                _pages = new List<IComponentPresentator>();
                var presentator = _appFactory.GetPresentator<IAdminViolationTablePresentator>(_mainView);
                _mainView.ShowFastSearch = presentator.ShowFastSearch;
                _currentPresentator = presentator;

                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());

            }
            else if (command == MenuCommand.Options)
            {
                _mainView.ClearCenter();

                _currentPage = 0;

                _pages = new List<IComponentPresentator>();
                var presentator = _appFactory.GetPresentator<IOptionsPresentator>(_mainView);
                _mainView.ShowFastSearch = presentator.ShowFastSearch;
                _currentPresentator = presentator;

                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());
            }
            else if (command == MenuCommand.Map)
            {
                _mainView.ClearCenter();

                _currentPage = 0;

                _pages = new List<IComponentPresentator>();
                var presentator = _appFactory.GetPresentator<IMapPresentator>(_mainView);
                _mainView.ShowFastSearch = presentator.ShowFastSearch;
                _currentPresentator = presentator;

                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());
            }
            else if (command == MenuCommand.Omissions)
            {
                _mainView.ClearCenter();

                _currentPage = 0;

                _pages = new List<IComponentPresentator>();
                var presentator = _appFactory.GetPresentator<IPassesPresentator>(_mainView);
                _mainView.ShowFastSearch = presentator.ShowFastSearch;
                _currentPresentator = presentator;

                _mainView.SetTopControls(_currentPresentator.TopControls);
                _mainView.SetComponent(_currentPresentator.RenderControl());
            }
            else
            {
                _mainView.ShowError("Эта функция будет реализована в следующей версии программы");
                return;
                //_mainView.SetComponent(_appFactory.GetComponent(command).GetControl());
            }

            if (_currentPresentator != null)
            {
                _currentPage = 0;
                _pages.Add(_currentPresentator);
            }

            _mainView.EndTask(false);
            #endregion

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
