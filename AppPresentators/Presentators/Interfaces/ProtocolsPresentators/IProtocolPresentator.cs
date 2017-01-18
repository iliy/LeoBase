using AppPresentators.Infrastructure;
using AppPresentators.VModels.MainMenu;
using AppPresentators.VModels.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Presentators.Interfaces.ProtocolsPresentators
{
    public delegate void RemoveProtocolEvent(int key);

    public interface IProtocolPresentator
    {
        event RemoveProtocolEvent OnRemove;
        ProtocolViewModel Protocol { get; set; }
        int Key { get; set; }
        Control GetControl();
        void Save();
        void Remove();
        void SetResult(ResultTypes command, object data);
    }
}
