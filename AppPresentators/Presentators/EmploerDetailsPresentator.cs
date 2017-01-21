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

namespace AppPresentators.Presentators
{
    public interface IEmployerDetailsPresentator : IComponentPresentator
    {
        EmployerDetailsModel Employer { get; set; }
    }

    public class EmploerDetailsPresentator : IEmployerDetailsPresentator
    {
        private IEmployerDetailsControl _control;
        private IApplicationFactory _appFactory;

        public EmployerDetailsModel Employer
        {
            get
            {
                return _control.Employer;
            }

            set
            {
                _control.Employer = value;
            }
        }

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

        public event SendResult SendResult;

        public void FastSearch(string message)
        {
        }

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            //if (resultType != ResultTypes.UPDATE_PERSONE)
            //{
            //    return;
            //}
            var service = _appFactory.GetService<IPersonesService>();

            var personeModel = service.GetPerson(Employer.EmployerID, true);

            var ee = personeModel as EmploeyrViewModel;

            if (ee == null) return;

            EmployerDetailsModel employer = new EmployerDetailsModel
            {
                FIO = personeModel.FirstName + " " + personeModel.SecondName + " " + personeModel.MiddleName,
                DateBerth = personeModel.DateBirthday.ToShortDateString(),
                Position = ee.Position,
                PlaceBerth = personeModel.PlaceOfBirth,
                EmployerID = personeModel.UserID
            };


            using (var db = new LeoBaseContext())
            {
                var searchForImage = db.Persones.FirstOrDefault(p => p.UserID == personeModel.UserID);

                if (searchForImage != null)
                {
                    employer.Image = searchForImage.Image;
                }

                employer.Addresses = new List<PersonAddressModelView>();

                var addresses = db.Addresses.Where(a => a.Persone.UserID == ee.UserID);

                if (addresses != null)
                {
                    foreach (var a in addresses)
                    {
                        employer.Addresses.Add(new PersonAddressModelView
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

                employer.Phones = new List<PhoneViewModel>();

                var phones = db.Phones.Where(p => p.Persone.UserID == personeModel.UserID);

                if (phones != null)
                {
                    foreach (var p in phones)
                    {
                        employer.Phones.Add(new PhoneViewModel
                        {
                            PhoneID = p.PhoneID,
                            PhoneNumber = p.PhoneNumber
                        });
                    }
                }

                employer.Violations = new List<AdminViolationRowModel>();

                var violations = db.AdminViolations.Include("Employer").Include("ViolatorPersone").Where(v => v.Employer.UserID == ee.UserID);

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

            Employer = employer;
        }

        public EmploerDetailsPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _control = _appFactory.GetComponent<IEmployerDetailsControl>();

            _control.ShowViolationDetails += ShowViolationDetails;

            _control.EditEmployer += EditEmployer;
        }

        private void EditEmployer()
        {
            var service = _appFactory.GetService<IPersonesService>();

            var persone = service.GetPerson(Employer.EmployerID, true);

            var mainPresentator = _appFactory.GetMainPresentator();

            var saveEmployerPresentator = _appFactory.GetPresentator<ISaveEmployerPresentator>();

            saveEmployerPresentator.Persone = persone;

            mainPresentator.ShowComponentForResult(this, saveEmployerPresentator, ResultTypes.UPDATE_PERSONE);
        }

        private void ShowViolationDetails(int id)
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
            
            mainPresentator.ShowComponentForResult(this, detailsViolatorPresentator, ResultTypes.DETAILS_VIOLATION);
        }
    }
}
