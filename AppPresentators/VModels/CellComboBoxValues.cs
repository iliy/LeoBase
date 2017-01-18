using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class CellComboBoxValues:List<string>
    {
        public string SelectedText { get; set; }
        public CellComboBoxValues(string selectedtext)
        {
            SelectedText = selectedtext;
        }
    }
}
