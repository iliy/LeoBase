using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components.Protocols
{
    public interface IProtocolAboutViolationView:IProtocolView
    {
        event Action SearchViolator;
        event Action SearchEmployer;

        event Action ChangeViolatorData;
        event Action ChangeEmployerData;

        EmploeyrViewModel Emploer { get; set; }
        PersoneViewModel Violator { get; set; }

        void SetEmployer(IPersoneViewModel employer);
        void SetViolator(IPersoneViewModel violator);
    }
}
