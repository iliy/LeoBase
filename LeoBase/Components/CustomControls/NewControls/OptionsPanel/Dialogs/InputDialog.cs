using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.NewControls.OptionsPanel.Dialogs
{
    public partial class InputDialog : Form
    {
        public string Result { get; private set; }

        public string Title {
            get
            {
                return lbText.Text;
            }
            set
            {
                lbText.Text = value;
            }
        }
        public InputDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = textBox1.Text;

            DialogResult = DialogResult.OK;

            Close();
        }
    }
}
