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
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using LeoBase.Components.CustomControls.SearchPanels;
using LeoBase.Forms;
using AppPresentators;

namespace LeoBase.Components.CustomControls
{
    

    public partial class EmployerTableControl : UserControl, IEmployersTableControl
    {
        public bool ShowForResult { get; set; } = false;
        private bool _nowTableUpdate = false;
        private CustomTable _customTable;
        private AllPersonesSearchPanel _searchPanel;
        private bool _searchAnimate = false;
        public event Action AddNewEmployer;

        public event EditPersone EditPersone;
        public event DeletePersone DeletePersone;
        public event ShowPersoneDetails ShowPersoneDetails;

        private List<Control> _topControls;
        private Button _btnEditEmployer;
        private Button _btnEmployerDelete;
        private Button _btnShowEmployerDetails;

        private int _selectedIndex;
        public List<Control> TopControls { get {
                return _topControls;

            } set { } }


        private void MakeTopControls()
        {
            if (ConfigApp.CurrentManager.Role.Equals("admin"))
            {
                AdminControls();
            }
            else
            {
                UserControls();
            }
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

        public EmployerTableControl()
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


            _orderTypes.Add("Убывание", OrderType.ASC);
            _orderTypes.Add("Возростание", OrderType.DESC);

            _customTable = new CustomTable();
            _customTable.Width = mainPanel.Width;
            _customTable.Height = mainPanel.Height - 30;
            _customTable.Location = new Point(0, 30);
            _customTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _customTable.OrderProperties = (Dictionary<string, int>)_orderProperties;

            _customTable.UpdateTable += () => UpdateTable();
            _customTable.SelectedItemChange += (index) =>
            {
                if(index != -1)
                {
                    _btnEditEmployer.Enabled = true;
                    _btnEmployerDelete.Enabled = true;
                    _btnShowEmployerDetails.Enabled = true;
                    _selectedIndex = index;
                }
            };

            mainPanel.Controls.Add(_customTable);

            _searchPanel = new AllPersonesSearchPanel(true);

            _searchPanel.Search += () => UpdateTable();

            _searchPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            searchPanel.Controls.Add(_searchPanel);

            animateSearchPanel.Tick += AnimateSearchPanel_Tick;

            _searchPanel.HideSearchPanel += () =>
            {
                if (!_searchAnimate)
                {
                    WidthAnimate animator = new WidthAnimate(searchPanel, searchPanel.Width, 0, 10, 20);
                    animator.OnAnimationComplete += () => button1.Visible = true;
                    animator.Start();
                }
            };
        }

        int animateTo = 300;

        int animateSpeed = 30;

        private void AnimateSearchPanel_Tick(object sender, EventArgs e)
        {
            if (searchPanel.Width != animateTo)
            {
                searchPanel.Width += animateSpeed;
            }
            else
            {
                if (animateTo == 0)
                {
                    button1.Visible = true;
                }
                animateSearchPanel.Stop();
                _searchAnimate = false;
            }
        }

        private List<EmploeyrViewModel> _data;

        public List<EmploeyrViewModel> Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
                _customTable.SetData<EmploeyrViewModel>(value);
            }
        }

        public DocumentSearchModel DocumentSearchModel
        {
            get
            {
                return _searchPanel.DocumentSearchModel;
            }
        }


        public PersonsSearchModel PersoneSearchModel
        {
            get
            {
                return _searchPanel.PersonSearchModel;
            }
        }

        public SearchAddressModel AddressSearchModel
        {
            get
            {
                return _searchPanel.AddressSearchModel;
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
                if(animateTo == 0)
                {
                    button1.Visible = true;
                }
                _pageModel = value;
                _customTable.PageModel = value;
            }
        }


       

        public event Action UpdateTable;
        public event Action StartTask;
        public event Action EndTask;
        public event Action<EmploeyrViewModel> SelectedItemForResult;

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        private void EmployerTableControl_Load(object sender, EventArgs e)
        {

        }

        public void Update()
        {
            _customTable.SetData(_data);
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void cmbItemsOnPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(UpdateTable != null)
                UpdateTable();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            UpdateTable();
        }

       
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            _searchPanel.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            WidthAnimate animator = new WidthAnimate(searchPanel, searchPanel.Width, 450, 10, 20);
            animator.Start();
        }

        public void LoadStart()
        {
            throw new NotImplementedException();
        }

        public void LoadEnd()
        {
            throw new NotImplementedException();
        }
    }
}
