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
    public partial class MainMenuItem : UserControl, IMainMenuItem
    {
        public MainMenuItem()
        {
            InitializeComponent();
            MouseMove += MainMenuItem_MouseMove;
            MouseDown += MainMenuItem_MouseDown;
        }

        private void MainMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void MainMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        public byte[] Icon
        {
            get
            {
                return new byte[0];
            }

            set
            {
                
            }
        }

        public MenuCommand MenuCommand { get; set; }

        public string Title
        {
            get
            {
                return lbTitle.Text;
            }

            set
            {
                lbTitle.Text = value;
            }
        }

        public Control GetControl()
        {
            return this;
        }

        public void Resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
