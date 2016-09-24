using AppPresentators.Presentators.Interfaces;
using LeoBase.Forms;
using LeoBase.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Настройка ninject для создания абстрактных репозиториев
            ApplicationFactory appFactory = new ApplicationFactory();
            IMainPresentator mainPresentator = appFactory.GetMainPresentator(new MainView(), appFactory);
            mainPresentator.Run();
            //Application.Run(new TestMetroForm());
        }
    }
}
