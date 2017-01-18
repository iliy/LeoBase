using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormControls.Controls.Models
{
    public class LeoDataComboBox:List<string>
    {
        public string SelectedValue { get; set; }
        public LeoDataComboBox(string selectedValue)
        {
            SelectedValue = selectedValue;
        }
    }
}
