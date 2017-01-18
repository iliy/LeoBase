using AppData.Contexts;
using AppData.CustomAttributes;
using AppData.Entities;
using AppData.Repositrys;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Services
{
    public class AdminViolationSearchModel
    {
        [Browsable(false)]
        public string FastSearchString { get; set; }
        [Browsable(false)]
        public int EmployerID { get; set; }
        [DisplayName("ФИО сотрудника")]
        public string EmployerFIO { get; set; } // 1
        [Browsable(false)]
        public int ViolatorID { get; set; }
        [DisplayName("ФИО нарушителя")]
        public string ViolatorFiO { get; set; }  // 2
        [Browsable(false)]
        public int OrganisationID { get; set; }
        [DisplayName("Оргнизация")]
        public string Organisation { get; set; } // 3

        [Browsable(false)]
        public MSquare CordsCreatedProtocol { get; set; } // 4
        [Browsable(false)]
        public MSquare CordsViolation { get; set; } // 6

        [DisplayName("Место составления протокола")]
        public string PlaceCreatedProtocol { get; set; } // 5
        [DisplayName("Место правонарушения")]
        public string PlaceViolation { get; set; }
        [ChildrenProperty("CompareDateCreatedProtocol")]
        [DisplayName("Дата составления протокола")]
        [ControlType(ControlType.DateTime)]
        public DateTime DateCreatedProtocol { get; set; } // 7
        [Browsable(false)]
        public CompareValue CompareDateCreatedProtocol { get; set; }
        [ChildrenProperty("CompareDateViolation")]
        [DisplayName("Дата правонарушения")]
        [ControlType(ControlType.DateTime)]
        public DateTime DateViolation { get; set; } // 8
        [Browsable(false)]
        public CompareValue CompareDateViolation { get; set; }
        [DisplayName("КОАП")]
        public string KOAP { get; set; } // 11
        [DisplayName("Найдена незаконная \r\nпродукция природопользования")]
        public string FindedNatureManagmentProducts { get; set; } // 12
        [DisplayName("Найденное оружие")]
        public string FindedWayponts { get; set; } // 13
        [DisplayName("Найденные орудия охоты\r\n и рыбной ловли")]
        public string FindedGunsHunterAndFishing { get; set; } // 14
        [DisplayName("ФИО свидетеля")]
        public string WitnesFIO { get; set; } // 15
        [DisplayName("Номер постановления")]
        public string RulingNumber { get; set; } // 16
        [DisplayName("Дата рассмотрения")]
        [ChildrenPropertyAttribute("CompareRulingDate")]
        [ControlType(ControlType.DateTime)]
        public DateTime RulingDate { get; set; } // 17
        [Browsable(false)]
        public CompareValue CompareRulingDate { get; set; }

        [DisplayName("Сумма наложения")]
        [ChildrenPropertyAttribute("CompareSumViolation")]
        [ControlType(ControlType.Real)]
        public decimal SumViolation { get; set; } // 19
        [Browsable(false)]
        public CompareValue CompareSumViolation { get; set; }
        

        [Browsable(false)]
        public string Products { get; set; } // 21
        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DisplayName("Оплачен штраф")]
        [DataPropertiesName("SearchByPaymantValues")]
        public SearchByPaymantEnum WasPaymant { get; set; } 

        [Browsable(false)]
        public List<SearchByPaymantComboModel> SearchByPaymantValues
        {
            get
            {
                return new List<SearchByPaymantComboModel>
                {
                    new SearchByPaymantComboModel
                    {
                        Display = "Не учитывать",
                        Value = SearchByPaymantEnum.NONE
                    },
                    new SearchByPaymantComboModel
                    {
                        Display = "Оплачены",
                        Value = SearchByPaymantEnum.WAS_PAYMANT
                    },
                    new SearchByPaymantComboModel
                    {
                        Display = "Не оплачен",
                        Value = SearchByPaymantEnum.NOT_PAYMENT
                    }
                };
            }
        }

        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DisplayName("Оплачен штраф")]
        [DataPropertiesName("SearchWasSentBailiffValues")]
        public SearchBySentBailiffEnum WasSentBailiff { get; set; }

        [Browsable(false)]
        public List<SearchBySentBailiffComboModel> SearchWasSentBailiffValues
        {
            get
            {
                return new List<SearchBySentBailiffComboModel>
                {
                    new SearchBySentBailiffComboModel
                    {
                        Display = "Не учитывать",
                        Value = SearchBySentBailiffEnum.NONE
                    },
                    new SearchBySentBailiffComboModel
                    {
                        Display = "Не отправлен",
                        Value = SearchBySentBailiffEnum.NOT_SEND
                    },
                    new SearchBySentBailiffComboModel
                    {
                        Display = "Отправлен",
                        Value = SearchBySentBailiffEnum.WAS_SEND
                    }
                };
            }
        }

        [ControlType(ControlType.ComboBox, "Value", "Display")]
        [DisplayName("Отправлено")]
        [DataPropertiesName("SearchByTypeSendingValues")]
        public SearchByTypeSendingEnum SearchByTypeSending { get; set; }

        [Browsable(false)]
        public List<SearchByTypeSendingComboModel> SearchByTypeSendingValues
        {
            get
            {
                return new List<SearchByTypeSendingComboModel>
                {
                    new SearchByTypeSendingComboModel
                    {
                        Display = "Не учитывать",
                        Value = SearchByTypeSendingEnum.NONE
                    },
                    new SearchByTypeSendingComboModel
                    {
                        Display = "Получил лично",
                        Value = SearchByTypeSendingEnum.GOT_PERSONALY
                    },
                    new SearchByTypeSendingComboModel
                    {
                        Display = "Отправлено почтой",
                        Value = SearchByTypeSendingEnum.SENDING_BY_POST
                    }
                };
            }
        }

    }


    public class SearchByPaymantComboModel
    {
        public string Display { get; set; }
        public SearchByPaymantEnum Value { get; set; }
    }

    public enum SearchByPaymantEnum
    {
        NONE,
        WAS_PAYMANT,
        NOT_PAYMENT
    }

    public class SearchBySentBailiffComboModel
    {
        public string Display { get; set; }
        public SearchBySentBailiffEnum Value { get; set; }
    }

    public enum SearchBySentBailiffEnum
    {
        NONE,
        WAS_SEND,
        NOT_SEND
    }

    public class SearchByTypeSendingComboModel
    {
        public string Display { get; set; }
        public SearchByTypeSendingEnum Value { get; set; }
    }

    public enum SearchByTypeSendingEnum
    {
        NONE,
        GOT_PERSONALY,
        SENDING_BY_POST
    }

    public class AdminViolationOrderModel {
        public OrderType OrderType { get; set; }
        public AdminViolationOrderTypes OrderBy { get; set; }
    }

    public enum AdminViolationOrderTypes
    {
        /// <summary>
        /// Нарушитель
        /// </summary>
        VIOLATOR = 0,
        
        /// <summary>
        /// Сотрудник
        /// </summary>
        EMPLOYER = 1,
        
        /// <summary>
        /// Дата рассмотрения
        /// </summary>
        DATE_CONSIDERATION = 3,
        
        /// <summary>
        /// Нарушение
        /// </summary>
        VIOLATION = 4,
        
        /// <summary>
        /// Сумма наложения
        /// </summary>
        SUM_VIOLATION = 5,

        /// <summary>
        /// Сумма взыскания
        /// </summary>
        SUM_RECOVERY = 6
    }

    public interface IAdminViolationService {
        AdminViolation GetViolation(int id);
        List<AdminViolationRowModel> GetTableData(PageModel pageModel = null, AdminViolationOrderModel orderModel = null, AdminViolationSearchModel searchModel = null);
        bool RemoveViolation(int id);
        bool SaveViolation(AdminViolation violation);
        PageModel PageModel { get; set; }
    }

    public class AdminViolationService : IAdminViolationService
    {
        private IAdminViolationRepository _repo;
       
        public string ErrorMessage { get; private set; } 

        public AdminViolationService()
        {
            _repo = new AdminViolationsRepository();
        }

        public PageModel PageModel { get; set; }
        public List<AdminViolationRowModel> GetTableData(PageModel pageModel = null, AdminViolationOrderModel orderModel = null, AdminViolationSearchModel searchModel = null)
        {
            using (var context = new LeoBaseContext())
            {
                var violations = _repo.Violations(context);

                if (searchModel != null)
                {
                    // Доработать модель поиска нарушений

                    // Поиск по ФИО сотрудника
                    if (!string.IsNullOrEmpty(searchModel.EmployerFIO))
                    {
                        string[] splitter = searchModel.EmployerFIO.Split(new[] { ' ' });
                        if (splitter.Length == 1) {
                            string buf = splitter[0];
                            violations = violations.Where(v => v.Employer.FirstName.Contains(buf) || v.Employer.SecondName.Contains(buf) || v.Employer.MiddleName.Contains(buf));
                        } else if (splitter.Length == 2)
                        {
                            string fn = splitter[0];
                            string sn = splitter[1];

                            violations = violations.Where(v => v.Employer.FirstName.Equals(fn) && v.Employer.SecondName.Contains(sn));
                        } else if (splitter.Length == 3)
                        {
                            string fn = splitter[0];
                            string sn = splitter[1];
                            string mn = splitter[2];

                            violations = violations.Where(v => v.Employer.FirstName.Equals(fn) && v.Employer.SecondName.Equals(sn) && v.Employer.MiddleName.Contains(mn));
                        }
                    }

                    if (!string.IsNullOrEmpty(searchModel.Organisation)) { } // Поиск по организациям

                    if (!string.IsNullOrEmpty(searchModel.ViolatorFiO))
                    {
                        string[] splitter = searchModel.ViolatorFiO.Split(new[] { ' ' });
                        if (splitter.Length == 1)
                        {
                            string buf = splitter[0];
                            violations = violations.Where(v => v.ViolatorPersone.FirstName.Contains(buf) || v.ViolatorPersone.SecondName.Contains(buf) || v.ViolatorPersone.MiddleName.Contains(buf));
                        } else if (splitter.Length == 2)
                        {
                            string fn = splitter[0];
                            string sn = splitter[1];

                            violations = violations.Where(v => v.ViolatorPersone.FirstName.Equals(fn) && v.ViolatorPersone.SecondName.Contains(sn));
                        }
                        else if (splitter.Length == 3)
                        {
                            string fn = splitter[0];
                            string sn = splitter[1];
                            string mn = splitter[2];

                            violations = violations.Where(v => v.ViolatorPersone.FirstName.Equals(fn) && v.ViolatorPersone.SecondName.Equals(sn) && v.ViolatorPersone.MiddleName.Contains(mn));
                        }
                    }

                    if (searchModel.CompareDateCreatedProtocol != CompareValue.NONE)
                    {
                        if (searchModel.DateCreatedProtocol.Year == 1) searchModel.DateCreatedProtocol = DateTime.Now;

                        switch (searchModel.CompareDateCreatedProtocol)
                        {
                            case CompareValue.LESS:
                                violations = violations.Where(v => v.DateCreatedProtocol < searchModel.DateCreatedProtocol);
                                break;
                            case CompareValue.MORE:
                                violations = violations.Where(v => v.DateCreatedProtocol > searchModel.DateCreatedProtocol);
                                break;
                            case CompareValue.EQUAL:
                                violations = violations.Where(v => v.DateCreatedProtocol == searchModel.DateCreatedProtocol);
                                break;
                        }
                    }

                    if (searchModel.CompareRulingDate != CompareValue.NONE)
                    {
                        if (searchModel.RulingDate.Year == 1) searchModel.RulingDate = DateTime.Now;

                        switch (searchModel.CompareRulingDate)
                        {
                            case CompareValue.LESS:
                                violations = violations.Where(v => v.Consideration < searchModel.RulingDate);
                                break;
                            case CompareValue.MORE:
                                violations = violations.Where(v => v.Consideration > searchModel.RulingDate);
                                break;
                            case CompareValue.EQUAL:
                                violations = violations.Where(v => v.Consideration == searchModel.RulingDate);
                                break;
                        }
                    }

                    if (searchModel.CompareSumViolation != CompareValue.NONE)
                    {
                        switch (searchModel.CompareSumViolation)
                        {
                            case CompareValue.LESS:
                                violations = violations.Where(v => v.SumViolation < searchModel.SumViolation);
                                break;
                            case CompareValue.MORE:
                                violations = violations.Where(v => v.SumViolation > searchModel.SumViolation);
                                break;
                            case CompareValue.EQUAL:
                                violations = violations.Where(v => v.SumViolation == searchModel.SumViolation);
                                break;
                        }
                    }

                    if (searchModel.CordsCreatedProtocol != null) { } // Поиск по координатам составления протокола

                    if (searchModel.CordsViolation != null) { } // Поиск по координатам правонарушения

                    if (searchModel.CompareDateViolation != CompareValue.NONE)
                    {
                        if (searchModel.DateViolation.Year == 1) searchModel.DateViolation = DateTime.Now;

                        switch (searchModel.CompareDateViolation)
                        {
                            case CompareValue.LESS:
                                violations = violations.Where(v => v.DateViolation < searchModel.DateViolation);
                                break;
                            case CompareValue.MORE:
                                violations = violations.Where(v => v.DateViolation > searchModel.DateViolation);
                                break;
                            case CompareValue.EQUAL:
                                violations = violations.Where(v => v.DateViolation == searchModel.DateViolation);
                                break;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.PlaceCreatedProtocol))
                    {
                        violations = violations.Where(v => v.PlaceCreatedProtocol.Contains(searchModel.PlaceCreatedProtocol));
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.PlaceViolation))
                    {
                        violations = violations.Where(v => v.PlaceViolation.Contains(searchModel.PlaceViolation));
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.KOAP))
                    {
                        violations = violations.Where(v => v.KOAP.Contains(searchModel.KOAP));
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.FindedNatureManagmentProducts))
                    {
                        violations = violations.Where(v => v.FindedNatureManagementProducts.Contains(searchModel.FindedNatureManagmentProducts));
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.FindedGunsHunterAndFishing))
                    {
                        violations = violations.Where(v => v.FindedGunsHuntingAndFishing.Contains(searchModel.FindedGunsHunterAndFishing));
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.FindedWayponts))
                    {
                        violations = violations.Where(v => v.FindedWeapons.Contains(searchModel.FindedWayponts));
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.WitnesFIO))
                    {
                        violations = violations.Where(v => v.WitnessFIO_1.Contains(searchModel.WitnesFIO) || v.WitnessFIO_2.Contains(searchModel.WitnesFIO));
                    }

                    if (!string.IsNullOrWhiteSpace(searchModel.RulingNumber))
                    {
                        violations = violations.Where(v => v.RulingNumber.Contains(searchModel.RulingNumber));
                    }

                    if(searchModel.WasPaymant != SearchByPaymantEnum.NONE)
                    {
                        switch (searchModel.WasPaymant)
                        {
                            case SearchByPaymantEnum.NOT_PAYMENT:
                                violations = violations.Where(v => v.SumRecovery < v.SumViolation);
                                break;
                            case SearchByPaymantEnum.WAS_PAYMANT:
                                violations = violations.Where(v => v.SumViolation == v.SumRecovery);
                                break;
                        }
                    }

                    if(searchModel.WasSentBailiff != SearchBySentBailiffEnum.NONE)
                    {
                        switch (searchModel.WasSentBailiff)
                        {
                            case SearchBySentBailiffEnum.NOT_SEND:
                                violations = violations.Where(v => !v.WasSentBailiff);
                                break;
                            case SearchBySentBailiffEnum.WAS_SEND:
                                violations = violations.Where(v => v.WasSentBailiff);
                                break;
                        }
                    }

                    if(searchModel.SearchByTypeSending != SearchByTypeSendingEnum.NONE)
                    {
                        switch (searchModel.SearchByTypeSending)
                        {
                            case SearchByTypeSendingEnum.GOT_PERSONALY:
                                violations = violations.Where(v => v.GotPersonaly);
                                break;
                            case SearchByTypeSendingEnum.SENDING_BY_POST:
                                violations = violations.Where(v => !v.GotPersonaly);
                                break;
                        }
                    }
                }

                if (orderModel != null)
                {
                    switch (orderModel.OrderBy)
                    {
                        case AdminViolationOrderTypes.DATE_CONSIDERATION:
                            if (orderModel.OrderType == OrderType.ASC)
                                violations = violations.OrderBy(v => v.Consideration);
                            else
                                violations = violations.OrderByDescending(v => v.Consideration);
                            break;
                        case AdminViolationOrderTypes.EMPLOYER:
                            if (orderModel.OrderType == OrderType.ASC)
                                violations = violations.OrderBy(v => v.Employer.FirstName + v.Employer.SecondName + v.Employer.MiddleName);
                            else
                                violations = violations.OrderByDescending(v => v.Employer.FirstName + v.Employer.SecondName + v.Employer.MiddleName);
                            break;
                        case AdminViolationOrderTypes.SUM_RECOVERY:
                            if (orderModel.OrderType == OrderType.ASC)
                                violations = violations.OrderBy(v => v.SumRecovery);
                            else
                                violations = violations.OrderByDescending(v => v.SumRecovery);
                            break;
                        case AdminViolationOrderTypes.SUM_VIOLATION:
                            if (orderModel.OrderType == OrderType.ASC)
                                violations = violations.OrderBy(v => v.SumViolation);
                            else
                                violations = violations.OrderByDescending(v => v.SumViolation);
                            break;
                        case AdminViolationOrderTypes.VIOLATION:
                            if (orderModel.OrderType == OrderType.ASC)
                                violations = violations.OrderBy(v => v.Violation);
                            else
                                violations = violations.OrderByDescending(v => v.Violation);
                            break;
                        case AdminViolationOrderTypes.VIOLATOR:
                            if (orderModel.OrderType == OrderType.ASC)
                                violations = violations.OrderBy(v => v.ViolatorPersone.FirstName + v.ViolatorPersone.SecondName + v.ViolatorPersone.MiddleName);
                            else
                                violations = violations.OrderByDescending(v => v.ViolatorPersone.FirstName + v.ViolatorPersone.SecondName + v.ViolatorPersone.MiddleName);
                            break;
                    }
                }

                PageModel = pageModel;

                PageModel.TotalItems = violations.Count();

                violations = violations.Skip(PageModel.ItemsOnPage * (PageModel.CurentPage - 1)).Take(PageModel.ItemsOnPage);
                

                List<AdminViolationRowModel> result = new List<AdminViolationRowModel>();

                foreach (var v in violations) {
                    var row = new AdminViolationRowModel();

                    if(v.ViolatorOrganisation != null)
                    {
                        row.ViolatorInfo = string.Format("Юридическое лицо: {0}",v.ViolatorOrganisation.Name);
                    }else
                    {
                        row.ViolatorInfo = string.Format("{0} {1} {2}", v.ViolatorPersone.FirstName, v.ViolatorPersone.SecondName, v.ViolatorPersone.MiddleName);
                    }

                    row.EmployerInfo = string.Format("{0} {1} {2}", v.Employer.FirstName, v.Employer.SecondName, v.Employer.MiddleName);

                    row.Coordinates = string.Format("{0}; {1}", v.ViolationN, v.ViolationE);

                    row.Consideration = v.Consideration;

                    row.DatePaymant = v.WasPaymant ? v.DatePaymant.ToShortDateString() : "";

                    row.DateSentBailiff = v.WasSentBailiff ? v.DateSentBailiff.ToShortDateString() : "";

                    row.InformationAbout2025 = v.WasAgenda2025 ? v.DateAgenda2025.ToShortDateString() : "";

                    row.InformationAboutNotice = v.WasNotice ? v.DateNotice.ToShortDateString() : "";

                    row.InformationAboutSending = v.GotPersonaly ? "Получил лично" 
                                                                 : v.WasSent 
                                                                    ? v.DateSent.ToShortDateString() 
                                                                    : "";

                    row.SumRecovery = v.SumRecovery;

                    row.SumViolation = v.SumViolation;

                    row.Violation = v.Violation;

                    row.ViolationID = v.ViolationID;

                    result.Add(row);
                }

                //PageModel = pageModel;

                //PageModel.TotalItems = violations.Count();

                return result;
            }
        }

        public AdminViolation GetViolation(int id)
        {
            return _repo.GetViolation(id);
        }

        public bool RemoveViolation(int id)
        {
            return _repo.RemoveViolation(id);
        }

        public bool SaveViolation(AdminViolation violation)
        {
            try { 
                return _repo.SaveViolation(violation) != null;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
