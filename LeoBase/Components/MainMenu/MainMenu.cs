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
            menuList.Controls.Add(new MainMenuItem
            {
                Icon = item.Icon,
                Title = item.Title,
                MenuCommand = item.MenuCommand
            });
        }

        public void AddMenuItems(List<MenuItemModel> items)
        {
            if (items == null) items = new List<MenuItemModel>();
            foreach(var item in items)
            {
                var mainMenuItem = new MainMenuItem
                {
                    Icon = item.Icon,
                    MenuCommand = item.MenuCommand,
                    Title = item.Title
                };
                mainMenuItem.Click += (s, e) => MenuItemSelected(item);
                menuList.Controls.Add(mainMenuItem);
            }
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
