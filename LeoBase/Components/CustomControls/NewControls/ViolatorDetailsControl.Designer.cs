namespace LeoBase.Components.CustomControls.NewControls
{
    partial class ViolatorDetailsControl
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
            this.avatarPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbPlaceWork = new System.Windows.Forms.TextBox();
            this.lbPlaceBerth = new System.Windows.Forms.TextBox();
            this.lbDateBerth = new System.Windows.Forms.TextBox();
            this.lbFIO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableAddresses = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableDocuments = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableViolations = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnViolationDetails = new System.Windows.Forms.Button();
            this.lbPhones = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableAddresses)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableDocuments)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableViolations)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1017, 46);
            this.panel1.TabIndex = 1;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbTitle.Location = new System.Drawing.Point(4, 6);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(305, 26);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "Подробности понарушителю";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.avatarPanel);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1017, 233);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Общая информация";
            // 
            // avatarPanel
            // 
            this.avatarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.avatarPanel.Location = new System.Drawing.Point(653, 16);
            this.avatarPanel.Name = "avatarPanel";
            this.avatarPanel.Size = new System.Drawing.Size(361, 214);
            this.avatarPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.lbPhones);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lbPlaceWork);
            this.panel2.Controls.Add(this.lbPlaceBerth);
            this.panel2.Controls.Add(this.lbDateBerth);
            this.panel2.Controls.Add(this.lbFIO);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(650, 214);
            this.panel2.TabIndex = 0;
            // 
            // lbPlaceWork
            // 
            this.lbPlaceWork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPlaceWork.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbPlaceWork.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPlaceWork.Location = new System.Drawing.Point(114, 103);
            this.lbPlaceWork.Multiline = true;
            this.lbPlaceWork.Name = "lbPlaceWork";
            this.lbPlaceWork.ReadOnly = true;
            this.lbPlaceWork.Size = new System.Drawing.Size(530, 46);
            this.lbPlaceWork.TabIndex = 64;
            this.lbPlaceWork.TabStop = false;
            // 
            // lbPlaceBerth
            // 
            this.lbPlaceBerth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPlaceBerth.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbPlaceBerth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPlaceBerth.Location = new System.Drawing.Point(114, 52);
            this.lbPlaceBerth.Multiline = true;
            this.lbPlaceBerth.Name = "lbPlaceBerth";
            this.lbPlaceBerth.ReadOnly = true;
            this.lbPlaceBerth.Size = new System.Drawing.Size(530, 45);
            this.lbPlaceBerth.TabIndex = 63;
            this.lbPlaceBerth.TabStop = false;
            // 
            // lbDateBerth
            // 
            this.lbDateBerth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDateBerth.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbDateBerth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbDateBerth.Location = new System.Drawing.Point(114, 33);
            this.lbDateBerth.Name = "lbDateBerth";
            this.lbDateBerth.ReadOnly = true;
            this.lbDateBerth.Size = new System.Drawing.Size(530, 13);
            this.lbDateBerth.TabIndex = 62;
            this.lbDateBerth.TabStop = false;
            // 
            // lbFIO
            // 
            this.lbFIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFIO.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbFIO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbFIO.Location = new System.Drawing.Point(114, 12);
            this.lbFIO.Name = "lbFIO";
            this.lbFIO.ReadOnly = true;
            this.lbFIO.Size = new System.Drawing.Size(530, 13);
            this.lbFIO.TabIndex = 61;
            this.lbFIO.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Место работы: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Место рождения: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата рождения: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableAddresses);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1017, 160);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Адреса";
            // 
            // tableAddresses
            // 
            this.tableAddresses.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableAddresses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableAddresses.Location = new System.Drawing.Point(3, 16);
            this.tableAddresses.MultiSelect = false;
            this.tableAddresses.Name = "tableAddresses";
            this.tableAddresses.ReadOnly = true;
            this.tableAddresses.Size = new System.Drawing.Size(1011, 141);
            this.tableAddresses.TabIndex = 0;
            this.tableAddresses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableAddresses_CellContentClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableDocuments);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 439);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1017, 154);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Документы";
            // 
            // tableDocuments
            // 
            this.tableDocuments.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableDocuments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDocuments.Location = new System.Drawing.Point(3, 16);
            this.tableDocuments.MultiSelect = false;
            this.tableDocuments.Name = "tableDocuments";
            this.tableDocuments.ReadOnly = true;
            this.tableDocuments.Size = new System.Drawing.Size(1011, 135);
            this.tableDocuments.TabIndex = 0;
            this.tableDocuments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableDocuments_CellContentClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableViolations);
            this.groupBox4.Controls.Add(this.panel4);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 593);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1017, 195);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Правонарушения";
            // 
            // tableViolations
            // 
            this.tableViolations.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableViolations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableViolations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableViolations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableViolations.Location = new System.Drawing.Point(3, 50);
            this.tableViolations.MultiSelect = false;
            this.tableViolations.Name = "tableViolations";
            this.tableViolations.ReadOnly = true;
            this.tableViolations.Size = new System.Drawing.Size(1011, 142);
            this.tableViolations.TabIndex = 1;
            this.tableViolations.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableViolations_CellContentClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnViolationDetails);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 16);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.panel4.Size = new System.Drawing.Size(1011, 34);
            this.panel4.TabIndex = 0;
            // 
            // btnViolationDetails
            // 
            this.btnViolationDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnViolationDetails.Location = new System.Drawing.Point(0, 0);
            this.btnViolationDetails.Name = "btnViolationDetails";
            this.btnViolationDetails.Size = new System.Drawing.Size(158, 29);
            this.btnViolationDetails.TabIndex = 0;
            this.btnViolationDetails.Text = "Подробнее";
            this.btnViolationDetails.UseVisualStyleBackColor = true;
            this.btnViolationDetails.Click += new System.EventHandler(this.btnViolationDetails_Click);
            // 
            // lbPhones
            // 
            this.lbPhones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPhones.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbPhones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPhones.Location = new System.Drawing.Point(113, 156);
            this.lbPhones.Multiline = true;
            this.lbPhones.Name = "lbPhones";
            this.lbPhones.ReadOnly = true;
            this.lbPhones.Size = new System.Drawing.Size(530, 55);
            this.lbPhones.TabIndex = 66;
            this.lbPhones.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 65;
            this.label5.Text = "Телефоны: ";
            // 
            // ViolatorDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ViolatorDetailsControl";
            this.Size = new System.Drawing.Size(1017, 896);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableAddresses)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableDocuments)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableViolations)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel avatarPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lbPlaceWork;
        private System.Windows.Forms.TextBox lbPlaceBerth;
        private System.Windows.Forms.TextBox lbDateBerth;
        private System.Windows.Forms.TextBox lbFIO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView tableAddresses;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView tableDocuments;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView tableViolations;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnViolationDetails;
        private System.Windows.Forms.TextBox lbPhones;
        private System.Windows.Forms.Label label5;
    }
}
