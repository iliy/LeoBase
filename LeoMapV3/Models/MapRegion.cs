using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoMapV3.Models
{
    public class MapRegion
    {
        public double N { get; set; }
        public double E { get; set; }
        public double W { get; set; }
        public double S { get; set; }

        public override string ToString()
        {
            return string.Format("E:{0:0.0000000}; N:{1:0.0000000}; W:{2:0.0000000}; S:{3:0.0000000}", E, N, W, S);
        }
    }
}
