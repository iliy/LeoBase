using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.MainMenu
{
    public class MenuItemModel
    {
        public string Title { get; set; }
        public MenuCommand MenuCommand { get; set; }
        public byte[] Icon { get; set; }
    }
}
