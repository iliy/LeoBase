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
    public class ProtocolAboutBringingRepository : IProtocolAboutBringingRepository
    {
        public IQueryable<ProtocolAboutBringing> ProtocolsAboutBringing
        {
            get
            {
                var db = new LeoBaseContext();
                return db.ProtocolsAboutBringing;
            }
        }

        public int SaveProtocolAboutBringing(ProtocolAboutBringing protocol)
        {
            using(var db = new LeoBaseContext())
            {
                db.ProtocolsAboutBringing.Add(protocol);
                db.SaveChanges();
                return protocol.ProtocolAboutBringingID;
            }
        }
    }
}
