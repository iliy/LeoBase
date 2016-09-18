using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class UserAddress
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public int AddressTypeID { get; set; }
        public string Address { get; set; }
    }
}
