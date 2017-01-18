using AppPresentators.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Services;
using AppPresentators.VModels;
using System.Windows.Forms;
using AppPresentators;
using System.Drawing;
using LeoBase.Components.CustomControls.SearchPanels;
using LeoBase.Components.TopMenu;

namespace LeoBase.Components.CustomControls
{
    public class AdminintrationViolationTableControl : CustomTablePage, IAdminViolationControl
    {
        private List<AdminViolationRowModel> _dataContext;
        public int SelectedID { get; private set; }
        private CustomTable _customTable;
        public List<AdminViolationRowModel> DataContext
        {
            get
            {
                return _dataContext;
            }

            set
            {
                _dataContext = value;
                _customTable.SetData<AdminViolationRowModel>(value);

                if (_tpControls != null)
                {
                    if (value == null || value.Count == 0)
                    {
                        _tpControls.EnabledDelete = false;

                        _tpControls.EnabledDetails = false;

                        _tpControls.EnabledEdit = false;
                    }
                }
                //_customTable.AddComboBox(new List<string> { "Оплачен", "Не оплачен" }, "Оплата", "SelectedPaymentText");
            }
        }

        AdminViolationOrderModel _orderModel;

        public AdminViolationOrderModel OrederModel
        {
            get
            {
                _orderModel = new AdminViolationOrderModel
                {
                    OrderBy = (AdminViolationOrderTypes)_customTable.SelectedOrderBy,
                    OrderType = _customTable.OrderType
                };
                return _orderModel;
            }
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

        public AdminViolationSearchModel SearchModel
        {
            get
            {
                return (AdminViolationSearchModel)_searhcPanel.DataSource;
            }
        }

        public bool ShowForResult { get; set; }

        public event Action AddViolation;
        public event ViolationEvent EditViolation;
        public event ViolationEvent RemoveViolation;
        public event ViolationEvent ShowDetailsViolation;
        public event Action UpdateTable;

        private AutoBinderPanel _searhcPanel;
        private Dictionary<string, int> _orderProperties;
        private Dictionary<string, OrderType> _orderTypes;

        private int _selectedIndex = -1;

        public AdminintrationViolationTableControl() : base()
        {
            MakeTopControls();


            _orderProperties = new Dictionary<string, int>();
            _orderTypes = new Dictionary<string, OrderType>();

            _orderProperties.Add("Нарушитель", (int)AdminViolationOrderTypes.VIOLATOR);
            _orderProperties.Add("Инспектор", (int)AdminViolationOrderTypes.EMPLOYER);
            _orderProperties.Add("Дата рассмотрения", (int)AdminViolationOrderTypes.DATE_CONSIDERATION);
            _orderProperties.Add("Нарешение", (int)AdminViolationOrderTypes.VIOLATION);
            _orderProperties.Add("Сумма наложения", (int)AdminViolationOrderTypes.SUM_VIOLATION);
            _orderProperties.Add("Сумма взыскания", (int)AdminViolationOrderTypes.SUM_RECOVERY);

            Title = "Нарушения";

            _orderTypes.Add("Убывание", OrderType.ASC);
            _orderTypes.Add("Возростание", OrderType.DESC);

            _customTable = new CustomTable();

            _customTable.OrderProperties = (Dictionary<string, int>)_orderProperties;

            _customTable.SelectedItemChange += _customTable_SelectedItemChange;

            _customTable.OnRowWasCreted += RowWasCreated;

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
                if (!ShowForResult && ShowDetailsViolation != null) ShowDetailsViolation(SelectedID);
            };

            this.AutoSize = true;

            _searhcPanel = new AutoBinderPanel();

            _searhcPanel.DataSource = new AdminViolationSearchModel();

            var searchPanel = new SearchPanelContainer();
            
            AddSearchPanel(searchPanel);

            searchPanel.SetCustomSearchPanel(_searhcPanel);

            _searhcPanel.DataSourceChanged += (s, e) =>
            {
                if (UpdateTable != null) UpdateTable();
            };

            searchPanel.OnSearchClick += () =>
            {
                if (UpdateTable != null) UpdateTable();
            };

            SetContent(_customTable);
        }

       

