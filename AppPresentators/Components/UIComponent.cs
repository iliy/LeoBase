using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Components
{
    public interface UIComponent
    {
        Control GetControl();
        void Resize(int width, int height);
        bool ShowForResult { get; set; }
        List<Control> TopControls { get; set; }
    }
}
