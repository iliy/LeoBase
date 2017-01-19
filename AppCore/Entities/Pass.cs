using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Pass
    {
        [Browsable(false)]
        [Key]
        public int PassID { get; set; }
        [DisplayName("Фамилия")]
        [ReadOnly(true)]
        public string FirstName { get; set; }
        [ReadOnly(true)]
        [DisplayName("Имя")]
        public string SecondName { get; set; }
        [ReadOnly(true)]
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
        [ReadOnly(true)]
        [DisplayName("Тип предоставленного документа")]
        public string DocumentType { get; set; }
        [ReadOnly(true)]
        [DisplayName("Серия документа")]
        public string Serial { get; set; }
        [ReadOnly(true)]
        [DisplayName("Номер документа")]
        public string Number { get; set; }
        [DisplayName("Когда выдан документ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy}")]
        [ReadOnly(true)]
        public DateTime WhenIssued { get; set; }
        [DisplayName("Кем выдан документ")]
        [ReadOnly(true)]
        public string WhoIssued { get; set; }
        [DisplayName("Выдан пропуск")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy}")]
        [ReadOnly(true)]
        public DateTime PassGiven { get; set; }
        [DisplayName("Дата закрытия пропуска")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        [ReadOnly(true)]
        public DateTime PassClosed { get; set; }
    }
}
