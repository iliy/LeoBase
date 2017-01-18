using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IViolatorTableControl : UIComponent
    {
        event Action UpdateTable;
        event Action StartTask;
        event Action EndTask;
        event Action AddNewEmployer;
        event Action<ViolatorViewModel> SelectedItemForResult;

        event ShowPersoneDetails ShowPersoneDetails;
        event EditPersone EditPersone;
        event DeletePersone DeletePersone;
        PageModel PageModel { get; set; }

        PersonsSearchModel PersoneSearchModel { get; }
        DocumentSearchModel DocumentSearchModel { get; }
        SearchAddressModel AddressSearchModel { get; }

        PersonsOrderModel OrderModel { get; set; }

        List<ViolatorViewModel> Data { get; set; }
        void LoadStart();
        void LoadEnd();
        void Update();
    }
}
