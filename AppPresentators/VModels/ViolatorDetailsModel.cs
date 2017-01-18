using AppData.Entities;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class ViolatorDetailsModel
    {
        public string FIO { get; set; }

        public string PlaceWork { get; set; }

        public DateTime DateBerth { get; set; }

        public string PlaceBerth { get; set; }

        public byte[] Image { get; set; }

        public List<PersoneDocumentModelView> Documents { get; set; }

        public List<PersonAddressModelView> Addresses { get; set; }

        public List<PhoneViewModel> Phones { get; set; }

        public List<AdminViolationRowModel> Violations { get; set; }
    }
}
