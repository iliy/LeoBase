using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces.ComponentPresentators
{
    public interface ISaveEmployerPresentator:IComponentPresentator
    {
        void SavePersone(IPersoneViewModel employer);
        IPersoneViewModel Persone { get; set; }
    }
}
