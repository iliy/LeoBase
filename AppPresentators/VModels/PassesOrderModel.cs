using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public enum PassesOrderProperties
    {
        BY_DATE_GIVED = 0,
        BY_DATE_CLOSED = 1,
        BY_FIO = 2
    }
    public class PassesOrderModel
    {
        public OrderType OrderType { get; set; }
        public PassesOrderProperties OrderProperties { get; set; }
    }
}
