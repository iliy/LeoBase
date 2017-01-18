using AppData.Entities;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using System.Windows.Forms;
using AppPresentators.Components;
using AppPresentators.Services;
using AppPresentators.VModels.Persons;
using AppData.Contexts;

namespace AppPresentators.Presentators
{
    public interface ISaveAdminViolationPresentatar: IComponentPresentator
    {
        AdminViolation Violation { get; set; }
        void Save();
    }

    public class SaveAdminViolationPresentator : ISaveAdminViolationPresentatar
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


        public AdminViolation Violation { get
            {
                return _view.Violation;
            }
            set
            {
                _view.Violation = value;
            }
        }

        public event SendResult SendResult;

        private ISaveAdminViolationControl _view;
        private IApplicationFactory _appFactory;
        private IAdminViolationService _sevice;

        public SaveAdminViolationPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;
            _view = _appFactory.GetComponent<ISaveAdminViolationControl>();
            _sevice = _appFactory.GetService<IAdminViolationService>();

            _view.Save += Save;

            _view.CreateEmployer += CreateEmployer;
            _view.CreateViolator += CreateViolator;

            _view.FindEmployer += FindEmployer;
            _view.FindViolator += FindViolator;
        }

        public void FastSearch(string message)
        {

        }

        public void CreateViolator()
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var saveEmployerPresentator = _appFactory.GetPresentator<ISaveEmployerPresentator>();

            saveEmployerPresentator.Persone = new ViolatorViewModel
            {
                IsEmploeyr = false,
                UserID = -1,
                Addresses = new List<PersonAddressModelView>
                {
                    new PersonAddressModelView {
                        Country = "Российская Федерация",
                        Subject = "Приморский край",
                        Area = "Хасанский район"
                        }
                },
                Phones = new List<PhoneViewModel>
                {
                    new PhoneViewModel { }
                },
                Documents = new List<PersoneDocumentModelView>
                {
                    new PersoneDocumentModelView
                        {
                            DocumentID = -1,
                            WhenIssued = new DateTime(1999, 1, 1)
                        }
                }
            };

            mainPresentator.ShowComponentForResult(this, saveEmployerPresentator, ResultTypes.ADD_VIOLATOR);
        }

        

        public void FindEmployer()
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var tableEmployerPresentator = _appFactory.GetPresentator<IEmployersPresentator>();
            tableEmployerPresentator.ShowForResult = true;
            mainPresentator.ShowComponentForResult(this, tableEmployerPresentator, ResultTypes.FIND_EMPLOYER);
        }

        public void FindViolator()
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var tableViolatorPresentator = _appFactory.GetPresentator<IViolatorTablePresentator>();
            tableViolatorPresentator.ShowForResult = true;
            mainPresentator.ShowComponentForResult(this, tableViolatorPresentator, ResultTypes.FIND_VIOLATOR);
        }

        public void CreateEmployer()
        {

            var mainPresentator = _appFactory.GetMainPresentator();
            var saveEmployerPresentator = _appFactory.GetPresentator<ISaveEmployerPresentator>();
            var persone = new EmploeyrViewModel();

            persone.IsEmploeyr = true;

            persone.UserID = -1;

            persone.PositionID = ConfigApp.EmployerPositionsList.First() != null ? ConfigApp.EmployerPositionsList.First().PositionID.ToString() : "";
            persone.Position = ConfigApp.EmployerPositionsList.First() != null ? ConfigApp.EmployerPositionsList.First().Name.ToString() : "";

            var addresses = new List<PersonAddressModelView>();
            var documents = new List<PersoneDocumentModelView>();
            var phones = new List<PhoneViewModel>();

            addresses.Add(new PersonAddressModelView
            {
                Country = "Российская Федерация",
                Subject = "Приморский край",
                Area = "Хасанский район"
            });

            documents.Add(new PersoneDocumentModelView
            {
                DocumentID = -1
            });

            phones.Add(new PhoneViewModel());

            persone.Addresses = addresses;
            persone.Phones = phones;
            persone.Documents = documents;

            saveEmployerPresentator.Persone = persone;

            mainPresentator.ShowComponentForResult(this, saveEmployerPresentator, ResultTypes.ADD_EMPLOYER);
        }

        public Control RenderControl()
        {
            return _view.GetControl();
        }

        public void Save()
        {
            if (_sevice.SaveViolation(_view.Violation))
            {
                _view.ShowMessage("Правонарушение успешно сохранено");

                if (ShowForResult)
                {
                    if (SendResult != null)
                    {
                        SendResult(ResultType, _view.Violation);
                    }
                }
            }
            else
            {
                _view.ShowMessage("При сохранение правонарушения возеникла ошибка!\r\nВозможно не все поля заполнены верно.");
            }
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            using (var db = new LeoBaseContext())
            {
                switch (resultType)
                {
                    case ResultTypes.ADD_EMPLOYER:
                        {
                            var employerID = db.Persones.Max(p => p.UserID);
                            var employer = db.Persones.FirstOrDefault(p => p.UserID == employerID);
                            if(employer != null) { 
                                _view.Violation.Employer = employer;
                                _view.UpdateEmployer();
                            }
                        }
                        break;
                    case ResultTypes.ADD_VIOLATOR:
                        {
                            var violatorID = db.Persones.Max(p => p.UserID);
                            var violator = db.Persones.Include("Documents").Include("Phones").Include("Address").FirstOrDefault(p => p.UserID == violatorID);
                            if(violator != null)
                            {
                                _view.Violation.ViolatorPersone = violator;
                                _view.UpdateViolator();
                            }
                        }
                        break;
                    case ResultTypes.FIND_EMPLOYER:
                        {
                            var employer = data as Persone;
                            _view.Violation.Employer = employer;
                            _view.UpdateEmployer();
                        }
                        break;
                    case ResultTypes.FIND_VIOLATOR:
                        {
                            var violator = data as Persone;
                            _view.Violation.ViolatorPersone = violator;
                            _view.UpdateViolator();
                        }
                        break;
                }

            }
        }
    }
}
