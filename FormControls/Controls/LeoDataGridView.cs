using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormControls.Controls.Models;
using System.Reflection;
using FormControls.Controls.Attributes;
using FormControls.Controls.Enums;
using FormControls.Controls.Events;

namespace FormControls.Controls
{
    public partial class LeoDataGridView : UserControl
    {
        private TableLayoutPanel _dataPanel;
        private TableLayoutPanel _headPanel;

        private bool _resizeStart = false;
        private HeadTitle _resizedColumn;
        private int _oldXPositionForResize;
        private TransparentPanel _resizedPanel;
        private PictureBox _resizeSeparator;
        #region Events
        public event TableOrderEvent OnTableOrder;

        #endregion

        public int ColumnCount { get; private set; } = 0;
        public int RowCount { get; private set; } = 0;

        public PageModel PageModel { get; set; }

        public object Data { get; private set; }

        private List<HeadTitle> _headers;

        private List<LeoDataViewRow> _rows;

        public void SetData<T>(List<T> data)
        {
            Data = data;

            _dataPanel.Controls.Clear();

            _headPanel.Controls.Clear();

            MakeHeaders(typeof(T));

            _headPanel.SendToBack();

            _dataPanel.ColumnCount = ColumnCount;

            if (data == null) return;

            _dataPanel.RowCount = data.Count;

            var metaList = _headers.Select(h => h.MetaData).ToList();

            _rows = new List<LeoDataViewRow>();

            int index = 0;

            foreach (var item in data)
            {
                var row = new LeoDataViewRow(item, metaList, index);

                _rows.Add(row);
                
                foreach(var control in row.Cells)
                {
                    _dataPanel.Controls.Add(control);
                }

                index++;
            }

            UpdateTableSize();
        }

        public LeoDataGridView()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);

            InitializeComponent();

            _dataPanel = new TableLayoutPanel();

            _headPanel = new TableLayoutPanel();

            _headPanel.Dock = DockStyle.Fill;

            _dataPanel.Dock = DockStyle.Top;

            _dataPanel.AutoSize = true;

            headersPanel.Controls.Add(_headPanel);

            dataPanel.Controls.Add(_dataPanel);

            this.MouseMove += TableMouseMove;

            _resizedPanel = new TransparentPanel();

            _resizeSeparator = new PictureBox();

            _resizeSeparator.Height = _resizedPanel.Height;

            _resizeSeparator.Width = 20;

            _resizeSeparator.BackColor = Color.Black;

            _resizedPanel.Dock = DockStyle.Fill;

            _resizedPanel.BackColor = Color.Transparent;
            
            headersPanel.Controls.Add(_resizedPanel);

            _resizedPanel.Controls.Add(_resizeSeparator);
           
            _resizedPanel.Visible = false;

            this.Load += LeoDataGridView_Load;

            this.Resize += TableResize;
        }

        private void TableResize(object sender, EventArgs e)
        {
            if (_dataPanel == null || _headPanel == null) return;
            //UpdateTableSize();
        }

        private void UpdateTableSize()
        {
            _headPanel.ColumnStyles.Clear();
            _dataPanel.ColumnStyles.Clear();

            for (int i = 0; i < ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Absolute, this.Width / ColumnCount);

                _headPanel.ColumnStyles.Add(cs);

                if (_dataPanel.Height > dataPanel.Height && i == ColumnCount - 1)
                    cs = new ColumnStyle(SizeType.Absolute, (this.Width / ColumnCount) - 100);
                else
                    cs = new ColumnStyle(SizeType.Absolute, this.Width / ColumnCount);

                _dataPanel.ColumnStyles.Add(cs);
            }
        }

        private void LeoDataGridView_Load(object sender, EventArgs e)
        {
            this.Refresh();
            _resizeSeparator.Left = 100;
        }

        private void TableMouseMove(object sender, MouseEventArgs e)
        {
            if (_resizeStart)
            {
                MessageBox.Show("It`s work!");
                _resizeStart = false;
            }
        }

        private void MakeHeaders(Type t)
        {
            _headers = new List<HeadTitle>();

            foreach (var propertie in t.GetProperties())
            {
                LeoDataViewHeadMetaData metadata = new LeoDataViewHeadMetaData();

                metadata.Show = true;

                metadata.CanOrder = true;

                metadata.Type = propertie.PropertyType.Name;

                metadata.FieldName = propertie.Name;

                foreach(var attr in propertie.GetCustomAttributesData())
                {
                    if (attr.AttributeType.Name.Equals("BrowsableAttribute"))
                    {
                        metadata.Show = propertie.GetCustomAttribute<BrowsableAttribute>().Browsable;
                    }else if (attr.AttributeType.Name.Equals("OrderAttribute"))
                    {
                        metadata.CanOrder = propertie.GetCustomAttribute<OrderAttribute>().Order;
                    }else if (attr.AttributeType.Name.Equals("DisplayNameAttribute"))
                    {
                        metadata.DisplayName = propertie.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                    }
                }

                ColumnCount += metadata.Show ? 1 : 0;

                var header = new HeadTitle(metadata);

                header.OnTableOrder += TableOrder;

                _headers.Add(header);
            }
            
            _headPanel.ColumnCount = ColumnCount;

            _headPanel.RowCount = 1;

            int index = 0;

            foreach (var head in _headers)
            {
                if (head.MetaData.Show)
                {
                    ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100 / ColumnCount);

                    head.MetaData.ColumnIndex = index;

                    _headPanel.Controls.Add(head);

                    _headPanel.ColumnStyles.Add(cs);

                    head.OnResizeStart += CustomResizeColumns;

                    head.OnHeaderMouseMove += HeadMouseMove;

                    index++;
                }
            }

            var firstHeader = _headers.FirstOrDefault(h => h.MetaData.Show);

            if (firstHeader != null) firstHeader.IsFirst = true;

            var lastHeader = _headers.FindLast(h => h.MetaData.Show);

            if (lastHeader != null) lastHeader.IsLast = true;
        }

        private void HeadMouseMove(HeadTitle sender, MouseEventArgs e)
        {
            _resizeStart = false;
            return;

            if (_resizedColumn == null)
            {
                _resizeStart = false;
            }

            if (_resizeStart)
            {
                _resizedPanel.Visible = true;

                int newPosition = Cursor.Position.X;

                _resizeSeparator.Left = newPosition;
                return;
                int delta = _oldXPositionForResize - newPosition;
                if (_headPanel.ColumnStyles[_resizedColumn.MetaData.ColumnIndex].Width + delta < 50) return;
                _headPanel.ColumnStyles[_resizedColumn.MetaData.ColumnIndex].Width += delta;

                _oldXPositionForResize = newPosition;
            }else
            {
                _oldXPositionForResize = Cursor.Position.X;
            }
        }

        private void CustomResizeColumns(HeadTitle sender, bool resized)
        {
            _resizeStart = resized;
            _resizedColumn = sender;
        }

        private void TableOrder(HeadTitle sender, OrderTypes type)
        {
            foreach(var header in _headers)
            {
                if (header != sender && header.MetaData.CanOrder) header.OrderType = OrderTypes.NONE;
            }

            if (OnTableOrder != null) OnTableOrder(sender, type);
        }
    }

}
