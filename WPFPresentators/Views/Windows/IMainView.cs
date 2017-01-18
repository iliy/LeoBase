using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentators.Views.Controls;

namespace WPFPresentators.Views.Windows
{
    public interface IMainView:IWindowView
    {
        #region Actions
        event Action Login;
        event Action BackPage;
        event Action NextPage;
        #endregion

        #region Methods
        void SetCenterPanel(IControlView view);
        void SetTopMenu(IControlView view);
        void SetMainMenu(IControlView view);
        void LoadStart();
        void LoadEnd();
        #endregion
    }
}
