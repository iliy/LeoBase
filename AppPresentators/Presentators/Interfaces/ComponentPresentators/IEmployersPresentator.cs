using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces.ComponentPresentators
{
    public interface IEmployersPresentator:IComponentPresentator
    {
        void GetPersones(PageModel pageModel, 
                         PersonsSearchModel searchModel, 
                         SearchAddressModel addressSearchModel,
                         PersonsOrderModel orderModel, 
                         DocumentSearchModel documentSearchModel);

    }
}
