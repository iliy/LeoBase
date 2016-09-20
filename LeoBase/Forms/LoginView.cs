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

namespace LeoBase.Forms
{
    public partial class LoginView : Form, ILoginView
    {

        public event Action Login;
        public event Action Cancel;
        public LoginView()
        {
            InitializeComponent();
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
            MessageBox.Show(errorMessage);
        }

        private void LoginView_Load(object sender, EventArgs ev)
        {
            btnLogin.Click += (object s, EventArgs e) => Login();
            btnCancel.Click += (object s, EventArgs e) => Cancel();
        }

        public void Show()
        {
            ShowDialog();
        }
    }
}
