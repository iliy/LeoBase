using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Определение о возбуждение дела об административном правонарушение
    /// и проведение административного расследования
    /// </summary>
    public class DefinitionAboutViolation: IProtocol
    {
        [Key]
        public int DefinitionAboutViolationID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Protocol Protocol { get; set; }
        public int ViolatorDocumentID { get; set; }
        public int OrganisationID { get; set; }
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
        /// Статья КОАП
        /// </summary>
        public string KOAP { get; set; }

        /// <summary>
        /// Методы фиксации правонарушения
        /// </summary>
        public string FixingMethods { get; set; }
    }
}
