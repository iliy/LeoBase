using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Employer
    {
        [Key]
        public int EmployerID { get; set; }

        public int PersoneID { get; set; }

        public int ViolationID { get; set; }
    }
}
