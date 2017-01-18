using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using AppData.CustomAttributes;
using System.ComponentModel;
using AppPresentators.VModels;
using System.Collections;
using System.Drawing;
using LeoBase.Components.CustomControls.Interfaces;
using LeoBase.Components.CustomControls.NewControls;
using System.IO;

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class AutoBinderPanel:Panel, IClearable
    {
        private object _dataSource = null;

        private TableLayoutPanel _container;

        private Dictionary<string, PropertyControlModel> _controls;

        public event EventHandler DataSourceChanged;

        public object DataSource { get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                BuildModel();
            }
        }

        private void BuildModel()
        {
            _controls = new Dictionary<string, PropertyControlModel>();

            int rowCount = 2;

            foreach(var prop in _dataSource.GetType().GetProperties())
            {
                bool showInHead = true;

                var propertyModel = new PropertyControlModel();
                
                foreach (var attribute in prop.GetCustomAttributesData())
                {
                    try { 
                    switch (attribute.AttributeType.Name)
                    {
                        case "BrowsableAttribute":
                            if(PanelType != AutoBinderPanelType.DETAILS && PanelType != AutoBinderPanelType.EDIT)
                                showInHead = prop.GetCustomAttribute<BrowsableAttribute>().Browsable;
                            break;
                        case "BrowsableForEditAndDetailsAttribute":
                            propertyModel.ShowForDetails = prop.GetCustomAttribute<BrowsableForEditAndDetailsAttribute>().BrowsableForDetails;
                            propertyModel.ShowForEdit = prop.GetCustomAttribute<BrowsableForEditAndDetailsAttribute>().BrowsableForEdit;
                            break;
                        case "DisplayNameAttribute":
                            propertyModel.Title = prop.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                            break;
                        case "ControlTypeAttribute":
                            var attributeControlType = prop.GetCustomAttribute<ControlTypeAttribute>();
                            propertyModel.ControlType = attributeControlType.ControlType;
                            if(propertyModel.ControlType == ControlType.ComboBox)
                            {
                                propertyModel.DisplayMember = attributeControlType.DisplayMember;
                                propertyModel.ValueMember = attributeControlType.ValueMember;

                                var attributeData = prop.GetCustomAttribute<DataPropertiesNameAttribute>();

                                if(attributeData != null)
                                {
                                    var type = _dataSource.GetType();

                                    var data = (IList)type.GetProperty(attributeData.PropertyDataName).GetValue(_dataSource);

                                    propertyModel.ComboBoxData = data;
                                }
                            }
                            break;
                        case "ChildrenPropertyAttribute":
                            propertyModel.CompareProperty = prop.GetCustomAttribute<ChildrenPropertyAttribute>().ChildrenPropertyName;
                            break;
                        case "DinamicBrowsableAttribute":
                            string propertyName = prop.GetCustomAttribute<DinamicBrowsableAttribute>().PropertyName;

                            bool findedValue = prop.GetCustomAttribute<DinamicBrowsableAttribute>().PropertyValue;

                            bool value = (bool)_dataSource.GetType().GetProperty(propertyName).GetValue(_dataSource);

                            showInHead = value == findedValue;
                            break;
                        case "PropertyNameSelectedTextAttribute":
                            string selectedTextPropertyName = prop.GetCustomAttribute<PropertyNameSelectedTextAttribute>().PropertyName;
                            propertyModel.SelectedText = (string)_dataSource.GetType().GetProperty(selectedTextPropertyName).GetValue(_dataSource);
                            break;
                        case "GenerateEmptyModelPropertyNameAttribute":
                            string funcGenerListItem = prop.GetCustomAttribute<GenerateEmptyModelPropertyNameAttribute>().PropertyName;
                            propertyModel.GetListObject = (Func<object>)_dataSource.GetType().GetProperty(funcGenerListItem).GetValue(_dataSource);
                            break;
                    }
                    }catch(Exception e)
                    {
                        int a = 10;
                    }


                }
                switch (PanelType)
                {
                    case AutoBinderPanelType.SEARCH:
                        if (!showInHead) continue;
                        break;
                    case AutoBinderPanelType.DETAILS:
                        if (!propertyModel.ShowForDetails) continue;
                        break;
                    case AutoBinderPanelType.EDIT:
                        if (!propertyModel.ShowForEdit) continue;
                        break;
                }

                rowCount++;
                try { 
                propertyModel.Value = prop.GetValue(_dataSource);
                }catch(Exception e)
                {
                    int a = 10;
                }
                Control control = null;

                if (propertyModel.ControlType == ControlType.Text)
                {
                    control = GetTextBox(propertyModel);

                    if(propertyModel.Value != null && propertyModel.Value != "0")
                        control.Text = propertyModel.Value.ToString();

                    ((LeoTextBox)control).TextChanged += (s, e) =>
                    {
                        _dataSource.GetType().GetProperty(prop.Name).SetValue(_dataSource, control.Text);
                        if(DataSourceChanged != null)
                        {
                            DataSourceChanged(control, new EventArgs());
                        }
                    };
                }
                else if (propertyModel.ControlType == ControlType.Int)
                {
                    control = GetTextBox(propertyModel);

                    if(propertyModel.Value != null)
                        control.Text = propertyModel.Value.ToString();

                    ((LeoTextBox)control).TextChanged += (s, e) =>
                    {
                        try
                        {
                            _dataSource.GetType().GetProperty(prop.Name).SetValue(_dataSource, Convert.ToInt32(control.Text));
                            if (DataSourceChanged != null) DataSourceChanged(control, new EventArgs());
                        }
                        catch { }
                    };
                    control.KeyPress += (s, e) =>
                    {
                        if (!Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                    };
                }
                else if (propertyModel.ControlType == ControlType.Real)
                {
                    control = GetTextBox(propertyModel);// new LeoTextBox();

                    if(propertyModel.Value != null && (decimal)propertyModel.Value != 0)
                        control.Text = propertyModel.Value.ToString();

                    ((LeoTextBox)control).TextChanged += (s, e) =>
                    {
                        try
                        {
                            decimal val = Convert.ToDecimal(control.Text.Replace('.', ','));
                            _dataSource.GetType().GetProperty(prop.Name).SetValue(_dataSource, val);
                            if (DataSourceChanged != null) DataSourceChanged(control, new EventArgs());
                        }
                        catch { }
                    };

                    control.KeyPress += (s, e) =>
                    {
                        if (e.KeyChar == 46 && (((TextBox)s).Text.Contains(".")))
                        {
                            e.Handled = true;
                        }
                        else if (!Char.IsDigit(e.KeyChar) && !e.KeyChar.ToString().Equals("."))
                        {
                            e.Handled = true;
                        }
                    };
                }
                else if (propertyModel.ControlType == ControlType.DateTime)
                {
                    #region Set DatePiker

                    control = new DateTimePicker();
                    
                    ((DateTimePicker)control).Value = propertyModel.Value != null && ((DateTime)propertyModel.Value).Year != 1 ? (DateTime)propertyModel.Value : DateTime.Now;

                    
                    ((DateTimePicker)control).ValueChanged += (s, e) =>
                    {
                        _dataSource.GetType().GetProperty(prop.Name).SetValue(_dataSource, ((DateTimePicker)control).Value);

                        if (DataSourceChanged != null) DataSourceChanged(control, new EventArgs());
                    };
                    #endregion
                }
                else if (propertyModel.ControlType == ControlType.ComboBox)
                {
                    control = GetComboBox(propertyModel); 

                    ((ComboBox)control).SelectedIndexChanged += (s, e) =>
                    {
                       _dataSource.GetType().GetProperty(prop.Name).SetValue(_dataSource, ((ComboBox)control).SelectedValue);

                        if (DataSourceChanged != null) DataSourceChanged(control, new EventArgs());
                    };

                    //propertyModel.Control = control;
                }else if(propertyModel.ControlType == ControlType.Image )
                {
                    PictureViewer viewer = new PictureViewer();

                    FlowLayoutPanel flow = new FlowLayoutPanel();

                    Button openFileDialogButton = new Button();

                    openFileDialogButton.Text = "Выбрать файл";
                    
                    if(propertyModel.Value != null && ((byte[])propertyModel.Value).Length != 0)
                        try { 
                            viewer.Image = (byte[])propertyModel.Value;
                        }
                        catch(Exception e) {
                            int a = 10;
                        }

                    viewer.Dock = DockStyle.Top;

                    openFileDialogButton.Dock = DockStyle.Bottom;

                    flow.Width = 180;

                    flow.Controls.Add(viewer);

                    flow.Controls.Add(openFileDialogButton);

                    flow.Height = viewer.Height + openFileDialogButton.Height + 6;

                    control = flow;

                    openFileDialogButton.Click += (s, e) =>
                    {
                        OpenFileDialog fileDialog = new OpenFileDialog();

                        fileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG)|*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG";

                        fileDialog.Multiselect = false;

                        if(fileDialog.ShowDialog() == DialogResult.OK)
                        {
                            FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read);

                            byte[] data = new byte[fs.Length];

                            fs.Read(data, 0, System.Convert.ToInt32(fs.Length));

                            fs.Close();
                            
                            viewer.Image = data;

                            _dataSource.GetType().GetProperty(prop.Name).SetValue(_dataSource, data);
                        }
                    };

                    viewer.OnDeleteClick += (pv) =>
                    {
                        if (pv.Image == null || pv.Image.Length == 0) return;

                        if(MessageBox.Show("Вы уверены, что хотите удалить изображение?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            viewer.Image = null;
                            _dataSource.GetType().GetProperty(prop.Name).SetValue(_dataSource, new byte[0]);
                        }
                    };
                }
                else if(propertyModel.ControlType == ControlType.List)
                {
                    TableLayoutPanel panel = new TableLayoutPanel();

                    var value = (IList)propertyModel.Value;

                    panel.ColumnCount = 1;
                    //int childrenIndexRow = -1;

                    if (PanelType == AutoBinderPanelType.EDIT)
                    {
                        Button btn = new Button();

                        Panel panelButton = new Panel();
                        
                        panelButton.Dock = DockStyle.Top;

                        panelButton.AutoSize = true;

                        btn.Text = "Добавить";

                        panelButton.Controls.Add(btn);

                        panel.Controls.Add(panelButton);
                        
                        btn.Click += (s, e) =>
                        {
                            if (propertyModel.GetListObject != null)
                            {
                                var item = propertyModel.GetListObject();

                                value.Add(item);

                                var autoBinderPanel = new AutoBinderPanel(PanelType);

                                autoBinderPanel.DataSource = item;

                                autoBinderPanel.Dock = DockStyle.Left;

                                autoBinderPanel.Width = 400;

                                Button deleteBtn = new Button();

                                deleteBtn.Text = "Удалить";

                                deleteBtn.Click += (s2, e2) =>
                                {
                                    if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Предупреждение", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                    {
                                        if (_container != null)
                                        {
                                            panel.Controls.Remove(deleteBtn);
                                            panel.Controls.Remove(autoBinderPanel);
                                        }
                                        value.Remove(item);
                                    }
                                };

                                panel.Controls.Add(deleteBtn);

                                panel.Controls.Add(autoBinderPanel);

                            }
                        };
                    }

                    panel.Dock = DockStyle.Top;

                    panel.AutoSize = true;

                    if(value != null)
                        foreach(var item in value)
                        {
                            var autoBinderPanel = new AutoBinderPanel(PanelType);

                            autoBinderPanel.DataSource = item;

                            autoBinderPanel.Dock = DockStyle.Left;

                            autoBinderPanel.Width = 400;

                            Button deleteBtn = new Button();

                            deleteBtn.Text = "Удалить";

                            deleteBtn.Click += (s, e) =>
                            {
                                if(MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Предупреждение", MessageBoxButtons.OKCancel) == DialogResult.OK) { 
                                    if(_container != null)
                                    {
                                        panel.Controls.Remove(deleteBtn);
                                        panel.Controls.Remove(autoBinderPanel);
                                    }
                                    value.Remove(item);
                                }
                            };

                            panel.Controls.Add(deleteBtn);

                            panel.Controls.Add(autoBinderPanel);
                        }
                    
                    control = panel;
                }

                #region CompareRegion
                if(propertyModel.CompareProperty != null)
                {
                    ComboBox compareCombo = new ComboBox();

                    compareCombo.DropDownStyle = ComboBoxStyle.DropDownList;

                    List<OrderComboBoxModel> data = new List<OrderComboBoxModel>
                    {
                        new OrderComboBoxModel
                        {
                            CompareType = CompareValue.NONE,
                            Value = "Не учитывать"
                        },
                        new OrderComboBoxModel
                        {
                            CompareType = CompareValue.MORE,
                            Value = "Больше"
                        },
                        new OrderComboBoxModel
                        {
                            CompareType = CompareValue.LESS,
                            Value = "Меньше"
                        },
                        new OrderComboBoxModel
                        {
                            CompareType = CompareValue.EQUAL,
                            Value = "Равно"
                        }

                    };

                    compareCombo.DisplayMember = "Value";

                    compareCombo.ValueMember = "CompareType";

                    compareCombo.DataSource = data;

                    if (propertyModel.CompareProperty != null)
                    {
                        compareCombo.SelectedIndexChanged += (s, e) =>
                        {
                            _dataSource.GetType().GetProperty(propertyModel.CompareProperty).SetValue(_dataSource, compareCombo.SelectedValue);

                            if (DataSourceChanged != null) DataSourceChanged(s, e);
                        };
                    }

                    TableLayoutPanel layoutPanel = new TableLayoutPanel();

                    layoutPanel.RowCount = 1;

                    layoutPanel.ColumnCount = 2;

                    for (int i = 0; i < layoutPanel.ColumnCount; i++)
                    {
                        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    }

                    layoutPanel.Controls.Add(control, 0, 0);

                    layoutPanel.Controls.Add(compareCombo, 1, 0);

                    propertyModel.Control = layoutPanel;
                }
                else
                {
                    propertyModel.Control = control;
                }
                #endregion

                _controls.Add(prop.Name, propertyModel);
            }

            this.Controls.Clear();

            
            switch (PanelType)
            {
                case AutoBinderPanelType.SEARCH:
                    SearchDesineRenderControls();
                    break;
                case AutoBinderPanelType.EDIT:
                    EditDesineRenderControls();
                    break;
                case AutoBinderPanelType.DETAILS:
                    DetailsDesineRenderControls();
                    break;
            }

            _container.AutoScroll = true;

            _container.Padding = new Padding(0, 0, 20, 0);
        }
        
        private ComboBox GetComboBox(PropertyControlModel propertyModel)
        {
            ComboBox result = null;
            switch (PanelType)
            {
                case AutoBinderPanelType.SEARCH:
                    result = GetComboBoxForSearch(propertyModel);
                    break;
                case AutoBinderPanelType.EDIT:
                    result = GetComboBoxForEdit(propertyModel);
                    break;
                case AutoBinderPanelType.DETAILS:
                    result = GetComboBoxForDetails(propertyModel);
                    break;
            }
            return result;
        }

        private LeoTextBox GetTextBox(PropertyControlModel propertyModel)
        {
            LeoTextBox textBox = null;
            switch (PanelType)
            {
                case AutoBinderPanelType.SEARCH:
                    textBox = GetTextBoxForSearch(propertyModel);
                    break;
                case AutoBinderPanelType.EDIT:
                    textBox = GetTextBoxForEdit(propertyModel);
                    break;
                case AutoBinderPanelType.DETAILS:
                    textBox = GetTextBoxForDetails(propertyModel);
                    break;
            }
            return textBox;
        }

        public AutoBinderPanelType PanelType { get; private set; }
        
        private Label _title;
        private PictureBox _titleImage;

        public string Title { get
            {
                if (_title == null) _title = new Label();
                return _title.Text;
            }
            set
            {
                if (_title == null) _title = new Label();
            }
        }
        public Image TitleImage { get; set; }
        public AutoBinderPanel(AutoBinderPanelType panelType = AutoBinderPanelType.SEARCH, string title = null, Image titleImage = null)
        {
            PanelType = panelType;
        }

        public void UpdatePropertyValue(string propertyName, object data)
        {
            if (!_controls.ContainsKey(propertyName)) return;

            var control = _controls[propertyName];

            switch (control.ControlType)
            {
                case ControlType.Text:
                case ControlType.Real:
                case ControlType.Int:
                    ((LeoTextBox)control.Control).Text = data.ToString();
                    break;
                case ControlType.DateTime:
                    ((DateTimePicker)control.Control).Value = (DateTime)data;
                    break;
                case ControlType.ComboBox:
                    ((ComboBox)control.Control).SelectedIndex = ((ComboBox)control.Control).FindString(data.ToString());// data;
                    break;
            }
        }

        public void Clear()
        {
            foreach(var control in _controls)
            {
                Control ctr = control.Value.Control;

                if (ctr.GetType().Name.Equals("TableLayoutPanel"))
                {
                    foreach(var c in ctr.Controls)
                    {
                        if (c.GetType().Name.Equals("LeoTextBox")) ((LeoTextBox)c).Text = "";
                        else if (c.GetType().Name.Equals("DateTimePicker")) ((DateTimePicker)c).Value = DateTime.Now;
                        else if (c.GetType().Name.Equals("ComboBox")) ((ComboBox)c).SelectedIndex = 0;
                    }
                }else { 
                    switch (control.Value.ControlType)
                    {
                        case ControlType.ComboBox:
                            ((ComboBox)(control.Value.Control)).SelectedIndex = 0;
                            break;
                        case ControlType.Real:
                        case ControlType.Int:
                        case ControlType.Text:
                            ((LeoTextBox)control.Value.Control).Text = "";
                            break;
                        case ControlType.DateTime:
                            ((DateTimePicker)control.Value.Control).Value = DateTime.Now;
                            break;
                    }
                }
            }
        }

        public Control GetControl()
        {
            return this;
        }
    }

    public enum AutoBinderPanelType
    {
        SEARCH,
        EDIT,
        DETAILS
    }

    public class OrderComboBoxModel
    {
        public CompareValue CompareType { get; set; }
        public string Value { get; set; }
    }

    public class PropertyControlModel
    {
        public string Title { get; set; }
        public ControlType ControlType { get; set; } = ControlType.Text;
        public Control Control { get; set; }
        public Control ChildrenControl { get; set; }
        public ControlType ChildrenControlType { get; set; } = ControlType.Text;
        public string CompareProperty { get; set; }
        public IList ComboBoxData { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public object Value { get; set; }
        public bool ShowForEdit { get; set; }
        public bool ShowForDetails { get; set; }
        public string SelectedText { get; set; }

        public Func<object> GetListObject { get; set; }
    }
}
