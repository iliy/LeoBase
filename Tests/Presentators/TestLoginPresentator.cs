using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AppPresentators.Services;
using AppPresentators.Views;
using AppPresentators.Presentators;
using AppPresentators.VModels;

namespace Tests.Presentators
{
    [TestClass]
    public class TestLoginPresentator
    {
        [TestMethod]
        public void TestValidMangerLogin()
        {
            Mock<ILoginService> service = new Mock<ILoginService>();
            Mock<IMainView> mainView = new Mock<IMainView>();
            Mock<ILoginView> loginView = new Mock<ILoginView>();
            LoginPresentator target = null;
            VManager manager = new VManager
            {
                Login = "login",
                Password = "password",
                ManagerID = 1,
                Role = "admin"
            };
            
            service.Setup(s => s.Login(
                        It.Is<string>(login => login.Equals("login")),
                        It.Is<string>(password => password.Equals("password"))
                        ))
                        .Returns(manager);

            loginView.Setup(s => s.Close());

            target = new LoginPresentator(mainView.Object, service.Object, loginView.Object);

            target.Login("login", "password");
            
            service.Verify(s => s.Login(It.IsAny<string>(), It.IsAny<string>()));

            loginView.Verify(lv => lv.Close(), Times.Once);

            loginView.Verify(lv => lv.ShowError(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void TestInvalidMangerLogin()
        {
            Mock<ILoginService> service = new Mock<ILoginService>();
            Mock<IMainView> mainView = new Mock<IMainView>();
            Mock<ILoginView> loginView = new Mock<ILoginView>();
            LoginPresentator target = null;
            VManager manager = new VManager
            {
                Login = "login",
                Password = "password",
                ManagerID = 1,
                Role = "admin"
            };

            service.Setup(s => s.Login(
                        It.Is<string>(login => login.Equals("login2")),
                        It.Is<string>(password => password.Equals("password2"))
                        ))
                        .Returns(manager);

            loginView.Setup(s => s.Close());

            target = new LoginPresentator(mainView.Object, service.Object, loginView.Object);
            
            target.Login("login", "password");

            service.Verify(s => s.Login(It.IsAny<string>(), It.IsAny<string>()));

            loginView.Verify(lv => lv.Close(), Times.Never);

            loginView.Verify(lv => lv.ShowError(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void TestSaveManagerToMainForm()
        {
            Mock<ILoginService> service = new Mock<ILoginService>();
            Mock<IMainView> mainView = new Mock<IMainView>();
            Mock<ILoginView> loginView = new Mock<ILoginView>();
            LoginPresentator target = null;
            VManager manager = new VManager
            {
                Login = "login",
                Password = "password",
                ManagerID = 1,
                Role = "admin"
            };

            mainView.SetupSet(mV => mV.Manager = It.Is<VManager>(m => m.Equals(manager))).Verifiable();

            service.Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(manager);

            loginView.Setup(s => s.Close());

            target = new LoginPresentator(mainView.Object, service.Object, loginView.Object);

            target.Login("login", "password");

            Assert.AreEqual(mainView.Object.Manager.Login, "login");
            //mainView.VerifySet(mv => mv.Manager.Login = manager.Login);
        }
    }
}
