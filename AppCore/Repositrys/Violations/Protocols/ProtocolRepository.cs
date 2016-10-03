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
    public class ProtocolRepository : IProtocolRepository
    {
        public IQueryable<Protocol> Protocols
        {
            get
            {
                var db = new LeoBaseContext();
                return db.Protocols;
            }
        }

        public int SaveProtocol(Protocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                db.Protocols.Add(protocol);
                db.SaveChanges();

                return protocol.ProtocolID;
            }
        }
    }
}
