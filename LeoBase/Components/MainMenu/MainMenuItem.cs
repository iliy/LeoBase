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
        public event ItemClicked ItemClicked;

        private bool _isActive = false;
        public bool IsActive { get
            {

                return _isActive;
            } set
            {
                _isActive = value;
                if (_isActive)
                {
                    this.BackColor = Color.FromArgb(69, 57, 33);
                    lbTitle.ForeColor = Color.FromArgb(201, 210, 145);
                }else
                {
                    this.BackColor = Color.FromArgb(102, 83, 53);
                    lbTitle.ForeColor = Color.FromArgb(208, 186, 149);
                }
            }
        }

        public MainMenuItem(string title, MenuCommand command):this(title, command, new byte[0])
        {
        }

        public MainMenuItem(string title, MenuCommand command, byte[] icon)
        {
            InitializeComponent();

            MouseMove += MainMenuItem_MouseMove;
            MouseDown += MainMenuItem_MouseDown;

            this.Click += (s, e) => _ItemClicked(command);
            lbTitle.Click += (s, e) => _ItemClicked(command);

            this.MouseMove += (s, e) => Hover();
            this.MouseLeave += (s, e) => Leave();

            lbTitle.MouseMove += (s, e) => Hover();
            lbTitle.MouseLeave += (s, e) => Leave();

            Title = title;
            MenuCommand = command;
            Icon = icon;
        }

        private void Leave()
        {
            if (!_isActive) { 
                this.BackColor = Color.FromArgb(102, 83, 53);
                lbTitle.ForeColor = Color.FromArgb(208, 186, 149);
            }
        }

        private void Hover()
        {
            this.BackColor = Color.FromArgb(69, 57, 33);
            lbTitle.ForeColor = Color.FromArgb(201, 210, 145);
        }

        private void _ItemClicked(MenuCommand command)
        {
            ItemClicked(command);
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

        public bool ShowForResult
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
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
