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
using MetroFramework.Forms;

namespace LeoBase.Forms
{
    public partial class MainView : MetroForm, IMainView
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
                if (_currentManager != null)
                {
                    lbAccauntInfo.Text = string.Format("Логин: {0}", _currentManager.Login);
                    btnLogout.Enabled = true;
                    accauntInfoPanel.Visible = true;
                }
                else
                {
                    accauntInfoPanel.Visible = false;
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
            if (centerPanel.Controls.IndexOf(component) != -1)
            {
                centerPanel.Controls.Remove(component);
                return true;
            }

            return false;
        }

        public void SetComponent(Control component)
        {
            centerPanel.Controls.Add(component);
        }

        public void MainView_Load(object sender, EventArgs ev)
        {
            MainResize();

            if (_currentManager == null) Login();

            //btnLogOut.Click += (s, e) => Login();
        }

        public void SetMenu(IMainMenu control)
        {
            _mainMenu = control;
            _mainMenu.Resize(menuPanel.Width, menuPanel.Height);
            
            menuPanel.Controls.Clear();
            menuPanel.Controls.Add(_mainMenu.GetControl());
        }

        public void ClearCenter()
        {
            centerPanel.Controls.Clear();
            centerPanel.Refresh();
        }
    }
}
