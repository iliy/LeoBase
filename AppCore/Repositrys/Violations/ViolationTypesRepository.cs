﻿using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys.Violations
{
    public class ViolationTypesRepository : IViolationTypeRepository
    {
        public IQueryable<ViolationType> ViolationTypes
        {
            get
            {
                var db = new LeoBaseContext();
                return db.ViolationsType;
            }
        }
    }
}