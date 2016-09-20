﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppPresentators.Infrastructure;
using AppPresentators.Views;
using Moq;
using AppPresentators.Presentators.Interfaces;
using AppPresentators.Presentators;
using LeoBase.Infrastructure;
using Ninject;
using AppPresentators.Services;
using AppPresentators.VModels;

namespace Tests.Presentators
{
    [TestClass]
    public class TestMainPresentator
    {
        private IApplicationFactory _appFactory;
        private IMainPresentator _mainPresentator;
        private Mock<IMainView> _mockMainView;
        private Mock<ILoginView> _mockLoginView;
        private Mock<ILoginPresentator> _mockLoginPresentator;
        private Mock<ILoginService> _mockLoginService;
        private IKernel _ninjectKernel;

        [TestInitialize]
        public void Initialize()
        {
            InitMock();

            InitMockMethodAndProperties();

            InitNinjectKernel();   
           
            _appFactory = new ApplicationFactory(_ninjectKernel);

            _mainPresentator = new MainPresentator(_mockMainView.Object, _appFactory);
        }

        private void InitNinjectKernel()
        {
            _ninjectKernel = new StandardKernel();

            _ninjectKernel.Bind<IMainView>().ToConstant(_mockMainView.Object);
            _ninjectKernel.Bind<ILoginView>().ToConstant(_mockLoginView.Object);
            _ninjectKernel.Bind<ILoginPresentator>().ToConstant(_mockLoginPresentator.Object);
            _ninjectKernel.Bind<ILoginService>().ToConstant(_mockLoginService.Object);
        }

        private void InitMockMethodAndProperties()
        {
            _mockMainView.Setup(m => m.ShowError(It.IsAny<string>()));
            _mockMainView.Setup(m => m.Show());
            _mockMainView.Setup(m => m.Close());
            _mockMainView.SetupAllProperties();
            
            _mockLoginView.SetupAllProperties();
            _mockLoginView.Setup(m => m.Show());

            _mockLoginService.Setup(m => m.Login(It.Is<string>(s => s.Equals("admin")), It.Is<string>(s => s.Equals("admin"))))
                .Returns(new VManager
                {
                    ManagerID = 1,
                    Login = "admin",
                    Password = "password",
                    Role = "admin"
                });

            _mockLoginPresentator.Setup(lp => lp.Login(It.IsAny<string>(), It.IsAny<string>()));
            _mockLoginPresentator.Setup(lp => lp.Run());
        }

        private void InitMock()
        {
            _mockMainView = new Mock<IMainView>();
            _mockLoginView = new Mock<ILoginView>();
            _mockLoginPresentator = new Mock<ILoginPresentator>();
            _mockLoginService = new Mock<ILoginService>();
        }

        [TestMethod]
        public void TestShowMainView()
        {
            _mainPresentator.Run();

            _mockMainView.Verify(m => m.Show());
        }

        [TestMethod]
        public void TestLoginOnFirstOpenMainView()
        {
            _mainPresentator.Run();

            _mainPresentator.Login();

            _mockMainView.Verify(m => m.Show());

            _mockLoginPresentator.Verify(m => m.Run());
        }
    }
}