        private void RowWasCreated(DataGridViewRow row)
        {
            var v = (AdminViolationRowModel)(row.DataBoundItem);
            if(v.SumRecovery == v.SumViolation)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(223, 240, 216);
            }else if(v.SumRecovery < v.SumViolation && (DateTime.Now - v.Consideration).Days > 70)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(242, 222, 222);
            }else if((DateTime.Now - v.Consideration).Days < 70)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(252, 248, 227);
            }
        }

        private void _customTable_SelectedItemChange(int index)
        {
            if (index != -1)
            {
                _btnUpdate.Enabled = true;
                _btnRemove.Enabled = true;
                _btnDetails.Enabled = true;
            }
            else
            {

                _btnUpdate.Enabled = false;
                _btnRemove.Enabled = false;
                _btnDetails.Enabled = false;
            }

            if (index >= 0 && index < DataContext.Count) SelectedID = DataContext[index].ViolationID;
        }

        public Control GetControl()
        {
            return this;
        }

        public DialogResult ShowConfitmDialog(string message, string title = null)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OKCancel);
        }

        void UIComponent.Resize(int width, int height)
        {

        }

        private Button _btnReport = new Button();

        private Button _btnAddNew = new Button();

        private Button _btnRemove = new Button();

        private Button _btnDetails = new Button();

        private Button _btnUpdate = new Button();


        private TopControls _tpControls;

        private void MakeTopControls()
        {
            _tpControls = new TopControls(ConfigApp.CurrentManager.Role);

            _tpControls.DetailsItem += () =>
            {
                if (_selectedIndex < 0 || _selectedIndex >= _dataContext.Count || _dataContext.Count == 0) return;

                if (ShowDetailsViolation != null) ShowDetailsViolation(SelectedID);
            };

            _tpControls.EditItem += () =>
            {
                if (_selectedIndex < 0 || _selectedIndex >= _dataContext.Count || _dataContext.Count == 0) return;

                if (EditViolation != null) EditViolation(SelectedID);
            };

            _tpControls.DeleteItem += () =>
            {
                if (_selectedIndex < 0 || _selectedIndex >= _dataContext.Count || _dataContext.Count == 0) return;

                if (MessageBox.Show("Вы уверены что хотите удалить правонарушение?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (RemoveViolation != null)
                    {
                        RemoveViolation(SelectedID);

                        if (_dataContext.Count == 0)
                        {
                            _selectedIndex = -1;

                            _tpControls.EnabledDelete = false;

                            _tpControls.EnabledDetails = false;

                            _tpControls.EnabledEdit = false;
                        }
                    }
                }
            };

            _tpControls.AddItem += () => {
                if (AddViolation != null)
                    AddViolation();
            };

            _tpControls.EnabledDelete = false;

            _tpControls.EnabledDetails = false;

            _tpControls.EnabledEdit = false;

            _tpControls.EnabledAdd = true;

            _tpControls.EnabledReport = true;

            _topControls = _tpControls;

            //if (!ConfigApp.CurrentManager.Role.Equals("admin"))
            //{
            //    _btnAddNew.Visible = false;
            //    _btnRemove.Visible = false;
            //    _btnUpdate.Visible = false;
            //}

            //_btnUpdate.Enabled = false;
            //_btnRemove.Enabled = false;
            //_btnDetails.Enabled = false;

            //_btnAddNew.Text = "Добавить";
            //_btnRemove.Text = "Удалить";
            //_btnUpdate.Text = "Редактировать";
            //_btnDetails.Text = "Подробнее";
            //_btnReport.Text = "Отчет";

            //_btnAddNew.Click += (s, e) =>
            //{
            //    if (AddViolation != null) AddViolation();
            //};

            //_btnUpdate.Click += (s, e) =>
            //{
            //    if (EditViolation != null) EditViolation(SelectedID);
            //};

            //_btnRemove.Click += (s, e) =>
            //{
            //    if(MessageBox.Show("Вы уверены что хотите удалить эту запись?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            //    {
            //        if (RemoveViolation != null) RemoveViolation(SelectedID);
            //    }
            //};

            //_btnDetails.Click += (s, e) =>
            //{
            //    if (ShowDetailsViolation != null) ShowDetailsViolation(SelectedID);
            //};

            //_topControls = new List<Control>();

            //_topControls.Add(_btnReport);
            //_topControls.Add(_btnDetails);
            //_topControls.Add(_btnAddNew);
            //_topControls.Add(_btnUpdate);
            //_topControls.Add(_btnRemove);
        }

        private List<Control> _topControls;

        public List<Control> TopControls
        {
            get
            {
                return _topControls;
            }

            set
            {
                _topControls = value;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AdminintrationViolationTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "AdminintrationViolationTableControl";
            this.Size = new System.Drawing.Size(1249, 1024);
            this.VisibleLoadPanel = true;
            this.ResumeLayout(false);

        }

        public void LoadStart()
        {
            _customTable.Visible = false;
            VisibleLoadPanel = true;
        }

        public void LoadEnd()
        {
            try { 
                VisibleLoadPanel = false;
                _customTable.Visible = true;
            }catch(Exception e)
            {

            }
        }
    }
}
