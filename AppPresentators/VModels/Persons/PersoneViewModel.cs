using AppData.Abstract;
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
        public int AddressID { get; set; }
        public string Country { get; set; }
        public string Subject { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public string Flat { get; set; }
        public string Note { get; set; }
    }

    public class PersoneDocumentModelView
    {
        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public string Serial { get; set; }
        public string Number { get; set; }
        public string IssuedBy { get; set; }
        public DateTime WhenIssued { get; set; }
        public string CodeDevision { get; set; }
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

    public class PersoneViewModel
    {
        [Browsable(false)]
        public int UserID { get; set; }

        [Browsable(false)]
        public int PositionID { get; set; }

        [ReadOnly(true)]
        [DisplayName("Должность")]
        public string Position { get; set; }

        public bool IsEmploeyr { get; set; }

        [DisplayName("Фамилия")]
        public string FirstName { get; set; }

        [DisplayName("Имя")]
        public string SecondName { get; set; }

        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime DateBirthday { get; set; }

        [DisplayName("Дата создания")]
        public DateTime WasBeCreated { get; set; }

        [DisplayName("Дата обновления")]
        public DateTime WasBeUpdated { get; set; }

        [DisplayName("Место рождения")]
        public string PlaceOfBirth { get; set; }

        [DisplayName("Место работы")]
        public string PlaceOfWork { get; set; }

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
            PlaceOfWork = persone.PlaceWork;
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
