using AppData.Entities;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Protocols
{
    /// <summary>
    /// Протокол о доставление
    /// </summary>
    public class ProtocolAboutBringingViewModel:ProtocolViewModel, IProtocol
    {
        public int ProtocolAboutBringingID { get; set; }

        public PersoneDocumentModelView ViolatorDocument { get; set; }
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
        /// Изъятое оружие
        /// </summary>
        public string WithdrawWeapons { get; set; }
        /// <summary>
        /// Изъятые боеприпасы
        /// </summary>
        public string WithdrawAmmunitions { get; set; }
        /// <summary>
        /// Изъятые орудия охоты и рыболовства
        /// </summary>
        public string WithdrawGunsHuntingAndFishing { get; set; }
        /// <summary>
        /// Изъятая продукция природопользования
        /// </summary>
        public string WithdrawNatureManagementProducts { get; set; }
        /// <summary>
        /// Изъятые документы
        /// </summary>
        public string WithdrawDocuments { get; set; }

        /// <summary>
        /// Методы фиксации правонарушения
        /// </summary>
        public string FixingMethods { get; set; }

        public Protocol Protocol { get; set; }
    }
}
