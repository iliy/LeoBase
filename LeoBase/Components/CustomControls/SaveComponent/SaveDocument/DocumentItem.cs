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
using AppPresentators;

namespace LeoBase.Components.CustomControls.SaveComponent.SaveDocument
{
    public partial class DocumentItem : UserControl
    {
        public event Action RemoveThis;

        public bool ShowRemoveButton
        {
            get
            {
                return button1.Visible;
            }
            set
            {
                button1.Visible = value;
            }
        }

        private int _docId = -1;

        

        public PersoneDocumentModelView Document
        {
            get
            {
                PersoneDocumentModelView doc = new PersoneDocumentModelView
                {
                    DocumentID = _docId,
                    CodeDevision = tbCode.Text.Trim(),
                    IssuedBy = tbWhoIssued.Text.Trim(),
                    DocumentTypeName = cmbDocumentType.Items[cmbDocumentType.SelectedIndex].ToString(),
                    Number = tbNumber.Text.Trim(),
                    Serial = tbSerial.Text.Trim(),
                    WhenIssued = dtpWhenIssued.Value
                };

                if(string.IsNullOrEmpty(doc.Number) || string.IsNullOrEmpty(doc.IssuedBy) || string.IsNullOrEmpty(doc.Serial))
                {
                    return null;
                }

                return doc;
            }
            set
            {
                _docId = value.DocumentID;

                for (int i = 0; i < cmbDocumentType.Items.Count; i++)
                {
                    if (cmbDocumentType.Items[i].ToString().Equals(value.DocumentTypeName))
                    {
                        cmbDocumentType.SelectedIndex = i;
                        break;
                    }
                }

                tbSerial.Text = value.Serial;
                tbNumber.Text = value.Number;
                tbWhoIssued.Text = value.IssuedBy;
                tbCode.Text = value.CodeDevision;
                dtpWhenIssued.Value = value.WhenIssued;
            }
        }

        public DocumentItem()
        {
            InitializeComponent();

            foreach(var dt in ConfigApp.DocumentsType)
            {
                cmbDocumentType.Items.Add(dt.Value);
            }

            cmbDocumentType.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RemoveThis != null) RemoveThis();
        }
    }
}
