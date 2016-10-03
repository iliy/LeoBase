using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    /// <summary>
    /// Протокол об аресте товаров, транспортных средств и иных вещей,
    /// явившихся орудиями совершения или предметами административного правонарушения
    /// </summary>
    public class ProtocolAboutArrest: IProtocol
    {
        [Key]
        public int ProtocolAboutArrestID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int ViolatorDocumentID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Protocol Protocol { get; set; }

        /// <summary>
        /// Сведения о лице, во владение которого находятся веши, на которые наложен арест
        /// </summary>
        public string AboutViolator { get; set; }

        /// <summary>
        /// Сведения о транспортном средстве
        /// </summary>
        public string AboutCar { get; set; }

        /// <summary>
        /// Сведения о других вещах
        /// </summary>
        public string AboutOtherThings { get; set; }

        /// <summary>
        /// Куда были переданы арестованые вещи
        /// </summary>
        public string ThingsWasTransfer { get; set; }

        /// <summary>
        /// Методы фиксации правонарушения
        /// </summary>
        public string FixingMethods { get; set; }
    }
}
