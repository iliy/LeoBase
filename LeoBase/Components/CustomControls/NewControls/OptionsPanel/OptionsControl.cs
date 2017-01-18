using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Components;
using AppData.Entities;
using AppPresentators;
using LeoBase.Components.CustomControls.NewControls.OptionsPanel.Dialogs;

namespace LeoBase.Components.CustomControls.NewControls.OptionsPanel
{
    public partial class OptionsControl : UserControl, IOptionsControl
    {
        public OptionsControl()
        {
            InitializeComponent();

            tbManagerLogin.Text = ConfigApp.CurrentManager.Login;
            
            groupDocumentTypes.Visible = ConfigApp.CurrentManager.Role.Equals("admin");


        }

        private List<DocumentType> _docTypes;

        private int _selectedDocumentTypeID = -1;

        public List<DocumentType> DocumentTypes
        {
            get
            {
                return _docTypes;
            }

            set
            {
                _docTypes = value;

                if(value != null)
                {
                    UpdateDocumentTypesTable();
                }
            }
        }

        private List<EmploeyrPosition> _positions;

        public List<EmploeyrPosition> Positions
        {
            get
            {
                return _positions;
            }

            set
            {
                _positions = value;

                if(value != null)
                {
                    UpdateEmployerPositionsTable();
                }
            }
        }


        private void UpdateDocumentTypesTable()
        {
            if (!ConfigApp.CurrentManager.Role.Equals("admin"))
            {
                groupDocumentTypes.Visible = false;

                return;
            }

            cmbDocumentTypes.ValueMember = "DocumentTypeID";

            cmbDocumentTypes.DisplayMember = "Name";

            cmbDocumentTypes.DataSource = _docTypes;
        }

        private void UpdateEmployerPositionsTable()
        {
            if (!ConfigApp.CurrentManager.Role.Equals("admin"))
            {
                return;
            }

        }
        
        private void cmbDocumentTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region Interface Realisation
        public bool ShowForResult { get; set; }

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

        public event Action<int> RemoveDocumentType;
        public event Action<string, int> SaveDocumentType;
        public event Action<string, string, string> UpdateCurrentManager;

        public Control GetControl()
        {
            return this;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        void UIComponent.Resize(int width, int height)
        {
        }

        #endregion

        private void btnSaveCurrentManager_Click(object sender, EventArgs e)
        {
            ConfirmPassword confirmDialog = new ConfirmPassword();

            string newPassword = tbManagerPassword1.Text;

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 5)
            {
                ShowError("Пароль должен быть не короче пяти символов.");

                return;
            }

            if (!newPassword.Equals(tbManagerPassword2.Text))
            {
                ShowError("Пароль и подтверждение пароля не совпадают.");

                return;
            }

            if (confirmDialog.ShowDialog() == DialogResult.OK)
            {
                if (!confirmDialog.Password.Equals(ConfigApp.CurrentManager.Password))
                {
                    ShowError("Введен неверный пароль!");

                    return;
                }
                
                if(UpdateCurrentManager != null)
                {
                    UpdateCurrentManager(tbManagerLogin.Text, confirmDialog.Password, tbManagerPassword1.Text);
                }
            }
        }

        private void btnRemoveDocumentType_Click(object sender, EventArgs e)
        {
            if (cmbDocumentTypes.SelectedValue == null)
            {
                ShowError("Выберите тип документа в выпадающем списке");

                return;
            }
            
            int selectedID = (int)cmbDocumentTypes.SelectedValue;
                
            if(RemoveDocumentType != null)
            {
                RemoveDocumentType(selectedID);
            }
        }

        private void btnUpdateDocumentType_Click(object sender, EventArgs e)
        {
            if (cmbDocumentTypes.SelectedValue == null)
            {
                ShowError("Выберите тип документа в выпадающем списке");

                return;
            }

            int selectedID = (int)cmbDocumentTypes.SelectedValue;

            InputDialog dialog = new InputDialog();

            dialog.Title = "Введите новое название для типа документа:";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                if(string.IsNullOrWhiteSpace(dialog.Result) || dialog.Result.Length < 5)
                {
                    ShowError("Длина названия для типа документов не может быть меньше пяти символов.");

                    return;
                }

                if(SaveDocumentType != null)
                {
                    SaveDocumentType(dialog.Result, selectedID);
                }
            }
        }

        private void btnAddDocumentType_Click(object sender, EventArgs e)
        {
            string docTypeName = tbNewDocumentType.Text;

            if (string.IsNullOrWhiteSpace(docTypeName) || docTypeName.Length < 5)
            {
                ShowError("Длина названия для типа документов не может быть меньше пяти символов.");

                return;
            }

            if (SaveDocumentType != null)
            {
                SaveDocumentType(docTypeName, -1);

                tbNewDocumentType.Text = "";
            }
        }
    }
}
