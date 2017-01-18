using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces.ComponentPresentators
{
    public interface IAddOrUpdateViolation:IComponentPresentator
    {
        ViolationViewModel Violation { get; set; }
        void SaveViolation(ViolationViewModel violation);
    }
}
