using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using AppPresentators.VModels;

namespace LeoBase.Components.CustomControls
{
    
    public partial class CustomDataTable: UserControl
    {
        private TableLayoutPanel _tableLayoutPanel;

        public event OnSelecteItemChanged OnSeleceItemChanged;

        private object _dataContext;

        public event HeadItemClick Order;
        public event GenerateRow OnRowGenerated;

        public void SetData<T>(List<T> data)
        {
            if (data == null) throw new ArgumentNullException();

            MakeTable<T>(data.Count);

            _dataContext = data;

            foreach(var item in data)
            {
                var row = new CustomDataTableRow(item, _headTitles);
                // Добавление контролов

                row.MouseMove += Row_MouseMove;
                row.MouseLeave += Row_MouseLeave;
                row.OnComboChanged += Row_OnComboChanged;
                row.MouseClick += Row_MouseClick;
                row.OnRowGenerated += Row_OnRowGenerated;

                row.MakeControls();

                foreach (var control in row.Controls)
                {
                    _tableLayoutPanel.Controls.Add(control);
                }
            }
        }

        private void Row_OnRowGenerated(CustomDataTableRow item)
        {
            if (OnRowGenerated != null) OnRowGenerated(item);
        }

        private CustomDataTableRow _selectedItem;

        private void Row_MouseClick(CustomDataTableRow sender)
        {
            if(_selectedItem == sender)
            {
                _selectedItem = null;
                sender.BackgroundColor = sender.DefaultBackgroundColor;
                if (OnSeleceItemChanged != null) OnSeleceItemChanged(null);
            }

            if (_selectedItem != null) _selectedItem.BackgroundColor = sender.OnActiveColor;
            
            _selectedItem = sender;

            sender.BackgroundColor = sender.OnActiveColor;

            if (OnSeleceItemChanged != null) OnSeleceItemChanged(sender);
        }

        private void Row_OnComboChanged(CustomDataTableRow parent, ComboBox sender, string columnName)
        {
            MessageBox.Show(string.Format("Значение для поля {0} было изменено на {1}", columnName, sender.SelectedValue));
        }

        private void Row_MouseLeave(CustomDataTableRow sender)
        {
            if(sender != _selectedItem)
                sender.BackgroundColor = sender.DefaultBackgroundColor;
        }

        private void Row_MouseMove(CustomDataTableRow sender)
        {
            if (sender != _selectedItem)
                sender.BackgroundColor = sender.OnMouseMoveColor;
        }

        private List<HeadPanel> _headers;

        private void MakeTable<T>(int rows)
        {
            SetDataType<T>();

            var browsebleHead = _headTitles.Where(h => h.Show);

            int columns = browsebleHead != null ? browsebleHead.Count() : 0;

            //_tableLayoutPanel = new TableLayoutPanel();

            _tableLayoutPanel.Controls.Clear();

            _tableLayoutPanel.ColumnCount = columns;

            _tableLayoutPanel.RowCount = rows + 1;

            _tableLayoutPanel.SuspendLayout();
            _tableLayoutPanel.Controls.Clear();
            _tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            _tableLayoutPanel.ColumnStyles.Clear();

            for (int i = 0; i < _tableLayoutPanel.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100 / _tableLayoutPanel.ColumnCount);
                _tableLayoutPanel.ColumnStyles.Add(cs);
            }

            for (int i = 0; i < _tableLayoutPanel.RowCount; i++)
            {
                RowStyle rs = null;
                if (i == 0)
                    rs = new RowStyle(SizeType.Absolute, 22);
                else
                    rs = new RowStyle(SizeType.Absolute, 30);

                _tableLayoutPanel.RowStyles.Add(rs);
            }

            _tableLayoutPanel.ResumeLayout();

            _headers = new List<HeadPanel>();

            foreach (var title in _headTitles)
            {
                if (title.Show) {
                    HeadPanel head = new HeadPanel(title);

                    //Label headButton = new Label();
                    //headButton.Padding = new Padding(3, 3, 3, 3);
                    //headButton.Margin = new Padding(0, 0, 0, 0);
                    //headButton.BorderStyle = BorderStyle.FixedSingle;
                    //headButton.TextAlign = ContentAlignment.MiddleCenter;
                    //headButton.Text = !string.IsNullOrEmpty(title.DisplayText) ? title.DisplayText : title.HeadName;
                    //headButton.Dock = DockStyle.Fill;
                    //headButton.Text = title.DisplayText;

                    head.Order += Head_Order;
                    _tableLayoutPanel.Controls.Add(head);
                    _headers.Add(head);
                }
            }

        }

