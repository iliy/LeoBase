﻿using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IEmployerDetailsControl : UIComponent
    {
        event Action MakeReport;
        event Action EditEmployer;
        EmployerDetailsModel Employer { get; set; }

        event Action<int> ShowViolationDetails;
    }
}
