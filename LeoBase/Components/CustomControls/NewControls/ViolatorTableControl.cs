﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Components;
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using AppPresentators;
using LeoBase.Components.CustomControls.SearchPanels;
using LeoBase.Components.CustomControls.Interfaces;
using LeoBase.Components.TopMenu;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class ViolatorTableControl : CustomTablePage, IViolatorTableControl
    {
        private CustomTable _customTable;

        private AutoBinderPanel _documentsSearchPanel;

        private AutoBinderPanel _personeSearchPanel;

        private AutoBinderPanel _addressSerchPanel;


        private int _selectedIndex;
        public ViolatorTableControl():base()
        {
            InitializeComponent();

            MakeTopControls();

            _orderProperties = new Dictionary<string, int>();
            _orderTypes = new Dictionary<string, OrderType>();

            _orderProperties.Add("Фамилия", (int)PersonsOrderProperties.FIRST_NAME);
            _orderProperties.Add("Имя", (int)PersonsOrderProperties.SECOND_NAME);
            _orderProperties.Add("Отчество", (int)PersonsOrderProperties.MIDDLE_NAME);
            _orderProperties.Add("Дата рождения", (int)PersonsOrderProperties.DATE_BERTHDAY);
            _orderProperties.Add("Возвраст", (int)PersonsOrderProperties.AGE);
            _orderProperties.Add("Дата создания", (int)PersonsOrderProperties.WAS_BE_CREATED);
            _orderProperties.Add("Дата последнего обновления", (int)PersonsOrderProperties.WAS_BE_UPDATED);

            Title = "Нарушители";

            _orderTypes.Add("Убывание", OrderType.DESC);
            _orderTypes.Add("Возростание", OrderType.ASC);

            _customTable = new CustomTable();

            _customTable.Location = new Point(0, 30);
            _customTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _customTable.OrderProperties = (Dictionary<string, int>)_orderProperties;

            _customTable.UpdateTable += () => UpdateTable();
            _customTable.SelectedItemChange += (index) =>
            {
                if (index != -1)
                {
                    _selectedIndex = index;


                    _tpControls.EnabledDetails = true;

                    if (ConfigApp.CurrentManager.Role == "user") return;

                    _tpControls.EnabledEdit = true;
                    _tpControls.EnabledDelete = true;
                }
            };

            _customTable.DoubleClick += () =>
            {
                if (ShowForResult && SelectedItemForResult != null)
                {
                    SelectedItemForResult(_data[_selectedIndex]);
                } else if (ShowPersoneDetails != null)
                {
                    ShowPersoneDetails(_data[_selectedIndex]);
                }
            };

            SetContent(_customTable);

            CreateSearchPanel();
        }

        private void CreateSearchPanel()
        {
            var searchPanel = new SearchPanelContainer();

            var layout = new ClearableFlowLayoutPanel();

            _documentsSearchPanel = new AutoBinderPanel();

            _addressSerchPanel = new AutoBinderPanel();

            _personeSearchPanel = new AutoBinderPanel();

            _documentsSearchPanel.DataSource = new DocumentSearchModel();

            _addressSerchPanel.DataSource = new SearchAddressModel();

            _personeSearchPanel.DataSource = new PersonsSearchModel();

            Action dataChanged = () =>
            {
                if (UpdateTable != null) UpdateTable();
            };

            _documentsSearchPanel.DataSourceChanged += (s, e) => dataChanged();

            _addressSerchPanel.DataSourceChanged += (s, e) => dataChanged();

            _personeSearchPanel.DataSourceChanged += (s, e) => dataChanged();

            _documentsSearchPanel.Width = 444;
            _personeSearchPanel.Width = 444;
            _addressSerchPanel.Width = 444;

            layout.AddControl(new List<IClearable> {  _personeSearchPanel, _documentsSearchPanel, _addressSerchPanel });

            layout.Width = _documentsSearchPanel.Width;

            layout.Height = _documentsSearchPanel.Height + _personeSearchPanel.Height + _addressSerchPanel.Height;

            searchPanel.SetCustomSearchPanel(layout);

            AddSearchPanel(searchPanel);

            searchPanel.OnSearchClick += () =>
            {
                if (UpdateTable != null) UpdateTable();
            };
        }

        public SearchAddressModel AddressSearchModel
        {
            get
            {
                return (SearchAddressModel)_addressSerchPanel.DataSource;
            }
        }

        private List<ViolatorViewModel> _data;

        public List<ViolatorViewModel> Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
                _customTable.SetData<ViolatorViewModel>(value);

                if (_tpControls != null)
                {
                    if (_data == null || _data.Count == 0)
                    {
                        _tpControls.EnabledDelete = false;

                        _tpControls.EnabledDetails = false;

                        _tpControls.EnabledEdit = false;
                    }
                }
            }
        }

        public DocumentSearchModel DocumentSearchModel
        {
            get
            {
                return (DocumentSearchModel)_documentsSearchPanel.DataSource;
            }
        }

        private PersonsOrderModel _orderModel;

        private Dictionary<string, int> _orderProperties;
        private Dictionary<string, OrderType> _orderTypes;

        public PersonsOrderModel OrderModel
        {
            get
            {
                _orderModel = new PersonsOrderModel
                {
                    OrderProperties = (PersonsOrderProperties)_customTable.SelectedOrderBy,
                    OrderType = _customTable.OrderType
                };
                return _orderModel;
            }

            set
            {
                _orderModel = value;
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


        public PersonsSearchModel PersoneSearchModel
        {
            get
            {
                return (PersonsSearchModel)_personeSearchPanel.DataSource;
            }
        }

        private bool _showForResult = false;
        public bool ShowForResult
        {
            get
            {
                return _showForResult;
            }
            set
            {
                _showForResult = value;
                MakeTopControls();
            }
        }
        
        public event Action AddNewEmployer;
        public event DeletePersone DeletePersone;
        public event EditPersone EditPersone;
        public event Action EndTask;
        public event ShowPersoneDetails ShowPersoneDetails;
        public event Action StartTask;
        public event Action UpdateTable;
        public event Action<ViolatorViewModel> SelectedItemForResult;
        public event Action MakeReport;

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {

        }
        private List<Control> _topControls;
        public List<Control> TopControls
        {
            get
            {
                return _topControls;

            }
            set { }
        }

        private Button _btnEditEmployer;
        private Button _btnEmployerDelete;
        private Button _btnShowEmployerDetails;
        private Button _btnReturnResult;

        private TopControls _tpControls;

        private void MakeTopControls()
        {
            if (ShowForResult)
            {
                _btnReturnResult = new Button();

                _btnReturnResult.Text = "Принять";

                _btnReturnResult.Click += (s, e) =>
                {
                    if(_selectedIndex >= 0 && _selectedIndex < _data.Count)
                        if (SelectedItemForResult != null)
                        {
                            SelectedItemForResult(_data[_selectedIndex]);
                        }
                };

                _topControls = new List<Control> { _btnReturnResult };
                return;
            }

            _tpControls = new TopControls(ConfigApp.CurrentManager.Role);

            _tpControls.DetailsItem += () =>
            {
                if (_selectedIndex < 0 || _selectedIndex >= _data.Count || _data.Count == 0) return;

                var item = _data[_selectedIndex];
                if (ShowPersoneDetails != null) ShowPersoneDetails(item);
            };

            _tpControls.ReportItem += () =>
            {
                if (MakeReport != null) MakeReport();
            };

            _tpControls.EditItem += () =>
            {
                if (_selectedIndex < 0 || _selectedIndex >= _data.Count || _data.Count == 0) return;

                var item = _data[_selectedIndex];
                if (EditPersone != null) EditPersone(item);
            };

            _tpControls.DeleteItem += () =>
            {
                if (_selectedIndex < 0 || _selectedIndex >= _data.Count || _data.Count == 0) return;

                var item = _data[_selectedIndex];
                if (MessageBox.Show("Вы уверены что хотите удалить сотрудника?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (DeletePersone != null)
                    {
                        DeletePersone(item);
                        if (_data.Count == 0)
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
                if (AddNewEmployer != null)
                    AddNewEmployer();
            };

            _tpControls.EnabledDelete = false;

            _tpControls.EnabledDetails = false;

            _tpControls.EnabledEdit = false;

            _tpControls.EnabledAdd = true;

            _tpControls.EnabledReport = true;

            _topControls = _tpControls;

            //if (ConfigApp.CurrentManager.Role.Equals("admin"))
            //{
            //    AdminControls();
            //}
            //else
            //{
            //    UserControls();
            //}
        }

        private void AdminControls()
        {
            Button btnCreateNewEmplyer = new Button();
            Button btnReportButton = new Button();

            _btnEditEmployer = new Button();
            _btnEmployerDelete = new Button();
            _btnShowEmployerDetails = new Button();

            _btnEditEmployer.Text = "Редактировать";
            _btnEmployerDelete.Text = "Удалить";
            _btnShowEmployerDetails.Text = "Подробнее";
            btnCreateNewEmplyer.Text = "Добавить нового сотрудника";
            btnReportButton.Text = "Отчет";

            _btnShowEmployerDetails.Click += (s, e) =>
            {
                var item = _data[_selectedIndex];
                if (ShowPersoneDetails != null) ShowPersoneDetails(item);
            };

            _btnEditEmployer.Click += (s, e) =>
            {
                var item = _data[_selectedIndex];
                if (EditPersone != null) EditPersone(item);
            };

            _btnEmployerDelete.Click += (s, e) =>
            {
                var item = _data[_selectedIndex];
                if (MessageBox.Show("Вы уверены что хотите удалить сотрудника?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (DeletePersone != null) DeletePersone(item);
                }
            };

            btnCreateNewEmplyer.Click += (s, e) => {
                if (AddNewEmployer != null)
                    AddNewEmployer();
            };

            _btnEditEmployer.Enabled = false;
            _btnEmployerDelete.Enabled = false;
            _btnEditEmployer.Enabled = false;

            btnReportButton.Margin = new Padding(3, 3, 20, 3);

            _topControls = new List<Control> { btnReportButton, _btnShowEmployerDetails, btnCreateNewEmplyer, _btnEditEmployer, _btnEmployerDelete };

            
        }

        private void UserControls()
        {
            _btnShowEmployerDetails = new Button();
            Button btnReportButton = new Button();

            _btnShowEmployerDetails.Text = "Подробнее";
            btnReportButton.Text = "Отчет";

            _btnShowEmployerDetails.Click += (s, e) =>
            {
                var item = _data[_selectedIndex];
                if (ShowPersoneDetails != null) ShowPersoneDetails(item);
            };

            _btnShowEmployerDetails.Enabled = false;
            btnReportButton.Margin = new Padding(3, 3, 20, 3);

            _topControls = new List<Control> { btnReportButton, _btnShowEmployerDetails };
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
