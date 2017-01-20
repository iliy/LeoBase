using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Components;
using AppData.Entities;
using AppPresentators.VModels.Map;
using LeoMapV3.Map;
using LeoMapV3.Data;
using LeoMapV3.Models;
using AppPresentators;
using System.IO;
using LeoBase.Components.TopMenu;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class MapControl : UserControl, IMapControl
    {

        private LeoMap _map;
        private TitleDBRepository _repo;

        private MapSearchModel SearchModel;

        public MapControl()
        {
            InitializeComponent();

            this.Load += MapControl_Load;

            SearchModel = new MapSearchModel();

            dtpFrom.Value = DateTime.Now.AddYears(-1);

            dtpTo.Value = DateTime.Now;
            
            SearchModel.DateFrom = dtpFrom.Value;

            SearchModel.DateTo = dtpTo.Value;

            SearchModel.MapRegion = new AppPresentators.VModels.Map.MapRegion
            {
                E = 133,
                N = 50,
                W = 120,
                S = 40
            };

            if (!File.Exists(ConfigApp.DefaultMapPath))
            {
                MessageBox.Show("Не удается найти карту по указанному пути: " + ConfigApp.DefaultMapPath, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try { 
                _repo = new TitleDBRepository(ConfigApp.DefaultMapPath); //"D:\\Maps\\test.lmap");
            }catch(Exception e)
            {
                MessageBox.Show("Не удается прочитать файл с картой: " + ConfigApp.DefaultMapPath, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _map = new LeoMap(_repo);

            _map.Dock = DockStyle.Fill;

            panelMap.Controls.Add(_map);

            _map.MouseMoving += (s, p) =>
            {
                lbCoords.Text = string.Format("N:{0:0.000000}; E:{1:0.000000}", p.N, p.E);
            };

            _map.RegionSelected += SelectedRegion;

            _map.PointClicked += ClickedToPoint;

            dtpFrom.ValueChanged += DtpFrom_ValueChanged;

            dtpTo.ValueChanged += DtpTo_ValueChanged;
        }

        private void DtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                return;
            }

            SearchModel.DateFrom = dtpFrom.Value;

            SearchModel.DateTo = dtpTo.Value;

            if (FilterViolations != null) FilterViolations(SearchModel);
        }

        private void DtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                return;
            }

            SearchModel.DateFrom = dtpFrom.Value;

            SearchModel.DateTo = dtpTo.Value;

            if (FilterViolations != null) FilterViolations(SearchModel);
        }

        private void MapControl_Load(object sender, EventArgs e)
        {
            if (_map != null) _map.ClearPoints();
            if (FilterViolations != null) FilterViolations(SearchModel);
        }

        private void ClickedToPoint(object arg1, LeoMapV3.Models.DPoint arg2)
        {
            var map = arg1 as LeoMap;

            if (map == null) return;

            if (OpenViolation != null) OpenViolation(((AdminViolation)(arg2.DataSource)).ViolationID);

            //if (!string.IsNullOrEmpty(arg2.ToolTip)) MessageBox.Show(string.Format("Tool tip:{0}\r\nX:{1}; Y:{2}", arg2.ToolTip, map.ClientMouseX, map.ClientMouseY));
        }

        private void SelectedRegion(object arg1, LeoMapV3.Models.MapRegion arg2)
        {
            if(dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("Даты для поиска указаны не верно.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SearchModel.MapRegion = new AppPresentators.VModels.Map.MapRegion
            {
                E = arg2.E,
                N = arg2.N,
                W = arg2.W,
                S = arg2.S
            };

            SearchModel.DateFrom = dtpFrom.Value;

            SearchModel.DateTo = dtpTo.Value;

            btnClearSelected.Enabled = true;

            if (FilterViolations != null) FilterViolations(SearchModel);
        }

        private List<AdminViolation> _violations;

        public List<AdminViolation> DataSource
        {
            get
            {
                return _violations;
            }

            set
            {
                if (_map != null) _map.ClearPoints();

                _violations = value;

                if (value != null && _map != null)
                {
                    foreach (var v in _violations) SetPoint(v);
                }

                if(_map != null) _map.Redraw();
            }
        }

        public bool ShowForResult { get; set; }

        public List<Control> TopControls { get
            {
                var _btnReport = new PictureButton(Properties.Resources.reportEnabled, Properties.Resources.reportDisabled, Properties.Resources.reportPress);
                _btnReport.Click += ReportClick;
                _btnReport.Enabled = true;
                return new List<Control> { _btnReport };
            }
            set { }
        }

        private void ReportClick(object sender, EventArgs e)
        {
            ///TODO:Сделать отчет
        }

        public event Action<MapSearchModel> FilterViolations;

        public event Action<int> OpenViolation;


        public void ClearPoints()
        {
            if (_map == null) return;

            _map.ClearPoints();
            _map.Redraw();
        }


        public Control GetControl()
        {
            return this;
        }

        public void SetPoint(AdminViolation violation)
        {
            if (_map == null) return;

            DPoint point = new DPoint
            {
                E = violation.ViolationE,
                N = violation.ViolationN,
                DataSource = violation,
                ToolTip = violation.Violation
            };

            _map.SetPoint(point);
        }

        public void SetPoint(MapPoint point)
        {
            if (_map == null) return;

            _map.SetPoint(new DPoint
            {
                E = point.E,
                N = point.N,
                ToolTip = point.ToolTip
            });
        }

        void UIComponent.Resize(int width, int height)
        {

        }

        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            if (_map == null) return;

            SearchModel.MapRegion = new AppPresentators.VModels.Map.MapRegion
            {
                E = 135,
                N = 45,
                W = 129,
                S = 39
            };

            btnClearSelected.Enabled = false;

            if (FilterViolations != null) FilterViolations(SearchModel);
        }
    }
}
