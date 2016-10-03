using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Предписание об устранение нарушений законодательства в сфере
    /// природопользования и охраны окружающей среды
    /// </summary>
    public class Injunction:IProtocol
    {
        [Key]
        public int InjunctionID { get; set; }
        public Protocol Protocol { get; set; }
        public int ViolatorDocumentID { get; set; }
        /// <summary>
        /// Дата акта проверки
        /// </summary>
        public DateTime ActInspectionDate { get; set; }
        /// <summary>
        /// Номер акта проверки
        /// </summary>
        public string ActInspectionNumber { get; set; }
        /// <summary>
        /// Что предписывает
        /// </summary>
        public string InjunctionInfo { get; set; }
        /// <summary>
        /// Пункты предписания
        /// </summary>
        public virtual List<InjunctionItem> InjuctionsItem { get; set; }
    }
    /// <summary>
    /// Пункт предписания
    /// </summary>
    public class InjunctionItem
    {
        [Key]
        public int InjunctionItemID { get; set; }
        /// <summary>
        /// Содержание пункта предписания
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Срок выполнения предписания
        /// </summary>
        public string Deedline { get; set; }
        /// <summary>
        /// Основание предписания
        /// </summary>
        public string BaseOrders { get; set; }
    }
}
