using AppData.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class DocumentSearchModel
    {
        [DisplayName("Тип документа")]
        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DataPropertiesName("DocumentTypes")]
        public string DocumentTypeID { get; set; }

        private string s = "";
        [DisplayName("Серия")]
        public string Serial { get; set; }
        [DisplayName("Номер")]
        public string Number { get; set; }
        [DisplayName("Кем выдан")]
        public string IssuedBy { get; set; }
        [Browsable(false)]
        public CompareString CompareIssuedBy { get; set; } = CompareString.CONTAINS;
        [ControlType(ControlType.DateTime)]
        [DisplayName("Когда выдан")]
        [ChildrenProperty("CompareWhenIssued")]
        public DateTime WhenIssued { get; set; }
        [Browsable(false)]
        public CompareValue CompareWhenIssued { get; set; } = CompareValue.NONE;
        [DisplayName("Код подразделения")]
        public string CodeDevision { get; set; }
        [Browsable(false)]
        public CompareString CompareCodeDevision { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public bool IsEmptySearchModel
        {
            get
            {
                return
                    (DocumentTypeID == "0" || DocumentTypeID == null)
                    && string.IsNullOrEmpty(Serial)
                    && string.IsNullOrEmpty(Number)
                    && string.IsNullOrEmpty(IssuedBy)
                    && string.IsNullOrEmpty(CodeDevision)
                    && WhenIssued.Year == 1;
            }
        }
        
        [Browsable(false)]
        public List<ComboBoxDefaultItem> DocumentTypes
        {
            get
            {
                var _dTypes = new List<ComboBoxDefaultItem>();

                _dTypes.Add(new ComboBoxDefaultItem
                {
                    Display = "Не учитывать",
                    Value = "0"
                });

                _dTypes.AddRange(ConfigApp.DocumentsType.Select(d => new ComboBoxDefaultItem { Display = d.Value, Value = d.Key.ToString() }).ToList());

                return _dTypes;
            }
        }
    }
}
