using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls
{
    public class HideSearchPanelBtn:Panel
    {
        public event Action OnClick;

        private Color _mouseHover = Color.FromArgb(255, 95, 85, 61);
        private Color _defaultColor = Color.FromArgb(255, 82, 66, 40);
        private Color _mouseDown = Color.FromArgb(255, 215, 212, 205);

        private Label _btnText;

        public HideSearchPanelBtn()
        {
            _btnText = new Label();

            _btnText.Text = "Закрыть расширенный поиск";
            _btnText.Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold);

            _btnText.Padding = new Padding(3, 5, 3, 5);

            this.Cursor = Cursors.Hand;

            //_btnText.Dock = DockStyle.Fill;

            _btnText.AutoSize = true;

            this.Dock = DockStyle.Fill;

            this.BackgroundImage = Properties.Resources.arrow_title_search;
            this.BackgroundImageLayout = ImageLayout.Center;

            this.Margin = new Padding(0);

            this.Padding = new Padding(0);


            this.MouseDown += OnMouseDown;

            _btnText.MouseDown += OnMouseDown;


            this.MouseUp += OnMouseUp;

            _btnText.MouseUp += OnMouseUp;


            this.MouseLeave += OnMouseLeave;

            _btnText.MouseLeave += OnMouseLeave;


            this.MouseMove += OnMouseMove;

            _btnText.MouseMove += OnMouseMove;


            this.Controls.Add(_btnText);

            this.Click += (s, e) =>
            {
                if (OnClick != null) OnClick();
            };

            _btnText.Click += (s, e) =>
            {
                if (OnClick != null) OnClick();
            };
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            _btnText.ForeColor = _mouseHover;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            _btnText.ForeColor = _defaultColor;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _btnText.ForeColor = _defaultColor;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _btnText.ForeColor = _mouseDown;
        }
    }
}
