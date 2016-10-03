using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Протокол об осмотре принадлежащих юридическому лицу или индивидуальному предпринимателю помещений, территорий
    /// и находящихся там вещей и документов
    /// </summary>
    public class ProtocolAboutInspectionOrganisation:IProtocol
    {
        [Key]
        public int ProtocolAboutInspectionOrganisationID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Protocol Protocol { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int OrganisationID { get; set; }
        /// <summary>
        /// Осмотренные территории и помещения
        /// </summary>
        public string InspectionTerritoryes { get; set; }
        /// <summary>
        /// Найденное оружие
        /// </summary>
        public string FindedWeapons { get; set; }
        /// <summary>
        /// Найденные боеприпасы
        /// </summary>
        public string FindedAmmunitions { get; set; }
        /// <summary>
        /// Найденые орудия для охоты и рыболовства 
        /// </summary>
        public string FindedGunsHuntingAndFishing { get; set; }
        /// <summary>
        /// Найденная продукция природопользования
        /// </summary>
        public string FindedNatureManagementProducts { get; set; }
        /// <summary>
        /// Найденные документы
        /// </summary>
        public string FindedDocuments { get; set; }

        /// <summary>
        /// Методы фиксации правонарушения
        /// </summary>
        public string FixingMethods { get; set; }
    }
}
