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
        public int PhoneID { get; set; }
        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}
