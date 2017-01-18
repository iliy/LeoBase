using AppData.Abstract;
using AppData.CustomAttributes;
using AppData.Entities;
using AppData.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.VModels.Persons
{
    public class PersonAddressModelView
    {
        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public int AddressID { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Страна")]
        public string Country { get; set; } = "Российская Федерация";
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Область")]
        public string Subject { get; set; } = "Приморский край";
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Район")]
        public string Area { get; set; } = "Хасанский район";
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Город")]
        public string City { get; set; } 
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Улица")]
        public string Street { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Номер дома")]
        public string HomeNumber { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Номер квартиры")]
        public string Flat { get; set; }

        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DataPropertiesName("NoteTypes")]
        [DisplayName("Проживает/прописан")]
        [PropertyNameSelectedText("Note")]
        public string Note { get; set; }

        [Browsable(false)]
        [BrowsableForEditAndDetails(false, false)]
        public List<ComboBoxDefaultItem> NoteTypes
        {
            get
            {
                return new List<ComboBoxDefaultItem>
                {
                    new ComboBoxDefaultItem { Display ="Проживает и прописан", Value = "Проживает и прописан" },
                    new ComboBoxDefaultItem { Display ="Проживает", Value = "Проживает" },
                    new ComboBoxDefaultItem { Display ="Прописан", Value = "Прописан" }
                };
            }
        }

        [BrowsableForEditAndDetails(false, false)]
        [Browsable(false)]
        public bool IsEmptyModel
        {
            get
            {
                bool defaultValue = Country == "Российская Федерация" && Subject == "Приморский край" && Area == "Хасанский район" &&
                       string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Street)  && string.IsNullOrEmpty(Flat) && string.IsNullOrEmpty(HomeNumber);

                bool emptyValue = string.IsNullOrEmpty(Country) && string.IsNullOrEmpty(Subject) && string.IsNullOrEmpty(Area) &&
                       string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Street) && string.IsNullOrEmpty(Flat) && string.IsNullOrEmpty(HomeNumber);

                return defaultValue || emptyValue;
            }
        }
    }

    public class PersoneDocumentModelView
    {
        [Browsable(false)]
        public int DocumentID { get; set; }
        [Browsable(false)]
        public int DocumentTypeID { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Тип документа")]
        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DataPropertiesName("DocumentTypes")]
        public string DocumentTypeName { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Серия")]
        public string Serial { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Номер")]
        public string Number { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Кем выдан")]
        public string IssuedBy { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [ControlType(ControlType.DateTime)]
        [DisplayName("Когда выдан")]
        public DateTime WhenIssued { get; set; }
        [BrowsableForEditAndDetails(true, true)]
        [DisplayName("Код подразделения")]
        public string CodeDevision { get; set; }

        [Browsable(false)]
        [BrowsableForEditAndDetails(false, false)]
        public List<ComboBoxDefaultItem> DocumentTypes
        {
            get
            {
                return ConfigApp.DocumentsType.Select(x => new ComboBoxDefaultItem { Display = x.Value, Value = x.Value }).ToList();
            }
        }

        [Browsable(false)]
        public PersoneViewModel Persone { get; set; }
        public PersoneDocumentModelView() { }
        public PersoneDocumentModelView(Document document, string documentType, Persone persone)
        {
            DocumentTypeName = documentType;

            Persone = new PersoneViewModel(persone);

            DocumentID = document.DocumentID;

            DocumentTypeID = document.Document_DocumentTypeID;

            Serial = document.Serial;

            Number = document.Number;

            IssuedBy = document.IssuedBy;

            WhenIssued = document.WhenIssued;

            CodeDevision = document.CodeDevision;
        }
    }

    public interface IPersoneViewModel
    {
        [Browsable(false)]
        int UserID { get; set; }
        

        [Browsable(false)]
        bool IsEmploeyr { get; set; }

        [DisplayName("Фамилия")]
        string FirstName { get; set; }

        [DisplayName("Имя")]
        string SecondName { get; set; }

        [DisplayName("Отчество")]
        string MiddleName { get; set; }

        [DisplayName("Дата рождения")]
        DateTime DateBirthday { get; set; }

        [DisplayName("Дата создания")]
        DateTime WasBeCreated { get; set; }

        [DisplayName("Дата обновления")]
        DateTime WasBeUpdated { get; set; }
        [DisplayName("Место рождения")]
        string PlaceOfBirth { get; set; }
        byte[] Image { get; set; }
        List<PersonAddressModelView> Addresses { get; set; }
        List<PersoneDocumentModelView> Documents { get; set; }

        List<PhoneViewModel> Phones { get; set; }
    }

    public class PersoneViewModel
    {
        [Browsable(false)]
        public int UserID { get; set; }

        [Browsable(false)]
        public int PositionID { get; set; }

        [Browsable(false)]
        public bool IsEmploeyr { get; set; }


        [DisplayName("Фамилия")]
        public string FirstName { get; set; }

        [DisplayName("Имя")]
        public string SecondName { get; set; }

        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime DateBirthday { get; set; }
        [DisplayName("Место рождения")]
        public string PlaceOfBirth { get; set; }

        [DisplayName("Дата создания")]
        public DateTime WasBeCreated { get; set; }

        [DisplayName("Дата обновления")]
        public DateTime WasBeUpdated { get; set; }
        [DisplayName("Место работы")]
        public string PlaceWork { get; set; }
        public List<PersoneDocumentModelView> Documents { get; set; }

        public List<PhoneViewModel> Phones { get; set; }

        public List<PersonAddressModelView> Addresses { get; set; }

        public byte[] Image { get; set; } = new byte[0];

        private IPersonePositionRepository _personPositionRepository;
        private IPersoneAddressRepository _personAddressRepository;
        private IPhonesRepository _phonesRepository;
        private IDocumentRepository _documentsRepository;
        private IDocumentTypeRepository _documentsTypeRepository;

        public PersoneViewModel(Persone persone) : this(RepositoryesFactory.GetInstance().Get<IPersoneAddressRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IPhonesRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IPersonePositionRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IDocumentRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IDocumentTypeRepository>()
                                      )
        {
            FirstName = persone.FirstName;
            SecondName = persone.SecondName;
            MiddleName = persone.MiddleName;
            DateBirthday = persone.DateBirthday;
            PlaceOfBirth = persone.PlaceOfBirth;
            WasBeCreated = persone.WasBeCreated;
            WasBeUpdated = persone.WasBeUpdated;
            PlaceWork = persone.PlaceWork;
            Image = persone.Image;


            Phones = new List<PhoneViewModel>();
            Documents = new List<PersoneDocumentModelView>();
            Addresses = new List<PersonAddressModelView>();

            if(persone.Phones != null)
                foreach(var phone in persone.Phones)
                {
                    Phones.Add(new PhoneViewModel
                    {
                        PhoneID = phone.PhoneID,
                        PhoneNumber = phone.PhoneNumber
                    });
                }

            if(persone.Documents != null)
            {
                foreach(var document in persone.Documents)
                {
                    Documents.Add(new PersoneDocumentModelView
                    {

                    });
                }
            }

            if(persone.Address != null)
            {
                foreach(var address in persone.Address)
                {
                    Addresses.Add(new PersonAddressModelView
                    {
                        Country = address.Country,
                        Subject = address.Subject,
                        Area = address.Area,
                        City = address.City,
                        HomeNumber = address.HomeNumber,
                        Flat = address.Flat,
                        Street = address.Street,
                        Note = address.Note
                    });
                }
            }
        }

        public PersoneViewModel():this(RepositoryesFactory.GetInstance().Get<IPersoneAddressRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IPhonesRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IPersonePositionRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IDocumentRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IDocumentTypeRepository>()
                                      )
        { }
        
        public PersoneViewModel(IPersoneAddressRepository addressRepository, 
                                IPhonesRepository phoneRepository, 
                                IPersonePositionRepository positionRepository,
                                IDocumentRepository documentRepository,
                                IDocumentTypeRepository documentTypeRepository)
        {
            _personAddressRepository = addressRepository;
            _phonesRepository = phoneRepository;
            _personPositionRepository = positionRepository;

            _documentsRepository = documentRepository;
            _documentsTypeRepository = documentTypeRepository;
        }

    }
}
