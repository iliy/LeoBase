using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract
{
    public interface IPersoneRepository
    {
        IQueryable<Persone> Persons { get; }
        int Count { get; }
        int AddPersone(Persone persone);
        bool Remove(int id);
        bool Update(Persone persone);
        bool SimpleRemove(int id);
    }
}
