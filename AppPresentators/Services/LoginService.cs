using AppData.Abstract;
using AppData.Entities;
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
        VManager Login(string login, string password);
    }

    public class TestLoginService : ILoginService
    {
        private IManagersRepository _managersRepository;
        public TestLoginService()
        {
            _managersRepository = RepositoryesFactory.GetInstance().Get<IManagersRepository>();
        }

        public TestLoginService(IManagersRepository managerRepository)
        {
            _managersRepository = managerRepository;
        }

        public VManager Login(string login, string password)
        {
            var manager = _managersRepository.Managers.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
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
