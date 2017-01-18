using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.VModels.Persons;

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class AllPersonesSearchPanel : UserControl
    {
        private PersoneSearchPanel _personeSearchPanel;
        private DocumentSearchPanel _documentSearchPanel;
        private AddressSearchPanel _addressSearchPanel;

        public event Action Search;

        public SearchAddressModel AddressSearchModel
        {
            get
            {
                return _addressSearchPanel.SearchAddressModel;
            }
        }

        public PersonsSearchModel PersonSearchModel
        {
            get
            {
                return _personeSearchPanel.SearchModel;
            }
        }

        public DocumentSearchModel DocumentSearchModel
        {
            get
            {
                return _documentSearchPanel.DocumentSearchModel;
            }
        }

        public AllPersonesSearchPanel():this(false)
        {
        }

        public AllPersonesSearchPanel(bool isEmployer)
        {
            InitializeComponent();

            _personeSearchPanel = new PersoneSearchPanel();
            _personeSearchPanel.ItSearchForEmployer = isEmployer;

            _addressSearchPanel = new AddressSearchPanel();

            _documentSearchPanel = new DocumentSearchPanel();

            _addressSearchPanel.Height = _personeSearchPanel.Height;
            _documentSearchPanel.Height = _personeSearchPanel.Height;

            _addressSearchPanel.Width = 420;
            _documentSearchPanel.Width = 420;
            tableLayoutPanel1.Width = 420;
            _personeSearchPanel.Width = 420;

            tableLayoutPanel1.Controls.Add(_personeSearchPanel, 0, 0);
            tableLayoutPanel1.Controls.Add(_addressSearchPanel, 0, 1);

            if (!isEmployer)
                tableLayoutPanel1.Controls.Add(_documentSearchPanel, 0, 2);

            this.Height = tableLayoutPanel1.Height + 90;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Search != null)
            {
                Search();
                if (HideSearchPanel != null) HideSearchPanel();
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            Clear();

            if (Search != null)
                Search();
        }

        public void Clear()
        {
            _personeSearchPanel.Clear();
            _addressSearchPanel.Clear();
            _documentSearchPanel.Clear();
        }
        public event Action HideSearchPanel;

        private void button1_Click(object sender, EventArgs e)
        {
            if (HideSearchPanel != null)
                HideSearchPanel();
        }
    }
}
