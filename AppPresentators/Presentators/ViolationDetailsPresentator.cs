using AppData.Entities;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using System.Windows.Forms;
using AppPresentators.Services;
using AppPresentators.Components;
using AppData.Contexts;
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using AppPresentators.Infrastructure.Orders;
using System.Drawing;
using System.IO;

namespace AppPresentators.Presentators
{
    public interface IViolationDetailsPresentator: IComponentPresentator
    {
        AdminViolation Violation { get; set; }
    }
    public class ViolationDetailsPresentator : IViolationDetailsPresentator, IOrderPage
    {
        public ResultTypes ResultType { get; set; }

        private static Image _map;

        public bool ShowFastSearch
        {
            get
            {
                return false;
            }
        }

        public bool ShowForResult { get; set; }

        public bool ShowSearch
        {
            get
            {
                return false;
            }
        }

        public List<Control> TopControls
        {
            get
            {
                return _view.TopControls;
            }
        }

        private static AdminViolation _violation;

        public AdminViolation Violation {
            get
            {
                return _view.Violation;
            }
            set
            {
                _violation = value;
                _view.Violation = value;
            }
        }

        public Infrastructure.Orders.OrderType OrderType
        {
            get
            {
                return Infrastructure.Orders.OrderType.SINGLE_PAGE;
            }
        }

        private string _orderPath;

        public string OrderDirPath
        {
            get
            {
                return _orderPath;
            }
        }

        public event SendResult SendResult;

        private IApplicationFactory _appFactory;
        private IViolationDetailsControl _view;

        public ViolationDetailsPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;
            _view = _appFactory.GetComponent<IViolationDetailsControl>();

            _view.Report += Report;

            _view.ShowViolatorDetails += ShowViolatorDetails;

            _view.ShowEmployerDetails += ShowEmployerDetails;

            _view.EditViolation += EditViolation;
        }

        private void EditViolation()
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var saveViolatorPresentator = _appFactory.GetPresentator<ISaveAdminViolationPresentatar>();
            
            using (var db = new LeoBaseContext())
            {
                saveViolatorPresentator.Violation = db.AdminViolations.Include("Employer")
                                                     .Include("ViolatorOrganisation")
                                                     .Include("ViolatorPersone")
                                                     .Include("ViolatorDocument")
                                                     .Include("Images")
                                                     .FirstOrDefault(v => v.ViolationID == Violation.ViolationID);

            }


