using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Протокол об административном правонарушение для юридического лица
    /// </summary>
    public class ProtocolAboutViolationOrganisation:IProtocol
    {
        [Key]
        public int ProtocolAboutViolationOrganisationID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int OrganisationID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Protocol Protocol { get; set; }
        public DateTime ViolationTime { get; set; }
        public string KOAP { get; set; }
        public string Description { get; set; }
    }
}
