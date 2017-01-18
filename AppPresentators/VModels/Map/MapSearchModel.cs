using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Map
{
    public class MapSearchModel
    {
        [DefaultValue(-1)]
        public int ViolationID { get; set; } 
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public MapRegion MapRegion { get; set; }
    }

    public class MapRegion
    {
        [DefaultValue(0)]
        public double E { get; set; }
        [DefaultValue(0)]
        public double W { get; set; }
        [DefaultValue(0)]
        public double N { get; set; }
        [DefaultValue(0)]
        public double S { get; set; }
    }
}
