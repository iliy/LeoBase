using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeoMapV3.Models;
using System.Runtime.Caching;
using System.Data.SQLite;
using System.Data.Common;
using System.IO;

namespace LeoMapV3.Data
{
    public class TitleDBRepository : ITitlesRepository
    {
        private string _mapPath;

        private MemoryCache _cache;

        private List<string> _cacheKeys;

        private static Dictionary<int, List<TitleCacheModel>> _titles = null;

        private static Dictionary<int, ZoomInformation> _zoomInformation = null;

        public TitleDBRepository(string dbFilePath)
        {
            _mapPath = dbFilePath;

            _cacheKeys = new List<string>();

            if(_titles == null)
            {
                LoadTitles();
            }

            // Загрузка начальных данных, скорей всего потребуется загрузить все тайтлы без изображений, для более быстрого доступа к ним по ID
            // для этого имеет смысл создать статичное приватное поле в этом классе, в которое будем записывать все тайтлы
            // чтобы при последующем обращение к карте не тянуть лишнюю информацию из базы.
        }

        private void LoadTitles()
        {
            _titles = new Dictionary<int, List<TitleCacheModel>>();

            string query = "select id, x, y, z, n, e, w, s from 'titles'";

            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");

            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source=" + _mapPath;

                connection.Open();

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var title = new TitleCacheModel();

                            title.ID = rdr.GetInt32(0);
                            title.X = rdr.GetInt32(1);
                            title.Y = rdr.GetInt32(2);
                            title.Z = rdr.GetInt32(3);
                            title.N = rdr.GetDouble(4);
                            title.E = rdr.GetDouble(5);
                            title.W = rdr.GetDouble(6);
                            title.S = rdr.GetDouble(7);

                            if (!_titles.ContainsKey(title.Z)) _titles.Add(title.Z, new List<TitleCacheModel>());

                            _titles[title.Z].Add(title);
                        }
                    }
                }

                connection.Close();
            }

        }

        public Point ConvertMapCoordsToPixels(DPoint point)
        {
            if (_titles == null) LoadTitles();

            if (!_titles.ContainsKey(point.Zoom)) throw new ArgumentException("Зум указан неверно!");

            var title = _titles[point.Zoom].FirstOrDefault(t => t.E >= point.E && t.W <= point.E && t.N >= point.N && t.S <= point.N);

            if (title == null) return new Point(-1, -1);

            int x = title.X * GetZoomInformation(point.Zoom).TitleWidth;
            int y = title.Y * GetZoomInformation(point.Zoom).TitleHeight;

            x += (int)Math.Round(GetZoomInformation(point.Zoom).TitleWidth * (point.E- title.W) / (title.E - title.W));

            y += GetZoomInformation(point.Zoom).TitleHeight - (int)Math.Round(GetZoomInformation(point.Zoom).TitleHeight * (point.N - title.S) / (title.N - title.S));

            return new Point(x, y);
        }

        public DPoint ConvertPixelsToMapCoords(int x, int y, int z)
        {
            var zInf = GetZoomInformation(z);

            if (zInf == null) throw new ArgumentException("Указанный зум не найден!");

            if (_titles == null) LoadTitles();

            if (!_titles.ContainsKey(z)) throw new ArgumentException("Указанный зум не найден!");
            
            int titleX = (x - x % zInf.TitleWidth) / zInf.TitleWidth;

            int titleY = (y - y % zInf.TitleHeight) / zInf.TitleHeight;

            var title = _titles[z].FirstOrDefault(t => t.X == titleX && t.Y == titleY);

            if (title == null) return null;

            DPoint result = new DPoint();

            double dE = title.E - title.W;
            double dN = title.N - title.S;

            double dX = (double)(x % zInf.TitleWidth) / (double)zInf.TitleWidth;
            double dY = 1 - (double)(y % zInf.TitleHeight) / (double)zInf.TitleHeight;

            
            result.N = title.S + dN * dY;
            result.E = title.W + dE * dX;
            result.Zoom = z;

            return result;
        }

        public TitleMapModel GetTitle(int x, int y, int z)
        {
            if (_cache == null) _cache = MemoryCache.Default;

            string cacheKey = "img_" + x + "_" + y + "_" + z;

            if (_cache.Contains(cacheKey)) return (TitleMapModel)_cache[cacheKey];

            if (_titles == null) LoadTitles();

            if (!_titles.ContainsKey(z)) return null;

            var title = _titles[z].FirstOrDefault(t => t.X == x && t.Y == y);

            if (title == null) return null;

            TitleMapModel result = new TitleMapModel();

            string query = "select n, s, e, w, i from 'titles' where id=" + title.ID;

            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source=" + _mapPath;

                connection.Open();

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            result.N = rdr.GetDouble(0);
                            result.S = rdr.GetDouble(1);
                            result.E = rdr.GetDouble(2);
                            result.W = rdr.GetDouble(3);
                            result.Image = CellToImage(rdr["i"]);
                        }
                    }
                }

                connection.Close();
            }

            _cache.Add(cacheKey, result, DateTime.Now.AddMinutes(5));

            _cacheKeys.Add(cacheKey);

            return result;
        }

        public ZoomInformation GetZoomInformation(int z)
        {
            if (_zoomInformation != null && _zoomInformation.ContainsKey(z)) return _zoomInformation[z];

            if (_cache == null) _cache = MemoryCache.Default;

            if (_cache.Contains("zInf_" + z)) return (ZoomInformation)_cache["zInf_" + z];

            if (_titles == null) LoadTitles();

            ZoomInformation result = new ZoomInformation();

            result.MaxX = _titles[z].Max(t => t.X);
            result.MaxY = _titles[z].Max(t => t.Y);
            result.Zoom = z;

            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");

            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source=" + _mapPath;

                connection.Open();

                var ft = _titles[z].First();

                int id = ft != null ? ft.ID : 0;

                string query = "select i from 'titles' where id = " + id;

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            var img = CellToImage(rdr["i"]);

                            result.TitleWidth = img.Width;
                            result.TitleHeight = img.Height;
                        }
                    }
                }

                query = "select max(n) from 'titles' where z=" + result.Zoom;

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            result.N = rdr.GetDouble(0);
                        }
                    }
                }

                query = "select max(e) from 'titles' where z=" + result.Zoom;

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            result.E = rdr.GetDouble(0);
                        }
                    }
                }

                query = "select min(s) from 'titles' where z=" + result.Zoom;

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using(var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            result.S = rdr.GetDouble(0);
                        }
                    }
                }

                query = "select min(w) from 'titles' where z=" + result.Zoom;

                using(var cmd = new SQLiteCommand(query, connection))
                {
                    using(var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            result.W = rdr.GetDouble(0);
                        }
                    }
                }

                connection.Close();
            }

            _cache.Add("zInf_" + z, result, DateTime.Now.AddMinutes(10));

            _cacheKeys.Add("zIng_" + z);

            return result;
        }

        
        public Dictionary<int, ZoomInformation> GetZoomsInformation()
        {
            if (_zoomInformation != null) return _zoomInformation;

            _zoomInformation = new Dictionary<int, ZoomInformation>();

            var zooms = GetZooms();

            foreach(var zoom in zooms)
            {
                _zoomInformation.Add(zoom, GetZoomInformation(zoom));
            }

            return _zoomInformation;
        }

        public List<int> GetZooms()
        {
            if (_cache == null) _cache = MemoryCache.Default;

            if (_cache.Contains("zooms")) return (List<int>)_cache["zooms"];

            string query = "select distinct z from 'titles'";

            List<int> result = new List<int>();

            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");

            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source=" + _mapPath;

                connection.Open();

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(rdr.GetInt32(0));
                        }
                    }
                }

                connection.Close();
            }

            _cache.Add("zooms", result, new DateTimeOffset(DateTime.Now.AddMinutes(10)));

            _cacheKeys.Add("zooms");

            return result;
        }

        public void ClearCache()
        {
            if (_cache == null) _cache = MemoryCache.Default;

            if (_cacheKeys == null || _cacheKeys.Count == 0)
            {
                _cacheKeys = new List<string>();

                foreach (var key in _cache)
                {
                    _cacheKeys.Add(key.Key);
                }
            }

            foreach (var key in _cacheKeys) _cache.Remove(key);

            _cacheKeys.Clear();
        }

        private Image CellToImage(object data)
        {
            return ByteArrayToImage((byte[])data);
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
