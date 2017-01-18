using LeoMapV3.Map.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LeoMapV3.Data;
using LeoMapV3.Models;

namespace LeoMapV3.Map
{
    public class DefaultMapRenderer : IMapRenderer
    {

        public event Action<string> OnError;

        private ITitlesRepository _repo;

        public int Width { get; set; }
        public int Height { get; set; }

        private Point CenterMap { get; set; }

        public bool WasRender { get; private set; } = false;

        public List<DPoint> Points { get; set; }

        private int _currentZoom = MapConfig.DefaultZoom;

        private List<PlaceMark> _visiblePlaceMarks;

        public DefaultMapRenderer(ITitlesRepository repo)
        {
            _repo = repo;
            _visiblePlaceMarks = new List<PlaceMark>();
        }

        public DPoint MouseAtPoint(int x, int y)
        {
            if (_visiblePlaceMarks == null) return null;

            var placeMark = _visiblePlaceMarks.FirstOrDefault(p => p.Rect.X <= x && p.Rect.Y <= y && p.Rect.X + p.Rect.Width >= x && p.Rect.Y + p.Rect.Height >= y);

            return placeMark != null ? placeMark.Point : null;
        }


        public Image GetImage()
        {
            _visiblePlaceMarks.Clear();

            int l = CenterMap.X - Width / 2;
            int t = CenterMap.Y - Height / 2;
            int r = CenterMap.X + Width / 2;
            int b = CenterMap.Y + Height / 2;

            int titleWidth = _repo.GetZoomInformation(_currentZoom).TitleWidth;
            int titleHeight = _repo.GetZoomInformation(_currentZoom).TitleHeight;
            
            int xStart = CoordsToIndexH(l, titleWidth);
            int xEnd = CoordsToIndexH(r, titleWidth);

            int yStart = CoordsToIndexV(t, titleHeight);
            int yEnd = CoordsToIndexV(b, titleHeight);

            Bitmap result = new Bitmap((xEnd - xStart + 1) * titleWidth, (yEnd - yStart + 1) * titleHeight);

            Graphics g = Graphics.FromImage(result);

            for (int x = xStart; x <= xEnd; x++)
            {
                for (int y = yStart; y <= yEnd; y++)
                {
                    var title = _repo.GetTitle(x, y, _currentZoom);

                    if (title == null) continue;

                    var img = title.Image;

                    Rectangle from = new Rectangle();

                    Rectangle to = new Rectangle();

                    from.X = 0;
                    from.Y = 0;
                    from.Width = img.Width;
                    from.Height = img.Height;

                    to.X = (x - xStart) * titleWidth - l % titleWidth;
                    to.Y = (y - yStart) * titleHeight - t % titleHeight;

                    to.Width = titleWidth;
                    to.Height = titleHeight;
                    
                    if(Points != null)
                    {
                        var titlePoints = Points.Where(p => title.E >= p.E && title.W <= p.E && title.N >= p.N && title.S <= p.N);

                        double dE = title.E - title.W;
                        double dN = title.N - title.S;

                        if (titlePoints != null)
                        {
                            Graphics titleImage = Graphics.FromImage(img);

                            var placeMarkImage = Properties.Resources.placeMark;

                            foreach (var p in titlePoints)
                            {
                                int xPoint = (int)Math.Round((double)img.Width * (p.E - title.W) / dE );
                                int yPoint = img.Height - (int)Math.Round((double)img.Height * (p.N - title.S) / dN );

                                if (xPoint - placeMarkImage.Width / 2 < 0) xPoint = placeMarkImage.Width / 2;
                                else if (xPoint + placeMarkImage.Width / 2 > img.Width) xPoint = img.Width - placeMarkImage.Width / 2;

                                if (yPoint   < 0) yPoint = 0;
                                else if (yPoint + placeMarkImage.Height > img.Height) yPoint = img.Height - placeMarkImage.Height;

                                Rectangle drawTo = new Rectangle();

                                drawTo.X = xPoint - placeMarkImage.Width / 2;
                                drawTo.Y = yPoint - placeMarkImage.Height;
                                drawTo.Width = placeMarkImage.Width;
                                drawTo.Height = placeMarkImage.Height;

                                titleImage.DrawImage(placeMarkImage, drawTo, new Rectangle(0, 0, placeMarkImage.Width, placeMarkImage.Height), GraphicsUnit.Pixel);

                                _visiblePlaceMarks.Add(new PlaceMark
                                {
                                    Rect = new Rectangle
                                    {
                                        X = drawTo.X + to.X,
                                        Y = drawTo.Y + to.Y,
                                        Width = placeMarkImage.Width,
                                        Height = placeMarkImage.Height
                                    },
                                    Point = p
                                });
                            }
                        }
                    }

                    g.DrawImage(img, to, from, GraphicsUnit.Pixel);
                }
            }


            return (Image)result;
        }

