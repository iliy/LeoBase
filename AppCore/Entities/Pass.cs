using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Pass
    {
        [Key]
        public int PassID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string DocumentType { get; set; }
        public string Serial { get; set; }
        public string Number { get; set; }
        public DateTime WhenIssued { get; set; }
        public string WhoIssued { get; set; }
        public DateTime PassGiven { get; set; }
        public DateTime PassClosed { get; set; }
    }
}
