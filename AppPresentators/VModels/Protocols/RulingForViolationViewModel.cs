using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Protocols
{
    /// <summary>
    /// Постановление по делу об административном правонарушение
    /// </summary>
    public class RulingForViolationViewModel:ProtocolViewModel
    {
        public int RulingForViolationID { get; set; }
        public PersoneViewModel Violator { get; set; }
        public OrganisationViewModel Organisation { get; set; }
        /// <summary>
        /// Номер постановления
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Когда установил
        /// </summary>
        public DateTime FixingDate { get; set; }
        /// <summary>
        /// Что установил
        /// </summary>
        public string FixingInfo { get; set; }
        /// <summary>
        /// Статья КОАП
        /// </summary>
        public string KOAP { get; set; }
        /// <summary>
        /// Штраф
        /// </summary>
        public decimal Fine { get; set; }
        /// <summary>
        /// Ущерб
        /// </summary>
        public decimal Damage { get; set; }
        /// <summary>
        /// Добытая продукция
        /// </summary>
        public string Products { get; set; }
        /// <summary>
        /// Стоимость добытой продукции
        /// </summary>
        public decimal ProductsPrice { get; set; }
        /// <summary>
        /// Решить вопрос об изъятых вещах
        /// </summary>
        public string AboutArrest { get; set; }
        /// <summary>
        /// Банковские реквизиты
        /// </summary>
        public string BankDetails { get; set; }
    }
}
