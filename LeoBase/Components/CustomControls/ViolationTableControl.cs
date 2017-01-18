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
using AppPresentators.Services;
using AppPresentators.VModels;
using LeoBase.Components.CustomControls.SearchPanels;
using AppPresentators;

namespace LeoBase.Components.CustomControls
{
    public partial class ViolationTableControl : UserControl, IViolationTableControl
    {
        private Dictionary<string, int> _orderProperties;
        private Dictionary<string, OrderType> _orderTypes;
        
        public ViolationTableControl()
        {
            InitializeComponent();
            SearchPanel = new ViolationSearchPanel();

            SearchPanel.Search += () => UpdateData();

            SearchPanel.HidePanel += () =>
            {
                WidthAnimate animator = new WidthAnimate(searchPanel, searchPanel.Width, 0, 5, 20);
                animator.OnAnimationComplete += () => btnShowSearch.Visible = true;
                animator.Start();
            };

            searchPanel.Controls.Add(SearchPanel);
            
            _table = new CustomTable();

            _orderProperties = new Dictionary<string, int>();
            _orderTypes = new Dictionary<string, OrderType>();

            _orderProperties.Add("Дата создания", (int)ViolationOrderProperties.WAS_BE_CREATED);
            _orderProperties.Add("Дата последнего обновления", (int)ViolationOrderProperties.WAS_BE_UPDATED);


            _orderTypes.Add("Убывание", OrderType.ASC);
            _orderTypes.Add("Возростание", OrderType.DESC);

            _table = new CustomTable();

            _table.Width = mainPanel.Width - 5;

            _table.Height = mainPanel.Height - 35;
            _table.Location = new Point(0, 30);

            _table.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            _table.Margin = new Padding(0, 0, 0, 0);

            _table.OrderProperties = (Dictionary<string, int>)_orderProperties;

            _table.UpdateTable += () => Update();
            
            mainPanel.Controls.Add(_table);

            animateSearchPanel.Tick += AnimateSearchPanel_Tick;

            MakeTopControls();

            _table.SelectedItemChange += (index) =>
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
            };
        }

        private Button _btnReport = new Button();

        private Button _btnAddNew = new Button();

        private Button _btnRemove = new Button();

        private Button _btnDetails = new Button();

        private Button _btnUpdate = new Button();

        private void MakeTopControls()
        {
            if (!ConfigApp.CurrentManager.Role.Equals("admin"))
            {
                _btnAddNew.Visible = false;
                _btnRemove.Visible = false;
                _btnUpdate.Visible = false;
            }

            _btnUpdate.Enabled = false;
            _btnRemove.Enabled = false;
            _btnDetails.Enabled = false;

            _btnAddNew.Text = "Добавить";
            _btnRemove.Text = "Удалить";
            _btnUpdate.Text = "Редактировать";
            _btnDetails.Text = "Подробнее";
            _btnReport.Text = "Отчет";

            _btnAddNew.Click += (s, e) =>
            {
                if (AddNewViolation != null) AddNewViolation();
            };

            _topControls = new List<Control>();

            _topControls.Add(_btnReport);
            _topControls.Add(_btnDetails);
            _topControls.Add(_btnAddNew);
            _topControls.Add(_btnUpdate);
            _topControls.Add(_btnRemove);
        }


        private void MakeAdminControls()
        {

        }

        private void MakeUserControls()
        {

        }

        public ViolationOrderModel OrderModel
        {
            get
            {
                return new ViolationOrderModel
                {
                    OrderProperties = (ViolationOrderProperties)_orderProperties[_table.SelectedOrderByValue],
                    OrderType = _table.OrderType
                };
            }
        }

        public PageModel PageModel
        {
            get
            {
                return _table.PageModel;
            }
            set
            {

            }
        }

        private ViolationSearchPanel SearchPanel;

        public ViolationSearchModel SearchModel
        {
            get
            {
                return SearchPanel.SearchModel;
            }
        }

        public bool ShowForResult { get; set; }

        private List<Control> _topControls;
        public List<Control> TopControls { get { return _topControls; } set { } }

        private List<ViolationViewModel> _data;

        public List<ViolationViewModel> Violations
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
                _table.SetData<ViolationViewModel>(_data);
            }
        }


        public event Action AddNewViolation;
        public event ViolationAction RemoveViolation;
        public event ViolationAction ShowDetailsViolation;
        public event Action UpdateData;
        public event ViolationAction UpdateViolation;

        private CustomTable _table;

        public Control GetControl()
        {
            return this;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        void UIComponent.Resize(int width, int height){}

        private int animateTo = 0;
        private int animateSpeed = 0;

        bool _searchAnimate = false;

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
                    btnShowSearch.Visible = true;
                }
                animateSearchPanel.Stop();
                _searchAnimate = false;
            }
        }

        private void btnShowSearch_Click(object sender, EventArgs e)
        {
            btnShowSearch.Visible = false;
            WidthAnimate animator = new WidthAnimate(searchPanel, searchPanel.Width, 450, 10, 20);
            animator.Start();
        }

        public void LoadStart()
        {
            //_customTable.Visible = false;
            //VisibleLoadPanel = true;
        }

        public void LoadEnd()
        {
            //VisibleLoadPanel = false;
            //_customTable.Visible = true;
        }
    }
}
