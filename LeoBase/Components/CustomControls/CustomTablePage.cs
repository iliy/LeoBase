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

namespace LeoBase.Components.CustomControls
{
    public partial class CustomTablePage : UserControl
    {
        private OpenSearchPanelButton openSearchPanelButton;

        private ISearchPanel _searchPanel;

        public bool VisibleLoadPanel
        {
            get
            {
                return loadPanel.Visible;
            }
            set
            {
                loadPanel.Visible = value;
            }
        }

        public string Title
        {
            get
            {
                return lbTitle.Text;
            }
            set
            {
                lbTitle.Text = value;
            }
        }

        public CustomTablePage()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;

            this.Margin = new Padding(0, 0, 0, 0);

            openSearchPanelButton = new OpenSearchPanelButton();

            mainTableContainer.Controls.Add(openSearchPanelButton, 0, 1);

            openSearchPanelButton.Click += (s, e) =>
            {
                //if (_searchPanel != null) _searchPanel.SearchButtonVisible = false;

                SearchPanelContainer.Visible = true;

                openSearchPanelButton.Visible = false;
            };
        }

        public void AddSearchPanel(ISearchPanel searchPanel)
        {
            _searchPanel = searchPanel;
            SearchPanelContainer.Controls.Add(searchPanel.Control);
            searchPanel.OnHideClick += HideSearchPanel;
        }

        public void HideSearchPanel()
        {
            //if (_searchPanel != null) _searchPanel.SearchButtonVisible = false;

            //WidthAnimate animate = new WidthAnimate(SearchPanelContainer, SearchPanelContainer.Width, 0, 10, 20);

            //animate.OnAnimationComplete += () => openSearchPanelButton.Visible = true;

            openSearchPanelButton.Visible = true;

            SearchPanelContainer.Visible = false;
        }

        public void SetContent(Control control)
        {
            control.Dock = DockStyle.Fill;
            mainTableContainer.Controls.Add(control, 0, 2);
        }


    }
}
