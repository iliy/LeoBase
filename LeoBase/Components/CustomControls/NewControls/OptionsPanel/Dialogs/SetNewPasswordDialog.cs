using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.NewControls.OptionsPanel.Dialogs
{
    public partial class SetNewPasswordDialog : Form
    {
        public string NewPassword { get; set; }
        public SetNewPasswordDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!tbPassword.Text.Equals(tbPasswordAgain.Text))
            {
                ShowError("Введенные вами пароли не совпадают");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                ShowError("Введите новый пароль");
                return;
            }

            if(tbPassword.Text.Length < 5)
            {
                ShowError("Пароль должен быть длинее");
                return;
            }

            NewPassword = tbPassword.Text;

            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void ShowError(string message)
        {
            lbError.Text = message;
            lbError.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
