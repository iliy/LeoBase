using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces.ComponentPresentators
{
    public interface IPersoneDetailsPresentator:IComponentPresentator
    {
        IPersoneViewModel Persone { get; set; }
        void UpdatePersone();
    }
}
