﻿using AppPresentators.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Views
{
    public interface IView
    {
        void Show();
        void Close();
    }
}
