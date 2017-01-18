namespace LeoBase.Components.CustomControls.NewControls
{
    partial class SavePageTemplate
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
            this.mainTableContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.centerPlace = new System.Windows.Forms.Panel();
            this.mainTableContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableContainer
            // 
            this.mainTableContainer.AutoScroll = true;
            this.mainTableContainer.ColumnCount = 1;
            this.mainTableContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableContainer.Controls.Add(this.lbTitle, 0, 0);
            this.mainTableContainer.Controls.Add(this.centerPlace, 0, 1);
            this.mainTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableContainer.Location = new System.Drawing.Point(0, 0);
            this.mainTableContainer.Name = "mainTableContainer";
            this.mainTableContainer.RowCount = 2;
            this.mainTableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableContainer.Size = new System.Drawing.Size(1118, 609);
            this.mainTableContainer.TabIndex = 1;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbTitle.Location = new System.Drawing.Point(10, 0);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(1105, 30);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Заголовок";
            // 
            // centerPlace
            // 
            this.centerPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.centerPlace.AutoScroll = true;
            this.centerPlace.Location = new System.Drawing.Point(3, 33);
            this.centerPlace.Name = "centerPlace";
            this.centerPlace.Size = new System.Drawing.Size(1112, 573);
            this.centerPlace.TabIndex = 1;
            // 
            // SavePageTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTableContainer);
            this.Name = "SavePageTemplate";
            this.Size = new System.Drawing.Size(1118, 609);
            this.mainTableContainer.ResumeLayout(false);
            this.mainTableContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableContainer;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Panel centerPlace;
    }
}
