using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class DocumentSearchModel
    {
        public int DocumentTypeID { get; set; } = 0;
        public string DocumentTypeName { get; set; }
        public string Serial { get; set; }
        public string Number { get; set; }
        public string IssuedBy { get; set; }
        public CompareString CompareIssuedBy { get; set; } = CompareString.EQUAL;
        public DateTime WhenIssued { get; set; }
        public CompareValue CompareWhenIssued { get; set; } = CompareValue.EQUAL;
        public string CodeDevision { get; set; }
        public CompareString CompareCodeDevision { get; set; } = CompareString.EQUAL;
        public bool IsEmptySearchModel
        {
            get
            {
                return
                    DocumentTypeID == 0
                    && string.IsNullOrEmpty(Serial)
                    && string.IsNullOrEmpty(Number)
                    && string.IsNullOrEmpty(IssuedBy)
                    && string.IsNullOrEmpty(CodeDevision)
                    && WhenIssued.Year == 1
                    && string.IsNullOrEmpty(DocumentTypeName);
            }
        }
    }
}
