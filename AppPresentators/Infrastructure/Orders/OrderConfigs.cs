using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure.Orders
{
    public class OrderConfigs
    {
        public OrderType OrderType {get; set;}
        
        public OrderTableConfig TableConfig { get; set; }
        public OrderSinglePageConfig SinglePageConfig { get; set; }
        public string OrderDirPath { get; set; }
        public string OrderName { get; set; }
    }

    public class OrderTableConfig
    {
        public bool CurrentPageOnly { get; set; }
        public bool ConsiderFilter { get; set; }
    }

    public class OrderSinglePageConfig
    {
        public bool DrawImages { get; set; }
    }
}
