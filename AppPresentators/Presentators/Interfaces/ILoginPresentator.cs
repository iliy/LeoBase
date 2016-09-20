using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces
{
    public delegate void LoginComplete(VManager manager);
    public interface ILoginPresentator:IPresentator
    {
        event LoginComplete LoginComplete;
        void Login(string userName, string password);
    }
}
