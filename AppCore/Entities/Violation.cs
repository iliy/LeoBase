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
        
        public virtual List<ViolationImage> Images { get; set; }

        public virtual ViolationType ViolationType { get; set; }
        public virtual List<Protocol> Protocols { get; set; }
        public DateTime DateSave { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime Date { get; set; }

        public double N { get; set; }

        public double E { get; set; }

        public string Description { get; set; }
    }
}
