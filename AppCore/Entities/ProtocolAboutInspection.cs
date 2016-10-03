using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Протокол о личном досмотре и досмотре вещей, находящихся при физическом лице
    /// </summary>
    public class ProtocolAboutInspection:IProtocol
    {
        [Key]
        public int ProtocolAboutInspectionID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Protocol Protocol { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int ViolatorDocumentID { get; set; }
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
