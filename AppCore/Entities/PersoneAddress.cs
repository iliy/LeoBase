using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class PersoneAddress
    {
        [Key]
        public int AddressID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Country { get; set; } = "Российская Федерация";
        
        [Required(AllowEmptyStrings = false)]
        public virtual Persone Persone { get; set; }

        public string Subject { get; set; } = "Приморский край";

        public string Area { get; set; } = "Хасанский район";

        public string City { get; set; }

        public string Street { get; set; }

        public string HomeNumber { get; set; }

        public string Flat { get; set; }

        [DefaultValue("Проживает и прописка")]
        public string Note { get; set; } = "Проживает и прописка";
    }
}
