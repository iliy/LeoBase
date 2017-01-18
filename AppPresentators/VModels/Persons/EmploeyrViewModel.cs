using AppData.Abstract;
using AppData.CustomAttributes;
using AppData.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class EmploeyrViewModel: IPersoneViewModel
    {
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Фамилия")]
        [ReadOnly(true)]
        public string FirstName { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ReadOnly(true)]
        [DisplayName("Имя")]
        public string SecondName { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ReadOnly(true)]
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [ReadOnly(true)]
        [DisplayName("Должность")]
        public string Position { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ReadOnly(true)]
        [DisplayName("Дата рождения")]
        [ControlType(ControlType.DateTime)]
        public DateTime DateBirthday { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ReadOnly(true)]
        [DisplayName("Место рождения")]
        public string PlaceOfBirth { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [ReadOnly(true)]
        [DisplayName("Добавлено в базу")]
        public DateTime WasBeCreated { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [ReadOnly(true)]
        [DisplayName("Последнее обновление")]
        public DateTime WasBeUpdated { get; set; }


        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public bool IsEmploeyr { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [Browsable(false)]
        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DataPropertiesName("PositionsData")]
        [DisplayName("Должность")]
        [PropertyNameSelectedText("Position")]
        public string PositionID { get; set; }


        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public int UserID { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.Image)]
        [DisplayName("Изображение")]
        [Browsable(false)]
        public byte[] Image { get; set; } = new byte[0];



        [BrowsableForEditAndDetails(true, true)]
        [Browsable(false)]
        [DisplayName("Адреса")]
        [ControlType(ControlType.List)]
        [GenerateEmptyModelPropertyName("GenerateEmptyAddress")]
        public List<PersonAddressModelView> Addresses { get; set; }

        [Browsable(false)]
        [BrowsableForEditAndDetails(false, false)]
        public Func<object> GenerateEmptyAddress
        {
            get
            {
                return () =>
                {
                    return new PersonAddressModelView
                    {
                        AddressID = -1
                    };
                };
            }
        }



        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public List<PersoneDocumentModelView> Documents { get; set; }



        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.List)]
        [GenerateEmptyModelPropertyName("GenerateEmptyPhone")]
        [Browsable(false)]
        public List<PhoneViewModel> Phones { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public Func<object> GenerateEmptyPhone
        {
            get
            {
                return () =>
                {
                    return new PhoneViewModel
                    {
                        PhoneID = -1
                    };
                };
            }
        }


        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public BindingList<ComboBoxDefaultItem> PositionsData
        {
            get
            {
                return new BindingList<ComboBoxDefaultItem>(ConfigApp.EmployerPositionsList.Select(dt => new ComboBoxDefaultItem { Display = dt.Name, Value = dt.PositionID.ToString() }).ToList());
            }
        }
    }
}
