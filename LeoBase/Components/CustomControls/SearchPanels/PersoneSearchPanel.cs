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

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class PersoneSearchPanel : UserControl
    {
        private PersonsSearchModel _searchModel;

        private bool _isEmployerSearch;
        public bool ItSearchForEmployer { get
            {
                return _isEmployerSearch;
            }
            set
            {
                _isEmployerSearch = value;
                if (value)
                {
                    lbPosition.Visible = true;
                    cmbPosition.Visible = true;

                    lbWorkPlace.Visible = false;
                    tbWorkPlace.Visible = false;
                    cbComapreWorkPlace.Visible = false;
                }
                else
                {
                    lbPosition.Visible = false;
                    cmbPosition.Visible = false;

                    lbWorkPlace.Visible = true;
                    tbWorkPlace.Visible = true;
                    cbComapreWorkPlace.Visible = true;
                }
            }
        }

        public List<string> Positions
        {
            set
            {
                cmbPosition.Items.Clear();
                foreach(var position in value)
                {
                    cmbPosition.Items.Add(position);
                }
            }
        }

        public PersonsSearchModel SearchModel
        {
            get
            {
                _searchModel = new PersonsSearchModel
                {
                    ComparePlaceOfBirth = cmbComparePlaceBerth.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                    PlaceOfBirth = tbPlaceBerth.Text.Trim(),
                    ComparePlaceWork = cbComapreWorkPlace.Checked ? CompareString.EQUAL : CompareString.CONTAINS,
                    PlaceWork = tbWorkPlace.Text.Trim(),
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
                    Position = cmbPosition.Items[cmbPosition.SelectedIndex].ToString() != null ?
                                       cmbPosition.Items[cmbPosition.SelectedIndex].ToString() :
                                       "",
                };
                return _searchModel;
            }
            set
            {
            }
        }
        public PersoneSearchPanel()
        {
            InitializeComponent();
            cmbAge.SelectedIndex = 0;
            chbDateBerthday.SelectedIndex = 0;
            cmbPosition.SelectedIndex = 0;
        }

        public void Clear()
        {
            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbMiddleName.Text = "";
            tbAge.Text = "";
            
            tbWorkPlace.Text = "";

            tbPlaceBerth.Text = "";

            dtpDateBerthday.Value = new DateTime(1900, 1, 1);
            cmbPosition.SelectedIndex = 0;

            chbFirstName.Checked = true;
            chbSecondName.Checked = true;
            chbMiddleName.Checked = true;
            cbComapreWorkPlace.Checked = true;
            cmbComparePlaceBerth.Checked = true;
            cmbAge.SelectedIndex = 0;
            chbDateBerthday.SelectedIndex = 0;
        }

        private void btnClearSearchModel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
