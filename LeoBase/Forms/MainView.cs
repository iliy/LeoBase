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

namespace LeoBase.Forms
{
    public partial class MainView : Form, IMainView
    {
        public event Action Login;

        private IVManager _currentManager;
        public MainView()
        {
            InitializeComponent();
        }

        public IVManager Manager
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

        private void MainView_Load(object sender, EventArgs ev)
        {
            btnLogOut.Click += (s, e) => Login();
        }
    }
}
