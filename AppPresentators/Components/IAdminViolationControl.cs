using AppPresentators.Services;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Components
{
    public delegate void ViolationEvent(int id);
    public interface IAdminViolationControl:UIComponent
    {
        event Action AddViolation;
        event ViolationEvent RemoveViolation;
        event ViolationEvent ShowDetailsViolation;
        event ViolationEvent EditViolation;
        event Action UpdateTable;
        event Action BuildReport;

        AdminViolationSearchModel SearchModel { get; }
        AdminViolationOrderModel OrederModel { get; }
        PageModel PageModel { get; set; }

        List<AdminViolationRowModel> DataContext { get; set; }
        DialogResult ShowConfitmDialog(string message, string title = null);

        void LoadStart();
        void LoadEnd();
    }
}
