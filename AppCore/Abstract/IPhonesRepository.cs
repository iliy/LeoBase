using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract
{
    public interface IPhonesRepository
    {
        IQueryable<Phone> Phones { get; }
        int AddPhone(Phone phone);
        bool Remove(int id);
        int RemoveAllUserPhones(int userid);
    }
}
