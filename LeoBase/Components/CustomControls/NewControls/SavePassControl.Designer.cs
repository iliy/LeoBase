namespace LeoBase.Components.CustomControls.NewControls
{
    partial class SavePassControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmpDocumentType = new System.Windows.Forms.ComboBox();
            this.dtpWhenIssued = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.tbWhoIssued = new LeoBase.Components.CustomControls.LeoTextBox();
            this.tbNumber = new LeoBase.Components.CustomControls.LeoTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbSerial = new LeoBase.Components.CustomControls.LeoTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpPassClosed = new System.Windows.Forms.DateTimePicker();
            this.dtpPassGiven = new System.Windows.Forms.DateTimePicker();
            this.tbMiddleName = new LeoBase.Components.CustomControls.LeoTextBox();
            this.tbSecondName = new LeoBase.Components.CustomControls.LeoTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFirstName = new LeoBase.Components.CustomControls.LeoTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1129, 49);
            this.panel1.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbTitle.Location = new System.Drawing.Point(3, 10);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(0, 26);
            this.lbTitle.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.cmpDocumentType);
            this.panel2.Controls.Add(this.dtpWhenIssued);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.tbWhoIssued);
            this.panel2.Controls.Add(this.tbNumber);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.tbSerial);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.dtpPassClosed);
            this.panel2.Controls.Add(this.dtpPassGiven);
            this.panel2.Controls.Add(this.tbMiddleName);
            this.panel2.Controls.Add(this.tbSecondName);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbFirstName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1129, 832);
            this.panel2.TabIndex = 1;
            // 
            // cmpDocumentType
            // 
            this.cmpDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmpDocumentType.FormattingEnabled = true;
            this.cmpDocumentType.Location = new System.Drawing.Point(177, 220);
            this.cmpDocumentType.Name = "cmpDocumentType";
            this.cmpDocumentType.Size = new System.Drawing.Size(293, 21);
            this.cmpDocumentType.TabIndex = 19;
            // 
            // dtpWhenIssued
            // 
            this.dtpWhenIssued.Location = new System.Drawing.Point(177, 350);
            this.dtpWhenIssued.Name = "dtpWhenIssued";
            this.dtpWhenIssued.Size = new System.Drawing.Size(293, 20);
            this.dtpWhenIssued.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(97, 352);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Когда выдан:";
            // 
            // tbWhoIssued
            // 
            this.tbWhoIssued.BackColor = System.Drawing.Color.White;
            this.tbWhoIssued.DataType = LeoBase.Components.CustomControls.LeoTextBoxDataType.TEXT;
            this.tbWhoIssued.Location = new System.Drawing.Point(177, 313);
            this.tbWhoIssued.Name = "tbWhoIssued";
            this.tbWhoIssued.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbWhoIssued.Size = new System.Drawing.Size(293, 27);
            this.tbWhoIssued.Style = LeoBase.Components.CustomControls.LeoTextBoxStyle.White;
            this.tbWhoIssued.TabIndex = 16;
            // 
            // tbNumber
            // 
            this.tbNumber.BackColor = System.Drawing.Color.White;
            this.tbNumber.DataType = LeoBase.Components.CustomControls.LeoTextBoxDataType.TEXT;
            this.tbNumber.Location = new System.Drawing.Point(177, 280);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbNumber.Size = new System.Drawing.Size(293, 27);
            this.tbNumber.Style = LeoBase.Components.CustomControls.LeoTextBoxStyle.White;
            this.tbNumber.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(107, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Кем выдан:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(127, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Номер:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(127, 253);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Серия:";
            // 
            // tbSerial
            // 
            this.tbSerial.BackColor = System.Drawing.Color.White;
            this.tbSerial.DataType = LeoBase.Components.CustomControls.LeoTextBoxDataType.TEXT;
            this.tbSerial.Location = new System.Drawing.Point(177, 247);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbSerial.Size = new System.Drawing.Size(293, 27);
            this.tbSerial.Style = LeoBase.Components.CustomControls.LeoTextBoxStyle.White;
            this.tbSerial.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Предоставленный документ:";
            // 
            // dtpPassClosed
            // 
            this.dtpPassClosed.Location = new System.Drawing.Point(177, 162);
            this.dtpPassClosed.Name = "dtpPassClosed";
            this.dtpPassClosed.Size = new System.Drawing.Size(293, 20);
            this.dtpPassClosed.TabIndex = 9;
            // 
            // dtpPassGiven
            // 
            this.dtpPassGiven.Location = new System.Drawing.Point(177, 130);
            this.dtpPassGiven.Name = "dtpPassGiven";
            this.dtpPassGiven.Size = new System.Drawing.Size(293, 20);
            this.dtpPassGiven.TabIndex = 8;
            // 
            // tbMiddleName
            // 
            this.tbMiddleName.BackColor = System.Drawing.Color.White;
            this.tbMiddleName.DataType = LeoBase.Components.CustomControls.LeoTextBoxDataType.TEXT;
            this.tbMiddleName.Location = new System.Drawing.Point(177, 88);
            this.tbMiddleName.Name = "tbMiddleName";
            this.tbMiddleName.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbMiddleName.Size = new System.Drawing.Size(293, 27);
            this.tbMiddleName.Style = LeoBase.Components.CustomControls.LeoTextBoxStyle.White;
            this.tbMiddleName.TabIndex = 7;
            // 
            // tbSecondName
            // 
            this.tbSecondName.BackColor = System.Drawing.Color.White;
            this.tbSecondName.DataType = LeoBase.Components.CustomControls.LeoTextBoxDataType.TEXT;
            this.tbSecondName.Location = new System.Drawing.Point(177, 55);
            this.tbSecondName.Name = "tbSecondName";
            this.tbSecondName.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbSecondName.Size = new System.Drawing.Size(293, 27);
            this.tbSecondName.Style = LeoBase.Components.CustomControls.LeoTextBoxStyle.White;
            this.tbSecondName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Пропуск выдан до:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Пропуск выдан:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Отчество:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия:";
            // 
            // tbFirstName
            // 
            this.tbFirstName.BackColor = System.Drawing.Color.White;
            this.tbFirstName.DataType = LeoBase.Components.CustomControls.LeoTextBoxDataType.TEXT;
            this.tbFirstName.Location = new System.Drawing.Point(177, 22);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tbFirstName.Size = new System.Drawing.Size(293, 27);
            this.tbFirstName.Style = LeoBase.Components.CustomControls.LeoTextBoxStyle.White;
            this.tbFirstName.TabIndex = 0;
            // 
            // SavePassControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SavePassControl";
            this.Size = new System.Drawing.Size(1129, 881);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.DateTimePicker dtpWhenIssued;
        private System.Windows.Forms.Label label10;
        private LeoTextBox tbWhoIssued;
        private LeoTextBox tbNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private LeoTextBox tbSerial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpPassClosed;
        private System.Windows.Forms.DateTimePicker dtpPassGiven;
        private LeoTextBox tbMiddleName;
        private LeoTextBox tbSecondName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private LeoTextBox tbFirstName;
        private System.Windows.Forms.ComboBox cmpDocumentType;
    }
}
