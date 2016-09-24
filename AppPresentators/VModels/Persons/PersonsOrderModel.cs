using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels.Persons
{
    public class PersonsOrderModel
    {
        public OrderType OrderType { get; set; }
        public PersonsOrderProperties OrderProperties { get; set; }
    }
}
