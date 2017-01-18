using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentators.Views.Windows;

namespace WPFPresentators.Views.Controls
{
    public interface IControlView
    {
        void Render(IMainView main);
    }
}
