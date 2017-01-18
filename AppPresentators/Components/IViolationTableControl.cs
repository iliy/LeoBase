using AppPresentators.Services;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public enum ViolationActionType
    {
        UPDATE = 1,
        DETAILS = 2,
        REMOVE = 3,
        SHOW_ON_MAP = 4,
        ADD_NEW_VIOLATION = 5
    }

    public delegate void ViolationAction(ViolationActionType type, ViolationViewModel violation);

    public interface IViolationTableControl:UIComponent
    {
        event Action AddNewViolation;
        event ViolationAction RemoveViolation;
        event ViolationAction UpdateViolation;
        event ViolationAction ShowDetailsViolation;
        event Action UpdateData;
        ViolationSearchModel SearchModel { get; }
        ViolationOrderModel OrderModel { get; }
        PageModel PageModel { get; set; }
        List<ViolationViewModel> Violations { get; set; }
        void ShowMessage(string message);

        void LoadStart();
        void LoadEnd();
    }
}
