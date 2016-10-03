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
    public class ManagersRepository : IManagersRepository
    {
        public IQueryable<Manager> Managers
        {
            get
            {
                var db = new LeoBaseContext();
                return db.Managers;
            }
        }
    }
}
