using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Components.MainMenu;
using AppPresentators.VModels.MainMenu;

namespace LeoBase.Components.MainMenu
{
    public partial class MainMenu : UserControl, IMainMenu
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public event MenuItemSelected MenuItemSelected;

        public void AddMenuItem(IMainMenuItem item)
        {
            menuList.Controls.Add(new MainMenuItem(item.Title, item.MenuCommand, item.Icon));
        }

        public void AddMenuItems(List<MenuItemModel> items)
        {
            if (items == null) items = new List<MenuItemModel>();
            foreach(var item in items)
            {
                var mainMenuItem = new MainMenuItem(
                    item.Title,
                    item.MenuCommand,
                    item.Icon
                );

                mainMenuItem.ItemClicked += (command) =>
                {
                    SelectItem(mainMenuItem);
                    MenuItemSelected(command);
                };

                menuList.Controls.Add(mainMenuItem);
            }
        }
        
        private void SelectItem(MainMenuItem item)
        {

        }

        public void RemoveMenuItem(int index)
        {
            if (index < 0 || index >= menuList.Controls.Count) return;
            menuList.Controls.RemoveAt(index);
            menuList.Refresh();
        }

        public void RemoveMenuItem(IMainMenuItem item)
        {

        }

        public Control GetControl()
        {
            return this;
        }

        public void Resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            menuList.Width = width;
            menuList.Height = height;
        }
    }
}
