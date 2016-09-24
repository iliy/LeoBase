using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces.Component
{
    public interface IViolationPagePresentator:IComponenPresentator
    {
        List<VViolation> GetViolations();
        List<VViolation> GetViolations(Func<VViolation, bool> func);
        List<VViolation> GetPage(int page, int pageLimit = 30);
        bool SaveViolation(VViolation violation);
        bool RemoveViolation(VViolation violation);
        bool RemoceViolation(int id);
        event Action ShowMap;
        event Action AddViolator;
        event Action AddEmployer;
        event Action ShowViolationDetails;
        event Action MakeOtchet;
    }
}
