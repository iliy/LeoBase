namespace LeoBase.Components.CustomControls.SaveComponent.SaveDocument
{
    partial class DocumentItem
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
            this.button1 = new System.Windows.Forms.Button();
            this.cmbDocumentType = new System.Windows.Forms.ComboBox();
            this.dtpWhenIssued = new System.Windows.Forms.DateTimePicker();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.tbWhoIssued = new System.Windows.Forms.TextBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.tbSerial = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(541, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Убрать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocumentType.FormattingEnabled = true;
            this.cmbDocumentType.Location = new System.Drawing.Point(99, 7);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.Size = new System.Drawing.Size(189, 21);
            this.cmbDocumentType.TabIndex = 24;
            // 
            // dtpWhenIssued
            // 
            this.dtpWhenIssued.Location = new System.Drawing.Point(417, 32);
            this.dtpWhenIssued.Name = "dtpWhenIssued";
            this.dtpWhenIssued.Size = new System.Drawing.Size(200, 20);
            this.dtpWhenIssued.TabIndex = 23;
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(417, 55);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(200, 20);
            this.tbCode.TabIndex = 22;
            // 
            // tbWhoIssued
            // 
            this.tbWhoIssued.Location = new System.Drawing.Point(417, 7);
            this.tbWhoIssued.Name = "tbWhoIssued";
            this.tbWhoIssued.Size = new System.Drawing.Size(200, 20);
            this.tbWhoIssued.TabIndex = 21;
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(99, 55);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(189, 20);
            this.tbNumber.TabIndex = 20;
            // 
            // tbSerial
            // 
            this.tbSerial.Location = new System.Drawing.Point(99, 33);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.Size = new System.Drawing.Size(189, 20);
            this.tbSerial.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Код подразделения:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Когда выдан:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Кем выдан:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Номер:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Серия:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Тип документа:";
            // 
            // DocumentItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbDocumentType);
            this.Controls.Add(this.dtpWhenIssued);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.tbWhoIssued);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.tbSerial);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DocumentItem";
            this.Size = new System.Drawing.Size(628, 120);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbDocumentType;
        private System.Windows.Forms.DateTimePicker dtpWhenIssued;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.TextBox tbWhoIssued;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.TextBox tbSerial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
