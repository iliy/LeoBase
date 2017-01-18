using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface ISaveAdminViolationControl: UIComponent
    {
        event Action Save;

        event Action FindEmployer;
        event Action FindViolator;

        event Action CreateEmployer;
        event Action CreateViolator;

        AdminViolation Violation { get; set; }

        void Update();
        void UpdateEmployer();
        void UpdateViolator();
        void ShowMessage(string message);
    }
}
