using AppData.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class PhoneViewModel
    {
        [Browsable(false)]
        [BrowsableForEditAndDetails(false, false)]
        public int PhoneID { get; set; }

        [Browsable(true)]
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}
