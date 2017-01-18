using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentators.Views.Windows
{
    public interface IWindowView
    {
        void Show();
        void ShowError(string message);
        void Close();
    }
}
