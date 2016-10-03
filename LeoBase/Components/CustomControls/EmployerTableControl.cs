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

namespace LeoBase.Components.CustomControls
{
    

    public partial class EmployerTableControl : UserControl, IEmployersTableControl
    {
        private bool _nowTableUpdate = false;
        private CustomTable _customTable;
        public EmployerTableControl()
        {
            InitializeComponent();
            cmbWhenIssuedBy.SelectedIndex = 0;
            cmbPosition.SelectedIndex = 0;
            cmbDocumentType.SelectedIndex = 0;
            cmbAge.SelectedIndex = 0;
            chbDateBerthday.SelectedIndex = 0;

            _orderProperties = new Dictionary<string, int>();
            _orderTypes = new Dictionary<string, OrderType>();

            _orderProperties.Add("Фамилия", (int)PersonsOrderProperties.FIRST_NAME);
            _orderProperties.Add("Имя", (int)PersonsOrderProperties.SECOND_NAME);
            _orderProperties.Add("Отчество", (int)PersonsOrderProperties.MIDDLE_NAME);
            _orderProperties.Add("Дата рождения", (int)PersonsOrderProperties.DATE_BERTHDAY);
            _orderProperties.Add("Возвраст", (int)PersonsOrderProperties.AGE);

            _orderTypes.Add("Убывание", OrderType.ASC);
            _orderTypes.Add("Возростание", OrderType.DESC);

            _customTable = new CustomTable();
            _customTable.Width = mainPanel.Width;
            _customTable.Height = mainPanel.Height;
            _customTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _customTable.OrderProperties = (Dictionary<string, int>)_orderProperties;

            _customTable.UpdateTable += () => UpdateTable();

            mainPanel.Controls.Add(_customTable);
        }


        private List<PersoneViewModel> _data;

