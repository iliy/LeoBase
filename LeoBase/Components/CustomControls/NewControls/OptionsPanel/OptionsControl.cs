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
        public event Func<List<Manager>> GetManagers;

        public event Action<Manager> AddManager;

        public event Action<Manager> UpdateManager;

        public event Action<Manager> RemoveManager;

        private List<Manager> _managers;

        private Manager _selectedManager;
        /*
         * 
                    {"user", "Пользователь"},
                    {"passesManager","Добавляет пропуски" }
         * */
        public OptionsControl()
        {
            InitializeComponent();

            tbManagerLogin.Text = ConfigApp.CurrentManager.Login;
            
            groupDocumentTypes.Visible = ConfigApp.CurrentManager.Role.Equals("admin");

            cmbUserRole.Items.Add("Пользователь");

            cmbUserRole.Items.Add("Добавляет пропуски");

            cmbUserRole.SelectedIndex = 0;

            if (ConfigApp.CurrentManager.Role.Equals("admin"))
            {
                usersGroupBox.Visible = true;

                if(GetManagers != null)
                {
                    _managers = GetManagers();

                    var tableSource = _managers.Select(m => new ManagerTableModel
                    {
                        Role = ConfigApp.ManagerRoleTranslate[m.Role],
                        Login = m.Login
                    }).ToList();

                    managersTable.DataSource = new BindingList<ManagerTableModel>(tableSource);

                    managersTable.Update();

                    managersTable.Refresh();

                    managersTable.CellClick += CellClicked;

                   
                }

            }else
            {
                usersGroupBox.Visible = false;
            }
        }

        private void CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            
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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (!tbUserPassword.Text.Equals(tbUserPasswordAgain.Text))
            {
                ShowError("Пароли не совпадают!");
                return;
            }

            string role = cmbUserRole.Items[cmbUserRole.SelectedIndex].ToString();


            if (string.IsNullOrEmpty(role))
            {
                ShowError("Не указаны права пользователя!");
                return;
            }

            if (!ConfigApp.ManagerRoleTranslate.ContainsValue(role))
            {
                ShowError("Права для пользователя не определены!");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbUserPassword.Text))
            {
                ShowError("Укажите пароль для нового пользователя!");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbUserLogin.Text))
            {
                ShowError("Укажите логин для нового пользователя!");
                return;
            }

            if (_managers.FirstOrDefault(m => m.Login.Equals(tbUserLogin.Text)) != null)
            {
                ShowError("Пользователь с таким логином уже существует!");
                return;
            }

            string roleTranslate = ConfigApp.ManagerRoleTranslate.FirstOrDefault(p => p.Value.Equals(role)).Key;

            Manager manager = new Manager
            {
                Role = roleTranslate,
                Login = tbUserLogin.Text,
                Password = tbUserPassword.Text
            };

            if (AddManager != null)
            {
                AddManager(manager);
            }

            UpdateManagerTable();

            tbUserLogin.Text = "";
            tbUserPassword.Text = "";
            tbUserPasswordAgain.Text = "";
        }

        public void UpdateManagerTable()
        {

            _selectedManager = null;

            btnDeleteUser.Enabled = false;

            btnResetPassword.Enabled = false;

            if (GetManagers != null)
            {
                _managers = GetManagers();

                var tableSource = _managers.Select(m => new ManagerTableModel
                {
                    Role = ConfigApp.ManagerRoleTranslate[m.Role],
                    Login = m.Login
                }).ToList();

                managersTable.DataSource = new BindingList<ManagerTableModel>(tableSource);

                managersTable.Update();

                managersTable.Refresh();
            }

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (_selectedManager == null) return;

            if(RemoveManager != null)
            {
                RemoveManager(_selectedManager);

                UpdateManagerTable();
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (_selectedManager == null) return;

            SetNewPasswordDialog dialog = new SetNewPasswordDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _selectedManager.Password = dialog.NewPassword;

                if (UpdateManager != null) UpdateManager(_selectedManager);

                UpdateManagerTable();
            }
        }

        private void managersTable_Click(object sender, EventArgs e)
        {
            
        }

        private void managersTable_SelectionChanged(object sender, EventArgs e)
        {
            btnDeleteUser.Enabled = false;

            btnResetPassword.Enabled = false;

            if (managersTable.SelectedRows.Count == 0) return;

            int row = managersTable.SelectedRows[0].Index;

            if (_managers == null || row < 0 || row >= _managers.Count) return;

            _selectedManager = _managers[row];

            btnDeleteUser.Enabled = true;

            btnResetPassword.Enabled = true;
        }
    }

    public class ManagerTableModel
    {
        [DisplayName("Логин")]
        [ReadOnly(true)]
        public string Login { get; set; }
        [DisplayName("Права")]
        [ReadOnly(true)]
        public string Role { get; set; }
    }
}
