using LeoMapV3.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoMapV3.Map.interfaces
{
    public interface ILeoMap
    {
        event Action<object, MapEventArgs> MouseMoving;
        event Action<object, MapRegion> RegionSelected;
        event Action<object, DPoint> PointClicked;
        int ClientMouseX { get; set; }
        int ClientMouseY { get; set; }
        void Redraw();
        void RedrawAsync();
        void SetPoint(DPoint point);
        void SetPoints(List<DPoint> points);
        Image GetMapImage();
        void LookAt(DPoint point);
        void ClearPoints();
        MapDragAndDropStates MouseState { get; set; }
    }
}
