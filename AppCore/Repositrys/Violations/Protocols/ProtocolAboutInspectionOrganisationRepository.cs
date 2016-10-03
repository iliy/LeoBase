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
    public class ProtocolAboutInspectionOrganisationRepository : IProtocolAboutInspectionOrganisationRepository
    {
        public IQueryable<ProtocolAboutInspectionOrganisation> ProtocolsAboutInspectionOrganisation
        {
            get
            {
                var db = new LeoBaseContext();
                return db.ProtocolsAboutInspectionOrganisation;
            }
        }
    }
}
