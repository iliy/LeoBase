using AppData.Abstract.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys.Violations.Protocols
{
    public class ProtocolAboutViolationPersoneRepository : IProtocolAboutViolationPersoneRepository
    {
        public IQueryable<ProtocolAboutViolationPersone> ProtocolsAboutViolationPersone
        {
            get
            {
                var db = new LeoBaseContext();
                return db.ProtocolsAboutViolationPersone;
            }
        }
    }
}
