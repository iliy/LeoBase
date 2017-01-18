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
    public partial class PhoneItem : UserControl
    {
        public event Action RemoveThis;

        private int _phoneID = -1;

        public PhoneViewModel Phone { get
            {
                return new PhoneViewModel
                {
                    PhoneID = _phoneID,
                    PhoneNumber = tbPhoneNumber.Text
                };
            }
            set
            {
                _phoneID = value.PhoneID;
                tbPhoneNumber.Text = value.PhoneNumber;
            }
        }

        public bool ShowRemoveButton
        {
            get
            {
                return btnRemove.Visible;
            }
            set
            {
                btnRemove.Visible = value;
            }
        }
        public PhoneItem()
        {
            InitializeComponent();
            btnRemove.Click += (s, e)=> 
            {
                if (RemoveThis != null) RemoveThis();
            };
        }
    }
}
