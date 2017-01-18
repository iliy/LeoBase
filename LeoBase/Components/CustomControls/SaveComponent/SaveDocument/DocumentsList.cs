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

namespace LeoBase.Components.CustomControls.SaveComponent.SaveDocument
{
    public partial class DocumentsList : UserControl
    {
        public List<PersoneDocumentModelView> Documents
        {
            get
            {
                List<PersoneDocumentModelView> result = new List<PersoneDocumentModelView>();
                foreach(var item in flowLayoutPanel1.Controls)
                {
                    var doc = (DocumentItem)item;
                    if(doc.Document != null)
                    {
                        result.Add(doc.Document);
                    }
                }
                return result;
            }
            set
            {
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Refresh();

                foreach(var item in value)
                {
                    DocumentItem doc = new DocumentItem();
                    doc.Document = item;
                    doc.RemoveThis += () => RemoveItem(doc);
                    if (value.Count == 1) doc.ShowRemoveButton = false;
                    flowLayoutPanel1.Controls.Add(doc);
                }
            }
        }

        private void RemoveItem(Control control)
        {
            flowLayoutPanel1.Controls.Remove(control);
        }
        public DocumentsList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DocumentItem doc = new DocumentItem();
            doc.Document = new PersoneDocumentModelView
            {
                DocumentID = -1,
                WhenIssued = new DateTime(1999, 1, 1)
            };
            doc.RemoveThis += () => RemoveItem(doc);
            flowLayoutPanel1.Controls.Add(doc);
        }
    }
}
