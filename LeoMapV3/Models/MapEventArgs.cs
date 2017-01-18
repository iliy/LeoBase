using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoMapV3.Models
{
    public class MapEventArgs
    {
        public int CanvasX { get; private set; }
        public int CanvasY { get; private set; }
        public double N { get; private set; }
        public double E { get; private set; }
        public int MapX { get; private set; }
        public int MapY { get; private set; }

        public MapEventArgs(int x, int y, double n, double e, int mx, int my)
        {
            CanvasX = x;
            CanvasY = y;
            N = n;
            E = e;
            MapX = mx;
            MapY = my;
        }

    }
}
