using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppData.Abstract;
using Moq;
using System.Collections.Generic;
using AppData.Entities;
using System.Linq;
using AppPresentators.Services;
using AppPresentators.VModels;

namespace Tests.Services
{
    [TestClass]
    public class TestTestLoginService
    {
        private TestLoginService _testLoginService;
        [TestInitialize]
        public void Initialize()
        {
            Mock<IManagersRepository> mockRepository = new Mock<IManagersRepository>();
            mockRepository.Setup(r => r.Managers).Returns(new List<Manager>
            {
                new Manager
                {
                    ManagerID = 1,
                    Login = "admin",
                    Password = "admin",
                    Role = "admin"
                },
                new Manager
                {
                    ManagerID = 2,
                    Login = "manager",
                    Password = "manager",
                    Role = "manager"
                },
                new Manager
                {
                    ManagerID = 3,
                    Login = "user",
                    Password = "user",
                    Role = "user"
                }
            }.AsQueryable());
            _testLoginService = new TestLoginService(mockRepository.Object);
        }

        [TestMethod]
        public void TestTrueAdminLogin()
        {
            IVManager admin = _testLoginService.Login("admin", "admin");

            Assert.IsNotNull(admin);

            Assert.AreEqual(admin.ManagerID, 1);

            Assert.AreEqual(admin.Role, "admin");
        }

        [TestMethod]
        public void TestTrueMangerLogin()
        {
            IVManager admin = _testLoginService.Login("manager", "manager");

            Assert.IsNotNull(admin);

            Assert.AreEqual(admin.ManagerID, 2);

            Assert.AreEqual(admin.Role, "manager");
        }

        [TestMethod]
        public void TestTrueUserLogin()
        {
            IVManager admin = _testLoginService.Login("user", "user");

            Assert.IsNotNull(admin);

            Assert.AreEqual(admin.ManagerID, 3);

            Assert.AreEqual(admin.Role, "user");
        }

        [TestMethod]
        public void TestFalseLogin()
        {
            IVManager nullUser = _testLoginService.Login("noname", "nopassword");

            Assert.IsNull(nullUser);
        }
    }
}
