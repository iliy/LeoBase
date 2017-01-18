using AppData.Entities;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IPassesTableControl:UIComponent
    {
        List<Pass> DataSource { get; set; }
        PassesOrderModel OrderModel { get; set; }
        PassesSearchModel SearchModel { get; set; }
        PageModel PageModel { get; set; }

        event Action<int> ShowDetailsPass;
        event Action<int> RemovePass;
        event Action<object> MakeReport;
        event Action<int> EditPass;
        event Action AddPass;
        event Action UpdateTable;

        void ShowMessage(string text, string title);
        void ShowError(string text);
        bool ShowDialog(string text);
    }
}
