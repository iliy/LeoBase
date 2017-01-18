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

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class AdminViolationSearchPanel : UserControl
    {
        public AdminViolationSearchModel SearchModel {get;set;}
        public AdminViolationSearchPanel()
        {
            InitializeComponent();

            var leoTextBox = new LeoTextBox();

            leoTextBox.Dock = DockStyle.Fill;

            tableLayoutPanel1.Controls.Add(leoTextBox);
        }
    }
}
