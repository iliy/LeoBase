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

            hideBtn.Click += (s, e) =>
            {
                if (OnHideClick != null) OnHideClick();
            };

            tableLayoutPanel1.Controls.Add(hideBtn, 0, 0);

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
    }
}
