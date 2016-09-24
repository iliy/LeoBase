using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Presentators.Interfaces.ComponentPresentators
{
    public interface IComponentPresentator
    {
        Control RenderControl();
    }
}
