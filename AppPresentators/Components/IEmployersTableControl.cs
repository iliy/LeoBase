using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public delegate void EditPersone(IPersoneViewModel persone);
    public delegate void DeletePersone(IPersoneViewModel persone);
    public delegate void ShowPersoneDetails(IPersoneViewModel persone);

    public interface IEmployersTableControl:UIComponent
    {
        event Action UpdateTable;
        event Action AddNewEmployer;
        event Action<EmploeyrViewModel> SelectedItemForResult;
        event ShowPersoneDetails ShowPersoneDetails;
        event EditPersone EditPersone;
        event DeletePersone DeletePersone;
        PageModel PageModel { get; set; }

        PersonsSearchModel PersoneSearchModel { get; }
        DocumentSearchModel DocumentSearchModel { get; }
        SearchAddressModel AddressSearchModel { get; }

        PersonsOrderModel OrderModel { get; set; }

        List<EmploeyrViewModel> Data { get; set; }

        void Update();

        void LoadStart();
        void LoadEnd();
    }
}
