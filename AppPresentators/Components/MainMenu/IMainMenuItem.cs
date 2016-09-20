using AppPresentators.VModels.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components.MainMenu
{
    public delegate void ItemClicked(MenuCommand command);
    public interface IMainMenuItem : UIComponent
    {
        string Title { get; set; }
        byte[] Icon { get; set; }
        MenuCommand MenuCommand {get;set;}
        event ItemClicked ItemClicked;
    }
}
