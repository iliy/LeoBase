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
    public class EmployersRepository : IEmployerRepository
    {
        public IQueryable<Employer> Employers
        {
            get
            {
                var db = new LeoBaseContext();
                return db.Employers;
            }
        }

        public int SaveEmployer(Employer employer)
        {
            using(var db = new LeoBaseContext())
            {
                db.Employers.Add(employer);
                db.SaveChanges();
                return employer.EmployerID;
            }
        }
    }
}
