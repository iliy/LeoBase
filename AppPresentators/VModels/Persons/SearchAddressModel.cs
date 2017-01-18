using AppData.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class SearchAddressModel
    {
        [DisplayName("Страна")]
        public string Country { get; set; }
        [DisplayName("Регион")]
        public string Subject { get; set; }
        [DisplayName("Район")]
        public string Area { get; set; }
        [DisplayName("Город")]
        public string City { get; set; }
        [DisplayName("Улица")]
        public string Street { get; set; }
        [DisplayName("Номер дома")]
        public string HomeNumber { get; set; }
        [DisplayName("Номер квартиры")]
        public string Flat { get; set; }
        
        [DisplayName("Проживает/прописан")]
        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DataPropertiesName("NoteData")]
        public string Note { get; set; }



        [Browsable(false)]
        public List<ComboBoxDefaultItem> NoteData
        {
            get
            {
                return new List<ComboBoxDefaultItem>
                {
                    new ComboBoxDefaultItem { Display ="Не использовать", Value = "Не использовать" },
                    new ComboBoxDefaultItem {Display = "Проживает", Value = "Проживает" },
                    new ComboBoxDefaultItem {Display = "Прописан", Value = "Прописан" },
                    new ComboBoxDefaultItem {Display = "Проживает и прописан", Value = "Проживает и прописан" }
                };
            }
        }
        [Browsable(false)]
        public CompareString CompareCountry { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString CompareSubject { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString CompareArea { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString CompareCity { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString CompareStreet { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString CompareNote { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public bool IsEmptyAddress { get
            {
                return string.IsNullOrEmpty(Country) && string.IsNullOrEmpty(Subject) && string.IsNullOrEmpty(Area)
                        && string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Street) && string.IsNullOrEmpty(HomeNumber)
                        && string.IsNullOrEmpty(Flat); 
            }
        }
    }
}
