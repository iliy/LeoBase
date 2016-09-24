using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class VViolator
    {
        public int ID { get; set; }
    }
    public class VEmployer
    {
        public int ID { get; set; }
    }
    public class VViolationType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class VViolation
    {
        private Violation _violation;
        
        public int ViolationID { get; set; }
        public string Date { get; set; }
        public VViolationType ViolationType { get; set; }
        public double N { get; set; }
        public double E { get; set; }
        public string Description { get; set; } 
        public List<VViolator> Violators { get; set; }
        public List<VEmployer> Employers { get; set; }
    }
}
