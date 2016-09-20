using AppPresentators.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.VModels;
using AppPresentators.Components;
using AppPresentators.Components.MainMenu;

namespace LeoBase.Forms
{
    public partial class MainView : Form, IMainView
    {
        public event Action Login;

        private VManager _currentManager;

        private IMainMenu _mainMenu;
        public MainView()
        {
            InitializeComponent();
            this.Resize += (s, e) => MainResize();
        }
        
        private void MainResize()
        {
            mainFlowLayoutPanel.Width = this.Width;
            mainFlowLayoutPanel.Height = this.Height;

            topFlowLayoutPanel.Width = this.Width;
            topFlowLayoutPanel.Height = 40;

            flowLayoutPanel1.Width = this.Width;
            flowLayoutPanel1.Height = mainFlowLayoutPanel.Height - 40;

            menuFlowLayoutPanel.Width = 200;
            menuFlowLayoutPanel.Height = flowLayoutPanel1.Height;

            centerFlowLayoutPanel.Width = flowLayoutPanel1.Width - menuFlowLayoutPanel.Width;
            centerFlowLayoutPanel.Height = flowLayoutPanel1.Height;

            if (_mainMenu != null)
                _mainMenu.Resize(200, menuFlowLayoutPanel.Height);
        }

        public VManager Manager
        {
            get
            {
                return _currentManager;
            }

            set
            {
                _currentManager = value;
                if(_currentManager != null)
                {
                    btnLogOut.Enabled = true;
                    lbLogin.Text = value.Login;
                    lbPassword.Text = value.Password;
                    lbRole.Text = value.Role;
                    lbUserID.Text = value.ManagerID.ToString();
                }
                else
                {
                    btnLogOut.Enabled = false;
                    lbLogin.Text = "";
                    lbPassword.Text = "";
                    lbRole.Text = "";
                    lbUserID.Text = "";
                }
            }
        }


        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        public void Show()
        {
            Application.Run(this);
        }

        public bool RemoveComponent(Control component)
        {
            if(centerFlowLayoutPanel.Controls.IndexOf(component) != -1)
            {
                centerFlowLayoutPanel.Controls.Remove(component);
                return true;
            }

            return false;
        }

        public void SetComponent(Control component)
        {
            centerFlowLayoutPanel.Controls.Add(component);
        }

        public void MainView_Load(object sender, EventArgs ev)
        {
            MainResize();

            if (_currentManager == null) Login();

            btnLogOut.Click += (s, e) => Login();
        }

        public void SetMenu(IMainMenu control)
        {
            _mainMenu = control;
            menuFlowLayoutPanel.Controls.Clear();
            menuFlowLayoutPanel.Refresh();
            menuFlowLayoutPanel.Controls.Add(_mainMenu.GetControl());
            _mainMenu.Resize(200, menuFlowLayoutPanel.Height);
        }

        public void ClearCenter()
        {
            centerFlowLayoutPanel.Controls.Clear();
            centerFlowLayoutPanel.Refresh();
        }
    }
}
