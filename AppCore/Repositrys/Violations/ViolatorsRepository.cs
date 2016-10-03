using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys.Violations
{
    public class ViolatorsRepository : IViolatorRepository
    {
        public IQueryable<Violator> Violators
        {
            get
            {
                var db = new LeoBaseContext();
                return db.Violators;
            }
        }

        public int SaveViolator(Violator violator)
        {
            using(var db = new LeoBaseContext())
            {
                db.Violators.Add(violator);
                db.SaveChanges();
                return violator.ViolatorID;
            }
        }
    }
}
