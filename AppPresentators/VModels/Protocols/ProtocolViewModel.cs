using AppData.Entities;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Protocols
{
    public class ProtocolViewModel: Protocol
    {
        public int ProtocolID { get; set; }

        /// <summary>
        /// Тип протокол
        /// </summary>
        public int ProtocolTypeID { get; set; }

        /// <summary>
        /// Название протокола
        /// </summary>
        public string ProtocolTypeName { get; set; }

        /// <summary>
        /// Сотрудник, составивший протокол
        /// </summary>
        public EmploeyrViewModel Employer { get; set; }

        public DateTime DateCreatedProtocol { get; set; }
        public string PlaceCreatedProtocol { get; set; }
        public double PlaceCreatedProtocolN { get; set; }
        public double PlaceCreatedProtocolE { get; set; }

        public string WitnessFIO_1 { get; set; }
        public string WitnessLive_1 { get; set; }
        public string WitnessFIO_2 { get; set; }
        public string WitnessLive_2 { get; set; }
        
    }
}
