using AppPresentators.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Presentators.Interfaces.ComponentPresentators
{
    public delegate void SendResult(ResultTypes resultType, object data);

    public interface IComponentPresentator
    {
        bool ShowForResult { get; set; }
        ResultTypes ResultType { get; set; }
        void SetResult(ResultTypes resultType, object data);
        event SendResult SendResult;
        Control RenderControl();
        void FastSearch(string message);
        bool ShowFastSearch { get; }
        bool ShowSearch { get; }
        List<Control> TopControls { get; }
    }
}
