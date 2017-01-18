using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Services;
using AppPresentators.VModels;

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class ViolationSearchPanel : UserControl
    {
        public event Action HidePanel;
        public event Action Search;
        public ViolationSearchModel SearchModel { get
            {
                var dateViolation = dtpViolation.Value;
                var dateWasCreated = dtpWasCreated.Value;
                var dateWasUpdated = dtpWasUpdate.Value;
                var description = tbDescription.Text;

                List<CompareValue> compareDate = new List<CompareValue>
                {
                    CompareValue.EQUAL,
                    CompareValue.MORE,
                    CompareValue.LESS
                };

                return new ViolationSearchModel
                {
                    DateFixed = dateViolation.Year != 1990 ?
                                dateViolation : new DateTime(1, 1, 1),
                    DateSave = dateWasCreated.Year != 1990 ?
                                dateWasCreated : new DateTime(1, 1, 1),
                    DateUpdate = dateWasUpdated.Year != 1990 ?
                                 dateWasUpdated : new DateTime(1, 1, 1),

                    CompareDateFixed = compareDate[cmbCompareViolation.SelectedIndex],
                    CompareDateUpdate = compareDate[cmdCompareWasUpdate.SelectedIndex],
                    CompareDateSave = compareDate[cmbCompareWasCreated.SelectedIndex],

                    Description = tbDescription.Text.Trim(),
                    CompareDescription = chbCompareDescription.Checked ? CompareString.EQUAL : CompareString.CONTAINS
                };
            } set
            {

            }
        }

        public ViolationSearchPanel()
        {
            InitializeComponent();

            Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (HidePanel != null)
                HidePanel();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Search != null)
                Search();
        }

        public void Clear()
        {
            dtpViolation.Value = new DateTime(1990, 1, 1);
            dtpWasCreated.Value = new DateTime(1990, 1, 1);
            dtpWasUpdate.Value = new DateTime(1990, 1, 1);

            tbDescription.Text = "";

            cmbCompareViolation.SelectedIndex = 0;
            cmbCompareWasCreated.SelectedIndex = 0;
            cmdCompareWasUpdate.SelectedIndex = 0;
            chbCompareDescription.Checked = true;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            Clear();
            if (Search != null) Search();
        }
    }
}
