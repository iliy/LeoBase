using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormControls.Controls.Models
{
    public class LeoDataViewHeadMetaData
    {
        public int ColumnIndex { get; set; }
        public string DisplayName { get; set; }
        public string FieldName { get; set; }
        public bool Show { get; set; }
        public bool CanOrder { get; set; }
        public string Type { get; set; }
    }
}
