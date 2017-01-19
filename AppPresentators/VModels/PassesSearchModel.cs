using AppData.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class PassesSearchModel
    {
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [DisplayName("Тип документа")]
        public string DocumentType { get; set; }
        [DisplayName("Серия документа")]
        public string Serial { get; set; }
        [DisplayName("Номер документа")]
        public string Number { get; set; }
        [DisplayName("Кем выдан")]
        public string IssuedBy { get; set; }

        [ControlType(ControlType.DateTime)]
        [DisplayName("Когда выдан")]
        [ChildrenProperty("CompareWhenIssued")]
        public DateTime WhenIssued { get; set; } = DateTime.Now;
        [Browsable(false)]
        public CompareValue CompareWhenIssued { get; set; } = CompareValue.NONE;


        [ControlType(ControlType.DateTime)]
        [DisplayName("Когда выдан пропуск")]
        [ChildrenProperty("CompareWhenGived")]
        public DateTime WhenGived { get; set; } = DateTime.Now;
        [Browsable(false)]
        public CompareValue CompareWhenGived { get; set; } = CompareValue.NONE;

        [ControlType(ControlType.DateTime)]
        [DisplayName("Пропуск выдан до")]
        [ChildrenProperty("CompareWhenClosed")]
        public DateTime WhenClosed { get; set; } = DateTime.Now;
        [Browsable(false)]
        public CompareValue CompareWhenClosed { get; set; } = CompareValue.NONE;

        public bool IsEmptySearchModel()
        {
            return string.IsNullOrWhiteSpace(FIO) && string.IsNullOrWhiteSpace(DocumentType) && string.IsNullOrWhiteSpace(Serial) && string.IsNullOrWhiteSpace(Number)
                && string.IsNullOrWhiteSpace(IssuedBy) && CompareWhenIssued == CompareValue.NONE && CompareWhenClosed == CompareValue.NONE && CompareWhenGived == CompareValue.NONE;

        }
    }
}
