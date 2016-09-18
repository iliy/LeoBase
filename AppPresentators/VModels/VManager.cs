using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public interface IVManager
    {
        string Login { get; set; }
        string Password { get; set; }
        string Role { get; set; }
        int ManagerID { get; set; }
    }
    public class VManager:IVManager
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int ManagerID { get; set; }
    }
}
