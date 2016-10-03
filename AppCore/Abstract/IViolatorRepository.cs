using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract
{
    public interface IViolatorRepository
    {
        IQueryable<Violator> Violators { get; }
        int SaveViolator(Violator violator);
    }
}
