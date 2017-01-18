using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.Interfaces
{
    public interface IClearable
    {
        void Clear();
        Control GetControl();
        DockStyle Dock { get; set; }
    }
}
