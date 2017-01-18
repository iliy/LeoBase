using LeoBase.Components.CustomControls.SearchPanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.Interfaces
{
    public interface ISearchPanel
    {
        event Action OnSearchClick;
        event Action OnHideClick;
        void SetCustomSearchPanel(IClearable control);
        Control Control { get; }
        bool SearchButtonVisible { get; set; }
    }
}
