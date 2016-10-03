using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class ProtocolType
    {
        [Key]
        public int ProtocolTypeID { get; set; }
        public string Name { get; set; }
    }
}
