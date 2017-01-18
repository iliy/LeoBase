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
        public string Name { get; set; }
        [Column(TypeName = "image")]
        [MaxLength]
        public byte[] Image { get; set; }
        public virtual AdminViolation AdminViolation { get; set; }
    }
}
