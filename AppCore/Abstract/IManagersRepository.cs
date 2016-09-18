using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract
{
    public interface IManagersRepository
    {
        IQueryable<Manager> Managers { get; }
    }
}
