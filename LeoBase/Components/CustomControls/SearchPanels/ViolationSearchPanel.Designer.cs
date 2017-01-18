namespace LeoBase.Components.CustomControls.SearchPanels
{
    partial class ViolationSearchPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClearAll = new MetroFramework.Controls.MetroTile();
            this.btnSearch = new MetroFramework.Controls.MetroTile();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpViolation = new System.Windows.Forms.DateTimePicker();
            this.cmbCompareViolation = new System.Windows.Forms.ComboBox();
            this.cmbCompareWasCreated = new System.Windows.Forms.ComboBox();
            this.dtpWasCreated = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdCompareWasUpdate = new System.Windows.Forms.ComboBox();
            this.dtpWasUpdate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.chbCompareDescription = new MetroFramework.Controls.MetroCheckBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnClearAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(1, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 538);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Расширенный поиск";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(3, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(436, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Свернуть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearAll.Location = new System.Drawing.Point(149, 505);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(123, 23);
            this.btnClearAll.Style = MetroFramework.MetroColorStyle.Yellow;
            this.btnClearAll.TabIndex = 8;
            this.btnClearAll.Text = "Очистить все";
            this.btnClearAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(6, 505);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(123, 23);
            this.btnSearch.Style = MetroFramework.MetroColorStyle.Green;
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 39);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(433, 460);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chbCompareDescription);
            this.panel1.Controls.Add(this.tbDescription);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmdCompareWasUpdate);
            this.panel1.Controls.Add(this.dtpWasUpdate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbCompareWasCreated);
            this.panel1.Controls.Add(this.dtpWasCreated);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbCompareViolation);
            this.panel1.Controls.Add(this.dtpViolation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 270);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата нарушения:";
            // 
            // dtpViolation
            // 
            this.dtpViolation.Location = new System.Drawing.Point(133, 13);
            this.dtpViolation.Name = "dtpViolation";
            this.dtpViolation.Size = new System.Drawing.Size(146, 20);
            this.dtpViolation.TabIndex = 1;
            this.dtpViolation.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // cmbCompareViolation
            // 
            this.cmbCompareViolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompareViolation.FormattingEnabled = true;
            this.cmbCompareViolation.Items.AddRange(new object[] {
            "Равно",
            "Больше",
            "Меньше"});
            this.cmbCompareViolation.Location = new System.Drawing.Point(282, 13);
            this.cmbCompareViolation.Name = "cmbCompareViolation";
            this.cmbCompareViolation.Size = new System.Drawing.Size(85, 21);
            this.cmbCompareViolation.TabIndex = 2;
            // 
            // cmbCompareWasCreated
            // 
            this.cmbCompareWasCreated.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompareWasCreated.FormattingEnabled = true;
            this.cmbCompareWasCreated.Items.AddRange(new object[] {
            "Равно",
            "Больше",
            "Меньше"});
            this.cmbCompareWasCreated.Location = new System.Drawing.Point(282, 49);
            this.cmbCompareWasCreated.Name = "cmbCompareWasCreated";
            this.cmbCompareWasCreated.Size = new System.Drawing.Size(85, 21);
            this.cmbCompareWasCreated.TabIndex = 5;
            // 
            // dtpWasCreated
            // 
            this.dtpWasCreated.Location = new System.Drawing.Point(133, 49);
            this.dtpWasCreated.Name = "dtpWasCreated";
            this.dtpWasCreated.Size = new System.Drawing.Size(146, 20);
            this.dtpWasCreated.TabIndex = 4;
            this.dtpWasCreated.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Занесено в базу:";
            // 
            // cmdCompareWasUpdate
            // 
            this.cmdCompareWasUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdCompareWasUpdate.FormattingEnabled = true;
            this.cmdCompareWasUpdate.Items.AddRange(new object[] {
            "Равно",
            "Больше",
            "Меньше"});
            this.cmdCompareWasUpdate.Location = new System.Drawing.Point(282, 84);
            this.cmdCompareWasUpdate.Name = "cmdCompareWasUpdate";
            this.cmdCompareWasUpdate.Size = new System.Drawing.Size(85, 21);
            this.cmdCompareWasUpdate.TabIndex = 8;
            // 
            // dtpWasUpdate
            // 
            this.dtpWasUpdate.Location = new System.Drawing.Point(133, 84);
            this.dtpWasUpdate.Name = "dtpWasUpdate";
            this.dtpWasUpdate.Size = new System.Drawing.Size(146, 20);
            this.dtpWasUpdate.TabIndex = 7;
            this.dtpWasUpdate.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Последнее обновление:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Краткое описание:";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(133, 119);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(146, 20);
            this.tbDescription.TabIndex = 10;
            // 
            // chbCompareDescription
            // 
            this.chbCompareDescription.AutoSize = true;
            this.chbCompareDescription.Checked = true;
            this.chbCompareDescription.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbCompareDescription.Location = new System.Drawing.Point(282, 122);
            this.chbCompareDescription.Name = "chbCompareDescription";
            this.chbCompareDescription.Size = new System.Drawing.Size(141, 15);
            this.chbCompareDescription.Style = MetroFramework.MetroColorStyle.Orange;
            this.chbCompareDescription.TabIndex = 11;
            this.chbCompareDescription.Text = "Полное соответствие";
            this.chbCompareDescription.UseVisualStyleBackColor = true;
            // 
            // ViolationSearcgPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ViolationSearcgPanel";
            this.Size = new System.Drawing.Size(445, 546);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroTile btnClearAll;
        private MetroFramework.Controls.MetroTile btnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbCompareWasCreated;
        private System.Windows.Forms.DateTimePicker dtpWasCreated;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCompareViolation;
        private System.Windows.Forms.DateTimePicker dtpViolation;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroCheckBox chbCompareDescription;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmdCompareWasUpdate;
        private System.Windows.Forms.DateTimePicker dtpWasUpdate;
        private System.Windows.Forms.Label label3;
    }
}
