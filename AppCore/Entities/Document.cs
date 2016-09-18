using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public int UserID { get; set; }
        public string Serial { get; set; }
        public string Number { get; set; }
        public string IssuedBy { get; set; }
        public string WhenIssued { get; set; }
        public string CodeDevision { get; set; }
    }
}
