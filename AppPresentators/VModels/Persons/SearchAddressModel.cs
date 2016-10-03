using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class SearchAddressModel
    {
        public string Country { get; set; }
        public CompareString CompareCountry { get; set; } = CompareString.EQUAL;
        public string Subject { get; set; }
        public CompareString CompareSubject { get; set; } = CompareString.EQUAL;
        public string Area { get; set; }
        public CompareString CompareArea { get; set; } = CompareString.EQUAL;
        public string City { get; set; }
        public CompareString CompareCity { get; set; } = CompareString.EQUAL;
        public string Street { get; set; }
        public CompareString CompareStreet { get; set; } = CompareString.EQUAL;
        public string Note { get; set; }
        public CompareString CompareNote { get; set; } = CompareString.EQUAL;
        public string HomeNumber { get; set; }
        public string Flat { get; set; }
        public bool IsEmptyAddress { get
            {
                return string.IsNullOrEmpty(Country) && string.IsNullOrEmpty(Subject) && string.IsNullOrEmpty(Area)
                        && string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Street) && string.IsNullOrEmpty(HomeNumber)
                        && string.IsNullOrEmpty(Flat); 
            }
        }
    }
}
