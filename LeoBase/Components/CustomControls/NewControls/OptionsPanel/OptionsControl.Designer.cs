namespace LeoBase.Components.CustomControls.NewControls.OptionsPanel
{
    partial class OptionsControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbManagerPassword2 = new System.Windows.Forms.TextBox();
            this.tbManagerPassword1 = new System.Windows.Forms.TextBox();
            this.tbManagerLogin = new System.Windows.Forms.TextBox();
            this.btnSaveCurrentManager = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupDocumentTypes = new System.Windows.Forms.GroupBox();
            this.btnRemoveDocumentType = new System.Windows.Forms.Button();
            this.btnUpdateDocumentType = new System.Windows.Forms.Button();
            this.cmbDocumentTypes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddDocumentType = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.usersGroupBox = new System.Windows.Forms.GroupBox();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.managersTable = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbUserRole = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbUserPasswordAgain = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.tbUserLogin = new System.Windows.Forms.TextBox();
            this.tbNewDocumentType = new LeoBase.Components.CustomControls.LeoTextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupDocumentTypes.SuspendLayout();
            this.usersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.managersTable)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 46);
            this.panel1.TabIndex = 7;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbTitle.Location = new System.Drawing.Point(4, 6);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(120, 26);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "Настройки";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbManagerPassword2);
            this.groupBox1.Controls.Add(this.tbManagerPassword1);
            this.groupBox1.Controls.Add(this.tbManagerLogin);
            this.groupBox1.Controls.Add(this.btnSaveCurrentManager);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(969, 147);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Текущий пользователь";
            // 
            // tbManagerPassword2
            // 
            this.tbManagerPassword2.Location = new System.Drawing.Point(152, 84);
            this.tbManagerPassword2.Name = "tbManagerPassword2";
            this.tbManagerPassword2.PasswordChar = '*';
            this.tbManagerPassword2.Size = new System.Drawing.Size(199, 20);
            this.tbManagerPassword2.TabIndex = 9;
            // 
            // tbManagerPassword1
            // 
            this.tbManagerPassword1.Location = new System.Drawing.Point(152, 51);
            this.tbManagerPassword1.Name = "tbManagerPassword1";
            this.tbManagerPassword1.PasswordChar = '*';
            this.tbManagerPassword1.Size = new System.Drawing.Size(199, 20);
            this.tbManagerPassword1.TabIndex = 8;
            // 
            // tbManagerLogin
            // 
            this.tbManagerLogin.Location = new System.Drawing.Point(152, 18);
            this.tbManagerLogin.Name = "tbManagerLogin";
            this.tbManagerLogin.Size = new System.Drawing.Size(199, 20);
            this.tbManagerLogin.TabIndex = 7;
            // 
            // btnSaveCurrentManager
            // 
            this.btnSaveCurrentManager.Location = new System.Drawing.Point(253, 118);
            this.btnSaveCurrentManager.Name = "btnSaveCurrentManager";
            this.btnSaveCurrentManager.Size = new System.Drawing.Size(98, 23);
            this.btnSaveCurrentManager.TabIndex = 6;
            this.btnSaveCurrentManager.Text = "Сохранить";
            this.btnSaveCurrentManager.UseVisualStyleBackColor = true;
            this.btnSaveCurrentManager.Click += new System.EventHandler(this.btnSaveCurrentManager_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Повторите новый пароль:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Новый пароль:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин:";
            // 
            // groupDocumentTypes
            // 
            this.groupDocumentTypes.Controls.Add(this.btnRemoveDocumentType);
            this.groupDocumentTypes.Controls.Add(this.btnUpdateDocumentType);
            this.groupDocumentTypes.Controls.Add(this.cmbDocumentTypes);
            this.groupDocumentTypes.Controls.Add(this.label5);
            this.groupDocumentTypes.Controls.Add(this.btnAddDocumentType);
            this.groupDocumentTypes.Controls.Add(this.tbNewDocumentType);
            this.groupDocumentTypes.Controls.Add(this.label4);
            this.groupDocumentTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupDocumentTypes.Location = new System.Drawing.Point(0, 193);
            this.groupDocumentTypes.Name = "groupDocumentTypes";
            this.groupDocumentTypes.Size = new System.Drawing.Size(969, 130);
            this.groupDocumentTypes.TabIndex = 9;
            this.groupDocumentTypes.TabStop = false;
            this.groupDocumentTypes.Text = "Типы документов";
            this.groupDocumentTypes.Visible = false;
            // 
            // btnRemoveDocumentType
            // 
            this.btnRemoveDocumentType.Location = new System.Drawing.Point(154, 92);
            this.btnRemoveDocumentType.Name = "btnRemoveDocumentType";
            this.btnRemoveDocumentType.Size = new System.Drawing.Size(98, 23);
            this.btnRemoveDocumentType.TabIndex = 11;
            this.btnRemoveDocumentType.Text = "Удалить";
            this.btnRemoveDocumentType.UseVisualStyleBackColor = true;
            this.btnRemoveDocumentType.Click += new System.EventHandler(this.btnRemoveDocumentType_Click);
            // 
            // btnUpdateDocumentType
            // 
            this.btnUpdateDocumentType.Location = new System.Drawing.Point(258, 92);
            this.btnUpdateDocumentType.Name = "btnUpdateDocumentType";
            this.btnUpdateDocumentType.Size = new System.Drawing.Size(98, 23);
            this.btnUpdateDocumentType.TabIndex = 10;
            this.btnUpdateDocumentType.Text = "Изменить";
            this.btnUpdateDocumentType.UseVisualStyleBackColor = true;
            this.btnUpdateDocumentType.Click += new System.EventHandler(this.btnUpdateDocumentType_Click);
            // 
            // cmbDocumentTypes
            // 
            this.cmbDocumentTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocumentTypes.FormattingEnabled = true;
            this.cmbDocumentTypes.Location = new System.Drawing.Point(156, 65);
            this.cmbDocumentTypes.Name = "cmbDocumentTypes";
            this.cmbDocumentTypes.Size = new System.Drawing.Size(197, 21);
            this.cmbDocumentTypes.TabIndex = 9;
            this.cmbDocumentTypes.SelectedIndexChanged += new System.EventHandler(this.cmbDocumentTypes_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Тип документа:";
            // 
            // btnAddDocumentType
            // 
            this.btnAddDocumentType.Location = new System.Drawing.Point(370, 23);
            this.btnAddDocumentType.Name = "btnAddDocumentType";
            this.btnAddDocumentType.Size = new System.Drawing.Size(98, 23);
            this.btnAddDocumentType.TabIndex = 7;
            this.btnAddDocumentType.Text = "Добавить";
            this.btnAddDocumentType.UseVisualStyleBackColor = true;
            this.btnAddDocumentType.Click += new System.EventHandler(this.btnAddDocumentType_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Навзание нового типа:";
            // 
            // usersGroupBox
            // 
            this.usersGroupBox.Controls.Add(this.btnResetPassword);
            this.usersGroupBox.Controls.Add(this.btnDeleteUser);
            this.usersGroupBox.Controls.Add(this.managersTable);
            this.usersGroupBox.Controls.Add(this.panel2);
            this.usersGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.usersGroupBox.Location = new System.Drawing.Point(0, 323);
            this.usersGroupBox.Name = "usersGroupBox";
            this.usersGroupBox.Size = new System.Drawing.Size(969, 241);
            this.usersGroupBox.TabIndex = 10;
            this.usersGroupBox.TabStop = false;
            this.usersGroupBox.Text = "Пользователи";
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Enabled = false;
            this.btnResetPassword.Location = new System.Drawing.Point(281, 113);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(119, 23);
            this.btnResetPassword.TabIndex = 13;
            this.btnResetPassword.Text = "Изменить пароль";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Enabled = false;
            this.btnDeleteUser.Location = new System.Drawing.Point(281, 84);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(119, 23);
            this.btnDeleteUser.TabIndex = 12;
            this.btnDeleteUser.Text = "Удалить";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // managersTable
            // 
            this.managersTable.AllowUserToAddRows = false;
            this.managersTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.managersTable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.managersTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.managersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.managersTable.Location = new System.Drawing.Point(6, 84);
            this.managersTable.MultiSelect = false;
            this.managersTable.Name = "managersTable";
            this.managersTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.managersTable.Size = new System.Drawing.Size(265, 151);
            this.managersTable.TabIndex = 11;
            this.managersTable.SelectionChanged += new System.EventHandler(this.managersTable_SelectionChanged);
            this.managersTable.Click += new System.EventHandler(this.managersTable_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAddUser);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cmbUserRole);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbUserPasswordAgain);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.tbUserPassword);
            this.panel2.Controls.Add(this.tbUserLogin);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(963, 62);
            this.panel2.TabIndex = 10;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(835, 26);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(124, 23);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "Добавить";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Добавление нового пользователя";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(639, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Права:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Логин:";
            // 
            // cmbUserRole
            // 
            this.cmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserRole.FormattingEnabled = true;
            this.cmbUserRole.Location = new System.Drawing.Point(687, 28);
            this.cmbUserRole.Name = "cmbUserRole";
            this.cmbUserRole.Size = new System.Drawing.Size(121, 21);
            this.cmbUserRole.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(192, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Пароль:";
            // 
            // tbUserPasswordAgain
            // 
            this.tbUserPasswordAgain.Location = new System.Drawing.Point(490, 27);
            this.tbUserPasswordAgain.Name = "tbUserPasswordAgain";
            this.tbUserPasswordAgain.Size = new System.Drawing.Size(143, 20);
            this.tbUserPasswordAgain.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(385, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Повторить пароль:";
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Location = new System.Drawing.Point(242, 26);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.Size = new System.Drawing.Size(136, 20);
            this.tbUserPassword.TabIndex = 5;
            // 
            // tbUserLogin
            // 
            this.tbUserLogin.Location = new System.Drawing.Point(50, 26);
            this.tbUserLogin.Name = "tbUserLogin";
            this.tbUserLogin.Size = new System.Drawing.Size(136, 20);
            this.tbUserLogin.TabIndex = 4;
            // 
            // tbNewDocumentType
            // 
            this.tbNewDocumentType.DataType = LeoBase.Components.CustomControls.LeoTextBoxDataType.TEXT;
            this.tbNewDocumentType.Location = new System.Drawing.Point(156, 21);
            this.tbNewDocumentType.Name = "tbNewDocumentType";
            this.tbNewDocumentType.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbNewDocumentType.Size = new System.Drawing.Size(197, 27);
            this.tbNewDocumentType.TabIndex = 6;
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.usersGroupBox);
            this.Controls.Add(this.groupDocumentTypes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(969, 733);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupDocumentTypes.ResumeLayout(false);
            this.groupDocumentTypes.PerformLayout();
            this.usersGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.managersTable)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveCurrentManager;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupDocumentTypes;
        private System.Windows.Forms.Button btnAddDocumentType;
        private LeoTextBox tbNewDocumentType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRemoveDocumentType;
        private System.Windows.Forms.Button btnUpdateDocumentType;
        private System.Windows.Forms.ComboBox cmbDocumentTypes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbManagerPassword2;
        private System.Windows.Forms.TextBox tbManagerPassword1;
        private System.Windows.Forms.TextBox tbManagerLogin;
        private System.Windows.Forms.GroupBox usersGroupBox;
        private System.Windows.Forms.DataGridView managersTable;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbUserRole;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbUserPasswordAgain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.TextBox tbUserLogin;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnDeleteUser;
    }
}
