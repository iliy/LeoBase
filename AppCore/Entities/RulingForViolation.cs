using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Постановление по делу об административном правонарушение
    /// </summary>
    public class RulingForViolation:IProtocol
    {
        [Key]
        public int RulingForViolationID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Protocol Protocol { get; set; }
        public int ViolatorDocumentID { get; set; }
        public int OrganisationID { get; set; }
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
