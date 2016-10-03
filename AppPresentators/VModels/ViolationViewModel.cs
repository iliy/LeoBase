using AppPresentators.VModels.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class ViolationViewModel
    {
        public int ViolationID { get; set; }

        //public virtual List<Violator> Violators { get; set; }

        //public virtual List<Employer> Employers { get; set; }

        public List<byte[]> Images { get; set; }

        public string ViolationType { get; set; }

        public List<ProtocolViewModel> Protocols { get; set; }

        public DateTime Date { get; set; }

        public double N { get; set; }

        public double E { get; set; }

        public string Description { get; set; }
    }
}
