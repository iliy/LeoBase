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
using AppPresentators.VModels;

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class DocumentSearchPanel : UserControl
    {
        public DocumentSearchModel DocumentSearchModel
        {
            get
            {
                return new DocumentSearchModel
                {
                    //DocumentTypeName = cmbDocumentType.Items[cmbDocumentType.SelectedIndex].ToString() != null ?
                    //                   cmbDocumentType.Items[cmbDocumentType.SelectedIndex].ToString() :
                    //                   "",
                    Serial = tbDocumentSerial.Text.Trim(),
                    Number = tbDocumentNumber.Text.Trim(),
                    CompareWhenIssued = cmbWhenIssuedBy.Items[cmbWhenIssuedBy.SelectedIndex] == null ? CompareValue.EQUAL :
                                        cmbWhenIssuedBy.Items[cmbWhenIssuedBy.SelectedIndex].ToString().Equals("Больше") ? CompareValue.MORE :
                                        cmbWhenIssuedBy.Items[cmbWhenIssuedBy.SelectedIndex].ToString().Equals("Меньше") ? CompareValue.LESS :
                                        CompareValue.EQUAL,
                    WhenIssued = dtpWhenIssued.Value.Year == 1900 ? new DateTime(1, 1, 1, 0, 0, 0) :
                                 dtpWhenIssued.Value,
                    IssuedBy = tbIssuedBy.Text.Trim(),
                    CompareIssuedBy = chbIssuedBy.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                    CodeDevision = tbCodeDevision.Text.Trim()
                };
            }
        }


        public DocumentSearchPanel()
        {
            InitializeComponent();

            cmbWhenIssuedBy.SelectedIndex = 0;
            cmbDocumentType.SelectedIndex = 0;
        }

        public void Clear()
        {
            cmbDocumentType.SelectedIndex = 0;
            tbDocumentSerial.Text = "";
            tbDocumentNumber.Text = "";
            tbIssuedBy.Text = "";
            dtpWhenIssued.Value = new DateTime(1900, 1, 1);
            cmbWhenIssuedBy.SelectedIndex = 0;
            tbCodeDevision.Text = "";
        }

        private void btnClearDocument_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
