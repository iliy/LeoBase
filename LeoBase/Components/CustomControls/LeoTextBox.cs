using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace LeoBase.Components.CustomControls
{
    public class LeoTextBox:Panel
    {
        private TextBox _textBox;

        public event EventHandler TextChanged;
        public event KeyPressEventHandler KeyPress;

        public override string Text {
            get
            {
                return _textBox.Text;
            }
            set
            {
                _textBox.Text = value;
            }
        }

        private LeoTextBoxDataType _dataType = LeoTextBoxDataType.TEXT;
        public LeoTextBoxDataType DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                _dataType = value;
            }
        }

        public LeoTextBox()
        {
            _textBox = new TextBox();

            _textBox.Dock = DockStyle.Fill;

            _textBox.BackColor = Color.FromArgb(248, 247, 245);

            _textBox.BorderStyle = BorderStyle.None;
            
            this.Padding = new Padding(7, 6, 7, 6);

            this.Controls.Add(_textBox);

           
            _textBox.TextChanged += _textBox_TextChanged;
            _textBox.KeyPress += _textBox_KeyPress;
        }

        private void _textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (KeyPress != null) KeyPress(sender, e);
        }

        private void _textBox_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null) TextChanged(sender, e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Image imageBg = ResizeImage(Properties.Resources.tbBackground, this.Width - 14 , 26);
            Image imageLeft = Properties.Resources.tbLeft;
            Image imageRight = Properties.Resources.tbRight;

            e.Graphics.DrawImage(imageBg, new Point(7, 0));

            e.Graphics.DrawImage(imageLeft, new Point(0, 0));

            e.Graphics.DrawImage(imageRight, new Point(this.Width - 7, 0));
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
    public enum LeoTextBoxDataType
    {
        REAL,
        NUMBER,
        TEXT
    }
}
