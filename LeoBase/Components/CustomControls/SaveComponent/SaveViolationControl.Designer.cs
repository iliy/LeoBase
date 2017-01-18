namespace LeoBase.Components.CustomControls.SaveComponent
{
    partial class SaveViolationControl
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.PageTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbN = new System.Windows.Forms.TextBox();
            this.tbE = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbProtocolTypes = new System.Windows.Forms.ComboBox();
            this.configAppBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.documentsPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.configAppBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата нарушения: ";
            // 
            // PageTitle
            // 
            this.PageTitle.AutoSize = true;
            this.PageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PageTitle.Location = new System.Drawing.Point(3, 3);
            this.PageTitle.Name = "PageTitle";
            this.PageTitle.Size = new System.Drawing.Size(293, 29);
            this.PageTitle.TabIndex = 8;
            this.PageTitle.Text = "Добавление нарушения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Подробное описание: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Северная широта(N): ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Восточная долгота(E): ";
            // 
            // dtDate
            // 
            this.dtDate.Location = new System.Drawing.Point(126, 50);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(200, 20);
            this.dtDate.TabIndex = 12;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(126, 73);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(200, 60);
            this.tbDescription.TabIndex = 13;
            // 
            // tbN
            // 
            this.tbN.Location = new System.Drawing.Point(126, 147);
            this.tbN.Name = "tbN";
            this.tbN.Size = new System.Drawing.Size(200, 20);
            this.tbN.TabIndex = 14;
            // 
            // tbE
            // 
            this.tbE.Location = new System.Drawing.Point(126, 179);
            this.tbE.Name = "tbE";
            this.tbE.Size = new System.Drawing.Size(200, 20);
            this.tbE.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(539, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Тип документа:";
            // 
            // cbProtocolTypes
            // 
            this.cbProtocolTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtocolTypes.FormattingEnabled = true;
            this.cbProtocolTypes.Location = new System.Drawing.Point(103, 217);
            this.cbProtocolTypes.Name = "cbProtocolTypes";
            this.cbProtocolTypes.Size = new System.Drawing.Size(430, 21);
            this.cbProtocolTypes.TabIndex = 17;
            // 
            // configAppBindingSource
            // 
            this.configAppBindingSource.DataSource = typeof(AppPresentators.ConfigApp);
            // 
            // documentsPanel
            // 
            this.documentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.documentsPanel.Location = new System.Drawing.Point(4, 252);
            this.documentsPanel.Name = "documentsPanel";
            this.documentsPanel.Size = new System.Drawing.Size(648, 377);
            this.documentsPanel.TabIndex = 20;
            // 
            // SaveViolationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.documentsPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbProtocolTypes);
            this.Controls.Add(this.tbE);
            this.Controls.Add(this.tbN);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PageTitle);
            this.Controls.Add(this.label1);
            this.Name = "SaveViolationControl";
            this.Size = new System.Drawing.Size(655, 632);
            ((System.ComponentModel.ISupportInitialize)(this.configAppBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PageTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbN;
        private System.Windows.Forms.TextBox tbE;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbProtocolTypes;
        private System.Windows.Forms.BindingSource configAppBindingSource;
        private System.Windows.Forms.FlowLayoutPanel documentsPanel;
    }
}
