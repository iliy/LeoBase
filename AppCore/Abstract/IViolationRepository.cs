﻿using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract
{
    public interface IViolationRepository
    {
        IQueryable<Violation> Violations { get; }
        bool AddViolation(Violation violation);
        bool UpdateViolation(Violation violation);
    }
}
