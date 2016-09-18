using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces
{
    public interface ILoginPresentator:IPresentator
    {
        void Login(string userName, string password);
    }
}
