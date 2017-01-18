using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure
{
    public enum ResultTypes
    {
        NONE,
        ADD_PERSONE,
        UPDATE_PERSONE,
        SEARCH_PERSONE,
        SEARCH_VIOLATOR,
        SEARCH_EMPLOYER,

        DETAILS_VIOLATOR,
        DETAILS_EMPLOYER,
        DETAILS_VIOLATION,

        UPDATE_VIOLATION,
        ADD_VIOLATION,
        SHOW_ON_MAP,

        ADD_VIOLATOR,
        ADD_EMPLOYER,

        FIND_EMPLOYER,
        FIND_VIOLATOR
    }
}
