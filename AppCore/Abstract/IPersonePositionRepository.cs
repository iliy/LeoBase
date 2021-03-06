﻿using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Abstract
{
    public interface IPersonePositionRepository
    {
        IQueryable<EmploeyrPosition> Positions { get; }
        bool Remove(int id);
        int AddPosition(EmploeyrPosition position);
    }
}
