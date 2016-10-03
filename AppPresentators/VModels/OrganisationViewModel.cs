using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class OrganisationViewModel
    {

        public int OrganisationID { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Info { get; set; }
        public PersoneViewModel Representative { get; set; }
    }
}
