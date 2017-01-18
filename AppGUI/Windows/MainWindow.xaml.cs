﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFPresentators;
using WPFPresentators.Views.Controls;
using WPFPresentators.Views.Windows;

namespace AppGUI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Login != null) Login();
        }

        public event Action BackPage;
        public event Action Login;
        public event Action NextPage;

        public void Show()
        {
            base.Show();

            if(AppConfig.CurrentManager == null && Login != null)
            {
                Login();
            }
        }

        public void LoadEnd()
        {
            throw new NotImplementedException();
        }

        public void LoadStart()
        {
            throw new NotImplementedException();
        }

        public void SetCenterPanel(IControlView view)
        {
            throw new NotImplementedException();
        }

        public void SetMainMenu(IControlView view)
        {
            throw new NotImplementedException();
        }

        public void SetTopMenu(IControlView view)
        {
            throw new NotImplementedException();
        }

        public void ShowError(string message)
        {
            throw new NotImplementedException();
        }
    }
}
