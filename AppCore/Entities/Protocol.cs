using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public interface IProtocol
    {
        Protocol Protocol { get; set; }
    }

    public class Protocol
    {
        [Key]
        public int ProtocolID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int ProtocolTypeID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public DateTime DateCreatedProtocol { get; set; }
        public string PlaceCreatedProtocol { get; set; }
        public double PlaceCreatedProtocolN { get; set; }
        public double PlaceCreatedProtocolE { get; set; }

        public string WitnessFIO_1 { get; set; }
        public string WitnessLive_1 { get; set; }
        public string WitnessFIO_2 { get; set; }
        public string WitnessLive_2 { get; set; }

        public DateTime DateSave { get; set; }
        public DateTime DateUpdate { get; set; }

        public int ViolationID { get; set; }
        public int EmployerID { get; set; }
    }
}
