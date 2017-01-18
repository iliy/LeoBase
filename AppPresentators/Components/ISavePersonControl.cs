using AppData.Entities;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface ISavePersonControl:UIComponent
    {
        event Action SavePersone;
        IPersoneViewModel Persone { get; set; }
        void ShowMessage(string message);
    }
}
