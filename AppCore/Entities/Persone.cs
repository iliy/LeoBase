using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Persone
    {
        [Key]
        public int UserID { get; set; }
        [DefaultValue(false)]
        public bool IsEmploeyr { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SecondName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string MiddleName { get; set; }
        public string PlaceWork { get; set; }

        public DateTime DateBirthday { get; set; }

        public DateTime WasBeCreated { get; set; } = DateTime.Now;
        public DateTime WasBeUpdated { get; set; } = DateTime.Now;
        
        public string PlaceOfBirth { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public virtual List<Document> Documents { get; set; }

        public virtual List<PersoneAddress> Address { get; set; }

        public virtual List<Phone> Phones { get; set; }

        public int Position_PositionID { get; set; }
    }
}
