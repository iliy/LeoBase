namespace FormControls.Controls
{
    partial class LeoDataGridView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.headersPanel = new System.Windows.Forms.Panel();
            this.paginationsPanel = new System.Windows.Forms.Panel();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.topPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.headersPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.paginationsPanel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataPanel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(886, 513);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Margin = new System.Windows.Forms.Padding(0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(886, 30);
            this.topPanel.TabIndex = 0;
            // 
            // headersPanel
            // 
            this.headersPanel.BackColor = System.Drawing.Color.White;
            this.headersPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headersPanel.Location = new System.Drawing.Point(0, 30);
            this.headersPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headersPanel.Name = "headersPanel";
            this.headersPanel.Size = new System.Drawing.Size(886, 30);
            this.headersPanel.TabIndex = 1;
            // 
            // paginationsPanel
            // 
            this.paginationsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paginationsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.paginationsPanel.Location = new System.Drawing.Point(0, 478);
            this.paginationsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.paginationsPanel.Name = "paginationsPanel";
            this.paginationsPanel.Size = new System.Drawing.Size(886, 35);
            this.paginationsPanel.TabIndex = 2;
            // 
            // dataPanel
            // 
            this.dataPanel.AutoScroll = true;
            this.dataPanel.BackColor = System.Drawing.Color.White;
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPanel.Location = new System.Drawing.Point(0, 60);
            this.dataPanel.Margin = new System.Windows.Forms.Padding(0);
            this.dataPanel.MinimumSize = new System.Drawing.Size(0, 150);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(886, 418);
            this.dataPanel.TabIndex = 3;
            // 
            // LeoDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LeoDataGridView";
            this.Size = new System.Drawing.Size(886, 513);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel headersPanel;
        private System.Windows.Forms.Panel paginationsPanel;
        private System.Windows.Forms.Panel dataPanel;
    }
}
