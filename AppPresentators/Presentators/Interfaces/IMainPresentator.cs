using AppPresentators.Views;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces
{
    public interface IMainPresentator:IPresentator
    {
        IMainView MainView { get;}
        IVManager CurrentManager { get; set; }
        void Login();
    }
}
