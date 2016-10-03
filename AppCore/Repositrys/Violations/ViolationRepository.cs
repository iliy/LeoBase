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
    public class ViolationRepository : IViolationRepository
    {
        public IQueryable<Violation> Violations
        {
            get
            {
                var context = new LeoBaseContext();
                return context.Violations;
            }
        }
    }
}
