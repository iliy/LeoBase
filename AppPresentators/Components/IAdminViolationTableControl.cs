using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public delegate void ViolationEvent(int id);

    public interface IAdminViolationTableControl: UIComponent
    {
        event Action AddNewAction;

        event ViolationEvent RemoveViolation;
        event ViolationEvent ShowDetails;
        event ViolationEvent EditViolation;


    }
}
