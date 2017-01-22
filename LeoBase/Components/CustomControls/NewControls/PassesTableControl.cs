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
using AppPresentators.VModels;
using LeoBase.Components.TopMenu;
using AppPresentators;
using LeoBase.Components.CustomControls.SearchPanels;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class PassesTableControl : CustomTablePage, IPassesTableControl
    {
        public int SelectedID { get; private set; }

        private int _selectedIndex = -1;

        private CustomTable _customTable;
        private TopControls _tpControls;
        private AutoBinderPanel _searchPanel;

        private Dictionary<string, int> _orderProperties;
        private Dictionary<string, OrderType> _orderTypes;

        public PassesTableControl():base()
        {
            InitializeComponent();

            MakeTopControls();

            _orderProperties = new Dictionary<string, int>();
            _orderTypes = new Dictionary<string, OrderType>();

            _orderProperties.Add("По дате завершения действия пропуска", (int)PassesOrderProperties.BY_DATE_CLOSED);
            _orderProperties.Add("По дате выдачи пропуска", (int)PassesOrderProperties.BY_DATE_GIVED);
            _orderProperties.Add("По фамилии", (int)PassesOrderProperties.BY_FIO);

            Title = "Пропуски";

            _orderTypes.Add("Убывание", OrderType.ASC);
            _orderTypes.Add("Возростание", OrderType.DESC);

            _customTable = new CustomTable();

            _customTable.OrderProperties = (Dictionary<string, int>)_orderProperties;

            _customTable.SelectedItemChange += _customTable_SelectedItemChange;

            //_customTable.OnRowWasCreted += RowWasCreated;

            _customTable.UpdateTable += () => UpdateTable();

            _customTable.SelectedItemChange += (index) =>
            {

                if (index != -1)
                {
                    _tpControls.EnabledDelete = true;

                    _tpControls.EnabledDetails = true;

                    _tpControls.EnabledEdit = true;

                }
                else
                {
                    _tpControls.EnabledDelete = false;

                    _tpControls.EnabledDetails = false;

                    _tpControls.EnabledEdit = false;
                }

                _selectedIndex = index;
            };

            _customTable.DoubleClick += () =>
            {
                if (!ShowForResult && ShowDetailsPass != null)
                {
                    if(_selectedIndex > 0 && _selectedIndex < _passes.Count) ShowDetailsPass(_passes[_selectedIndex].PassID);
                }
            };

            this.AutoSize = true;

            _searchPanel = new AutoBinderPanel();

            _searchPanel.DataSource = new PassesSearchModel();

            var searchPanel = new SearchPanelContainer();

            searchPanel.SetCustomSearchPanel(_searchPanel);

            AddSearchPanel(searchPanel);

            _searchPanel.DataSourceChanged += (s, e) =>
            {
                if (UpdateTable != null) UpdateTable();
            };

            searchPanel.OnSearchClick += () =>
            {
                if (UpdateTable != null) UpdateTable();
            };

            SetContent(_customTable);
        }

        private void _customTable_SelectedItemChange(int index)
        {
            if (index > 0 && index <= _passes.Count) _selectedIndex = index;
        }

        private List<Pass> _passes;

        public List<Pass> DataSource
        {
            get
            {
                return _passes;
            }

            set
            {
                _passes = value;

                if(_passes == null)
                {
                    _customTable.SetData<PassTableModel>(new List<PassTableModel>());
                    return;
                }

                _customTable.SetData<PassTableModel>(value.Select(p => new PassTableModel
                {
                    FIO = p.FirstName + " " + p.SecondName + " " + p.MiddleName,
                    Document = p.DocumentType,
                    Number = p.Number,
                    PassClosen = p.PassClosed.ToShortDateString(),
                    PassGiven = p.PassGiven.ToShortDateString(),
                    Serial = p.Serial,
                    WhenIssued = p.WhenIssued.ToShortDateString(),
                    WhoIssued = p.WhoIssued
                }).ToList());

                if (_tpControls != null)
                {
                    if (value == null || value.Count == 0)
                    {
                        _tpControls.EnabledDelete = false;

                        _tpControls.EnabledDetails = false;

                        _tpControls.EnabledEdit = false;

                        SelectedID = 0;
                    }
                }
            }
        }

        private void MakeTopControls()
        {
            _tpControls = new TopControls(ConfigApp.CurrentManager.Role, true);

            _tpControls.DetailsItem += () =>
            {
                if (_selectedIndex >= 0 && _selectedIndex < _passes.Count && ShowDetailsPass != null) ShowDetailsPass(_passes[_selectedIndex].PassID);
            };

            _tpControls.DeleteItem += () =>
            {
                if (_selectedIndex >= 0 && _selectedIndex < _passes.Count && RemovePass != null) RemovePass(_passes[_selectedIndex].PassID);
            };

            _tpControls.EditItem += () =>
            {
                if (_selectedIndex >= 0 && _selectedIndex < _passes.Count && EditPass != null) EditPass(_passes[_selectedIndex].PassID);
            };

            _tpControls.AddItem += () =>
            {
                if (AddPass != null) AddPass();
            };

            _tpControls.ReportItem += () =>
            {
                if (MakeReport != null) MakeReport(_passes);
            };

            _tpControls.EnabledDelete = false;

            _tpControls.EnabledDetails = false;

            _tpControls.EnabledEdit = false;

            _tpControls.EnabledAdd = true;

            _tpControls.EnabledReport = true;
        }

        public PassesOrderModel OrderModel
        {
            get
            {
                var orderModel = new PassesOrderModel
                {
                    OrderProperties = (PassesOrderProperties)_customTable.SelectedOrderBy,
                    OrderType = _customTable.OrderType
                };
                return orderModel;
            }
            set { }
        }

        private PageModel _pageModel;

        public PageModel PageModel
        {
            get
            {
                if (_pageModel == null) _pageModel = new PageModel();
                _pageModel.ItemsOnPage = 10;
                _pageModel = _customTable.PageModel;
                return _pageModel;
            }
            set
            {
                _pageModel = value;
                _customTable.PageModel = value;
            }
        }

        public PassesSearchModel SearchModel
        {
            get
            {
                return (PassesSearchModel)_searchPanel.DataSource;
            }

            set
            {

            }
        }

        public bool ShowForResult { get; set; }
        public List<Control> TopControls
        {
            get
            {
                return _tpControls;
            }

            set { }
        }

        public event Action AddPass;
        public event Action<int> EditPass;
        public event Action<object> MakeReport;
        public event Action<int> RemovePass;
        public event Action<int> ShowDetailsPass;
        public event Action UpdateTable;

        public Control GetControl()
        {
            return this;
        }

        public bool ShowDialog(string text)
        {
            return MessageBox.Show(text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }

        public void ShowError(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowMessage(string text, string title)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void UIComponent.Resize(int width, int height)
        {
        }
        public void LoadStart()
        {
            _customTable.Visible = false;
            VisibleLoadPanel = true;
        }

        public void LoadEnd()
        {
            try
            {
                VisibleLoadPanel = false;
                _customTable.Visible = true;
            }
            catch (Exception e)
            {

            }
        }
    }

    public class PassTableModel
    {
        [DisplayName("ФИО")]
        [ReadOnly(true)]
        public string FIO { get; set; }
        [ReadOnly(true)]
        [DisplayName("Пропуск выдан")]
        public string PassGiven { get; set; }
        [ReadOnly(true)]
        [DisplayName("Пропуск заканчивается")]
        public string PassClosen { get; set; }
        [ReadOnly(true)]
        [DisplayName("Предоставлен документ")]
        public string Document { get; set; }
        [ReadOnly(true)]
        [DisplayName("Серия")]
        public string Serial { get; set; }
        [ReadOnly(true)]
        [DisplayName("Номер")]
        public string Number { get; set; }
        [ReadOnly(true)]
        [DisplayName("Кем выдан")]
        public string WhoIssued { get; set; }
        [ReadOnly(true)]
        [DisplayName("Когда выдан")]
        public string WhenIssued { get; set; }
    }
}
