﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Протокол об административном правонарушение для физического лица
    /// </summary>
    public class ProtocolAboutViolationPersone:IProtocol
    {
        [Key]
        public int ProtocolAboutViolationPersoneID { get; set; }
        [Required(AllowEmptyStrings = false)]

        public Protocol Protocol { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int ViolatorDocumentID { get; set; }
        /// <summary>
        /// Дата и время правонарушения
        /// </summary>
        public DateTime ViolationDate { get; set; }
        /// <summary>
        /// Описание правонарушения
        /// </summary>
        public string ViolationDescription { get; set; }
        /// <summary>
        /// Статья КОАП за данное нарушение
        /// </summary>
        public string KOAP { get; set; }
        /// <summary>
        /// Найденная продукция природопользования
        /// </summary>
        public string FindedNatureManagementProducts { get; set; }
        /// <summary>
        /// Найденное оружие
        /// </summary>
        public string FindedWeapons { get; set; }
        /// <summary>
        /// Найденые орудия для охоты и рыболовства 
        /// </summary>
        public string FindedGunsHuntingAndFishing { get; set; }
    }
}