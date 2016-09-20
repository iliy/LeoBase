using AppPresentators.VModels.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components.MainMenu
{
    public interface IMainMenuItem : UIComponent
    {
        string Title { get; set; }
        byte[] Icon { get; set; }
        MenuCommand MenuCommand {get;set;}
    }
}
