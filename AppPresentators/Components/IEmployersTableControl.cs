using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IEmployersTableControl:UIComponent
    {
        event Action UpdateTable;
        
        PageModel PageModel { get; set; }

        PersonsSearchModel SearchModel { get; set; }

        PersonsOrderModel OrderModel { get; set; }
        DocumentSearchModel DocumentSearchModel { get; set; }

        List<PersoneViewModel> Data { get; set; }

        void Update();
    }
}
