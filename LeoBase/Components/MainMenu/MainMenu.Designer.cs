namespace LeoBase.Components.MainMenu
{
    partial class MainMenu
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
            this.menuList = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // menuList
            // 
            this.menuList.BackColor = System.Drawing.Color.Transparent;
            this.menuList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.menuList.Location = new System.Drawing.Point(2, 2);
            this.menuList.Margin = new System.Windows.Forms.Padding(2);
            this.menuList.Name = "menuList";
            this.menuList.Size = new System.Drawing.Size(198, 100);
            this.menuList.TabIndex = 0;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(83)))), ((int)(((byte)(53)))));
            this.Controls.Add(this.menuList);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MainMenu";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.Size = new System.Drawing.Size(203, 148);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel menuList;
    }
}
