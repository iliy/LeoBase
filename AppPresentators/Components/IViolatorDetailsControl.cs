using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IViolatorDetailsControl: UIComponent
    {
        event Action<int> ShowDetailsViolation;
        ViolatorDetailsModel Violator { get; set; }
    }
}
