﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Presentators.Interfaces.ProtocolsPresentators
{
    public interface IProtocolAboutViolationPresentator:IProtocolPresentator
    {
        IRulingForViolationPresentator Ruling { get; set; }
    }
}
