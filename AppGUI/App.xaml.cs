using AppGUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFPresentators.Presentators;
using WPFPresentators.Views.Windows;

namespace AppGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ComponentsFactory factory = new ComponentsFactory();

            MainPresentator mainPresentator = new MainPresentator(factory);
        }
    }
}
