using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class EmploeyrPosition
    {
        [Key]
        public int PositionID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
