using AppPresentators.Services;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces.ComponentPresentators
{
    public interface IViolationTablePresentator:IComponentPresentator
    {
        void Update(ViolationSearchModel search, ViolationOrderModel order, PageModel page);
    }
}