        public Task<Image> GetImageAsync()
        {
            _visiblePlaceMarks.Clear();

            return Task.Run(() =>
            {
                int l = CenterMap.X - Width / 2;
                int t = CenterMap.Y - Height / 2;
                int r = CenterMap.X + Width / 2;
                int b = CenterMap.Y + Height / 2;

                int titleWidth = _repo.GetZoomInformation(_currentZoom).TitleWidth;
                int titleHeight = _repo.GetZoomInformation(_currentZoom).TitleHeight;

                int xStart = CoordsToIndexH(l, titleWidth);
                int xEnd = CoordsToIndexH(r, titleWidth);

                int yStart = CoordsToIndexV(t, titleHeight);
                int yEnd = CoordsToIndexV(b, titleHeight);

                Bitmap result = new Bitmap((xEnd - xStart + 1) * titleWidth, (yEnd - yStart + 1) * titleHeight);

                Graphics g = Graphics.FromImage(result);

                for(int x = xStart; x<=xEnd; x++)
                {
                    for(int y = yStart; y <= yEnd; y++)
                    {
                        var title = _repo.GetTitle(x, y, _currentZoom);

                        if (title == null) continue;

                        var img = title.Image;

                        Rectangle from = new Rectangle();

                        Rectangle to = new Rectangle();

                        from.X = 0;
                        from.Y = 0;
                        from.Width = img.Width;
                        from.Height = img.Height;

                        to.X = (x - xStart) * titleWidth - l % titleWidth;
                        to.Y = (y - yStart) * titleHeight - t % titleHeight;

                        to.Width = titleWidth;
                        to.Height = titleHeight;

                        if (Points != null)
                        {
                            var titlePoints = Points.Where(p => title.E >= p.E && title.W <= p.E && title.N >= p.N && title.S <= p.N);

                            double dE = title.E - title.W;
                            double dN = title.N - title.S;

                            if (titlePoints != null)
                            {
                                Graphics titleImage = Graphics.FromImage(img);

                                var placeMarkImage = Properties.Resources.placeMark;

                                foreach (var p in titlePoints)
                                {
                                    int xPoint = (int)Math.Round((double)img.Width * (p.E - title.W) / dE);
                                    int yPoint = img.Height - (int)Math.Round((double)img.Height * (p.N - title.S) / dN);

                                    if (xPoint - placeMarkImage.Width / 2 < 0) xPoint = placeMarkImage.Width / 2;
                                    else if (xPoint + placeMarkImage.Width / 2 > img.Width) xPoint = img.Width - placeMarkImage.Width / 2;

                                    if (yPoint < 0) yPoint = 0;
                                    else if (yPoint + placeMarkImage.Height > img.Height) yPoint = img.Height - placeMarkImage.Height;

                                    Rectangle drawTo = new Rectangle();

                                    drawTo.X = xPoint - placeMarkImage.Width / 2;
                                    drawTo.Y = yPoint - placeMarkImage.Height;
                                    drawTo.Width = placeMarkImage.Width;
                                    drawTo.Height = placeMarkImage.Height;

                                    titleImage.DrawImage(placeMarkImage, drawTo, new Rectangle(0, 0, placeMarkImage.Width, placeMarkImage.Height), GraphicsUnit.Pixel);

                                    _visiblePlaceMarks.Add(new PlaceMark
                                    {
                                        Rect = new Rectangle
                                        {
                                            X = drawTo.X + to.X,
                                            Y = drawTo.Y + to.Y,
                                            Width = placeMarkImage.Width,
                                            Height = placeMarkImage.Height
                                        },
                                        Point = p
                                    });
                                }
                            }
                        }

                        g.DrawImage(img, to, from, GraphicsUnit.Pixel);
                    }
                }

                
                return (Image)result;
            });
        }
        private static int CoordsToIndexH(int x, int titleWidth)
        {
            return (x - x % titleWidth) / titleWidth;
        }

