using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract
{
    public interface IPersoneAddressRepository
    {
        IQueryable<PersoneAddress> Addresses { get; }
        bool Remove(int id);
        int RemoveUserAddresses(int userid);
        int AddAdress(PersoneAddress address);
    }
}
