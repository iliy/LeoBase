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
        public EmployerTableControl()
        {
            InitializeComponent();
            cmbWhenIssuedBy.SelectedIndex = 0;
            cmbPosition.SelectedIndex = 0;
            cmbDocumentType.SelectedIndex = 0;
            cmbAge.SelectedIndex = 0;
            chbDateBerthday.SelectedIndex = 0;
            cmbOrderBy.SelectedIndex = 0;
            cmbOrderType.SelectedIndex = 0;
            cmbItemsOnPage.SelectedIndex = 0;

            _orderProperties = new Dictionary<string, PersonsOrderProperties>();
            _orderTypes = new Dictionary<string, OrderType>();

            _orderProperties.Add("Фамилия", PersonsOrderProperties.FIRST_NAME);
            _orderProperties.Add("Имя", PersonsOrderProperties.SECOND_NAME);
            _orderProperties.Add("Отчество", PersonsOrderProperties.MIDDLE_NAME);
            _orderProperties.Add("Дата рождения", PersonsOrderProperties.DATE_BERTHDAY);
            _orderProperties.Add("Возвраст", PersonsOrderProperties.AGE);

            _orderTypes.Add("Убывание", OrderType.ASC);
            _orderTypes.Add("Возростание", OrderType.DESC);
        }


        private List<PersoneViewModel> _data;

        public List<PersoneViewModel> Data
        {
            get
            {
                cmbCurrentPage.Items.Clear();

                cmbCurrentPage.Update();
                cmbCurrentPage.Refresh();

                for (int i = 0; i < PageModel.TotalPages; i++)
                    cmbCurrentPage.Items.Add(Convert.ToString(i));

                cmbCurrentPage.Update();
                cmbCurrentPage.Refresh();

                cmbCurrentPage.SelectedIndex = 0;
                return _data;
            }

            set
            {
                _data = value;
                tblPersones.DataSource = new BindingList<PersoneViewModel>(_data);
                tblPersones.Update();
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

        private Dictionary<string, PersonsOrderProperties> _orderProperties;
        private Dictionary<string, OrderType> _orderTypes;

        public PersonsOrderModel OrderModel
        {
            get
            {
                _orderModel = new PersonsOrderModel
                {
                    OrderProperties = _orderProperties[cmbOrderBy.Items[cmbOrderBy.SelectedIndex].ToString()],
                    OrderType = _orderTypes[cmbOrderType.Items[cmbOrderType.SelectedIndex].ToString()]
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

                _pageModel.ItemsOnPage = Convert.ToInt32(cmbItemsOnPage.Items[cmbItemsOnPage.SelectedIndex].ToString());
                _pageModel.CurentPage = cmbCurrentPage.SelectedIndex == -1 ? 
                                                1 : 
                                                Convert.ToInt32(cmbCurrentPage.Items[cmbCurrentPage.SelectedIndex].ToString());

                return _pageModel;
            }

            set
            {
                _pageModel = value;
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
            tblPersones.DataSource = new BindingList<PersoneViewModel>(_data);
            tblPersones.Update();
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
            UpdateTable();
        }
    }
}