        private static int CoordsToIndexV(int y, int titleHeight)
        {
            return (y - y % titleHeight) / titleHeight;
        }

        public void LookAt(int x, int y)
        {
            var zInf = _repo.GetZoomInformation(_currentZoom);

            int titleWidth = zInf.TitleWidth;
            int titleHeight = zInf.TitleHeight;

            int totalWidth = titleWidth * zInf.MaxX;
            int totalHeight = titleHeight * zInf.MaxY;

            if (x - Width / 2 < 0 && x + Width / 2 > totalWidth) x = totalWidth / 2;
            else if (x - Width / 2 < 0) x = Width / 2;
            else if (x + Width / 2 > totalWidth) x = totalWidth - Width / 2;

            if (y - Height / 2 < 0 && y + Height / 2 > totalHeight) y = totalHeight / 2;
            else if (y - Height / 2 < 0) y = Height / 2;
            else if (y + Height / 2 > totalHeight) y = totalHeight - Height / 2;

            CenterMap = new Point(x, y);

            WasRender = true;
        }
        
        public void MoveCenter(int dx, int dy)
        {
            int nX = CenterMap.X - dx;
            int nY = CenterMap.Y - dy;

            var zInf = _repo.GetZoomInformation(_currentZoom);

            int titleWidth = zInf.TitleWidth;
            int titleHeight = zInf.TitleHeight;

            int totalWidth = titleWidth * zInf.MaxX;
            int totalHeight = titleHeight * zInf.MaxY;

            if (nX - Width / 2 < 0) nX = Width / 2;
            if (nY - Height / 2 < 0) nY = Height / 2;

            if (nX + Width / 2 > totalWidth) nX = totalWidth - Width / 2;
            if (nY + Height / 2 > totalHeight) nY = totalHeight - Height / 2;

            CenterMap = new Point(nX, nY);
        }

        public void LookAt(DPoint point)
        {
            if (_repo.GetZooms().Contains(point.Zoom)) _currentZoom = point.Zoom;

            else point.Zoom = _currentZoom;

            var center = _repo.ConvertMapCoordsToPixels(point);

            LookAt(center.X, center.Y);
        }

        public MapEventArgs GetMapEventArgs(int x, int y)
        {
            int mx = x - Width / 2 + CenterMap.X;
            int my = y - Height / 2 + CenterMap.Y;

            var dPoint = _repo.ConvertPixelsToMapCoords(mx, my, _currentZoom);

            if (dPoint == null) return null;

            MapEventArgs eventArgs = new MapEventArgs(x, y, dPoint.N, dPoint.E, mx, my);

            return eventArgs;
        }

        public bool ZoomMinus(int dx, int dy)
        {
            if (_repo.GetZooms().Contains(_currentZoom - 1))
            {
                int nX = (CenterMap.X + dx) / 2;
                int nY = (CenterMap.Y + dy) / 2;

                CenterMap = new Point(nX, nY);
                
                _currentZoom--;

                return true;
            }
            return false;
        }

        public bool ZoomPlus(int dx, int dy)
        {
            if (_repo.GetZooms().Contains(_currentZoom + 1)) {

                int nx = (CenterMap.X + dx) * 2;
                int ny = (CenterMap.Y + dy) * 2;

                CenterMap = new Point(nx, ny);

                _currentZoom++;

                return true;
            }
            return false;
        }
    }
}
