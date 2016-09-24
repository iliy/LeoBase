using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Persone
    {
        public int UserID { get; set; }
        public bool IsEmploeyr { get; set; } = false;
        public int PositionID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateBirthday { get; set; }
    }
}
