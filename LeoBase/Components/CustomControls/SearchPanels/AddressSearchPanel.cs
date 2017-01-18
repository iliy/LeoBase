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
    public partial class AddressSearchPanel : UserControl
    {

        public SearchAddressModel SearchAddressModel
        {
            get
            {
                return new SearchAddressModel
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
                };
            }
        }
        public AddressSearchPanel()
        {
            InitializeComponent();
        }

        public void Clear()
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

        private void btnClearAddressSearchModel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
