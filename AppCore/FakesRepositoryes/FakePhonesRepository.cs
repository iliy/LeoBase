using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;

namespace AppData.FakesRepositoryes
{
    public class FakePhonesRepository : IPhonesRepository
    {
        private List<Phone> _phones = new List<Phone>
            {
                //new Phone
                //{
                //    PhoneID = 1,
                //    UserID = 1,
                //    PhoneNumber = "1100000"
                //},
                //new Phone
                //{
                //    PhoneID = 2,
                //    UserID = 2,
                //    PhoneNumber = "2200000"
                //},
                //new Phone
                //{
                //    PhoneID = 3,
                //    UserID = 2,
                //    PhoneNumber = "3200000"
                //},
                //new Phone
                //{
                //    PhoneID = 4,
                //    UserID = 3,
                //    PhoneNumber = "4300000"
                //},
                //new Phone
                //{
                //    PhoneID = 5,
                //    UserID = 4,
                //    PhoneNumber = "5400000"
                //},
                //new Phone
                //{
                //    PhoneID = 6,
                //    UserID = 5,
                //    PhoneNumber = "6500000"
                //},
                //new Phone
                //{
                //    PhoneID = 7,
                //    UserID = 5,
                //    PhoneNumber = "7500000"
                //}
            };

        public IQueryable<Phone> Phones
        {
            get
            {
                return _phones.AsQueryable();
            }
        }

        public int AddPhone(Phone phone)
        {
            int id = Phones.Max(p => p.PhoneID) + 1;

            phone.PhoneID = id;

            _phones.Add(phone);

            return id;
        }

        public bool Remove(int id)
        {
            var phone = Phones.FirstOrDefault(p => p.PhoneID == id);

            if (phone == null)
                return false;

            return _phones.Remove(phone);
        }

        public int RemoveAllUserPhones(int userid)
        {
            return 0;
            //return _phones.RemoveAll(p => p.UserID == userid);
        }
    }
}
