using AppData.Abstract;
using AppData.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class PersoneViewModel
    {
        [ReadOnly(true)]
        public int UserID { get; set; }
        public int PositionID { get; set; }
        public bool IsEmploeyr { get; set; }
        [DisplayName("Фамилия")]
        public string FirstName { get; set; }
        [DisplayName("Имя")]
        public string SecondName { get; set; }
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
        [DisplayName("Дата рождения")]
        public DateTime DateBirthday { get; set; }

        public List<string> _phones;

        public List<string> Phones
        {
            get
            {
                if (_phones != null) return _phones;

                if (_phonesRepository == null) return null;

                var phones = _phonesRepository.Phones
                                              .Where(p => p.UserID == UserID);

                if (phones == null) return new List<string>();

                return phones.Select(p => p.PhoneNumber).ToList();
            }
            set
            {
                _phones = value;
            }
        }

        private List<PersonAddressModelView> _addresses;

        public List<PersonAddressModelView> Addresses
        {
            get
            {
                if (_addresses != null) return _addresses;

                if (_personAddressRepository == null) return null;

                var adresses = _personAddressRepository.Addresses
                                                       .Where(a => a.UserID == UserID);

                if (adresses == null) return new List<PersonAddressModelView>();

                return adresses.Select(a => new PersonAddressModelView
                {
                    AddressID = a.AddressID,
                    Country = a.Country,
                    Subject = a.Subject,
                    Area = a.Area,
                    City = a.City,
                    HomeNumber = a.HomeNumber,
                    Flat = a.Flat,
                    Note = a.Note
                }).ToList();
            }
            set
            {
                _addresses = value;
            }
        }

        private string _position;

        public string Position
        {
            get
            {
                if (!string.IsNullOrEmpty(_position)) return _position;

                if (_personPositionRepository == null) return null;

                var position = _personPositionRepository.Positions
                                                        .FirstOrDefault(p => p.PositionID == PositionID);

                if (position == null) return "";

                return position.Name;
            }
            set
            {
                _position = value;
            }
        }

        private IPersonePositionRepository _personPositionRepository;
        private IPersoneAddressRepository _personAddressRepository;
        private IPhonesRepository _phonesRepository;

        public PersoneViewModel():this(RepositoryesFactory.GetInstance().Get<IPersoneAddressRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IPhonesRepository>(),
                                       RepositoryesFactory.GetInstance().Get<IPersonePositionRepository>()
                                       )
        {}
        
        public PersoneViewModel(IPersoneAddressRepository addressRepository, IPhonesRepository phoneRepository, IPersonePositionRepository positionRepository)
        {
            _personAddressRepository = addressRepository;
            _phonesRepository = phoneRepository;
            _personPositionRepository = positionRepository;
        }

        public void Refresh()
        {
            _position = null;
        }
    }
}
