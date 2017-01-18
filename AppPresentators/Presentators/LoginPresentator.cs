using AppPresentators.Presentators.Interfaces;
using AppPresentators.Services;
using AppPresentators.Views;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators
{
    public class LoginPresentator : ILoginPresentator
    {
        private IMainView _mainView;
        private ILoginService _service;
        private ILoginView _view;
        
        public event LoginComplete LoginComplete;

        public LoginPresentator(IMainView main, ILoginService service, ILoginView view)
        {
            _mainView = main;
            _service = service;
            _view = view;

            _view.Login += () => Login(_view.UserName, _view.Password);
            _view.Cancel += () => Cancel();
            _view.CloseAndLogin += () => LoginAndCloseForm();
        }

        public void Cancel()
        {
            _view.Close();
            _mainView.Close();
        }

        public void LoginAndCloseForm()
        {
            if (ConfigApp.CurrentManager == null)
            {
                _view.ShowError("Неверные логин или пароль");
            }
            else
            {
                _mainView.Manager = ConfigApp.CurrentManager;
                if (LoginComplete != null) LoginComplete(ConfigApp.CurrentManager);
                _view.Close();
            }
        }


        public void Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                _view.ShowError("Не указано имя пользователя");
            }

            if (string.IsNullOrEmpty(password))
            {
                _view.ShowError("Пароль не указан");
            }

            ConfigApp.CurrentManager = _service.Login(userName, password);
        }

        public void Run()
        {
            _view.Show();
        }
    }
}
