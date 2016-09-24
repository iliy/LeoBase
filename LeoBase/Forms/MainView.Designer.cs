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
            this.topPanel = new MetroFramework.Controls.MetroPanel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.menuPanel = new MetroFramework.Controls.MetroPanel();
            this.centerPanel = new MetroFramework.Controls.MetroPanel();
            this.accauntInfoPanel = new MetroFramework.Controls.MetroPanel();
            this.btnLogout = new MetroFramework.Controls.MetroButton();
            this.lbAccauntInfo = new MetroFramework.Controls.MetroLabel();
            this.topPanel.SuspendLayout();
            this.accauntInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.topPanel.Controls.Add(this.accauntInfoPanel);
            this.topPanel.Controls.Add(this.metroButton2);
            this.topPanel.Controls.Add(this.metroButton1);
            this.topPanel.HorizontalScrollbarBarColor = true;
            this.topPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.topPanel.HorizontalScrollbarSize = 10;
            this.topPanel.Location = new System.Drawing.Point(9, 57);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(827, 35);
            this.topPanel.Style = MetroFramework.MetroColorStyle.Orange;
            this.topPanel.TabIndex = 0;
            this.topPanel.VerticalScrollbarBarColor = true;
            this.topPanel.VerticalScrollbarHighlightOnWheel = false;
            this.topPanel.VerticalScrollbarSize = 10;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(6, 6);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(37, 23);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "Back";
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(45, 6);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(41, 23);
            this.metroButton2.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroButton2.TabIndex = 3;
            this.metroButton2.Text = "Next";
            // 
            // menuPanel
            // 
            this.menuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.menuPanel.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.menuPanel.HorizontalScrollbarBarColor = true;
            this.menuPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.menuPanel.HorizontalScrollbarSize = 10;
            this.menuPanel.Location = new System.Drawing.Point(9, 98);
            this.menuPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(135, 519);
            this.menuPanel.TabIndex = 1;
            this.menuPanel.VerticalScrollbarBarColor = true;
            this.menuPanel.VerticalScrollbarHighlightOnWheel = false;
            this.menuPanel.VerticalScrollbarSize = 10;
            // 
            // centerPanel
            // 
            this.centerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.centerPanel.AutoScroll = true;
            this.centerPanel.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.centerPanel.HorizontalScrollbar = true;
            this.centerPanel.HorizontalScrollbarBarColor = true;
            this.centerPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.centerPanel.HorizontalScrollbarSize = 10;
            this.centerPanel.Location = new System.Drawing.Point(150, 98);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(686, 519);
            this.centerPanel.TabIndex = 2;
            this.centerPanel.VerticalScrollbar = true;
            this.centerPanel.VerticalScrollbarBarColor = true;
            this.centerPanel.VerticalScrollbarHighlightOnWheel = false;
            this.centerPanel.VerticalScrollbarSize = 10;
            // 
            // accauntInfoPanel
            // 
            this.accauntInfoPanel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.accauntInfoPanel.Controls.Add(this.btnLogout);
            this.accauntInfoPanel.Controls.Add(this.lbAccauntInfo);
            this.accauntInfoPanel.HorizontalScrollbarBarColor = true;
            this.accauntInfoPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.accauntInfoPanel.HorizontalScrollbarSize = 10;
            this.accauntInfoPanel.Location = new System.Drawing.Point(98, 4);
            this.accauntInfoPanel.Name = "accauntInfoPanel";
            this.accauntInfoPanel.Size = new System.Drawing.Size(273, 29);
            this.accauntInfoPanel.TabIndex = 4;
            this.accauntInfoPanel.VerticalScrollbarBarColor = true;
            this.accauntInfoPanel.VerticalScrollbarHighlightOnWheel = false;
            this.accauntInfoPanel.VerticalScrollbarSize = 10;
            this.accauntInfoPanel.Visible = false;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(3, 3);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(51, 23);
            this.btnLogout.Style = MetroFramework.MetroColorStyle.Orange;
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Выход";
            // 
            // lbAccauntInfo
            // 
            this.lbAccauntInfo.AutoSize = true;
            this.lbAccauntInfo.Location = new System.Drawing.Point(60, 5);
            this.lbAccauntInfo.Name = "lbAccauntInfo";
            this.lbAccauntInfo.Size = new System.Drawing.Size(81, 19);
            this.lbAccauntInfo.TabIndex = 6;
            this.lbAccauntInfo.Text = "metroLabel1";
            this.lbAccauntInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(843, 640);
            this.Controls.Add(this.centerPanel);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.topPanel);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MinimumSize = new System.Drawing.Size(720, 520);
            this.Name = "MainView";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainView_Load);
            this.topPanel.ResumeLayout(false);
            this.accauntInfoPanel.ResumeLayout(false);
            this.accauntInfoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel topPanel;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroPanel menuPanel;
        private MetroFramework.Controls.MetroPanel centerPanel;
        private MetroFramework.Controls.MetroPanel accauntInfoPanel;
        private MetroFramework.Controls.MetroButton btnLogout;
        private MetroFramework.Controls.MetroLabel lbAccauntInfo;
    }
}