            mainPresentator.ShowComponentForResult(this, saveViolatorPresentator, ResultTypes.UPDATE_VIOLATION);
        }

        private void ShowEmployerDetails(int id)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var employerDetailsPresentator = _appFactory.GetPresentator<IEmployerDetailsPresentator>();

            var personeServices = _appFactory.GetService<IPersonesService>();

            var ee = personeServices.GetPerson(id, true) as EmploeyrViewModel;

            EmployerDetailsModel employer = new EmployerDetailsModel
            {
                EmployerID = ee.UserID,
                FIO = ee.FirstName + " " + ee.SecondName + " " + ee.MiddleName,
                Addresses = ee.Addresses,
                Phones = ee.Phones,
                PlaceBerth = ee.PlaceOfBirth,
                DateBerth = ee.DateBirthday.ToShortDateString(),
                Position = ee.Position,
                Image = ee.Image,
                Violations = new List<AdminViolationRowModel>()
            };

            using (var db = new LeoBaseContext())
            {

                var violations = db.AdminViolations.Include("Employer").Include("ViolatorPersone").Where(v => v.Employer.UserID == id);

                foreach (var v in violations)
                {
                    var row = new AdminViolationRowModel();

                    if (v.ViolatorOrganisation != null)
                    {
                        row.ViolatorInfo = string.Format("Юридическое лицо: {0}", v.ViolatorOrganisation.Name);
                    }
                    else
                    {
                        row.ViolatorInfo = string.Format("{0} {1} {2}", v.ViolatorPersone.FirstName, v.ViolatorPersone.SecondName, v.ViolatorPersone.MiddleName);
                    }

                    row.EmployerInfo = string.Format("{0} {1} {2}", v.Employer.FirstName, v.Employer.SecondName, v.Employer.MiddleName);

                    row.Coordinates = string.Format("{0}; {1}", Math.Round(v.ViolationN, 8), Math.Round(v.ViolationE, 8));

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

                    employer.Violations.Add(row);
                }
            }

            employerDetailsPresentator.Employer = employer;

            mainPresentator.ShowComponentForResult(this, employerDetailsPresentator, ResultTypes.DETAILS_EMPLOYER);
        }

        private void ShowViolatorDetails(int id)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var violatorDetailsPresentator = _appFactory.GetPresentator<IViolatorDetailsPresentator>();

            var personeServices = _appFactory.GetService<IPersonesService>();

            var vv = personeServices.GetPerson(id, true) as ViolatorViewModel;

            ViolatorDetailsModel violator = new ViolatorDetailsModel
            {
                ViolatorID = vv.UserID,
                FIO = vv.FirstName + " " + vv.SecondName + " " + vv.MiddleName,
                Addresses = vv.Addresses,
                Documents = vv.Documents,
                Phones = vv.Phones,
                PlaceBerth = vv.PlaceOfBirth,
                DateBerth = vv.DateBirthday,
                PlaceWork = vv.PlaceOfWork,
                Image = vv.Image,
                Violations = new List<AdminViolationRowModel>()
            };

            using (var db = new LeoBaseContext()) {
                
                var violations = db.AdminViolations.Include("Employer").Include("ViolatorPersone").Where(v => v.ViolatorPersone.UserID == id);

                foreach (var v in violations)
                {
                    var row = new AdminViolationRowModel();

                    if (v.ViolatorOrganisation != null)
                    {
                        row.ViolatorInfo = string.Format("Юридическое лицо: {0}", v.ViolatorOrganisation.Name);
                    }
                    else
                    {
                        row.ViolatorInfo = string.Format("{0} {1} {2}", v.ViolatorPersone.FirstName, v.ViolatorPersone.SecondName, v.ViolatorPersone.MiddleName);
                    }

                    row.EmployerInfo = string.Format("{0} {1} {2}", v.Employer.FirstName, v.Employer.SecondName, v.Employer.MiddleName);

                    row.Coordinates = string.Format("{0}; {1}", Math.Round(v.ViolationN, 8), Math.Round(v.ViolationE, 8));

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

                    violator.Violations.Add(row);
                }
            }
            violatorDetailsPresentator.Violator = violator;

            mainPresentator.ShowComponentForResult(this, violatorDetailsPresentator, ResultTypes.DETAILS_VIOLATOR);
        }

        private void Report()
        {
            var mainView = _appFactory.GetMainView();

            _map = _view.GetMap();

            mainView.MakeOrder(this);
        }

        public void FastSearch(string message)
        {

        }

        public Control RenderControl()
        {
            return _view.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            using (var db = new LeoBaseContext())
            {
                Violation = db.AdminViolations.Include("Employer")
                                                     .Include("ViolatorOrganisation")
                                                     .Include("ViolatorPersone")
                                                     .Include("ViolatorDocument")
                                                     .Include("Images")
                                                     .FirstOrDefault(v => v.ViolationID == Violation.ViolationID);

            }
        }

        public void BuildOrder(IOrderBuilder orderBuilder, OrderConfigs configs)
        {
            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText("Отчет по правонарушению ", System.Drawing.Color.Black, TextStyle.BOLD, 24);
            orderBuilder.EndPharagraph();

            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText(" ", System.Drawing.Color.Black, TextStyle.BOLD, 24);
            orderBuilder.EndPharagraph();

            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText("Информация по правонарушению", System.Drawing.Color.Black, TextStyle.BOLD, 14);
            orderBuilder.EndPharagraph();
            
            WriteOrderInformation(orderBuilder, "Место составления протокола: ", _violation.PlaceCreatedProtocol, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Координаты места составления протокола: ", string.Format("N: {0}; E: {1}", _violation.CreatedProtocolN, _violation.CreatedProtocolE), System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Дата/время составления протокола: ", string.Format("{0}.{1}.{2} {3}:{4}", _violation.DateCreatedProtocol.Day, _violation.DateCreatedProtocol.Month, _violation.DateCreatedProtocol.Year, _violation.DateCreatedProtocol.Hour, _violation.DateCreatedProtocol.Minute), System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Координаты места правонарушения: ", string.Format("N: {0}; E: {1}", _violation.ViolationN, _violation.ViolationE), System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Дата/время правонарушения: ", string.Format("{0}.{1}.{2} {3}:{4}", _violation.DateViolation.Day, _violation.DateViolation.Month, _violation.DateViolation.Year, _violation.DateViolation.Hour, _violation.DateViolation.Minute), System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "КОАП: ", _violation.KOAP, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Найдена продукция природопользования: ", _violation.FindedNatureManagementProducts, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Найдено оружие: ", _violation.FindedWeapons, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Найдены орудия охоты и рыболовства: ", _violation.FindedGunsHuntingAndFishing, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "ФИО свидетеля: ", _violation.WitnessFIO_1, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Свидетель проживает: ", _violation.WitnessLive_1, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "ФИО свидетеля: ", _violation.WitnessFIO_2, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Свидетель проживает: ", _violation.WitnessLive_2, System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Сотрудник, составивший протокол: ", string.Format("{0} ; {1} {2} {3}", ConfigApp.EmploerPositions[_violation.Employer.Position_PositionID], _violation.Employer.FirstName, _violation.Employer.SecondName, _violation.Employer.MiddleName), System.Drawing.Color.Black);

            string violatorInformation = string.Format("{0} {1} {2}; ", _violation.ViolatorPersone.FirstName, _violation.ViolatorPersone.SecondName, _violation.ViolatorPersone.MiddleName);

            violatorInformation += string.Format("Дата рождения: {0}; ", _violation.ViolatorPersone.DateBirthday.ToShortDateString());

            violatorInformation += string.Format("Место работы: {0}; ", _violation.ViolatorPersone.PlaceWork);

            violatorInformation += string.Format("Место рождения: {0}; ", _violation.ViolatorPersone.PlaceOfBirth);

            violatorInformation += string.Format("Документ: {0} {1} {2} {3} {4}",
                                                            ConfigApp.DocumentsType[_violation.ViolatorDocument.Document_DocumentTypeID],
                                                            _violation.ViolatorDocument.Serial,
                                                            _violation.ViolatorDocument.Number,
                                                            _violation.ViolatorDocument.WhenIssued.ToShortDateString(),
                                                            _violation.ViolatorDocument.IssuedBy
                                                            );

            WriteOrderInformation(orderBuilder, "Информация по правонарушителю: ", violatorInformation, System.Drawing.Color.Black);

            if (_map != null) orderBuilder.DrawImage(_map, Align.CENTER);

            
            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText(" ", System.Drawing.Color.Black, TextStyle.BOLD, 24);
            orderBuilder.EndPharagraph();


            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText("Информация по правонарушению", System.Drawing.Color.Black, TextStyle.BOLD, 14);
            orderBuilder.EndPharagraph();


            WriteOrderInformation(orderBuilder, "Номер постановления: ", _violation.RulingNumber, Color.Black);

            WriteOrderInformation(orderBuilder, "Дата/время рассмотрения: ", string.Format("{0}.{1}.{2} {3}:{4}", _violation.Consideration.Day, _violation.Consideration.Month, _violation.Consideration.Year, _violation.Consideration.Hour, _violation.Consideration.Minute), System.Drawing.Color.Black);

            WriteOrderInformation(orderBuilder, "Нарушение: ", _violation.Violation, Color.Black);

            WriteOrderInformation(orderBuilder, "Сумма наложения: ", _violation.SumViolation.ToString(), Color.Black);


            if (_violation.GotPersonaly)
            {
                WriteOrderInformation(orderBuilder, "Отправлено: ", "Получил лично", Color.Black);
            }
            else if (Violation.WasSent)
            {
                WriteOrderInformation(orderBuilder, "Отправлено: ",
                     string.Format("Отправлено: {0}; Номер отправления: {1}", _violation.DateSent.ToShortDateString(), _violation.NumberSent), Color.Black);
            }
            else
            {
                WriteOrderInformation(orderBuilder, "Отправлено: ", "Не отправлено", Color.Black);
            }


            if (_violation.WasSentBailiff)
            {
                WriteOrderInformation(orderBuilder, "Отправлено судебным приставам: ",
                    string.Format("Отправлено: {0}; Номер отправления: {1}", _violation.DateSentBailiff.ToShortDateString(), _violation.NumberSentBailigg), Color.Black);
            }
            else
            {
                WriteOrderInformation(orderBuilder, "Отправлено судебным приставам: ", "не отправлено", Color.Black);
            }

            if (_violation.WasNotice)
            {
                WriteOrderInformation(orderBuilder, "Извещение: ",
                    string.Format("Извещение получено: {0}; Номер извещения: {1} ", _violation.DateNotice.ToShortDateString(), _violation.NumberNotice), Color.Black);
            }
            else
            {
                WriteOrderInformation(orderBuilder, "Извещение: ", "Извещения не было", Color.Black);
            }

            if (_violation.WasAgenda2025)
            {
                WriteOrderInformation(orderBuilder, "Повестка по статье 20.25: ", _violation.DateAgenda2025.ToShortDateString(), Color.Black);;
            }
            else
            {
                WriteOrderInformation(orderBuilder, "Повестка по статье 20.25: ", "Повестки по статье 20.25 не было", Color.Black); 
            }

            if (_violation.WasPaymant)
            {
                if (_violation.SumRecovery >= _violation.SumViolation)
                {
                    WriteOrderInformation(orderBuilder, "Оплата: ", "Оплачено " + _violation.DatePaymant.ToShortDateString(), Color.Green);
                }
                else
                {
                    WriteOrderInformation(orderBuilder, "Оплата: ", string.Format("Оплачено не полностью ({0} из {1}): {2}", _violation.SumRecovery, _violation.SumViolation, _violation.DatePaymant.ToShortDateString()), Color.Red);
                }
            }
            else
            {
                WriteOrderInformation(orderBuilder, "Оплата: ", "Не оплачено", Color.Red);
            }

            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText(" ", System.Drawing.Color.Black, TextStyle.BOLD, 14);
            orderBuilder.EndPharagraph();

            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText(" ", System.Drawing.Color.Black, TextStyle.BOLD, 14);
            orderBuilder.EndPharagraph();

            orderBuilder.StartPharagraph(Align.LEFT);
            orderBuilder.WriteText("Изображения", System.Drawing.Color.Black, TextStyle.BOLD, 14);
            orderBuilder.EndPharagraph();

            if (configs.SinglePageConfig.DrawImages)
            {
                foreach (var img in Violation.Images)
                {
                    using (var ms = new MemoryStream(img.Image))
                    {
                        var image = Image.FromStream(ms);

                        orderBuilder.DrawImage(image, Align.CENTER);
                    }
                }
            }
            
            _orderPath = orderBuilder.Save();
        }
        
        private Image ImageFromByteArray(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }

        private void WriteOrderInformation(IOrderBuilder builder, string lable, string text, System.Drawing.Color color)
        {
            builder.StartPharagraph(Align.LEFT);
            builder.WriteText(lable, System.Drawing.Color.Black, TextStyle.BOLD);
            builder.WriteText(text, color);
            builder.EndPharagraph();
        }
    }
}
