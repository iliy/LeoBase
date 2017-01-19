﻿using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IPassDetailsControl:UIComponent
    {
        Pass Pass { get; set; }
        event Action MakeReport;
    }
}
