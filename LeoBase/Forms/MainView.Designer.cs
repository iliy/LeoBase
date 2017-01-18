namespace LeoBase.Forms
{
    partial class MainView
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
            this.components = new System.ComponentModel.Container();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.fastSearchAnimate = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelFastSearch = new System.Windows.Forms.Panel();
            this.tbFastSearch = new System.Windows.Forms.TextBox();
            this.contentButtons = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.managerPanel = new System.Windows.Forms.Panel();
            this.lbAccauntInfo = new System.Windows.Forms.Label();
            this.btnsInfo = new System.Windows.Forms.ToolTip(this.components);
            this.menuPanel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.btnNext = new System.Windows.Forms.PictureBox();
            this.btnLogout = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelFastSearch.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.managerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogout)).BeginInit();
            this.SuspendLayout();
            // 
            // centerPanel
            // 
            this.centerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.centerPanel.AutoScroll = true;
            this.centerPanel.Location = new System.Drawing.Point(185, 46);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(657, 596);
            this.centerPanel.TabIndex = 2;
            // 
            // fastSearchAnimate
            // 
            this.fastSearchAnimate.Interval = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(218)))), ((int)(((byte)(118)))));
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.flowLayoutPanel1);
            this.panel3.Controls.Add(this.managerPanel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(843, 43);
            this.panel3.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panelFastSearch, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.contentButtons, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(61, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(254, 43);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panelFastSearch
            // 
            this.panelFastSearch.BackColor = System.Drawing.Color.Transparent;
            this.panelFastSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelFastSearch.Controls.Add(this.tbFastSearch);
            this.panelFastSearch.Location = new System.Drawing.Point(12, 0);
            this.panelFastSearch.Margin = new System.Windows.Forms.Padding(0);
            this.panelFastSearch.Name = "panelFastSearch";
            this.panelFastSearch.Size = new System.Drawing.Size(242, 41);
            this.panelFastSearch.TabIndex = 3;
            this.panelFastSearch.Visible = false;
            // 
            // tbFastSearch
            // 
            this.tbFastSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFastSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFastSearch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbFastSearch.Location = new System.Drawing.Point(6, 7);
            this.tbFastSearch.Name = "tbFastSearch";
            this.tbFastSearch.Size = new System.Drawing.Size(230, 26);
            this.tbFastSearch.TabIndex = 0;
            // 
            // contentButtons
            // 
            this.contentButtons.AutoSize = true;
            this.contentButtons.ColumnCount = 2;
            this.contentButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contentButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contentButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.contentButtons.Location = new System.Drawing.Point(3, 3);
            this.contentButtons.Name = "contentButtons";
            this.contentButtons.Padding = new System.Windows.Forms.Padding(3);
            this.contentButtons.RowCount = 1;
            this.contentButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contentButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.contentButtons.Size = new System.Drawing.Size(6, 37);
            this.contentButtons.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnBack);
            this.flowLayoutPanel1.Controls.Add(this.btnNext);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(61, 43);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // managerPanel
            // 
            this.managerPanel.AutoSize = true;
            this.managerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(218)))), ((int)(((byte)(118)))));
            this.managerPanel.Controls.Add(this.btnLogout);
            this.managerPanel.Controls.Add(this.lbAccauntInfo);
            this.managerPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.managerPanel.Location = new System.Drawing.Point(651, 0);
            this.managerPanel.Name = "managerPanel";
            this.managerPanel.Size = new System.Drawing.Size(192, 43);
            this.managerPanel.TabIndex = 4;
            this.managerPanel.Visible = false;
            // 
            // lbAccauntInfo
            // 
            this.lbAccauntInfo.AutoSize = true;
            this.lbAccauntInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbAccauntInfo.Font = new System.Drawing.Font("Arial", 10F);
            this.lbAccauntInfo.Location = new System.Drawing.Point(42, 13);
            this.lbAccauntInfo.Name = "lbAccauntInfo";
            this.lbAccauntInfo.Size = new System.Drawing.Size(46, 16);
            this.lbAccauntInfo.TabIndex = 1;
            this.lbAccauntInfo.Text = "label3";
            this.lbAccauntInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuPanel
            // 
            this.menuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(83)))), ((int)(((byte)(53)))));
            this.menuPanel.Location = new System.Drawing.Point(0, 43);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(184, 599);
            this.menuPanel.TabIndex = 8;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Window;
            this.btnBack.Image = global::LeoBase.Properties.Resources.strLeft;
            this.btnBack.Location = new System.Drawing.Point(6, 6);
            this.btnBack.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(21, 29);
            this.btnBack.TabIndex = 2;
            this.btnBack.TabStop = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Window;
            this.btnNext.Image = global::LeoBase.Properties.Resources.strRight;
            this.btnNext.Location = new System.Drawing.Point(33, 6);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 6, 6, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(21, 29);
            this.btnNext.TabIndex = 3;
            this.btnNext.TabStop = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Image = global::LeoBase.Properties.Resources.LogOutBtn;
            this.btnLogout.Location = new System.Drawing.Point(157, 6);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(32, 31);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.TabStop = false;
            this.btnsInfo.SetToolTip(this.btnLogout, "Вход под другим пользователем");
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(843, 640);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.centerPanel);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MinimumSize = new System.Drawing.Size(720, 520);
            this.Name = "MainView";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainView_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelFastSearch.ResumeLayout(false);
            this.panelFastSearch.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.managerPanel.ResumeLayout(false);
            this.managerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel centerPanel;
        private System.Windows.Forms.Timer fastSearchAnimate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel managerPanel;
        private System.Windows.Forms.PictureBox btnLogout;
        private System.Windows.Forms.Label lbAccauntInfo;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.PictureBox btnNext;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelFastSearch;
        private System.Windows.Forms.TextBox tbFastSearch;
        private System.Windows.Forms.TableLayoutPanel contentButtons;
        private System.Windows.Forms.ToolTip btnsInfo;
        private System.Windows.Forms.Panel menuPanel;
    }
}