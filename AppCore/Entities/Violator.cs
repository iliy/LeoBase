using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Violator
    {
        [Key]
        public int ViolatorID { get; set; }
        public int PersoneID { get; set; }
        public int ViolationID { get; set; }
        public string ViolatorType { get; set; } = "Гражданин"; // "Гражданин или организация"
        public Protocol Protocol { get; set; }
    }
}
