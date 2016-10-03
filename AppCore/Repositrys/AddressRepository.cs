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
    public class AddressRepository : IPersoneAddressRepository
    {
        public IQueryable<PersoneAddress> Addresses
        {
            get
            {
                var context = new LeoBaseContext();

                return context.Addresses;
            }
        }

        public int AddAdress(PersoneAddress address)
        {
            using(var db = new LeoBaseContext())
            {
                db.Addresses.Add(address);
                db.SaveChanges();
            }
            return 0;
        }

        public bool Remove(int id)
        {
            using(var db = new LeoBaseContext())
            {
                var address = db.Addresses.FirstOrDefault(a => a.AddressID == id);
                if(address != null)
                {
                    db.Addresses.Remove(address);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public int RemoveUserAddresses(int userid)
        {
            using(var db = new LeoBaseContext())
            {
                var addresses = db.Addresses.Where(a => a.Persone.UserID == userid);

                int result = addresses.Count();

                db.Addresses.RemoveRange(addresses);

                db.SaveChanges();

                return result;
            }
        }
    }
}
