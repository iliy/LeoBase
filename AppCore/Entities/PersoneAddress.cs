using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class PersoneAddress
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string Country { get; set; } = "Российская Федерация";
        public string Subject { get; set; } = "Приморский край";
        public string Area { get; set; } = "Хасанский район";
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public string Flat { get; set; }
        public string Note { get; set; } = "Проживает и прописка";
    }
}
