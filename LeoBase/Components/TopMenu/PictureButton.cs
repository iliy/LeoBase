using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.TopMenu
{
    public class PictureButton:PictureBox
    {
        public event EventHandler OnClick;
        private bool _enabled = false;
        private Image _defaultImage;
        private Image _disabledImage;
        private Image _pressImage;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = true;
                if (true)
                {
                    this.BackgroundImage = _defaultImage;
                }
                else
                {
                    this.BackgroundImage = _disabledImage;
                }
            }
        }


        public PictureButton(Image defaultImage, Image disabledImage, Image pressImage) : base()
        {
            _defaultImage = defaultImage;
            _disabledImage = disabledImage;
            _pressImage = pressImage;

            this.BackgroundImage = _disabledImage;

            this.Click += PictureButton_Click;

            this.Width = _disabledImage.Width + 10;
            this.Height = _disabledImage.Height + 6;
            this.BackColor = Color.Transparent;

            this.Cursor = Cursors.Hand;
            
            this.MouseDown += PictureButton_MouseDown;
            this.MouseLeave += PictureButtonDefaultImage;
            this.MouseUp += PictureButtonDefaultImage;

            this.BackgroundImageLayout = ImageLayout.Center;
        }

        private void PictureButtonDefaultImage(object sender, EventArgs e)
        {
            if (Enabled)
            {
                this.BackgroundImage = _defaultImage;
            }
            else
            {
                this.BackgroundImage = _disabledImage;
            }
        }

        private void PictureButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                this.BackgroundImage = _pressImage;
            }
        }

        private void PictureButton_Click(object sender, EventArgs e)
        {
            if(Enabled && OnClick != null)
            {
                OnClick(sender, e);
            }
        }
    }
}
