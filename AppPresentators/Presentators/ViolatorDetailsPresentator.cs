using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using System.Windows.Forms;
using AppPresentators.Components;
using AppData.Contexts;
using AppPresentators.Services;
using AppPresentators.VModels.Persons;
using AppPresentators.Infrastructure.Orders;
using System.Drawing;
using System.IO;

namespace AppPresentators.Presentators
{
    public interface IViolatorDetailsPresentator: IComponentPresentator
    {
        ViolatorDetailsModel Violator { get; set; }
    }

    public class ViolatorDetailsPresentator : IViolatorDetailsPresentator, IOrderPage
    {
        public ResultTypes ResultType { get; set; }

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
                return _control.TopControls;
            }
        }
        private static ViolatorDetailsModel _violator;

        public ViolatorDetailsModel Violator
        {
            get
            {
                return _control.Violator;
            }

            set
            {
                _violator = value;
                _control.Violator = value;
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

        private IViolatorDetailsControl _control;

        private IApplicationFactory _appFactory;

        public ViolatorDetailsPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _control = _appFactory.GetComponent<IViolatorDetailsControl>();

            _control.ShowDetailsViolation += ShowDetailsViolation;

            _control.EditViolator += EditViolator;

            _control.MakeReport += MakeReport;
        }

        private void MakeReport()
        {
            var mainView = _appFactory.GetMainView();

            mainView.MakeOrder(this);
        }

        private void EditViolator()
        {
            var service = _appFactory.GetService<IPersonesService>();
            var mainPresentator = _appFactory.GetMainPresentator();
            var saveViolatorPresentator = _appFactory.GetPresentator<ISaveEmployerPresentator>();

            var violator = service.GetPerson(Violator.ViolatorID, true);


            saveViolatorPresentator.Persone = violator;

            mainPresentator.ShowComponentForResult(this, saveViolatorPresentator, ResultTypes.UPDATE_PERSONE);
        }

        private void ShowDetailsViolation(int id)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var detailsViolatorPresentator = _appFactory.GetPresentator<IViolationDetailsPresentator>();
            
            using (var db = new LeoBaseContext())
            {
                detailsViolatorPresentator.Violation = db.AdminViolations.Include("Employer")
                                                     .Include("ViolatorOrganisation")
                                                     .Include("ViolatorPersone")
                                                     .Include("ViolatorDocument")
                                                     .Include("Images")
                                                     .FirstOrDefault(v => v.ViolationID == id);

            }


            mainPresentator.ShowComponentForResult(this, detailsViolatorPresentator, ResultTypes.UPDATE_VIOLATION);
        }

        public void FastSearch(string message)
        {

        }

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            var service = _appFactory.GetService<IPersonesService>();

            var personeModel = service.GetPerson(Violator.ViolatorID, true);

            var vv = personeModel as ViolatorViewModel;

            if (vv == null) return;

            ViolatorDetailsModel violator = new ViolatorDetailsModel
            {
                ViolatorID = personeModel.UserID,
                FIO = personeModel.FirstName + " " + personeModel.SecondName + " " + personeModel.MiddleName,
                Addresses = personeModel.Addresses,
                Documents = personeModel.Documents,
                Phones = personeModel.Phones,
                PlaceBerth = personeModel.PlaceOfBirth,
                DateBerth = personeModel.DateBirthday,
                PlaceWork = vv.PlaceOfWork,
                Image = vv.Image,
                Violations = new List<AdminViolationRowModel>()
            };

            using (var db = new LeoBaseContext())
            {
                var persone = db.Persones.FirstOrDefault(p => p.UserID == personeModel.UserID);

                if (persone != null) violator.Image = persone.Image;

                var documents = db.Documents.Where(d => d.Persone.UserID == personeModel.UserID);

                if (documents != null)
                {

                    violator.Documents = new List<PersoneDocumentModelView>();

                    foreach (var d in documents)
                    {
                        violator.Documents.Add(new PersoneDocumentModelView
                        {
                            CodeDevision = d.CodeDevision,
                            DocumentID = d.DocumentID,
                            DocumentTypeID = d.Document_DocumentTypeID,
                            DocumentTypeName = ConfigApp.DocumentsType[d.Document_DocumentTypeID],
                            IssuedBy = d.IssuedBy,
                            Number = d.Number,
                            Serial = d.Serial,
                            WhenIssued = d.WhenIssued
                        });
                    }
                }

                var addresses = db.Addresses.Where(a => a.Persone.UserID == personeModel.UserID);

                if (addresses != null)
                {
                    violator.Addresses = new List<PersonAddressModelView>();
                    foreach (var a in addresses)
                    {
                        violator.Addresses.Add(new PersonAddressModelView
                        {
                            AddressID = a.AddressID,
                            Area = a.Area,
                            City = a.City,
                            Country = a.Country,
                            Flat = a.Flat,
                            HomeNumber = a.HomeNumber,
                            Note = a.Note,
                            Street = a.Street,
                            Subject = a.Subject
                        });
                    }
                }

                var phones = db.Phones.Where(p => p.Persone.UserID == personeModel.UserID);

                if (phones != null)
                {

                    violator.Phones = new List<PhoneViewModel>();
                    foreach (var p in phones)
                    {
                        violator.Phones.Add(new PhoneViewModel
                        {
                            PhoneID = p.PhoneID,
                            PhoneNumber = p.PhoneNumber
                        });
                    }
                }

                var violations = db.AdminViolations.Include("Employer").Include("ViolatorPersone").Where(v => v.ViolatorPersone.UserID == vv.UserID);
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

            Violator = violator;
        }

        public void BuildOrder(IOrderBuilder orderBuilder, OrderConfigs configs)
        {
            if (configs.SinglePageConfig.DrawImages)
            {
                using (var ms = new MemoryStream(_violator.Image))
                {
                    var image = Image.FromStream(ms);

                    orderBuilder.DrawImage(image, Align.CENTER);
                }
            }

            /// TODO: Доделать вывод информации о правонарушителе в отчет

            _orderPath = orderBuilder.Save();
        }
    }
}
