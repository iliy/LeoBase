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
    public partial class AddressesList : UserControl
    {
        public List<PersonAddressModelView> Addresses
        {
            get
            {
                List<PersonAddressModelView> result = new List<PersonAddressModelView>();
                foreach(var c in flowLayoutPanel1.Controls)
                {
                    var addressItem = (AddressItem)c;
                    var address = addressItem.Address;
                    if(address != null)
                        result.Add(addressItem.Address);
                }
                return result;
            }
            set
            {
                foreach(var item in value)
                {
                    var addressItem = new AddressItem();
                    addressItem.Address = item;
                    if(value.Count == 1)
                    {
                        addressItem.ShowRemoveButton = false;
                    }
                    addressItem.RemoveThis += () => RemoveItem(addressItem);
                    flowLayoutPanel1.Controls.Add(addressItem);
                }
            }
        }
        public AddressesList()
        {
            InitializeComponent();
        }

        private void RemoveItem(Control control)
        {
            flowLayoutPanel1.Controls.Remove(control);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddressItem addressItem = new AddressItem();

            addressItem.Address = new PersonAddressModelView
            {
                AddressID = -1,
                Country = "Российская Федерация",
                Subject = "Приморский край",
                Area = "Хасанский район"
            };

            addressItem.RemoveThis += () => RemoveItem(addressItem);

            flowLayoutPanel1.Controls.Add(addressItem);
        }
    }
}
