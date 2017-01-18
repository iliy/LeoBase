namespace LeoBase.Components.CustomControls.SearchPanels
{
    partial class SearchPanelContainer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearSearchPanel = new System.Windows.Forms.Button();
            this.btnSearchGO = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(239)))), ((int)(((byte)(236)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(342, 490);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMargin = new System.Drawing.Size(20, 0);
            this.panel1.Controls.Add(this.btnClearSearchPanel);
            this.panel1.Controls.Add(this.btnSearchGO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(342, 460);
            this.panel1.TabIndex = 0;
            // 
            // btnClearSearchPanel
            // 
            this.btnClearSearchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSearchPanel.Location = new System.Drawing.Point(183, 3);
            this.btnClearSearchPanel.Name = "btnClearSearchPanel";
            this.btnClearSearchPanel.Size = new System.Drawing.Size(156, 24);
            this.btnClearSearchPanel.TabIndex = 1;
            this.btnClearSearchPanel.Text = "Очистить";
            this.btnClearSearchPanel.UseVisualStyleBackColor = true;
            this.btnClearSearchPanel.Click += new System.EventHandler(this.btnClearSearchPanel_Click);
            // 
            // btnSearchGO
            // 
            this.btnSearchGO.Location = new System.Drawing.Point(3, 3);
            this.btnSearchGO.Name = "btnSearchGO";
            this.btnSearchGO.Size = new System.Drawing.Size(156, 24);
            this.btnSearchGO.TabIndex = 0;
            this.btnSearchGO.Text = "Поиск";
            this.btnSearchGO.UseVisualStyleBackColor = true;
            this.btnSearchGO.Visible = false;
            this.btnSearchGO.Click += new System.EventHandler(this.btnSearchGO_Click);
            // 
            // SearchPanelContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SearchPanelContainer";
            this.Size = new System.Drawing.Size(342, 490);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearchGO;
        private System.Windows.Forms.Button btnClearSearchPanel;
    }
}
