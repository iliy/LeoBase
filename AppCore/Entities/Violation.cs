using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Violation
    {
        public int ViolationID { get; set; }
        public int ViolationTypeID { get; set; }
        public string Date { get; set; }
        public double N { get; set; }
        public double E { get; set; }
        public string Description { get; set; }
    }
}
