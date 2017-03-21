using AppPresentators.Infrastructure.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure
{
    public interface IOrderPage
    {
        OrderType OrderType { get; }
        string OrderDirPath { get; }
        void BuildOrder(IOrderBuilder orderBuilder, OrderConfigs configs);
    }
}
