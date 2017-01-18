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

namespace AppPresentators.Presentators
{
    public interface IViolationDetailsPresentator: IComponentPresentator
    {
        AdminViolation Violation { get; set; }
    }
    public class ViolationDetailsPresentator : IViolationDetailsPresentator
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
                return _view.TopControls;
            }
        }
        

        public AdminViolation Violation {
            get
            {
                return _view.Violation;
            }
            set
            {
                _view.Violation = value;
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
        }

        private void ShowEmployerDetails(int id)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var employerDetailsPresentator = _appFactory.GetPresentator<IEmployerDetailsPresentator>();

            var personeServices = _appFactory.GetService<IPersonesService>();

            var ee = personeServices.GetPerson(id, true) as EmploeyrViewModel;

            EmployerDetailsModel employer = new EmployerDetailsModel
            {
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

        }
    }
}
