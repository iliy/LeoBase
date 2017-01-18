using AppData.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class ViolatorViewModel:IPersoneViewModel
    {

        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Фамилия")]
        [ReadOnly(true)]
        public string FirstName { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ReadOnly(true)]
        [DisplayName("Имя")]
        public string SecondName { get; set; }
        [ReadOnly(true)]
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
        [ReadOnly(true)]

        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.DateTime)]
        [DisplayName("Дата рождения")]
        public DateTime DateBirthday { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ReadOnly(true)]
        [DisplayName("Место рождения")]
        public string PlaceOfBirth { get; set; }
        [ReadOnly(true)]

        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Место работы")]
        public string PlaceOfWork { get; set; }
        [ReadOnly(true)]

        [BrowsableForEditAndDetails(false, false)]
        [DisplayName("Добавлено в базу")]
        public DateTime WasBeCreated { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [ReadOnly(true)]
        [DisplayName("Последнее обновление")]
        public DateTime WasBeUpdated { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public bool IsEmploeyr { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public int UserID { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.Image)]
        [DisplayName("Изображение")]
        [Browsable(false)]
        public  byte[] Image { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Адреса")]
        [ControlType(ControlType.List)]
        [GenerateEmptyModelPropertyName("GenerateEmptyAddress")]
        [Browsable(false)]
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
                        AddressID = -1,
                        Note = "Проживает и прописан"
                    };
                };
            }
        }

        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.List)]
        [GenerateEmptyModelPropertyName("GenerateEmptyDocument")]
        [DisplayName("Документы")]
        [Browsable(false)]
        public List<PersoneDocumentModelView> Documents { get; set; }

        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public Func<object> GenerateEmptyDocument
        {
            get
            {
                return () =>
                {
                    return new PersoneDocumentModelView
                    {
                        DocumentTypeName = ConfigApp.DocumentsType.First().Value,
                        DocumentID = -1
                    };
                };
            }
        }



        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.List)]
        [GenerateEmptyModelPropertyName("GenerateEmptyPhone")]
        [DisplayName("Телефоны")]
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
    }
}
