using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Views
{
    public interface ILoginView:IView
    {
        string UserName { get; set; }
        string Password { get; set; }
        event Action Login;
        event Action Cancel;
        void ShowError(string errorMessage);
        event Action CloseAndLogin;

    }
}
