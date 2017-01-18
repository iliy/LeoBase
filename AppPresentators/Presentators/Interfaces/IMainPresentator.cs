using AppPresentators.Infrastructure;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
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
        void AddMenu();
        void SetNextPage(IComponentPresentator presentator);
        void ShowComponentForResult(IComponentPresentator sender, IComponentPresentator executor, ResultTypes resultType);
        void SendResult(IComponentPresentator recipient, ResultTypes resultType, object data);
        void GetBackPage();
        void GetNextPage();
        object DialogResult { get; set; }
    }
}
