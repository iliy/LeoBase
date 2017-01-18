using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class AdminViolation
    {
        [Key]
        public int ViolationID { get; set; }
        public virtual Persone Employer { get; set; }
        public virtual Organisation ViolatorOrganisation { get; set; }
        public virtual Persone ViolatorPersone { get; set; }
        public virtual Document ViolatorDocument { get; set; }

        public double CreatedProtocolN { get; set; }
        public double CreatedProtocolE { get; set; }

        public double ViolationN { get; set; }
        public double ViolationE { get; set; }
        public DateTime DateCreatedProtocol { get; set; }
        public DateTime DateViolation { get; set; }
        public string PlaceCreatedProtocol { get; set; }
        public string PlaceViolation { get; set; }
        /// <summary>
        /// Описание правонарушения
        /// </summary>
        public string ViolationDescription { get; set; }
        /// <summary>
        /// Статья КОАП за данное нарушение
        /// </summary>
        public string KOAP { get; set; }
        /// <summary>
        /// Найденная продукция природопользования
        /// </summary>
        public string FindedNatureManagementProducts { get; set; }
        /// <summary>
        /// Найденное оружие
        /// </summary>
        public string FindedWeapons { get; set; }
        /// <summary>
        /// Найденые орудия для охоты и рыболовства 
        /// </summary>
        public string FindedGunsHuntingAndFishing { get; set; }
        public string WitnessFIO_1 { get; set; }
        public string WitnessLive_1 { get; set; }
        public string WitnessFIO_2 { get; set; }
        public string WitnessLive_2 { get; set; }

        public DateTime DateSave { get; set; }
        public DateTime DateUpdate { get; set; }

        #region Information Obout Ruling

        /// <summary>
        /// Номер постановления
        /// </summary>
        public string RulingNumber { get; set; }

        /// <summary>
        /// Дата рассмотрения
        /// </summary>
        public DateTime Consideration { get; set; }
        
        /// <summary>
        /// Нарушение
        /// </summary>
        public string Violation { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime DatePaymant { get; set; }
        /// <summary>
        /// Было-ли оплачено
        /// </summary>
        public bool WasPaymant { get; set; }
        /// <summary>
        /// Сумма наложения
        /// </summary>
        public decimal SumViolation { get; set; }

        /// <summary>
        /// Сумма взыскания
        /// </summary>
        public decimal SumRecovery { get; set; }
        
        /// <summary>
        /// Дата отправления
        /// </summary>
        public DateTime DateSent { get; set; }
        /// <summary>
        /// Было-ли отправлено
        /// </summary>
        public bool WasSent { get; set; }
        /// <summary>
        /// Номер отправления
        /// </summary>
        public string NumberSent { get; set; }
        /// <summary>
        /// Получил лично
        /// </summary>
        public bool GotPersonaly { get; set; }
        
        /// <summary>
        /// Дата получения
        /// </summary>
        public DateTime DateReceiving { get; set; }
        /// <summary>
        /// Было-ли получено
        /// </summary>
        public bool WasReceiving { get; set; }
        
        /// <summary>
        /// Дата отправления судебным приставам
        /// </summary>
        public DateTime DateSentBailiff { get; set; }
        /// <summary>
        /// Было-ли отправлено судебным приставам
        /// </summary>
        public bool WasSentBailiff { get; set; }

        /// <summary>
        /// Номер отправления судебным приставам
        /// </summary>
        public string NumberSentBailigg { get; set; }


        /// <summary>
        /// Дата извещения
        /// </summary>
        public DateTime DateNotice { get; set; }
        /// <summary>
        /// Номер извещения
        /// </summary>
        public string NumberNotice { get; set; }
        /// <summary>
        /// Было-ли извещение
        /// </summary>
        public bool WasNotice { get; set; }


        /// <summary>
        /// Дата повестки по 20.25
        /// </summary>
        public DateTime DateAgenda2025 { get; set; }
        /// <summary>
        /// Была-ли повестка по статье 20.25
        /// </summary>
        public bool WasAgenda2025 { get; set; }

        #endregion

        /// <summary>
        /// Изображения
        /// </summary>
        public List<ViolationImage> Images { get; set; }


        public void Update(AdminViolation violation)
        {

        }
    }
}
