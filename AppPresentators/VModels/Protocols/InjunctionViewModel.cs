using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Protocols
{
    /// <summary>
    /// Предписание об устранение нарушений законодательства в сфере
    /// природопользования и охраны окружающей среды
    /// </summary>
    public class InjunctionViewModel:ProtocolViewModel
    {
        public int InjunctionID { get; set; }
        public PersoneViewModel Violator { get; set; }
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
        public virtual List<InjunctionItemViewModel> InjuctionsItem { get; set; }
    }

    /// <summary>
    /// Пункт предписания
    /// </summary>
    public class InjunctionItemViewModel
    {
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
