using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeoMapV3.Models;
using System.Runtime.Caching;

namespace LeoMapV3.Data
{
    public class TestTitleRepository : ITitlesRepository
    {
        private string path = "D:\\Maps\\EmptyTitles\\";

        private MemoryCache _cache;

        #region ZoomInformation
        private Dictionary<int, ZoomInformation> _zoomInformation = new Dictionary<int, ZoomInformation>
        {
            {10, new ZoomInformation
            {
                MaxX = 2,
                MaxY = 3,
                E = 10,
                W = 2,
                N = 14,
                S = 1,
                Zoom = 10,
                TitleHeight = 400,
                TitleWidth = 256
            }},
            {11, new ZoomInformation
            {
                MaxX = 4,
                MaxY = 6,
                E = 10,
                W = 2,
                N = 14,
                S = 1,
                Zoom = 11,
                TitleHeight = 400,
                TitleWidth = 256
            }},
            { 12, new ZoomInformation
            {
                MaxX = 8,
                MaxY = 12,
                E = 10,
                W = 2,
                N = 14,
                S = 1,
                Zoom = 12,
                TitleHeight = 400,
                TitleWidth = 256
            }},
            { 13, new ZoomInformation
            {
                MaxX = 16,
                MaxY = 24,
                E = 10,
                W = 2,
                N = 14,
                S = 1,
                Zoom = 13,
                TitleHeight = 400,
                TitleWidth = 256
            }},{14, new ZoomInformation
            {
                MaxX = 32,
                MaxY = 48,
                E = 10,
                W = 2,
                N = 14,
                S = 1,
                Zoom = 14,
                TitleHeight = 400,
                TitleWidth = 256
            }},

        };
        #endregion

        public TitleMapModel GetTitle(int x, int y, int z)
        {
            if (x < 0 || y < 0 || x > _zoomInformation[z].MaxX || y > _zoomInformation[z].MaxY) return null;
            if (_cache == null) _cache = MemoryCache.Default;

            Image result;

            if(_cache.Contains("img_" + z))
            {
                result = (Image)_cache["img_" + z];
            }
            else
            {
                result = Image.FromFile(path + "z" + z + ".jpg");
                _cache.Add("img_" + z, result, DateTime.Now.AddMinutes(2));
            }

            Bitmap bmp = new Bitmap(result.Width, result.Height);

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(result, new Rectangle(0, 0, result.Width, result.Height), new Rectangle(0, 0, result.Width, result.Height), GraphicsUnit.Pixel);

            int xt = result.Width / 2 - 20;

            int yt = result.Height / 2 - 10;

            g.DrawString("X:" + x + "; Y:" + y, new Font("Arial", 18, FontStyle.Bold), Brushes.Red, new Point(xt, yt));

            return new TitleMapModel { Image = bmp };
        }

        public ZoomInformation GetZoomInformation(int z)
        {
            if (_zoomInformation.ContainsKey(z)) return _zoomInformation[z];
            return null;
        }
        public void ClearCache()
        {
           
        }

        public Point ConvertMapCoordsToPixels(DPoint point)
        {
            return new Point();
        }

        public List<int> GetZooms()
        {
            return new List<int>
            {
                10, 11, 12, 13, 14
            };
        }

        public Dictionary<int, ZoomInformation> GetZoomsInformation()
        {
            return _zoomInformation;
        }
        public DPoint ConvertPixelsToMapCoords(int x, int y, int z)
        {
            return null;
        }
    }
}
