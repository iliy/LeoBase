using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Phone
    {
        [Key]
        public int PhoneID { get; set; }
        public virtual Persone Persone { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }
    }
}
