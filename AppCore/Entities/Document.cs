using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }
        public int Document_DocumentTypeID { get; set; }
        public virtual Persone Persone { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Serial { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Number { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string IssuedBy { get; set; }

        [Required(AllowEmptyStrings = false)]
        public DateTime WhenIssued { get; set; }

        public string CodeDevision { get; set; }
    }
}
