using AppData.Contexts;
using AppPresentators.Components;
using AppPresentators.Infrastructure;
using AppPresentators.Presentators.Interfaces;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using AppPresentators.Services;
using AppPresentators.Views;
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Presentators
{
    public class EmployersPresentator : IEmployersPresentator
    {
        private IApplicationFactory _appFactory;
        private IEmployersTableControl _control;
        private IPersonesService _service;
        private IMainView _mainView;

        public event SendResult SendResult;

        private BackgroundWorker _pageLoader;

        public bool ShowFastSearch { get { return true; } }
        public bool ShowSearch { get { return true; } }

        private bool _showForResult = false;
        public bool ShowForResult
        {
            get
            {
                return _showForResult;
            }
            set
            {
                _showForResult = value;
                _control.ShowForResult = value;
            }
        }


        public ResultTypes ResultType { get; set; }

        public List<Control> TopControls {
            get
            {
                return _control.TopControls;
            }
        }
        public void SetResult(ResultTypes resultType, object data)
        {
            switch (resultType)
            {
                case ResultTypes.ADD_PERSONE:
                    bool result = (bool)data;
                    if (result)
                    {
                        GetPersones(_control.PageModel, _control.PersoneSearchModel, _control.AddressSearchModel, _control.OrderModel, _control.DocumentSearchModel);
                    }
                    break;
            }
        }

        public EmployersPresentator(IMainView main, IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _mainView = main;

            _service = _appFactory.GetService<IPersonesService>();

            _control = _appFactory.GetComponent<IEmployersTableControl>();

            _control.UpdateTable += () => GetPersones(_control.PageModel, _control.PersoneSearchModel, _control.AddressSearchModel, _control.OrderModel, _control.DocumentSearchModel);

            _control.AddNewEmployer += AddNewEmployer;

            _control.EditPersone += UpdatePersone;

            _control.DeletePersone += DeletePersone;

            _control.ShowPersoneDetails += ShowEmployerDetails;

            _pageLoader = new BackgroundWorker();

            _pageLoader.DoWork += PageLoading;

            _pageLoader.RunWorkerCompleted += PageLoaded;

            _control.SelectedItemForResult += SelectedItemForResult;
        }

        private void SelectedItemForResult(EmploeyrViewModel obj)
        {
            if (ShowForResult)
            {
                if (SendResult != null) SendResult(ResultType, _service.ConverToEnity(_service.GetPerson(obj.UserID, true)));
            }
        }

        private void PageLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            var answer = (LoadEmplyerDataResultModel)e.Result;

            _control.Data = answer.Data;

            _control.PageModel = answer.PageModel;

            _control.Update();

            _control.LoadEnd();
        }

        private void PageLoading(object sender, DoWorkEventArgs e)
        {
            var request = (LoadEmployerDataRequestModel)e.Argument;

            var pageModel = request.PageModel;
            var searchModel = request.SearchModel;
            var addressSearchModel = request.AddressSearchModel;
            var documentSearchModel = new DocumentSearchModel();
            var orderModel = request.OrderModel;

            if (pageModel == null)
                pageModel = new PageModel
                {
                    ItemsOnPage = 10,
                    CurentPage = 1
                };

            if (searchModel == null)
                searchModel = new PersonsSearchModel();

            if (addressSearchModel == null)
                addressSearchModel = new SearchAddressModel();

            if (orderModel == null)
                orderModel = new PersonsOrderModel();

            searchModel.IsEmployer = true;

            _service.PageModel = pageModel;
            _service.DocumentSearchModel = documentSearchModel;
            _service.SearchModel = searchModel;
            _service.AddressSearchModel = addressSearchModel;
            _service.OrderModel = orderModel;

            var result = _service.GetPersons();

            var answer = new LoadEmplyerDataResultModel();

            if (result == null)
                answer.Data = new List<EmploeyrViewModel>();
            else
                answer.Data = result.Select(v => (EmploeyrViewModel)v).ToList();

            answer.PageModel = _service.PageModel;

            e.Result = answer;
        }
        

        public void ShowEmployerDetails(IPersoneViewModel personeModel)
        {
            var ee = personeModel as EmploeyrViewModel;

            if (ee == null) return;

            var mainPresentator = _appFactory.GetMainPresentator();
            var detailsEmployerPresentator = _appFactory.GetPresentator<IEmployerDetailsPresentator>();
            
            EmployerDetailsModel employer = new EmployerDetailsModel
            {
                FIO = personeModel.FirstName + " " + personeModel.SecondName + " " + personeModel.MiddleName,
                DateBerth = personeModel.DateBirthday.ToShortDateString(),
                Position = ee.Position,
                PlaceBerth = personeModel.PlaceOfBirth,
                EmployerID = personeModel.UserID
            };


            using(var db = new LeoBaseContext())
            {
                var searchForImage = db.Persones.FirstOrDefault(p => p.UserID == personeModel.UserID);

                if(searchForImage != null)
                {
                    employer.Image = searchForImage.Image;
                }

                employer.Addresses = new List<PersonAddressModelView>();

                var addresses = db.Addresses.Where(a => a.Persone.UserID == ee.UserID);

                if(addresses != null)
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

            detailsEmployerPresentator.Employer = employer;

            mainPresentator.ShowComponentForResult(this, detailsEmployerPresentator, ResultTypes.UPDATE_PERSONE);
        }

        public void DeletePersone(IPersoneViewModel personeModel)
        {
            _service.SimpleRemove(personeModel);
            GetPersones(_control.PageModel, _control.PersoneSearchModel, _control.AddressSearchModel, _control.OrderModel, _control.DocumentSearchModel);
        }

        public void UpdatePersone(IPersoneViewModel personeModel)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var saveEmployerPresentator = _appFactory.GetPresentator<ISaveEmployerPresentator>();

            saveEmployerPresentator.Persone = personeModel;

            mainPresentator.ShowComponentForResult(this, saveEmployerPresentator, ResultTypes.UPDATE_PERSONE);
        }

        public void AddNewEmployer()
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

            mainPresentator.ShowComponentForResult(this, saveEmployerPresentator, ResultTypes.ADD_PERSONE);
            //mainPresentator.SetNextPage(saveEmployerPresentator);
        }

        public void FastSearch(string query)
        {
            string[] qs = query.Split(new char[] { ' ' });
            PersonsSearchModel searchModel = new PersonsSearchModel();

            if(qs.Length >= 1)
            {
                searchModel.FirstName = qs[0];
                searchModel.CompareFirstName = CompareString.CONTAINS;
            }

            if(qs.Length >= 2)
            {
                searchModel.SecondName = qs[1];
                searchModel.CompareSecondName = CompareString.CONTAINS;
            }

            if(qs.Length >= 3)
            {
                searchModel.MiddleName = qs[2];
                searchModel.CompareMiddleName = CompareString.CONTAINS;
            }
            
            GetPersones(_control.PageModel, searchModel, new SearchAddressModel(), _control.OrderModel, new DocumentSearchModel());
        }

        public void GetPersones(PageModel pageModel, PersonsSearchModel searchModel, SearchAddressModel addressSearchModel, PersonsOrderModel orderModel, DocumentSearchModel documentSearchModel)
        {
            if (!_mainView.Enabled || _pageLoader.IsBusy) return;

            LoadEmployerDataRequestModel request = new LoadEmployerDataRequestModel
            {
                AddressSearchModel = addressSearchModel,
                OrderModel = orderModel,
                SearchModel = searchModel,
                PageModel = pageModel
            };

            _control.LoadStart();

            _pageLoader.RunWorkerAsync(request);
        }

        public Control RenderControl()
        {
            GetPersones(_control.PageModel, _control.PersoneSearchModel, _control.AddressSearchModel, _control.OrderModel, _control.DocumentSearchModel);
            return _control.GetControl();
        }

    }

    public class LoadEmployerDataRequestModel
    {
        public PageModel PageModel { get; set; }
        public PersonsSearchModel SearchModel { get; set; }
        public SearchAddressModel AddressSearchModel { get; set; }
        public PersonsOrderModel OrderModel { get; set; }
    }

    public class LoadEmplyerDataResultModel
    {
        public List<EmploeyrViewModel> Data { get; set; }
        public PageModel PageModel { get; set; }
    }
}
