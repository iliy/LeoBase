using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IViolationDetailsControl: UIComponent
    {
        AdminViolation Violation { get; set; }

        event Action Report;

        event Action<int> ShowViolatorDetails;

        event Action<int> ShowEmployerDetails;
        event Action EditViolation;

        Image GetMap();
    }
}
