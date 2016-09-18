using AppPresentators.Presentators.Interfaces;
using AppPresentators.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure
{
    public interface IApplicationFactory
    {
        ILoginPresentator GetLoginView(IMainView mainView);
    }
}
