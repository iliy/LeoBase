using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Phone
    {
        public int PhoneID { get; set; }
        public int UserID { get; set; }
        public string PhoneNumber { get; set; }
    }
}
