using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormControls.Controls.Attributes
{
    public class OrderAttribute:Attribute
    {
        public bool Order { get; set; } = true;
        public OrderAttribute() { }
        public OrderAttribute(bool order)
        {
            Order = order;
        }
    }
}
