using AppPresentators.Components;
using AppPresentators.Views;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Forms
{
    public partial class LoginView : MetroForm, ILoginView
    {

        public event Action Login;
        public event Action Cancel;

        private bool _wasLoginClicked = false;

        public LoginView()
        {
            InitializeComponent();
            this.Resizable = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        public string Password
        {
            get
            {
                return tbPassword.Text;
            }

            set
            {
                tbPassword.Text = value;
            }
        }

        public string UserName
        {
            get
            {
                return tbLogin.Text;
            }

            set
            {
                tbLogin.Text = value;
            }
        }

        public void ShowError(string errorMessage)
        {
            _wasLoginClicked = false;
            MessageBox.Show(errorMessage);
        }

        private void LoginView_Load(object sender, EventArgs ev)
        {
            btnLogin.Click += (s, e) =>
            {
                _wasLoginClicked = true;
                Login();
            };
            btnCancel.Click += (s, e) => Cancel();
            this.FormClosed += (s, e) =>
            {
                if (!_wasLoginClicked)
                    Cancel();
            };
        }

        public void Show()
        {
            ShowDialog();
        }
    }
}
