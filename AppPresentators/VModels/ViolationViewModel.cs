using AppPresentators.VModels.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class ViolationViewModel
    {
        [Browsable(false)]
        public int ViolationID { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Северная широта(N)")]
        public double N { get; set; }
        [DisplayName("Восточная долгота(E)")]
        public double E { get; set; }

        [DisplayName("Возбуждено дело")]
        [Browsable(false)]
        public string ViolationType { get; set; } = "Административное";
        public List<ProtocolViewModel> Protocols { get; set; }
        public List<byte[]> Images { get; set; }
    }
}
