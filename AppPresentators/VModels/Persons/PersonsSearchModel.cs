using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class PersonsSearchModel
    {
        public int PersoneID { get; set; } = 0;
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

        public string PlaceOfBirth { get; set; }
        public CompareString ComparePlaceOfBirth { get; set; } = CompareString.EQUAL;

        public DateTime WasBeCreated { get; set; }
        public CompareValue CompareWasBeCreated { get; set; } = CompareValue.EQUAL;

        public DateTime WasBeUpdated { get; set; }
        public CompareValue CompareWasBeUpdated { get; set; } = CompareValue.EQUAL;

        public string PlaceWork { get; set; }
        public CompareString ComparePlaceWork { get; set; } = CompareString.EQUAL;

        public bool IsEmptySearch { get
            {
                return 
                    string.IsNullOrEmpty(FirstName) 
                    && string.IsNullOrEmpty(SecondName) 
                    && string.IsNullOrEmpty(MiddleName)
                    && (WasBeCreated.Year == 1)
                    && (WasBeUpdated.Year == 1)
                    && (DateBirthday.Year == 1)
                    && string.IsNullOrEmpty(Position)
                    && Age == 0 
                    && (Address == null || Address.IsEmptyAddress) 
                    && string.IsNullOrEmpty(PlaceOfBirth)
                    && (IsEmployer == null)
                    && string.IsNullOrEmpty(PlaceWork);
            }
        }
    }
}
