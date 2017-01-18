using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentators.Views.Windows
{
    public interface ILoginView:IWindowView
    {
        #region Actions
        event Action OnLogin;
        #endregion

        #region Properties
        string Login { get; }
        string Password { get; }
        #endregion
    }
}
