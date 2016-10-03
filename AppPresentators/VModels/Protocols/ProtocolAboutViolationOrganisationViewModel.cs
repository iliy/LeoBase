using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Protocols
{
    /// <summary>
    /// Протокол об административном правонарушение для юридического лица
    /// </summary>
    public class ProtocolAboutViolationOrganisationViewModel:ProtocolViewModel
    {
        public int ProtocolAboutViolationOrganisationID { get; set; }
        public OrganisationViewModel Organisation { get; set; }
        public DateTime ViolationTime { get; set; }
        public string KOAP { get; set; }
    }
}
