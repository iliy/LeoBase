using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormControls.Controls.Models
{
    public class PageModel
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int ItemsOnPage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Round( (TotalItems + (ItemsOnPage - TotalItems % ItemsOnPage)) / (double)ItemsOnPage);
            }
        }
    }
}
