namespace LeoBase.Components.CustomControls.SearchPanels
{
    partial class DocumentSearchPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbWhenIssuedBy = new System.Windows.Forms.ComboBox();
            this.cmbDocumentType = new System.Windows.Forms.ComboBox();
            this.btnClearDocument = new MetroFramework.Controls.MetroTile();
            this.tbCodeDevision = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel19 = new MetroFramework.Controls.MetroLabel();
            this.dtpWhenIssued = new System.Windows.Forms.DateTimePicker();
            this.metroLabel18 = new MetroFramework.Controls.MetroLabel();
            this.chbIssuedBy = new MetroFramework.Controls.MetroCheckBox();
            this.tbIssuedBy = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel17 = new MetroFramework.Controls.MetroLabel();
            this.tbDocumentNumber = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel16 = new MetroFramework.Controls.MetroLabel();
            this.tbDocumentSerial = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel15 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel14 = new MetroFramework.Controls.MetroLabel();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbWhenIssuedBy);
            this.groupBox4.Controls.Add(this.cmbDocumentType);
            this.groupBox4.Controls.Add(this.btnClearDocument);
            this.groupBox4.Controls.Add(this.tbCodeDevision);
            this.groupBox4.Controls.Add(this.metroLabel19);
            this.groupBox4.Controls.Add(this.dtpWhenIssued);
            this.groupBox4.Controls.Add(this.metroLabel18);
            this.groupBox4.Controls.Add(this.chbIssuedBy);
            this.groupBox4.Controls.Add(this.tbIssuedBy);
            this.groupBox4.Controls.Add(this.metroLabel17);
            this.groupBox4.Controls.Add(this.tbDocumentNumber);
            this.groupBox4.Controls.Add(this.metroLabel16);
            this.groupBox4.Controls.Add(this.tbDocumentSerial);
            this.groupBox4.Controls.Add(this.metroLabel15);
            this.groupBox4.Controls.Add(this.metroLabel14);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(384, 245);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Документ";
            // 
            // cmbWhenIssuedBy
            // 
            this.cmbWhenIssuedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWhenIssuedBy.FormattingEnabled = true;
            this.cmbWhenIssuedBy.Items.AddRange(new object[] {
            "Равно",
            "Больше",
            "Меньше"});
            this.cmbWhenIssuedBy.Location = new System.Drawing.Point(239, 138);
            this.cmbWhenIssuedBy.Name = "cmbWhenIssuedBy";
            this.cmbWhenIssuedBy.Size = new System.Drawing.Size(133, 21);
            this.cmbWhenIssuedBy.TabIndex = 43;
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocumentType.FormattingEnabled = true;
            this.cmbDocumentType.Items.AddRange(new object[] {
            "",
            "Паспорт",
            "Водителькие права"});
            this.cmbDocumentType.Location = new System.Drawing.Point(112, 19);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.Size = new System.Drawing.Size(121, 21);
            this.cmbDocumentType.TabIndex = 42;
            // 
            // btnClearDocument
            // 
            this.btnClearDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearDocument.Location = new System.Drawing.Point(257, 216);
            this.btnClearDocument.Name = "btnClearDocument";
            this.btnClearDocument.Size = new System.Drawing.Size(123, 23);
            this.btnClearDocument.Style = MetroFramework.MetroColorStyle.Yellow;
            this.btnClearDocument.TabIndex = 41;
            this.btnClearDocument.Text = "Очистить";
            this.btnClearDocument.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClearDocument.Click += new System.EventHandler(this.btnClearDocument_Click);
            // 
            // tbCodeDevision
            // 
            this.tbCodeDevision.Location = new System.Drawing.Point(115, 166);
            this.tbCodeDevision.Name = "tbCodeDevision";
            this.tbCodeDevision.Size = new System.Drawing.Size(118, 23);
            this.tbCodeDevision.TabIndex = 40;
            // 
            // metroLabel19
            // 
            this.metroLabel19.AutoSize = true;
            this.metroLabel19.Location = new System.Drawing.Point(72, 166);
            this.metroLabel19.Name = "metroLabel19";
            this.metroLabel19.Size = new System.Drawing.Size(34, 19);
            this.metroLabel19.TabIndex = 39;
            this.metroLabel19.Text = "Код:";
            // 
            // dtpWhenIssued
            // 
            this.dtpWhenIssued.Location = new System.Drawing.Point(115, 138);
            this.dtpWhenIssued.Name = "dtpWhenIssued";
            this.dtpWhenIssued.Size = new System.Drawing.Size(118, 20);
            this.dtpWhenIssued.TabIndex = 37;
            this.dtpWhenIssued.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // metroLabel18
            // 
            this.metroLabel18.AutoSize = true;
            this.metroLabel18.Location = new System.Drawing.Point(22, 138);
            this.metroLabel18.Name = "metroLabel18";
            this.metroLabel18.Size = new System.Drawing.Size(88, 19);
            this.metroLabel18.TabIndex = 36;
            this.metroLabel18.Text = "Когда выдан:";
            // 
            // chbIssuedBy
            // 
            this.chbIssuedBy.AutoSize = true;
            this.chbIssuedBy.Checked = true;
            this.chbIssuedBy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbIssuedBy.Location = new System.Drawing.Point(239, 112);
            this.chbIssuedBy.Name = "chbIssuedBy";
            this.chbIssuedBy.Size = new System.Drawing.Size(136, 15);
            this.chbIssuedBy.Style = MetroFramework.MetroColorStyle.Yellow;
            this.chbIssuedBy.TabIndex = 35;
            this.chbIssuedBy.Text = "Полное соответсвие";
            this.chbIssuedBy.UseVisualStyleBackColor = true;
            // 
            // tbIssuedBy
            // 
            this.tbIssuedBy.Location = new System.Drawing.Point(115, 108);
            this.tbIssuedBy.Name = "tbIssuedBy";
            this.tbIssuedBy.Size = new System.Drawing.Size(118, 23);
            this.tbIssuedBy.TabIndex = 34;
            // 
            // metroLabel17
            // 
            this.metroLabel17.AutoSize = true;
            this.metroLabel17.Location = new System.Drawing.Point(29, 112);
            this.metroLabel17.Name = "metroLabel17";
            this.metroLabel17.Size = new System.Drawing.Size(77, 19);
            this.metroLabel17.TabIndex = 33;
            this.metroLabel17.Text = "Кем выдан:";
            // 
            // tbDocumentNumber
            // 
            this.tbDocumentNumber.Location = new System.Drawing.Point(115, 79);
            this.tbDocumentNumber.Name = "tbDocumentNumber";
            this.tbDocumentNumber.Size = new System.Drawing.Size(118, 23);
            this.tbDocumentNumber.TabIndex = 32;
            // 
            // metroLabel16
            // 
            this.metroLabel16.AutoSize = true;
            this.metroLabel16.Location = new System.Drawing.Point(53, 81);
            this.metroLabel16.Name = "metroLabel16";
            this.metroLabel16.Size = new System.Drawing.Size(53, 19);
            this.metroLabel16.TabIndex = 31;
            this.metroLabel16.Text = "Номер:";
            // 
            // tbDocumentSerial
            // 
            this.tbDocumentSerial.Location = new System.Drawing.Point(115, 50);
            this.tbDocumentSerial.Name = "tbDocumentSerial";
            this.tbDocumentSerial.Size = new System.Drawing.Size(118, 23);
            this.tbDocumentSerial.TabIndex = 30;
            // 
            // metroLabel15
            // 
            this.metroLabel15.AutoSize = true;
            this.metroLabel15.Location = new System.Drawing.Point(55, 52);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(51, 19);
            this.metroLabel15.TabIndex = 29;
            this.metroLabel15.Text = "Серия:";
            // 
            // metroLabel14
            // 
            this.metroLabel14.AutoSize = true;
            this.metroLabel14.Location = new System.Drawing.Point(4, 20);
            this.metroLabel14.Name = "metroLabel14";
            this.metroLabel14.Size = new System.Drawing.Size(102, 19);
            this.metroLabel14.TabIndex = 27;
            this.metroLabel14.Text = "Тип документа:";
            // 
            // DocumentSearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Name = "DocumentSearchPanel";
            this.Size = new System.Drawing.Size(384, 245);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbWhenIssuedBy;
        private System.Windows.Forms.ComboBox cmbDocumentType;
        private MetroFramework.Controls.MetroTile btnClearDocument;
        private MetroFramework.Controls.MetroTextBox tbCodeDevision;
        private MetroFramework.Controls.MetroLabel metroLabel19;
        private System.Windows.Forms.DateTimePicker dtpWhenIssued;
        private MetroFramework.Controls.MetroLabel metroLabel18;
        private MetroFramework.Controls.MetroCheckBox chbIssuedBy;
        private MetroFramework.Controls.MetroTextBox tbIssuedBy;
        private MetroFramework.Controls.MetroLabel metroLabel17;
        private MetroFramework.Controls.MetroTextBox tbDocumentNumber;
        private MetroFramework.Controls.MetroLabel metroLabel16;
        private MetroFramework.Controls.MetroTextBox tbDocumentSerial;
        private MetroFramework.Controls.MetroLabel metroLabel15;
        private MetroFramework.Controls.MetroLabel metroLabel14;
    }
}
