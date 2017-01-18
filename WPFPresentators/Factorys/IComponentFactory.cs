using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentators.Presentators;
using WPFPresentators.Services;
using WPFPresentators.Views.Controls;
using WPFPresentators.Views.Windows;

namespace WPFPresentators.Factorys
{
    public interface IComponentFactory
    {
        T GetWindow<T>() where T : IWindowView;
        T GetControl<T>() where T : IControlView;
        T GetPresentator<T>();
        T GetService<T>();
    }
}
