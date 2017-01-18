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

namespace LeoBase.Components.CustomControls.SaveComponent.SaveAddress
{
    public partial class AddressItem : UserControl
    {
        public event Action RemoveThis;

        private int _addressID = -1;

        public bool ShowRemoveButton
        {
            get
            {
                return button1.Visible;
            }
            set
            {
                button1.Visible = value;
            }
        }
        public PersonAddressModelView Address
        {
            get
            {
                if (string.IsNullOrWhiteSpace(tbCity.Text) ||
                    string.IsNullOrWhiteSpace(tbRegion.Text) ||
                    string.IsNullOrWhiteSpace(tbRayon.Text) ||
                    string.IsNullOrWhiteSpace(tbCity.Text) ||
                    string.IsNullOrWhiteSpace(tbStreet.Text) ||
                    string.IsNullOrWhiteSpace(tbHome.Text)) return null;
                return new PersonAddressModelView
                {
                    Country = tbCountry.Text,
                    Subject = tbRegion.Text,
                    Area = tbRayon.Text,
                    City = tbCity.Text,
                    Street = tbStreet.Text,
                    HomeNumber = tbHome.Text,
                    Flat = tbFlat.Text,
                    Note = cmbNote.Items[cmbNote.SelectedIndex].ToString()
                };
            }
            set
            {
                tbCountry.Text = value.Country;
                tbRegion.Text = value.Subject;
                tbRayon.Text = value.Area;
                tbCity.Text = value.City;
                tbStreet.Text = value.Street;
                tbHome.Text = value.HomeNumber;
                tbFlat.Text = value.Flat;
                _addressID = value.AddressID;
                for(int i = 0; i < cmbNote.Items.Count; i++)
                {
                    if (cmbNote.Items[i].ToString().Equals(value.Note))
                    {
                        cmbNote.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        public AddressItem()
        {
            InitializeComponent();
            cmbNote.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RemoveThis != null) RemoveThis();
        }
    }
}
