using AppData.Abstract;
using AppData.Infrastructure;
using AppPresentators.Services;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            PersonesService service = new PersonesService();
            //var a = service.GetPersons();
            service.DocumentSearchModel = new DocumentSearchModel
            {
                DocumentTypeName = "Водительские права"
            };

            var result = service.GetPersons();

            //RepositoryesFactory.Init();
            //IUserRepository test = RepositoryesFactory.Get<IUserRepository>();
            //foreach(var user in test.Users.Where(u => u.UserTypeID == 2))
            //{
            //    Console.WriteLine(string.Format("{0} {1} {2}", user.FirstName, user.SecondName, user.MiddleName));
            //    var violationsRepo = RepositoryesFactory.Get<IViolationRepository>();
            //    var employersRepo = RepositoryesFactory.Get<IEmployerRepository>();
            //    var a = employersRepo.Employers.Where(u => u.UserID == user.UserID).Select(u => u.ViolationID);
            //    var r = violationsRepo.Violations.Where(v => a.Contains(v.ViolationID));
            //    foreach(var aa in r)
            //    {
            //        Console.WriteLine(aa.Description);
            //    }
            //}
            //Console.ReadKey();
        }
    }
}
