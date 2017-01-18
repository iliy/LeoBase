using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using AppPresentators.VModels.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IPersoneDetailsControl: UIComponent
    {
        IPersoneViewModel Persone { get; set; }
        List<ViolationViewModel> Violations { get; set; }
        List<ProtocolViewModel> Protocols { get; set; }
        Dictionary<int, string> Positions { get; set; }
    }
}
