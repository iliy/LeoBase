using AppPresentators.VModels.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components.MainMenu
{
    public delegate void MenuItemSelected(MenuItemModel item);
    public interface IMainMenu:UIComponent
    {
        void AddMenuItem(IMainMenuItem item);
        void AddMenuItems(List<MenuItemModel> items);
        void RemoveMenuItem(IMainMenuItem item);
        void RemoveMenuItem(int index);

        event MenuItemSelected MenuItemSelected;
    }
}
