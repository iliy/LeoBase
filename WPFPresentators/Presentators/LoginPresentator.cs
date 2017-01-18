using BFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentators.Services;
using WPFPresentators.Views.Windows;

namespace WPFPresentators.Presentators
{
    public delegate void LoginComplete(Manager manager);
    public class LoginPresentator:IPresentator
    {
        private ILoginView _view;
        private ILoginService _service;

        public event LoginComplete OnLoginComplete;

        public LoginPresentator(ILoginView view, ILoginService service)
        {
            _view = view;
            _service = service;

            _view.OnLogin += () => Login(_view.Login, _view.Password);
        }

        private void Login(string login, string password)
        {
            var manager = _service.Login(login, password);

            if (manager == null) {
                ShowError("Неверно указан логин или пароль.");
                return;
            }

            if (OnLoginComplete != null)
            {
                OnLoginComplete(manager);
                _view.Close();
            }
        }

        public void Show()
        {
            _view.Show();
        }

        public void ShowError(string message)
        {
            _view.ShowError(message);
        }
    }
}
