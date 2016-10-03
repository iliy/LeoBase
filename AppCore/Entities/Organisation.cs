using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Organisation
    {
        [Key]
        public int OrganisationID { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Info { get; set; }
        public virtual Document RepresentativeDocument { get; set; }
    }
}
