using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoMapV3.Models
{
    public class DPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double E { get; set; }
        public double N { get; set; }
        public int Zoom { get; set; } = MapConfig.DefaultZoom;
        public string ToolTip { get; set; }
        public object DataSource { get; set; }
    }
}