        public List<PersoneViewModel> Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
                _customTable.SetData<PersoneViewModel>(value);
                //tblPersones.DataSource = new BindingList<PersoneViewModel>(_data);
                //tblPersones.Update();
                //_nowTableUpdate = false;
            }
        }

        private DocumentSearchModel _documentSearchModel;
        public DocumentSearchModel DocumentSearchModel
        {
            get
            {
                _documentSearchModel = new DocumentSearchModel
                {
                    DocumentTypeName = cmbDocumentType.Items[cmbDocumentType.SelectedIndex].ToString() != null ?
                                       cmbDocumentType.Items[cmbDocumentType.SelectedIndex].ToString() :
                                       "",
                    Serial = tbDocumentSerial.Text.Trim(),
                    Number = tbDocumentNumber.Text.Trim(),
                    CompareWhenIssued = cmbWhenIssuedBy.Items[cmbWhenIssuedBy.SelectedIndex] == null ? CompareValue.EQUAL :
                                        cmbWhenIssuedBy.Items[cmbWhenIssuedBy.SelectedIndex].ToString().Equals("Больше") ? CompareValue.MORE :
                                        cmbWhenIssuedBy.Items[cmbWhenIssuedBy.SelectedIndex].ToString().Equals("Меньше") ? CompareValue.LESS :
                                        CompareValue.EQUAL,
                    WhenIssued = dtpWhenIssued.Value.Year == 1900 ? new DateTime(1, 1, 1, 0, 0, 0) :
                                 dtpWhenIssued.Value,
                    IssuedBy = tbIssuedBy.Text.Trim(),
                    CompareIssuedBy = chbIssuedBy.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                    CodeDevision = tbCodeDevision.Text.Trim()
                };

                return _documentSearchModel;
            }

            set
            {
                
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
                //_pageModel.CurentPage = 1;
                _pageModel.ItemsOnPage = 10;
                _pageModel = _customTable.PageModel;
                //_pageModel.ItemsOnPage = Convert.ToInt32(cmbItemsOnPage.Items[cmbItemsOnPage.SelectedIndex].ToString());
                //_pageModel.CurentPage = bindingSource1.Current == null ? 1 : (int)bindingSource1.Current;
                return _pageModel;
            }

            set
            {
                //if (bindingSource1.Current == null
                //    || _pageModel.CurentPage != (int)bindingSource1.Current
                //    || _pageModel.ItemsOnPage != value.ItemsOnPage)
                //{
                //    List<int> list = new List<int>();

                //    for (int i = 1; i <= value.TotalPages; i++)
                //    {
                //        list.Add(i);
                //    }

                //    bindingSource1.DataSource = list;
                //    bindingNavigator1.BindingSource = bindingSource1;
                //    bindingNavigator1.Refresh();

                //    
                //}
                _pageModel = value;
                _customTable.PageModel = value;
            }
        }

        private PersonsSearchModel _searchModel = null;

        public PersonsSearchModel SearchModel
        {
            get
            {
                _searchModel = new PersonsSearchModel
                {
                    FirstName = tbFirstName.Text.Trim(),
                    CompareFirstName = chbFirstName.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                    SecondName = tbSecondName.Text.Trim(),
                    CompareSecondName = chbSecondName.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                    MiddleName = tbMiddleName.Text.Trim(),
                    CompareMiddleName = chbMiddleName.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                    DateBirthday = dtpDateBerthday.Value.Year != 1900 ? 
                                    dtpDateBerthday.Value : 
                                    new DateTime(1, 1, 1, 0, 0, 0),
                    CompareDate = chbDateBerthday.Items[chbDateBerthday.SelectedIndex] == null ? CompareValue.EQUAL :
                                  chbDateBerthday.Items[chbDateBerthday.SelectedIndex].ToString().Equals("Больше") ? CompareValue.MORE :
                                  chbDateBerthday.Items[chbDateBerthday.SelectedIndex].ToString().Equals("Меньше") ? CompareValue.LESS :
                                  CompareValue.EQUAL,
                    Age = Convert.ToInt32((string.IsNullOrEmpty(tbAge.Text.Trim()) 
                                            ? "0" : 
                                            tbAge.Text.Trim())),
                    CompareAge = cmbAge.Items[cmbAge.SelectedIndex] == null ? CompareValue.EQUAL :
                                 cmbAge.Items[cmbAge.SelectedIndex].ToString().Equals("Больше") ? CompareValue.MORE :
                                 cmbAge.Items[cmbAge.SelectedIndex].ToString().Equals("Меньше") ? CompareValue.LESS :
                                 CompareValue.EQUAL,
                    Position = cmbPosition.SelectedValue == null ? "" : cmbPosition.SelectedValue.ToString(),
                    Address = new SearchAddressModel
                    {
                        Country = tbCountry.Text.Trim(),
                        CompareCountry = chbCounty.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                        Subject = tbRegion.Text.Trim(),
                        CompareSubject = chbRegion.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                        Area = tbArea.Text.Trim(),
                        CompareArea = chbArea.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                        City = tbCity.Text.Trim(),
                        CompareCity = chbCity.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                        Street = tbStreet.Text.Trim(),
                        CompareStreet = chbStreet.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                        HomeNumber = tbHomeNumber.Text.Trim(),
                        Flat = tbFlat.Text.Trim(),
                        Note = ""
                    }
                };
                return _searchModel;
            }

            set
            {
            }
        }

        public event Action UpdateTable;
        public event Action StartTask;
        public event Action EndTask;

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

        private void ClearSearchModel()
        {
            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbMiddleName.Text = "";
            tbAge.Text = "";
            dtpDateBerthday.Value = new DateTime(1900, 1, 1);
            cmbPosition.SelectedIndex = 0;

            chbFirstName.Checked = true;
            chbSecondName.Checked = true;
            chbMiddleName.Checked = true;
            cmbAge.SelectedIndex = 0;
            chbDateBerthday.SelectedIndex = 0;
        }

        private void ClearAddressModel()
        {
            tbCountry.Text = "";
            tbRegion.Text = "";
            tbArea.Text = "";
            tbStreet.Text = "";
            tbCity.Text = "";
            tbHomeNumber.Text = "";
            tbFlat.Text = "";

            chbCounty.Checked = true;
            chbRegion.Checked = true;
            chbCity.Checked = true;
            chbArea.Checked = true;
            chbStreet.Checked = true;
        }

        private void ClearDocumentModel()
        {
            cmbDocumentType.SelectedIndex = 0;
            tbDocumentSerial.Text = "";
            tbDocumentNumber.Text = "";
            tbIssuedBy.Text = "";
            dtpWhenIssued.Value = new DateTime(1900, 1, 1);
            cmbWhenIssuedBy.SelectedIndex = 0;
            tbCodeDevision.Text = "";
        }

        private void btnClearSearchModel_Click(object sender, EventArgs e)
        {
            ClearSearchModel();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void btnClearAddressSearchModel_Click(object sender, EventArgs e)
        {
            ClearAddressModel();
        }

        private void btnClearDocument_Click(object sender, EventArgs e)
        {
            ClearDocumentModel();
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

        private void metroTile1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void btnClearSearchModel_Click_1(object sender, EventArgs e)
        {
            ClearSearchModel();
        }
    }
}
