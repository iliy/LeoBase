using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Components
{
    public delegate void AddNewProtocol(int id);
    public interface ISaveOrUpdateViolationControl:UIComponent
    {
        event Action Save;
        event Action FindEmplyer;
        event Action FindViolator;
        event Action FindRulingCreator;
        
        ViolationViewModel Violation { get; set; }

        void AddProtocol(Control control);

        void RemoveProtocol(Control control);

        DialogResult ShowDialog(string title, string message);
    }
}
