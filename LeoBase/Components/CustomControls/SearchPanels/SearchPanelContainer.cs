using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeoBase.Components.CustomControls.Interfaces;

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class SearchPanelContainer : UserControl, ISearchPanel
    {
        private HideSearchPanelBtn hideBtn;
        public bool SearchButtonVisible {
            get
            {
                return hideBtn.Visible;
            }
            set
            {
                hideBtn.Visible = value;
            }
        }
        public SearchPanelContainer()
        {
            InitializeComponent();

            hideBtn = new HideSearchPanelBtn();

            this.Dock = DockStyle.Fill;

            hideBtn.OnClick += () =>
            {
                if (OnHideClick != null) OnHideClick();
            };

            tableLayoutPanel1.Controls.Add(hideBtn, 0, 0);

            hideBtn.Margin = new Padding(2, 2, 2, 2);

            tableLayoutPanel1.HorizontalScroll.Enabled = false;
            tableLayoutPanel1.HorizontalScroll.Visible = false;
        }

        public Control Control
        {
            get
            {
                return this;
            }
        }

        public event Action OnHideClick;
        public event Action OnSearchClick;
        protected object SearchData { get; set; }
        private IClearable _control;
        public void SetCustomSearchPanel(IClearable control)
        {
            control.Dock = DockStyle.Fill;

            _control = control;

            tableLayoutPanel1.Controls.Add(control.GetControl(), 0, 1);

            control.GetControl().Margin = new Padding(2, 2, 2, 2);

            var bmp = new Bitmap(tableLayoutPanel1.Width, tableLayoutPanel1.Height);

            Graphics g = Graphics.FromImage(bmp);

            Color borderColor = Color.FromArgb(0xcecbc6);
            Color shadowBorderColor = Color.FromArgb(0xe6e5e1);

            Pen borderPen = new Pen(borderColor, 2);
            Pen shadowPen = new Pen(shadowBorderColor, 2);

            g.DrawLine(borderPen, new Point(0, hideBtn.Height + 1), new Point(this.Width, hideBtn.Height + 1));

            g.DrawLine(borderPen, new Point(0, 0), new Point(this.Width, this.Height));

            tableLayoutPanel1.BackgroundImage = bmp;

            control.GetControl().BackgroundImage = bmp;
        }
        private void btnSearchGO_Click(object sender, EventArgs e)
        {
            if(OnSearchClick != null)
            {
                OnSearchClick();
            }
        }

        private void btnClearSearchPanel_Click(object sender, EventArgs e)
        {
            ClearPanel();

            if (OnSearchClick != null) OnSearchClick();
        }

        protected void ClearPanel()
        {
            _control.Clear();
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    base.OnPaintBackground(e);

        //    Color borderColor = Color.FromArgb(0xcecbc6);
        //    Color shadowBorderColor = Color.FromArgb(0xe6e5e1);

        //    Pen borderPen = new Pen(borderColor, 1);
        //    Pen shadowPen = new Pen(shadowBorderColor, 1);

        //    e.Graphics.DrawLine(borderPen, new Point(0, hideBtn.Height + 1), new Point(this.Width, hideBtn.Height + 1));

        //    e.Graphics.DrawLine(borderPen, new Point(this.Width - 2, 0), new Point(this.Width - 2, this.Height));

        //    e.Graphics.DrawLine(shadowPen, new Point(0, hideBtn.Height + 2), new Point(this.Width, hideBtn.Height + 2));


        //}
    }
}
