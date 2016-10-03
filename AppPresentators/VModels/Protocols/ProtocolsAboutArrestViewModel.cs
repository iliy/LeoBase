using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Protocols
{
    /// <summary>
    /// Протоклы об аресте
    /// </summary>
    public class ProtocolsAboutArrestViewModel:ProtocolViewModel
    {
        public int ProtocolAboutArrestID { get; set; }

        /// <summary>
        /// Нарушитель
        /// </summary>
        public PersoneViewModel Violator { get; set; }

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
