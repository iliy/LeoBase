using AppPresentators.Components;
using AppPresentators.Components.MainMenu;
using AppPresentators.Infrastructure;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Views
{
    public interface IMainView:IView
    {
        #region Models
        VManager Manager { get; set; }
        bool Enabled { get; set; }
        bool ShowFastSearch { get; set; }
        string SearchQuery { get; set; }
        #endregion

        #region Methods
        void ShowError(string errorMessage);
        bool RemoveComponent(Control control);
        void SetComponent(Control control);
        void SetTopControls(List<Control> controls);
        void SetMenu(IMainMenu control);
        void ClearCenter();
        void StartTask();
        void EndTask(bool setOldState);
        void MakeOrder(IOrderPage orderPage);
        #endregion

        #region Actions
        event Action Login;
        event Action FastSearchGO;
        event Action GoNextPage;
        event Action GoBackPage;
        #endregion

    }
}
