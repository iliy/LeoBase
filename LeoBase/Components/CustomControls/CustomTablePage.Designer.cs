namespace LeoBase.Components.CustomControls
{
    partial class CustomTablePage
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
            this.mainTableContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.loadPanel = new System.Windows.Forms.Panel();
            this.SearchPanelContainer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.mainTableContainer.SuspendLayout();
            this.loadPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mainTableContainer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SearchPanelContainer, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(883, 512);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // mainTableContainer
            // 
            this.mainTableContainer.ColumnCount = 1;
            this.mainTableContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableContainer.Controls.Add(this.lbTitle, 0, 0);
            this.mainTableContainer.Controls.Add(this.loadPanel, 0, 2);
            this.mainTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableContainer.Location = new System.Drawing.Point(453, 3);
            this.mainTableContainer.Name = "mainTableContainer";
            this.mainTableContainer.RowCount = 3;
            this.mainTableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableContainer.Size = new System.Drawing.Size(427, 506);
            this.mainTableContainer.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbTitle.Location = new System.Drawing.Point(10, 0);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(414, 30);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Заголовок";
            // 
            // loadPanel
            // 
            this.loadPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.loadPanel.Controls.Add(this.pictureBox1);
            this.loadPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadPanel.Location = new System.Drawing.Point(0, 50);
            this.loadPanel.Margin = new System.Windows.Forms.Padding(0);
            this.loadPanel.Name = "loadPanel";
            this.loadPanel.Size = new System.Drawing.Size(427, 459);
            this.loadPanel.TabIndex = 1;
            this.loadPanel.Visible = false;
            // 
            // SearchPanelContainer
            // 
            this.SearchPanelContainer.AutoScroll = true;
            this.SearchPanelContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchPanelContainer.Location = new System.Drawing.Point(0, 0);
            this.SearchPanelContainer.Margin = new System.Windows.Forms.Padding(0);
            this.SearchPanelContainer.Name = "SearchPanelContainer";
            this.SearchPanelContainer.Size = new System.Drawing.Size(450, 512);
            this.SearchPanelContainer.TabIndex = 1;
            this.SearchPanelContainer.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::LeoBase.Properties.Resources.smallLoader;
            this.pictureBox1.Location = new System.Drawing.Point(207, 113);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 35);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // CustomTablePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CustomTablePage";
            this.Size = new System.Drawing.Size(883, 512);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.mainTableContainer.ResumeLayout(false);
            this.mainTableContainer.PerformLayout();
            this.loadPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel mainTableContainer;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Panel SearchPanelContainer;
        private System.Windows.Forms.Panel loadPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
