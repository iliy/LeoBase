using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities
{
    public class Violator
    {
        public int ViolatorID { get; set; }
        public int ViolationID { get; set; }
        public int UserID { get; set; }
    }
}
