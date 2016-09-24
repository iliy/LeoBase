using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class PersonsSearchModel
    {
        public string FirstName { get; set; }
        public CompareString CompareFirstName { get; set; } = CompareString.EQUAL;

        public string SecondName { get; set; }
        public CompareString CompareSecondName { get; set; } = CompareString.EQUAL;

        public string MiddleName { get; set; }
        public CompareString CompareMiddleName { get; set; } = CompareString.EQUAL;

        public DateTime DateBirthday { get; set; }
        public CompareValue CompareDate { get; set; } = CompareValue.EQUAL;

        public int Age { get; set; } = 0;
        public CompareValue CompareAge { get; set; } = CompareValue.EQUAL;

        public SearchAddressModel Address { get; set; }

        public string Position { get; set; }

        public bool? IsEmployer { get; set; } = null;

        public bool IsEmptySearch { get
            {
                return 
                    string.IsNullOrEmpty(FirstName) 
                    && string.IsNullOrEmpty(SecondName) 
                    && string.IsNullOrEmpty(MiddleName)
                    && DateBirthday.Year == 1 
                    && Age == 0 
                    && (Address == null || Address.IsEmptyAddress)
                    && string.IsNullOrEmpty(Position) 
                    && (IsEmployer == null);
            }
        }
    }
}
