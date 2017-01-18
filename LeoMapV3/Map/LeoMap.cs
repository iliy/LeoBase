using LeoMapV3.Map.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeoMapV3.Models;
using System.Drawing;
using LeoMapV3.Data;

namespace LeoMapV3.Map
{
    public class LeoMap : Panel, ILeoMap
    {
        private ITitlesRepository _repo;

        private IMapRenderer _render;

        private PictureBox _pictureBox;

        private bool _dragAndDropStart;

        private int _oldX;

        private int _oldY;

        private int _dX;

        private int _dY;

        private Timer _timerDragAndDrop;
        private Label _lbToolTip;

        public event Action<object, MapEventArgs> MouseMoving;
        public event Action<object, MapRegion> RegionSelected;
        public event Action<object, DPoint> PointClicked;

        public int ClientMouseX { get; set; }
        public int ClientMouseY { get; set; }

        public MapDragAndDropStates MouseState { get; set; } = MapConfig.DefaultMouseState;



        public LeoMap(ITitlesRepository repo)
        {
            _timerDragAndDrop = new Timer();

            _lbToolTip = new Label();

            _lbToolTip.AutoSize = true;

            _lbToolTip.BackColor = Color.White;

            _lbToolTip.Padding = new Padding(3);

            _lbToolTip.Visible = false;

            this.Controls.Add(_lbToolTip);

            _timerDragAndDrop.Interval = 20;

            _timerDragAndDrop.Tick += _timerDragAndDrop_Tick;

            _pictureBox = new PictureBox();

            _pictureBox.Size = new Size(256, 400);

            _repo = repo;

            _render = new DefaultMapRenderer(_repo);
            
            this.Resize += OnResize;

            this.Controls.Add(_pictureBox);

            _pictureBox.MouseDown += OnMouseDown;

            _pictureBox.MouseMove += OnMouseMove;

            _pictureBox.MouseUp += OnMouseUp;

            _pictureBox.MouseWheel += EditZoom;

            _pictureBox.MouseClick += PictureBoxMouseClick;
            
            DoubleBuffered = true;
        }

        private void PictureBoxMouseClick(object sender, MouseEventArgs e)
        {
            if (_dragAndDropStart)
            {
                if (Math.Abs(_oldX - e.X) <= 2 || Math.Abs(_oldY - e.Y) <= 2)
                {
                    _dragAndDropStart = false;

                    _timerDragAndDrop.Stop();

                    _render.MoveCenter(_pictureBox.Left, _pictureBox.Top);

                    var img = _render.GetImage();

                    _pictureBox.Image = img;

                    _pictureBox.Left = 0;

                    _pictureBox.Top = 0;
                }
                else
                {
                    return;
                }
            }

            var point = _render.MouseAtPoint(e.X, e.Y);

            if(point != null && PointClicked != null)
            {
                ClientMouseX = e.X;
                ClientMouseY = e.Y;

                PointClicked(this, point);
            }
        }

        public async void RedrawAsync()
        {
            _pictureBox.Image = await _render.GetImageAsync();
        }

        public void Redraw()
        {
            _pictureBox.Image = _render.GetImage();
        }

        private  void EditZoom(object sender, MouseEventArgs e)
        {
            int dx = e.X - this.Width / 2;
            int dy = e.Y - this.Height / 2;

            bool zoomEdited = false;

            if(MouseState == MapDragAndDropStates.NONE)
            {
                dx = 0;
                dy = 0;
            }

            if (e.Delta < 0)
            {
                zoomEdited = _render.ZoomMinus(dx, dy);
            }
            else
            {
                zoomEdited = _render.ZoomPlus(dx, dy);
            }

            if (zoomEdited)
            {
                _pictureBox.Image = _render.GetImage();
            }
        }

