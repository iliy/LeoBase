using LeoMapV2.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoMapV2.Map.interfaces
{
    public interface IMapRenderer
    {
        event Action<string> OnError;

        Image GetImage();
        Task<Image> GetImageAsync();

        int Width { get; set; }
        int Height { get; set; }
        List<DPoint> Points { get; set; }
        void LookAt(int x, int y);
        void LookAt(DPoint point);
        bool ZoomPlus(int dx, int dy);
        bool ZoomMinus(int dx, int dy);
        void MoveCenter(int dx, int dy);
        bool WasRender { get; }
        MapEventArgs GetMapEventArgs(int x, int y);
        DPoint MouseAtPoint(int x, int y);
    }
}
