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

namespace LeoBase.Components.CustomControls.SaveComponent.SavePhone
{
    public partial class PhonesList : UserControl
    {

        public List<PhoneViewModel> Phones
        {
            get
            {
                List<PhoneViewModel> result = new List<PhoneViewModel>();

                foreach(var item in flPhoneList.Controls)
                {
                    var phoneItem = (PhoneItem)item;
                    var phoneNumber = phoneItem.Phone;
                    if (phoneNumber != null)
                        result.Add(phoneNumber);
                }

                return result;
            }
            set
            {
                flPhoneList.Controls.Clear();
                flPhoneList.Refresh();

                foreach (var phoneNumber in value)
                {
                    var phoneItem = new PhoneItem
                    {
                        Phone = phoneNumber
                    };

                    phoneItem.RemoveThis += () => RemoveItem(phoneItem);

                    flPhoneList.Controls.Add(phoneItem);

                    if(flPhoneList.Controls.Count == 1)
                    {
                        phoneItem.ShowRemoveButton = false;
                    }
                }
            }
        }

        private void RemoveItem(PhoneItem item)
        {
            flPhoneList.Controls.Remove(item);
        }

        public PhonesList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var phoneItem = new PhoneItem();

            phoneItem.RemoveThis += () => RemoveItem(phoneItem);

            flPhoneList.Controls.Add(phoneItem);
        }
    }
}
