﻿using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Abstract
{
    public interface IPhonesRepository
    {
        IQueryable<Phone> Phones { get; }
    }
}
