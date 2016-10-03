﻿using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract.Protocols
{
    public interface IInjunctionRepository
    {
        IQueryable<Injunction> Injunctions { get; }
    }
}