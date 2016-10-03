using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Protocols
{
    /// <summary>
    /// Протокол об изъятии
    /// </summary>
    public class ProtocolAboutWithdrawViewModel:ProtocolViewModel
    {
        public int ProtocolAboutWithdrawID { get; set; }
        public PersoneViewModel Violator { get; set; }
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
    }
}
