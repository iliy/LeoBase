using AppPresentators.Infrastructure;
using AppPresentators.VModels.MainMenu;
using AppPresentators.VModels.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Components.Protocols
{
    public interface IProtocolView
    {
        event Action Remove;
        ProtocolViewModel Protocol { get; set;}
        void SetResult(ResultTypes resultType, object data);
        Control GetControl();
    }
}
