namespace LeoBase.Forms
{
    partial class OrderDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.penelOrderType = new System.Windows.Forms.Panel();
            this.cmbOrderType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.formatPanel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lbPath = new System.Windows.Forms.Label();
            this.btnSelectPathForOrder = new System.Windows.Forms.Button();
            this.cmbOrderFormat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelTableConfig = new System.Windows.Forms.Panel();
            this.chbSearchResult = new System.Windows.Forms.CheckBox();
            this.chbCurrentPage = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSingleWithImages = new System.Windows.Forms.Panel();
            this.chbOutputImages = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnsPunel = new System.Windows.Forms.Panel();
            this.btnOrderCancel = new System.Windows.Forms.Button();
            this.btnOrderComplete = new System.Windows.Forms.Button();
            this.panelLoad = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panelComplete = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.orderSaveFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.label11 = new System.Windows.Forms.Label();
            this.tbOrderName = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.penelOrderType.SuspendLayout();
            this.formatPanel.SuspendLayout();
            this.panelTableConfig.SuspendLayout();
            this.panelSingleWithImages.SuspendLayout();
            this.btnsPunel.SuspendLayout();
            this.panelLoad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelComplete.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.penelOrderType);
            this.flowLayoutPanel1.Controls.Add(this.formatPanel);
            this.flowLayoutPanel1.Controls.Add(this.panelTableConfig);
            this.flowLayoutPanel1.Controls.Add(this.panelSingleWithImages);
            this.flowLayoutPanel1.Controls.Add(this.btnsPunel);
            this.flowLayoutPanel1.Controls.Add(this.panelLoad);
            this.flowLayoutPanel1.Controls.Add(this.panelComplete);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(518, 542);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // penelOrderType
            // 
            this.penelOrderType.Controls.Add(this.cmbOrderType);
            this.penelOrderType.Controls.Add(this.label1);
            this.penelOrderType.Dock = System.Windows.Forms.DockStyle.Top;
            this.penelOrderType.Location = new System.Drawing.Point(3, 3);
            this.penelOrderType.Name = "penelOrderType";
            this.penelOrderType.Size = new System.Drawing.Size(512, 43);
            this.penelOrderType.TabIndex = 0;
            // 
            // cmbOrderType
            // 
            this.cmbOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbOrderType.FormattingEnabled = true;
            this.cmbOrderType.Items.AddRange(new object[] {
            "Сохранить в файл",
            "Печать"});
            this.cmbOrderType.Location = new System.Drawing.Point(225, 8);
            this.cmbOrderType.Name = "cmbOrderType";
            this.cmbOrderType.Size = new System.Drawing.Size(216, 24);
            this.cmbOrderType.TabIndex = 1;
            this.cmbOrderType.SelectedIndexChanged += new System.EventHandler(this.cmbOrderType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите способ отчетности:";
            // 
            // formatPanel
            // 
            this.formatPanel.Controls.Add(this.tbOrderName);
            this.formatPanel.Controls.Add(this.label11);
            this.formatPanel.Controls.Add(this.label7);
            this.formatPanel.Controls.Add(this.lbPath);
            this.formatPanel.Controls.Add(this.btnSelectPathForOrder);
            this.formatPanel.Controls.Add(this.cmbOrderFormat);
            this.formatPanel.Controls.Add(this.label5);
            this.formatPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.formatPanel.Location = new System.Drawing.Point(3, 52);
            this.formatPanel.Name = "formatPanel";
            this.formatPanel.Size = new System.Drawing.Size(512, 87);
            this.formatPanel.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(143, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Укажите путь:";
            // 
            // lbPath
            // 
            this.lbPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPath.Location = new System.Drawing.Point(227, 29);
            this.lbPath.Name = "lbPath";
            this.lbPath.Padding = new System.Windows.Forms.Padding(3);
            this.lbPath.Size = new System.Drawing.Size(133, 21);
            this.lbPath.TabIndex = 3;
            // 
            // btnSelectPathForOrder
            // 
            this.btnSelectPathForOrder.Location = new System.Drawing.Point(366, 28);
            this.btnSelectPathForOrder.Name = "btnSelectPathForOrder";
            this.btnSelectPathForOrder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectPathForOrder.TabIndex = 2;
            this.btnSelectPathForOrder.Text = "Выбрать";
            this.btnSelectPathForOrder.UseVisualStyleBackColor = true;
            this.btnSelectPathForOrder.Click += new System.EventHandler(this.btnSelectPathForOrder_Click);
            // 
            // cmbOrderFormat
            // 
            this.cmbOrderFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderFormat.FormattingEnabled = true;
            this.cmbOrderFormat.Items.AddRange(new object[] {
            "PDF",
            "XLSX   (MS Excel)",
            "DOCX (MS Word)"});
            this.cmbOrderFormat.Location = new System.Drawing.Point(225, 1);
            this.cmbOrderFormat.Name = "cmbOrderFormat";
            this.cmbOrderFormat.Size = new System.Drawing.Size(216, 21);
            this.cmbOrderFormat.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Формат отчета:";
            // 
            // panelTableConfig
            // 
            this.panelTableConfig.Controls.Add(this.chbSearchResult);
            this.panelTableConfig.Controls.Add(this.chbCurrentPage);
            this.panelTableConfig.Controls.Add(this.label4);
            this.panelTableConfig.Controls.Add(this.label3);
            this.panelTableConfig.Controls.Add(this.label2);
            this.panelTableConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTableConfig.Location = new System.Drawing.Point(3, 145);
            this.panelTableConfig.Name = "panelTableConfig";
            this.panelTableConfig.Size = new System.Drawing.Size(512, 99);
            this.panelTableConfig.TabIndex = 2;
            this.panelTableConfig.Visible = false;
            // 
            // chbSearchResult
            // 
            this.chbSearchResult.AutoSize = true;
            this.chbSearchResult.Checked = true;
            this.chbSearchResult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSearchResult.Location = new System.Drawing.Point(190, 68);
            this.chbSearchResult.Name = "chbSearchResult";
            this.chbSearchResult.Size = new System.Drawing.Size(15, 14);
            this.chbSearchResult.TabIndex = 4;
            this.chbSearchResult.UseVisualStyleBackColor = true;
            // 
            // chbCurrentPage
            // 
            this.chbCurrentPage.AutoSize = true;
            this.chbCurrentPage.Location = new System.Drawing.Point(190, 38);
            this.chbCurrentPage.Name = "chbCurrentPage";
            this.chbCurrentPage.Size = new System.Drawing.Size(15, 14);
            this.chbCurrentPage.TabIndex = 3;
            this.chbCurrentPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Вывод только текущей страницы:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Учитывать результаты поиска:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(4, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Настройки отчета";
            // 
            // panelSingleWithImages
            // 
            this.panelSingleWithImages.Controls.Add(this.chbOutputImages);
            this.panelSingleWithImages.Controls.Add(this.label6);
            this.panelSingleWithImages.Controls.Add(this.label8);
            this.panelSingleWithImages.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSingleWithImages.Location = new System.Drawing.Point(3, 250);
            this.panelSingleWithImages.Name = "panelSingleWithImages";
            this.panelSingleWithImages.Size = new System.Drawing.Size(512, 61);
            this.panelSingleWithImages.TabIndex = 3;
            this.panelSingleWithImages.Visible = false;
            // 
            // chbOutputImages
            // 
            this.chbOutputImages.AutoSize = true;
            this.chbOutputImages.Checked = true;
            this.chbOutputImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOutputImages.Location = new System.Drawing.Point(190, 38);
            this.chbOutputImages.Name = "chbOutputImages";
            this.chbOutputImages.Size = new System.Drawing.Size(15, 14);
            this.chbOutputImages.TabIndex = 3;
            this.chbOutputImages.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Выводить изображения:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(4, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "Настройки отчета";
            // 
            // btnsPunel
            // 
            this.btnsPunel.Controls.Add(this.btnOrderCancel);
            this.btnsPunel.Controls.Add(this.btnOrderComplete);
            this.btnsPunel.Location = new System.Drawing.Point(3, 317);
            this.btnsPunel.Name = "btnsPunel";
            this.btnsPunel.Size = new System.Drawing.Size(512, 36);
            this.btnsPunel.TabIndex = 4;
            // 
            // btnOrderCancel
            // 
            this.btnOrderCancel.Location = new System.Drawing.Point(404, 7);
            this.btnOrderCancel.Name = "btnOrderCancel";
            this.btnOrderCancel.Size = new System.Drawing.Size(96, 23);
            this.btnOrderCancel.TabIndex = 1;
            this.btnOrderCancel.Text = "Отмена";
            this.btnOrderCancel.UseVisualStyleBackColor = true;
            this.btnOrderCancel.Click += new System.EventHandler(this.btnOrderCancel_Click);
            // 
            // btnOrderComplete
            // 
            this.btnOrderComplete.Location = new System.Drawing.Point(255, 7);
            this.btnOrderComplete.Name = "btnOrderComplete";
            this.btnOrderComplete.Size = new System.Drawing.Size(143, 23);
            this.btnOrderComplete.TabIndex = 0;
            this.btnOrderComplete.Text = "Сформировать отчет";
            this.btnOrderComplete.UseVisualStyleBackColor = true;
            this.btnOrderComplete.Click += new System.EventHandler(this.btnOrderComplete_Click);
            // 
            // panelLoad
            // 
            this.panelLoad.Controls.Add(this.pictureBox1);
            this.panelLoad.Controls.Add(this.label9);
            this.panelLoad.Location = new System.Drawing.Point(3, 359);
            this.panelLoad.Name = "panelLoad";
            this.panelLoad.Size = new System.Drawing.Size(512, 82);
            this.panelLoad.TabIndex = 5;
            this.panelLoad.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LeoBase.Properties.Resources.smallLoader;
            this.pictureBox1.Location = new System.Drawing.Point(253, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 43);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(187, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Формирование отчета";
            // 
            // panelComplete
            // 
            this.panelComplete.Controls.Add(this.button2);
            this.panelComplete.Controls.Add(this.button1);
            this.panelComplete.Controls.Add(this.label10);
            this.panelComplete.Location = new System.Drawing.Point(3, 447);
            this.panelComplete.Name = "panelComplete";
            this.panelComplete.Size = new System.Drawing.Size(512, 80);
            this.panelComplete.TabIndex = 6;
            this.panelComplete.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(283, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Закрыть";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Открыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(197, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "Отчет сформирован";
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(127, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Название отчета:";
            // 
            // tbOrderName
            // 
            this.tbOrderName.Location = new System.Drawing.Point(225, 60);
            this.tbOrderName.Name = "tbOrderName";
            this.tbOrderName.Size = new System.Drawing.Size(216, 20);
            this.tbOrderName.TabIndex = 6;
            // 
            // OrderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(518, 542);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OrderDialog";
            this.Text = "Сформировать отчет";
            this.Load += new System.EventHandler(this.OrderDialog_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.penelOrderType.ResumeLayout(false);
            this.penelOrderType.PerformLayout();
            this.formatPanel.ResumeLayout(false);
            this.formatPanel.PerformLayout();
            this.panelTableConfig.ResumeLayout(false);
            this.panelTableConfig.PerformLayout();
            this.panelSingleWithImages.ResumeLayout(false);
            this.panelSingleWithImages.PerformLayout();
            this.btnsPunel.ResumeLayout(false);
            this.panelLoad.ResumeLayout(false);
            this.panelLoad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelComplete.ResumeLayout(false);
            this.panelComplete.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel penelOrderType;
        private System.Windows.Forms.ComboBox cmbOrderType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel formatPanel;
        private System.Windows.Forms.ComboBox cmbOrderFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelTableConfig;
        private System.Windows.Forms.CheckBox chbSearchResult;
        private System.Windows.Forms.CheckBox chbCurrentPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelSingleWithImages;
        private System.Windows.Forms.CheckBox chbOutputImages;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Button btnSelectPathForOrder;
        private System.Windows.Forms.Panel btnsPunel;
        private System.Windows.Forms.Button btnOrderCancel;
        private System.Windows.Forms.Button btnOrderComplete;
        private System.Windows.Forms.FolderBrowserDialog orderSaveFolderDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.Panel panelLoad;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panelComplete;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbOrderName;
        private System.Windows.Forms.Label label11;
    }
}