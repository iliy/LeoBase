using AppData.Abstract;
using AppData.Infrastructure;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Services
{
    public interface ILoginService
    {
        IVManager Login(string login, string password);
    }

    public class TestLoginService : ILoginService
    {
        public IVManager Login(string login, string password)
        {
            var managers = RepositoryesFactory.Get<IManagersRepository>().Managers;
            var manager = managers.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
            if (manager == null)
                return null;
            return new VManager
            {
                ManagerID = manager.ManagerID,
                Login = manager.Login,
                Password = manager.Password,
                Role = manager.Role
            };
        }
    }
}
