using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoMapV3.Models
{
    public class ZoomInformation
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int Zoom { get; set; }
        public double E { get; set; }
        public double W { get; set; }
        public double N { get; set; }
        public double S { get; set; }
        public int TitleWidth { get; set; }
        public int TitleHeight { get; set; }
    }
}
