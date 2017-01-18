using LeoMapV2.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoMapV2.Data
{
    public interface ITitlesRepository
    {
        TitleMapModel GetTitle(int x, int y, int z);
        Dictionary<int, ZoomInformation> GetZoomsInformation();
        ZoomInformation GetZoomInformation(int z);
        List<int> GetZooms();
        void ClearCache();
        Point ConvertMapCoordsToPixels(DPoint point);
        DPoint ConvertPixelsToMapCoords(int x, int y, int z);
    }
}
