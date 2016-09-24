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
    public interface IViolationService
    {
        List<Violation> GetViolations(int page = 0, int itemsOnPage = 30);
        List<Violation> GetViolations(Func<Violation, bool> func, int page = 0, int itemsOnPage = 30);

        List<ViolationType> GetViolationTypes();
        List<ViolationType> GetViolationTypes(Func<ViolationType, bool> func);

        List<User> GetEmployers(int violationID);
        List<User> GetViolators(int violationID); 
    }
    public class ViolationService:IViolationService
    {
        private IViolationRepository _violationsRepository;
        private IViolationTypeRepository _violationsTypeRepository;
        private IEmployerRepository _emploersRepository;
        private IViolatorRepository _violatorsRepository;
        private IUserRepository _usersRepository;

        public ViolationService():this(RepositoryesFactory.GetInstance()){}

        public ViolationService(RepositoryesFactory factory)
        {
            _violationsRepository = factory.Get<IViolationRepository>();
            _violationsTypeRepository = factory.Get<IViolationTypeRepository>();
            _emploersRepository = factory.Get<IEmployerRepository>();
            _violatorsRepository = factory.Get<IViolatorRepository>();
            _usersRepository = factory.Get<IUserRepository>();
        }

        public List<Violation> GetViolations(int page = 0, int itemsOnPage = 30)
        {
            return GetViolations(m => true);
        }

        public List<Violation> GetViolations(Func<Violation, bool> func, int page = 0, int itemsOnPage = 30)
        {
            return _violationsRepository.Violations
                                        .Where(v => func(v))
                                        .Skip(page * itemsOnPage)
                                        .Take(itemsOnPage)
                                        .ToList();
        }

        public List<ViolationType> GetViolationTypes()
        {
            return GetViolationTypes(x => true);
        }

        public List<ViolationType> GetViolationTypes(Func<ViolationType, bool> func)
        {
            return _violationsTypeRepository.ViolationTypes
                                            .Where(vt => func(vt))
                                            .ToList();
        }

        public List<User> GetEmployers(int violationID)
        {
            var usersId = _emploersRepository
                                .Employers
                                .Where(v => v.ViolationID == violationID)
                                .Select(x => x.UserID)
                                .ToArray();

            return _usersRepository.Users.Where(u => usersId.Contains(u.UserID)).ToList();
        }

        public List<User> GetViolators(int violationID)
        {
            var usersId = _violatorsRepository
                                .Violators
                                .Where(v => v.ViolationID == violationID)
                                .Select(x => x.UserID)
                                .ToArray();

            return _usersRepository.Users.Where(u => usersId.Contains(u.UserID)).ToList();
        }
    }
}
