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
        public bool ShowForResult
        {
            get
            {
                return false;
            }

            set
            {
            }
        }

        public List<Control> TopControls
        {
            get
            {
                return new List<Control>();
            }

            set
            {
            }
        }

        public MainMenu()
        {
            InitializeComponent();

        }

       

        public event MenuItemSelected MenuItemSelected;

        public void AddMenuItem(IMainMenuItem item)
        {
            MainMenuItem it = new MainMenuItem(item.Title, item.MenuCommand, item.Icon);
            it.Width = menuList.Width;
            it.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            menuList.Controls.Add(it);
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
            foreach(var i in this.Controls)
            {
                if(i is MainMenuItem)
                {
                    ((MainMenuItem)i).IsActive = false;
                }
            }
            foreach(var i in menuList.Controls)
            {
                if(i is MainMenuItem)
                {
                    ((MainMenuItem)i).IsActive = false;
                }
            }
            item.IsActive = true;
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
            menuList.Width = width - 3;
            menuList.Height = height - 2;
            foreach(var item in menuList.Controls)
            {
                ((Control)item).Width = menuList.Width;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Pen penLeft = new Pen(Color.FromArgb(86, 53, 36), 2);
            Pen penTop = new Pen(Color.FromArgb(79, 85, 25), 2);
            Pen penRight = new Pen(Color.FromArgb(88, 74, 47), 2);

            e.Graphics.DrawLine(penLeft, new Point(1, 1), new Point(1, this.Height));
            e.Graphics.DrawLine(penRight, new Point(this.Width - 1, 1), new Point(this.Width - 1, this.Height));

            e.Graphics.DrawLine(penTop, new Point(1, 1), new Point(this.Width, 1));
            e.Graphics.DrawLine(penLeft, new Point(1, this.Height - 1), new Point(this.Width, this.Height - 1));
        }

        public void SelecteItem(MenuCommand command)
        {
            foreach(var item in menuList.Controls)
            {
                if(item is MainMenuItem)
                {
                    if(((MainMenuItem)item).MenuCommand == command) { 
                        SelectItem(item as MainMenuItem);
                        MenuItemSelected(command);
                        break;
                    }
                }
            }
        }
    }
}
