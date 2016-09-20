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

        public LoginPresentator(IMainView main, ILoginService service, ILoginView view)
        {
            _mainView = main;
            _service = service;
            _view = view;

            _view.Login += () => Login(_view.UserName, _view.Password);
            _view.Cancel += () => Cancel();
        }

        public void Cancel()
        {
            _view.Close();
            _mainView.Close();
        }

        public void Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("username");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            var manager = _service.Login(userName,password);

            if(manager == null)
            {
                _view.ShowError("Неверные имя пользователя или пароль");
            }else
            {
                _mainView.Manager = manager;
                _view.Close();
            }
        }

        public void Run()
        {
            _view.Show();
        }
    }
}
