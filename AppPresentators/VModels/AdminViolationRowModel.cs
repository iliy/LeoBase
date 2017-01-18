using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.VModels
{
    public class AdminViolationRowModel
    {
        [Browsable(false)]
        public int ViolationID { get; set; }

        [DisplayName("Нарушитель")]
        [ReadOnly(true)]
        public string ViolatorInfo { get; set; }

        [DisplayName("ФИО сотрудника")]
        [ReadOnly(true)]
        public string EmployerInfo { get; set; }

        [DisplayName("Координаты")]
        [ReadOnly(true)]
        public string Coordinates { get; set; }

        [DisplayName("Рассмотрение")]
        [ReadOnly(true)]
        public DateTime Consideration { get; set; }

        [ReadOnly(true)]
        [DisplayName("Нарушение")]
        public string Violation { get; set; }

        [ReadOnly(true)]
        [DisplayName("Дата оплаты")]
        public string DatePaymant { get; set; }

        [ReadOnly(true)]
        [DisplayName("Сумма наложения")]
        public decimal SumViolation { get; set; }

        [ReadOnly(true)]
        [DisplayName("Сумма взыскания")]
        public decimal SumRecovery { get; set; }

        [ReadOnly(true)]
        [DisplayName("Отправление")]
        public string InformationAboutSending { get; set; }

        [ReadOnly(true)]
        [DisplayName("Отправлено судебным приставам")]
        public string DateSentBailiff { get; set; }


        [ReadOnly(true)]
        [DisplayName("Извещение")]
        public string InformationAboutNotice { get; set; }

        [ReadOnly(true)]
        [DisplayName("Повестка по статье 20.25")]
        public string InformationAbout2025 { get; set; }
    }
}
