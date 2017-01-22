using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls
{
    public class OpenSearchPanelButton:Panel
    {
        public event Action OnClickButton;

        private Color _mouseHover = Color.FromArgb(255, 178, 178, 177);
        private Color _defaultColor = Color.FromArgb(255, 89, 78, 60);
        private Color _mouseDown = Color.FromArgb(255, 222, 221, 219);

        private Label _btnText;
        private PictureBox _lupa;

        public OpenSearchPanelButton()
        {
            _btnText = new Label();

            _lupa = new PictureBox();

            _lupa.Width = 18;

            _lupa.Height = 18;

            _lupa.Image = Properties.Resources.lupa_small;

            _btnText.ForeColor = _defaultColor;

            _btnText.Text = "Открыть расширенный поиск";

            _btnText.Font = new System.Drawing.Font("Arial", 11, FontStyle.Regular);

            this.Cursor = Cursors.Hand;

            _btnText.AutoSize = true;
            
            _btnText.MouseDown += OnMouseDown;

            this.MouseDown += OnMouseDown;

            _btnText.MouseUp += OnMouseUp;

            this.MouseUp += OnMouseUp;

            _btnText.MouseLeave += OnMouseLeave;

            this.MouseLeave += OnMouseLeave;

            _btnText.MouseMove += OnMouseMove;

            this.MouseMove += OnMouseMove;

            this.Controls.Add(_lupa);

            _btnText.Left = 20;

            this.Controls.Add(_btnText);

            this.Width = _btnText.Width + 22;

            this.Height = _btnText.Height + 6;

            this.Click += OnClick;
            _btnText.Click += OnClick;
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (OnClickButton != null) OnClickButton();
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
