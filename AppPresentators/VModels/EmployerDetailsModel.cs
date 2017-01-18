using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class EmployerDetailsModel
    {
        public int EmployerID { get; set; }
        public string FIO { get; set; }
        public string Position { get; set; }
        public string DateBerth { get; set; }
        public string PlaceBerth { get; set; }

        public byte[] Image { get; set; }
        public List<PersonAddressModelView> Addresses { get; set; }

        public List<PhoneViewModel> Phones { get; set; }

        public List<AdminViolationRowModel> Violations { get; set; }
    }
}
