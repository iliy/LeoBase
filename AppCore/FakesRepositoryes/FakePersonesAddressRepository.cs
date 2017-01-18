using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;

namespace AppData.FakesRepositoryes
{
    public class FakePersonesAddressRepository : IPersoneAddressRepository
    {
        private List<PersoneAddress> _addresses = new List<PersoneAddress>
        {
            new PersoneAddress
                {
                    AddressID = 1,
                    Persone = new Persone
                    {
                        UserID = 1
                    },
                    City = "пгт. Славянка",
                    Street = "Героев-Хасана",
                    HomeNumber = "25",
                    Flat = "27",
                    Note = "Проживает"
                },
                new PersoneAddress
                {
                    AddressID = 2,
                    Persone = new Persone
                    {
                        UserID = 1
                    },
                    City = "пгт.Славянка",
                    Street = "Лазо",
                    HomeNumber = "26",
                    Flat = "53",
                    Note = "Прописка"
                },


                new PersoneAddress
                {
                    AddressID = 3,
                    Persone = new Persone
                    {
                        UserID = 2
                    },
                    City = "с.Барабаш",
                    Street = "Хасанская",
                    HomeNumber = "2",
                    Note = "Прописка"
                },
                new PersoneAddress
                {
                    AddressID = 4,
                    Persone = new Persone
                    {
                        UserID = 2
                    },
                    City = "с.Барабаш",
                    Street = "Космонавтов",
                    HomeNumber = "3",
                    Note = "Проживает"
                },
                new PersoneAddress
                {
                    AddressID = 5,
                    Persone = new Persone
                    {
                        UserID = 3
                    },
                    City = "c.Филиповка",
                    Street = "Гагарина",
                    HomeNumber = "7",
                    Flat = "15"
                },
                new PersoneAddress
                {
                    AddressID = 6,
                    Persone = new Persone
                    {
                        UserID = 4
                    },
                    City = "с.Барабаш",
                    Street = "Моложденая",
                    HomeNumber = "7"
                },
                new PersoneAddress
                {
                    AddressID = 7,
                    Persone = new Persone
                    {
                        UserID = 5
                    },
                    City = "с.Безверхово",
                    Street = "Моложденая",
                    HomeNumber = "7",
                    Flat = "6"
                }
        };

        public IQueryable<PersoneAddress> Addresses
        {
            get
            {
                return _addresses.AsQueryable();
            }
        }

        public int AddAdress(PersoneAddress address)
        {
            int id = Addresses.Max(a => a.AddressID) + 1;

            address.AddressID = id;

            _addresses.Add(address);

            return id;
        }

        public bool Remove(int id)
        {
            var address = Addresses.FirstOrDefault(a => a.AddressID == id);

            if (address == null)
                return false;

            return _addresses.Remove(address);
        }

        public int RemoveUserAddresses(int userid)
        {
            return 0;
            //return _addresses.RemoveAll(a => a.UserID == userid);
        }
    }
}
