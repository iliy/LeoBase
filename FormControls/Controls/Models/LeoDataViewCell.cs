using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormControls.Controls.Models
{
    public class LeoDataViewCell:Panel
    {
        public Control Control { get; private set; }
        public LeoDataViewCell()
        {
            this.Dock = DockStyle.Top;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(0, 0, 0, 0);
            this.Height = 50;
        }

        public void SetControl(Control control)
        {
            this.Controls.Clear();
            this.Controls.Add(control);
        }
    }
}
