using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface ITestComponent:UIComponent
    {
        string Title { get; set; }
    }
}