        private void Head_Order(TableHeaderItem item, TableOrderType orderType)
        {
            foreach(var i in _headers)
            {
                if(i.HeadItem != item)
                {
                    i.OrderType = TableOrderType.NONE;
                }
            }

            if (Order != null) Order(item, orderType);
        }

        private List<TableHeaderItem> _headTitles;

        public void SetDataType(Type type) {
            _headTitles = new List<TableHeaderItem>();

            foreach (var title in type.GetProperties()) {
                bool showInHead = true;

                TableHeaderItem item = new TableHeaderItem();

                foreach (var attribute in title.GetCustomAttributesData()) {
                    if (attribute.AttributeType.Name.Equals("BrowsableAttribute")) {
                        showInHead = title.GetCustomAttribute<BrowsableAttribute>().Browsable;
                    }
                    else if(attribute.AttributeType.Name.Equals("DisplayNameAttribute"))
                    {
                        item.DisplayText = title.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                    }
                }

                item.HeadName = title.Name;

                item.CellType = title.PropertyType.Name;

                item.Show = showInHead;

                _headTitles.Add(item);
            }
        }

        public void SetDataType<T>()
        {
            SetDataType(typeof(T));
        }

        public CustomDataTable()
        {
            InitializeComponent();
            
            this.Dock = DockStyle.Fill;

            _tableLayoutPanel = new TableLayoutPanel();

            _tableLayoutPanel.Dock = DockStyle.Top;

            _tableLayoutPanel.AutoSize = true;

            Label padgination = new Label();

            padgination.Text = "Test";

            padgination.Dock = DockStyle.Top;

            this.Controls.Add(padgination);

            Controls.Add(_tableLayoutPanel);
        }
    }

    public enum CustomDataTableRowMouseEventType
    {
        MouseLeave,
        MouseMove,
        MouseHover,
        MouseClick,
        MouseDoubleClick
    }

    public delegate void CustomDataTableRowMouseEvent(CustomDataTableRow sender);
    public delegate void CustomDataTableRowComboBoxEvent(CustomDataTableRow parent, ComboBox sender, string columnName);
    public delegate void GenerateRow(CustomDataTableRow item);
    public delegate void OnSelecteItemChanged(CustomDataTableRow item);
    public class CustomDataTableRow
    {
        private List<Control> _controls;
        public object Item { get; set; }
        public List<TableHeaderItem> Metadata { get; private set; }

        public event CustomDataTableRowMouseEvent MouseLeave;
        public event CustomDataTableRowMouseEvent MouseMove;
        public event CustomDataTableRowMouseEvent MouseHover;
        public event CustomDataTableRowMouseEvent MouseClick;
        public event CustomDataTableRowMouseEvent MouseDoubleClick;

        public event CustomDataTableRowComboBoxEvent OnComboChanged;
        public event GenerateRow OnRowGenerated;

        public Color OnMouseMoveColor { get; set; } = Color.DarkGray;

        public Color OnActiveColor { get; set; } = Color.LightGray;

        public Color DefaultBackgroundColor { get; set; } = Color.White;

        public Color _backgroundColor { get; set; } = Color.White;

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                foreach(var control in _controls)
                {
                    control.BackColor = _backgroundColor;
                }
            }
        }

        public List<Control> Controls
        {
            get
            {
                return _controls;
            }
            set
            {
                _controls = value;
            }
        }
        public CustomDataTableRow(object item, List<TableHeaderItem> metaData) {
            Metadata = metaData;
            Item = item;
        }

        public void MakeControls()
        {
            _controls = new List<Control>();

            foreach (var metaData in Metadata)
            {
                if (!metaData.Show) continue;

                var info = Item.GetType().GetProperty(metaData.HeadName);

                if (info == null) continue;

                if (metaData.CellType.ToLower() == "decimal" || metaData.CellType.ToLower() == "string")
                {
                    string title = info.GetValue(Item).ToString();

                    Label label = new Label();

                    label.Text = title;
                    
                    AddEvents(label);

                    _controls.Add(label);
                }
                else if(metaData.CellType.ToLower() == "cellcomboboxvalues")
                {
                    var data = (CellComboBoxValues)info.GetValue(Item);

                    ComboBox combo = new ComboBox();

                    combo.DropDownStyle = ComboBoxStyle.DropDownList;

                    combo.DataSource = data;

                    combo.SelectedText = data.SelectedText;
                    
                    combo.MouseMove += (s, e) =>
                    {
                        if (MouseMove != null) MouseMove(this);
                    };

                    combo.MouseHover += (s, e) =>
                    {
                        if (MouseHover != null) MouseHover(this);
                    };

                    combo.MouseLeave += (s, e) =>
                    {
                        if (MouseLeave != null) MouseLeave(this);
                    };

                    combo.SelectedValueChanged += (s, e) =>
                    {
                        if (OnComboChanged != null) OnComboChanged(this, combo, metaData.HeadName);
                    };

                    _controls.Add(combo);
                }
            }

            foreach (var control in _controls) {
                control.Cursor = Cursors.Hand;
                control.Dock = DockStyle.Fill;
                control.Padding = new Padding(0, 0, 0, 0);
                control.Margin = new Padding(0, 0, 0, 0);
            }

            if (OnRowGenerated != null) OnRowGenerated(this);
        }

        private void AddEvents(Control control)
        {
            control.MouseClick += (s, e) =>
            {
                if (MouseClick != null) MouseClick(this);
            };

            control.MouseMove += (s, e) =>
            {
                if (MouseMove != null) MouseMove(this);
            };

            control.DoubleClick += (s, e) =>
            {
                if (MouseDoubleClick != null) MouseDoubleClick(this);
            };

            control.MouseHover += (s, e) =>
            {
                if (MouseHover != null) MouseHover(this);
            };

            control.MouseLeave += (s, e) =>
            {
                if (MouseLeave != null) MouseLeave(this);
            };
        }
    }

    public delegate void HeadItemClick(TableHeaderItem item, TableOrderType orderType);
    public class HeadPanel:Panel
    {
        public event HeadItemClick Order;

        private TableOrderType _orderType = TableOrderType.NONE;

        public TableOrderType OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                switch (value)
                {
                    case TableOrderType.ASC:
                        _orderImage.Image = Properties.Resources.OrderAsc;
                        break;
                    case TableOrderType.DESC:
                        _orderImage.Image = Properties.Resources.OrderDesc;
                        break;
                    case TableOrderType.NONE:
                        _orderImage.Image = null;
                        break;
                }
                _orderType = value;
                /// Изменяем изображение
            }
        }


        public string Title
        {
            get
            {
                return _title.Text;
            }
            set
            {
                _title.Text = value;
            }
        }

        private Label _title;
        public TableHeaderItem HeadItem;
        private PictureBox _orderImage;
        public HeadPanel(TableHeaderItem item)
        {
            HeadItem = item;

            this.AutoSize = true;

            this.Margin = new Padding(0, 0, 0, 0);

            this.Padding = new Padding(10, 3, 10, 3);

            this.Cursor = Cursors.Hand;

            this.BorderStyle = BorderStyle.FixedSingle;

            _title = new Label();

            _title.Text = item.DisplayText ?? item.HeadName;

            _title.AutoSize = true;

            this.Controls.Add(_title);

            this.Dock = DockStyle.Fill;

            _orderImage = new PictureBox();

            _orderImage.Dock = DockStyle.Right;
            
            _title.Dock = DockStyle.Left;

            this.Controls.Add(_orderImage);

            _title.Click += HeadPanel_Click;

            this.Click += HeadPanel_Click;

            _orderImage.Click += HeadPanel_Click;
            // Добавить изображение сортировки
        }

        private void HeadPanel_Click(object sender, EventArgs e)
        {
            if (OrderType == TableOrderType.NONE) OrderType = TableOrderType.ASC;
            else if (OrderType == TableOrderType.ASC) OrderType = TableOrderType.DESC;
            else if (OrderType == TableOrderType.DESC) OrderType = TableOrderType.ASC;

            if (Order != null) Order(HeadItem, _orderType);
        }
    }

    public enum TableOrderType
    {
        DESC,
        ASC,
        NONE
    }

    public class TableHeaderItem
    {
        public string DisplayText { get; set; }
        public string HeadName { get; set; }
        public string CellType { get; set; }
        public bool Show { get; set; }
    }
}
