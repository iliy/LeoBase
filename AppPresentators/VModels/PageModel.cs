using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class PageModel
    {
        public int CurentPage { get; set; }
        public int ItemsOnPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get
            {
                return 1 + (TotalItems - TotalItems % ItemsOnPage) / ItemsOnPage;
            }}
    }
}
