using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class ViolationImage
    {
        [Key]
        public int ViolationImageID { get; set; }

        public virtual Violation Violation { get; set; }
        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
    }
}
