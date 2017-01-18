using AppData.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AppPresentators.VModels.Persons
{
    public class PersonsSearchModel
    {
        [Browsable(false)]
        public int PersoneID { get; set; } = 0;

        [DisplayName("Фамилия")]
        public string FirstName { get; set; }

        [DisplayName("Имя")]
        public string SecondName { get; set; }

        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [DisplayName("Дата рождения")]
        [ControlType(ControlType.DateTime)]
        [ChildrenProperty("CompareDate")]
        public DateTime DateBirthday { get; set; }
        [Browsable(false)]
        public CompareValue CompareDate { get; set; } = CompareValue.NONE;

        [DisplayName("Возраст")]
        [ControlType(ControlType.Int)]
        [ChildrenProperty("CompareAge")]
        [Browsable(false)]
        public int Age { get; set; } = 0;
        [Browsable(false)]
        public CompareValue CompareAge { get; set; } = CompareValue.NONE;
        [DisplayName("Адрес")]
        public SearchAddressModel Address { get; set; }
        [ControlType(ControlType.ComboBox, "Display", "Value")]
        [DataPropertiesName("PositionData")]
        [DisplayName("Должность")]
        [DinamicBrowsable("IsEmployer", true)]
        public string Position { get; set; }
        [Browsable(false)]
        public bool IsEmployer { get; set; }

        [DisplayName("Место рождения")]
        [DinamicBrowsable("IsEmployer", false)]
        public string PlaceOfBirth { get; set; }
        [DisplayName("Место работы")]
        [DinamicBrowsable("IsEmployer", false)]
        public string PlaceWork { get; set; }

        [Browsable(false)]
        public DateTime WasBeCreated { get; set; }
        [Browsable(false)]
        public CompareValue CompareWasBeCreated { get; set; } = CompareValue.EQUAL;


        [Browsable(false)]
        public DateTime WasBeUpdated { get; set; }

        [Browsable(false)]
        public CompareValue CompareWasBeUpdated { get; set; } = CompareValue.EQUAL;




        [Browsable(false)]
        public CompareString CompareSecondName { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString CompareMiddleName { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString ComparePlaceWork { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString ComparePlaceOfBirth { get; set; } = CompareString.CONTAINS;
        [Browsable(false)]
        public CompareString CompareFirstName { get; set; } = CompareString.CONTAINS;

        [Browsable(false)]
        public List<ComboBoxDefaultItem> PositionData
        {
            get
            {
                List<ComboBoxDefaultItem> data = new List<ComboBoxDefaultItem>();

                data.Add(new ComboBoxDefaultItem { Display = "Не использовать", Value = "Не использовать" });

                data.AddRange(ConfigApp.EmployerPositionsList.Select(p => new ComboBoxDefaultItem { Display = p.Name, Value = p.Name }).ToList());

                return data;
            }
        }
        [Browsable(false)]
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
