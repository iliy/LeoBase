using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.VModels.Persons;
using AppPresentators.VModels;

namespace LeoBase.Components.CustomControls
{
    public partial class PersonsSearchPanel : UserControl
    {
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

        public PersonsSearchPanel()
        {
            InitializeComponent();

            cmbWhenIssuedBy.SelectedIndex = 0;
            cmbPosition.SelectedIndex = 0;
            cmbDocumentType.SelectedIndex = 0;
            cmbAge.SelectedIndex = 0;
            chbDateBerthday.SelectedIndex = 0;
        }
    }
}
