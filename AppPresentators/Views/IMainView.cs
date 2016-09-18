using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Views
{
    public interface IMainView:IView
    {
        #region Models
        IVManager Manager { get; set; }
        #endregion

        #region Methods
        void ShowError(string errorMessage);

        #endregion

        #region Actions
        event Action Login;
        #endregion
        
    }
}
