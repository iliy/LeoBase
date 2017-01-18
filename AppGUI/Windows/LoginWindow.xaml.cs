using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFPresentators.Views.Windows;

namespace AppGUI.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, ILoginView
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public string Login { get
            {
                return txtLogin.Text;
            }
        }

        public string Password { get
            {
                return txtPassword.Text;
            }
        }

        public event Action OnLogin;

        public void ShowError(string message)
        {
            lbError.Content = message;
        }

        public void Show()
        {
            base.ShowDialog();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (OnLogin != null) OnLogin();
        }
    }
}
