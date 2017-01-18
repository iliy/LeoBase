using AppPresentators;
using AppPresentators.Components;
using AppPresentators.Views;
using LeoBase.Properties;
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
    public partial class LoginView : Form, ILoginView
    {

        public event Action Login;
        public event Action Cancel;
        public event Action CloseAndLogin;

        private bool _wasLoginClicked = false;

        public LoginView()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Text = null;
        }

        public string Password
        {
            get
            {
                if (tbPassword == null) return "";

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
                if (tbLogin == null) return "";

                return tbLogin.Text;
            }

            set
            {
                tbLogin.Text = value;
            }
        }

        private static string _errorMessage = "";

        public void ShowError(string errorMessage)
        {
            _errorMessage = errorMessage;

            //pbLoader.Visible = false;
            //tbLogin.Enabled = true;
            //tbPassword.Enabled = true;

            //_wasLoginClicked = false;
            //lbError.Text = errorMessage;
            //lbError.Visible = true;
        }

        private void LoginView_Load(object sender, EventArgs ev)
        {
            btnLogin.Click += (s, e) =>
            {
                _wasLoginClicked = true;
                Login();
            };

            //tbLogin.BackColor = Color.FromArgb(66, 66, 66);
            //tbPassword.BackColor = Color.FromArgb(66, 66, 66);

            lbError.BackColor = Color.Transparent;

            //tbLogin.ForeColor = Color.FromArgb(138, 138, 138);
            //tbPassword.ForeColor = Color.FromArgb(138, 138, 138);

            tbLogin.GotFocus += tb_GotFocus;
            tbLogin.LostFocus += tb_LostFocus;

            tbPassword.GotFocus += tb_GotFocus;
            tbPassword.LostFocus += tb_LostFocus;

            tbLogin.KeyDown += tb_KeyDown;
            tbPassword.KeyDown += tb_KeyDown;

            this.KeyDown += tb_KeyDown;

            btnCancel.Click += (s, e) => Cancel();
            this.FormClosed += (s, e) =>
            {
                pbLoader.Visible = false;
                if (!_wasLoginClicked)
                    Cancel();
            };

            //tbLogin.Text = "admin";
            //tbPassword.Text = "admin";

            loaderWorker.RunWorkerCompleted += LoaderWorker_RunWorkerCompleted;
            loaderWorker.DoWork += LoaderWorker_DoWork;
        }

        private void LoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Login != null)
            {
                Login();

            }
        }

        private void LoaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(ConfigApp.CurrentManager == null)
            {
                ShowError("Неверные логин или пароль");

                pbLoader.Visible = false;
                tbLogin.Enabled = true;
                tbPassword.Enabled = true;

                _wasLoginClicked = false;
                lbError.Text = _errorMessage;
                lbError.Visible = true;
            }
            else
            {
                if(CloseAndLogin != null)
                    CloseAndLogin();
            }

        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (loaderWorker.IsBusy) return;

                _wasLoginClicked = true;
                pbLoader.Visible = true;
                tbLogin.Enabled = false;
                tbPassword.Enabled = false;

                this.Refresh();

                loaderWorker.RunWorkerAsync();
            }else if(e.KeyCode == Keys.Escape)
            {
                if (Cancel != null) Cancel();
            }
        }

        private void tb_LostFocus(object sender, EventArgs e)
        {
            if (sender == tbLogin)
            {
                pbLogin.Image = Resources.tbAutorizeNotFocuse;
            }
            else
            {
                pbPassword.Image = Resources.tbAutorizeNotFocuse;
            }
        }

        private void tb_GotFocus(object sender, EventArgs e)
        {
            if(sender == tbLogin)
            {
                pbLogin.Image = Resources.tbAutorizeFocuse;
            }else
            {
                pbPassword.Image = Resources.tbAutorizeFocuse;
            }
        }

        public void Show()
        {
            //loaderWorker.RunWorkerAsync();
            ShowDialog();
        }
    }
}
