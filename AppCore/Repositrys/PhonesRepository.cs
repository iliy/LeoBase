using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys
{
    public class PhonesRepository : IPhonesRepository
    {
        public IQueryable<Phone> Phones
        {
            get
            {
                var db = new LeoBaseContext();

                return db.Phones;
            }
        }

        public int AddPhone(Phone phone)
        {
            using(var db = new LeoBaseContext())
            {
                var p = db.Phones.Add(phone);

                db.SaveChanges();

                return p.PhoneID;
            }
        }

        public bool Remove(int id)
        {
            using(var db = new LeoBaseContext())
            {
                var phone = db.Phones.FirstOrDefault(p => p.PhoneID == id);
                if(phone != null)
                {
                    db.Phones.Remove(phone);

                    db.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public int RemoveAllUserPhones(int userid)
        {
            using(var db = new LeoBaseContext())
            {
                var phones = db.Phones.Where(p => p.Persone.UserID == userid);

                var count = phones.Count();

                db.Phones.RemoveRange(phones);

                db.SaveChanges();

                return count;
            }
        }
    }
}
