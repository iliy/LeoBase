using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls
{
    public class HideSearchPanelBtn:Label
    {
        private Color _mouseHover = Color.LightBlue;
        private Color _defaultColor = Color.Blue;
        private Color _mouseDown = Color.DarkBlue;

        public HideSearchPanelBtn()
        {
            this.Text = "<- Закрыть расширенный поиск";
            this.Font = new System.Drawing.Font(this.Font.FontFamily, 10);

            this.Cursor = Cursors.Hand;

            this.Dock = DockStyle.Fill;

            this.MouseDown += (s, e) =>
            {
                this.ForeColor = _mouseDown;
            };
            this.MouseUp += (s, e) =>
            {
                this.ForeColor = _defaultColor;
            };
            this.MouseLeave += (s, e) =>
            {
                this.ForeColor = _defaultColor;
            };
            this.MouseMove += (s, e) =>
            {
                this.ForeColor = _mouseHover;
            };
        }
    }
}
