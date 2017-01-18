using BFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentators.Services
{
    public interface ILoginService:IService
    {
        Manager Login(string login, string password);
    }

    public class LoginService:ILoginService
    {
        public Manager Login(string login, string password)
        {
            using(var db = new DataBaseDataContext())
            {
                return db.Managers.FirstOrDefault(m => m.Login.Equals(login) && m.Password.Equals(password));
            }
        }
    }
}
