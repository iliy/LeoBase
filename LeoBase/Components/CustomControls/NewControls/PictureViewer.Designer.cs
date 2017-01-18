namespace LeoBase.Components.CustomControls.NewControls
{
    partial class PictureViewer
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
            this.btnDelete = new System.Windows.Forms.PictureBox();
            this.btnZoom = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chbSelectedItem = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImage = global::LeoBase.Properties.Resources.deleteLeave;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Location = new System.Drawing.Point(146, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(31, 29);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.TabStop = false;
            this.toolTip1.SetToolTip(this.btnDelete, "Удалить");
            this.btnDelete.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoom.BackColor = System.Drawing.Color.Transparent;
            this.btnZoom.BackgroundImage = global::LeoBase.Properties.Resources.zoomLeave;
            this.btnZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnZoom.Location = new System.Drawing.Point(3, 33);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(169, 161);
            this.btnZoom.TabIndex = 1;
            this.btnZoom.TabStop = false;
            this.toolTip1.SetToolTip(this.btnZoom, "Увеличить");
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // chbSelectedItem
            // 
            this.chbSelectedItem.AutoSize = true;
            this.chbSelectedItem.Location = new System.Drawing.Point(3, 3);
            this.chbSelectedItem.Name = "chbSelectedItem";
            this.chbSelectedItem.Size = new System.Drawing.Size(15, 14);
            this.chbSelectedItem.TabIndex = 2;
            this.chbSelectedItem.UseVisualStyleBackColor = true;
            this.chbSelectedItem.Visible = false;
            // 
            // PictureViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.chbSelectedItem);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnDelete);
            this.Name = "PictureViewer";
            this.Size = new System.Drawing.Size(175, 213);
            this.Load += new System.EventHandler(this.PictureViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnDelete;
        private System.Windows.Forms.PictureBox btnZoom;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbSelectedItem;
    }
}