        private void _timerDragAndDrop_Tick(object sender, EventArgs e)
        {
            int dx = 0;
            int dy = 0;

            if(_pictureBox.Left + _dX != _pictureBox.Left)
            {
                dx = _dX / 10;

                _pictureBox.Left += dx;
            }

            if(_pictureBox.Top + _dY != _pictureBox.Top)
            {
                dy = _dY / 10;

                _pictureBox.Top += dy;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _pictureBox.Cursor = Cursors.Default;

            if (MouseState == MapDragAndDropStates.NONE) return;

            if (!_dragAndDropStart) {
                if(e.Button == MouseButtons.Right)
                {
                    _pictureBox.Image = _buffer;

                    var startPoint = _render.GetMapEventArgs(_oldX, _oldY);
                    var endPoint = _render.GetMapEventArgs(e.X, e.Y);

                    double no = Math.Max(startPoint.N, endPoint.N);
                    double so = Math.Min(startPoint.N, endPoint.N);
                    double es = Math.Max(startPoint.E, endPoint.E);
                    double ws = Math.Min(startPoint.E, endPoint.E);

                    if(RegionSelected != null)
                    {
                        var region = new MapRegion
                        {
                            N = no,
                            S = so,
                            E = es,
                            W = ws
                        };

                        RegionSelected(this, region);
                    }
                    
                }
                return;
            }

            _dragAndDropStart = false;

            _timerDragAndDrop.Stop();

            _render.MoveCenter(_pictureBox.Left, _pictureBox.Top);

            var img = _render.GetImage();

            _pictureBox.Image = img;

            _pictureBox.Left = 0;

            _pictureBox.Top = 0;
        }

        private int _oldMovingX = 0;

        private int _oldMovingY = 0;

        private int _oldMovingWidth = 0;

        private int _oldMovingHeight = 0;

        private Rectangle _oldSelectedImage = new Rectangle();

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //if (MouseState == MapDragAndDropStates.NONE) return;

            if (!_dragAndDropStart) {

                if (e.Button == MouseButtons.Right && MouseState != MapDragAndDropStates.NONE)
                {
                    int x = Math.Min(_oldX, e.X);
                    int y = Math.Min(_oldY, e.Y);
                    int width = Math.Max(_oldX, e.X) - x;
                    int height = Math.Max(_oldY, e.Y) - y;
                    
                    Graphics g = Graphics.FromImage(_pictureBox.Image);
                    
                    _selectedImage = Graphics.FromImage(_pictureBox.Image);

                    Brush brush = new SolidBrush(Color.FromArgb(120, 100, 100, 100));

                    g.DrawImage(_buffer, _oldSelectedImage, _oldSelectedImage, GraphicsUnit.Pixel);

                    g.FillRectangle(brush, new Rectangle(x, y, width, height));

                    _oldSelectedImage.X = x;
                    _oldSelectedImage.Y = y;
                    _oldSelectedImage.Width = width;
                    _oldSelectedImage.Height = height;
                    
                    _pictureBox.Image = _pictureBox.Image;

                } else if (_render.MouseAtPoint(e.X, e.Y) != null)
                {
                    var point = _render.MouseAtPoint(e.X, e.Y);

                    _pictureBox.Cursor = Cursors.Hand;

                    if (!string.IsNullOrEmpty(point.ToolTip))
                    {
                        _lbToolTip.Left = e.X + 2;
                        _lbToolTip.Top = e.Y - _lbToolTip.Height - 3;
                        _lbToolTip.Text = point.ToolTip;
                        _lbToolTip.Visible = true;
                    }
                }else
                {
                    _pictureBox.Cursor = Cursors.Default;

                    _lbToolTip.Visible = false;
                }

                if (MouseMoving != null)
                {
                    var arg = _render.GetMapEventArgs(e.X, e.Y);

                    if (arg != null) MouseMoving(this, arg);
                }
                return;
            }

            _lbToolTip.Visible = false;

            if (e.Button == MouseButtons.None)
            {
                _dragAndDropStart = false;

                _timerDragAndDrop.Stop();

                _render.MoveCenter(_pictureBox.Left, _pictureBox.Top);

                var img = _render.GetImage();

                _pictureBox.Image = img;

                _pictureBox.Left = 0;

                _pictureBox.Top = 0;

                return;
            }

            if (MouseState == MapDragAndDropStates.NONE) return;
                 
            _dX = e.X - _oldX;

            _dY = e.Y - _oldY;
        }

        private Bitmap _buffer;
        private Graphics _selectedImage;

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (MouseState == MapDragAndDropStates.NONE) return;

            if(e.Button == MouseButtons.Left) {

                _pictureBox.Cursor = Cursors.NoMove2D;

                _dragAndDropStart = true;

                _oldX = e.X;

                _oldY = e.Y;

                _timerDragAndDrop.Start();
            }else if(e.Button == MouseButtons.Right)
            {
                _pictureBox.Cursor = Cursors.Cross;

                _oldX = e.X;

                _oldY = e.Y;

                _buffer = new Bitmap(_pictureBox.Image);

                _oldSelectedImage.X = 0;
                _oldSelectedImage.Y = 0;
                _oldSelectedImage.Width = 0;
                _oldSelectedImage.Height = 0;
                
                //Graphics g = Graphics.FromImage(_buffer);

                //g.DrawImage(_pictureBox.Image, new Rectangle(0, 0, _pictureBox.Width, _pictureBox.Height), new Rectangle(0, 0, _pictureBox.Width, _pictureBox.Height), GraphicsUnit.Pixel);
            }
        }

        private async void OnResize(object sender, EventArgs e)
        {
            _pictureBox.Width = this.Width;

            _pictureBox.Height = this.Height;

            _render.Width = this.Width;

            _render.Height = this.Height;

            _pictureBox.Image = await UpdateMapImage();
        }

        public Image GetMapImage()
        {
            return _pictureBox.Image;
        }

        public  void LookAt(DPoint point)
        {
            _render.LookAt(point);

            _pictureBox.Image = _render.GetImage();
        }

        public void SetPoint(DPoint point)
        {
            if (_render.Points == null) _render.Points = new List<DPoint>();

            _render.Points.Add(point);

            _repo.ClearCache();
        }

        public void SetPoints(List<DPoint> points)
        {
            if (_render.Points == null) _render.Points = new List<DPoint>();

            _render.Points.AddRange(points);

            _repo.ClearCache();
        }

        public void ClearPoints()
        {
            _render.Points = new List<DPoint>();

            _repo.ClearCache();
        }

        private async Task<Image> UpdateMapImage()
        {
            if (!_render.WasRender)
            { 
                var defaultZoomInformation = _repo.GetZoomInformation(MapConfig.DefaultZoom);

                int xC = defaultZoomInformation.MaxX * defaultZoomInformation.TitleWidth / 2;
                int yC = defaultZoomInformation.MaxY * defaultZoomInformation.TitleHeight / 2;

                _render.LookAt(xC, yC);
            }
            return await _render.GetImageAsync();
            //;
        }
    }
}
