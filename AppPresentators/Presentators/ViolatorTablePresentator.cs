using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System.Windows.Forms;
using AppPresentators.Components;
using AppPresentators.Services;
using AppPresentators.Views;
using System.ComponentModel;
using System.Threading;
using AppData.Contexts;

namespace AppPresentators.Presentators
{
    public class ViolatorTablePresentator : IViolatorTablePresentator
    {
        private IApplicationFactory _appFactory;
        private IViolatorTableControl _control;
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

        public List<Control> TopControls
        {
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

        public ViolatorTablePresentator(IMainView main, IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _mainView = main;

            _service = _appFactory.GetService<IPersonesService>();

            _control = _appFactory.GetComponent<IViolatorTableControl>();

            _control.UpdateTable += () => GetPersones(_control.PageModel, _control.PersoneSearchModel, _control.AddressSearchModel, _control.OrderModel, _control.DocumentSearchModel);

            _control.AddNewEmployer += AddNewEmployer;

            _control.EditPersone += UpdatePersone;

            _control.DeletePersone += DeletePersone;

            _control.ShowPersoneDetails += ShowEmployerDetails;

            _pageLoader = new BackgroundWorker();

            _pageLoader.DoWork += PageLoading;

            _pageLoader.RunWorkerCompleted += PageLoadingComplete;

            _control.SelectedItemForResult += SelectedItemForResult;
        }

        private void PageLoadingComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            var answer = (LoadViolatorDataResultModel)e.Result;

            _control.Data = answer.Data;

            _control.PageModel = answer.PageModel;

            _control.Update();

            _control.LoadEnd();
        }

        private void PageLoading(object sender, DoWorkEventArgs e)
        {
            var request = (LoadViolatorDataRequestModel)e.Argument;

            var pageModel = request.PageModel;

            var searchModel = request.SearchModel;

            var addressSearchModel = request.AddressSearchModel;

            var documentSearchModel = request.DocumentSearchModel;

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

            if (documentSearchModel == null)
                documentSearchModel = new DocumentSearchModel();

            if (orderModel == null)
                orderModel = new PersonsOrderModel();

            searchModel.IsEmployer = false;

            _service.PageModel = pageModel;
            _service.DocumentSearchModel = documentSearchModel;
            _service.SearchModel = searchModel;
            _service.AddressSearchModel = addressSearchModel;
            _service.OrderModel = orderModel;

            var result = _service.GetPersons();

            var answer = new LoadViolatorDataResultModel();

            if (result == null)
                answer.Data = new List<ViolatorViewModel>();
            else
                answer.Data = result.Select(v => (ViolatorViewModel)v).ToList();

            answer.PageModel = _service.PageModel;

            e.Result = answer;
        }

        private void SelectedItemForResult(ViolatorViewModel obj)
        {
            if (ShowForResult)
            {
                if (SendResult != null) SendResult(ResultType, _service.ConverToEnity(_service.GetPerson(obj.UserID, true)));
            }
        }

        public void ShowEmployerDetails(IPersoneViewModel personeModel)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var violatorDetailsPresentator = _appFactory.GetPresentator<IViolatorDetailsPresentator>();

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

            using(var db = new LeoBaseContext())
            {
                var persone = db.Persones.FirstOrDefault(p => p.UserID == personeModel.UserID);

                if (persone != null) violator.Image = persone.Image;

                var documents = db.Documents.Where(d => d.Persone.UserID == personeModel.UserID);

                if(documents != null) {

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

                if(addresses != null)
                {
                    violator.Addresses = new List<PersonAddressModelView>();
                    foreach(var a in addresses)
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

                if(phones != null)
                {

                    violator.Phones = new List<PhoneViewModel>();
                    foreach(var p in phones)
                    {
                        violator.Phones.Add(new PhoneViewModel
                        {
                            PhoneID = p.PhoneID,
                            PhoneNumber = p.PhoneNumber
                        });
                    }
                }

                var violations = db.AdminViolations.Include("Employer").Include("ViolatorPersone").Where(v => v.ViolatorPersone.UserID == vv.UserID);
                foreach(var v in violations)
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

            mainPresentator.ShowComponentForResult(this, violatorDetailsPresentator, ResultTypes.UPDATE_PERSONE);
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

            mainPresentator.ShowComponentForResult(this, saveEmployerPresentator, ResultTypes.ADD_PERSONE);
        }

        public void FastSearch(string query)
        {
            string[] qs = query.Split(new char[] { ' ' });
            PersonsSearchModel searchModel = new PersonsSearchModel();

            if (qs.Length >= 1)
            {
                searchModel.FirstName = qs[0];
                searchModel.CompareFirstName = CompareString.CONTAINS;
            }

            if (qs.Length >= 2)
            {
                searchModel.SecondName = qs[1];
                searchModel.CompareSecondName = CompareString.CONTAINS;
            }

            if (qs.Length >= 3)
            {
                searchModel.MiddleName = qs[2];
                searchModel.CompareMiddleName = CompareString.CONTAINS;
            }

            GetPersones(_control.PageModel, searchModel, new SearchAddressModel(), _control.OrderModel, new DocumentSearchModel());
        }

        public void GetPersones(PageModel pageModel, PersonsSearchModel searchModel, SearchAddressModel addressSearchModel, PersonsOrderModel orderModel, DocumentSearchModel documentSearchModel)
        {
            if (!_mainView.Enabled || _pageLoader.IsBusy) return;

            LoadViolatorDataRequestModel loadRequest = new LoadViolatorDataRequestModel
            {
                PageModel = pageModel,
                SearchModel = searchModel,
                AddressSearchModel = addressSearchModel,
                OrderModel = orderModel,
                DocumentSearchModel = documentSearchModel
            };

            _control.LoadStart();

            _pageLoader.RunWorkerAsync(loadRequest);
        }

        public Control RenderControl()
        {
            GetPersones(_control.PageModel, _control.PersoneSearchModel, _control.AddressSearchModel, _control.OrderModel, _control.DocumentSearchModel);
            return _control.GetControl();
        }
    }

    public class LoadViolatorDataRequestModel
    {
        public PageModel PageModel { get; set; }
        public PersonsSearchModel SearchModel { get; set; }
        public SearchAddressModel AddressSearchModel { get; set; }
        public PersonsOrderModel OrderModel { get; set; }
        public DocumentSearchModel DocumentSearchModel { get; set; }
    }

    public class LoadViolatorDataResultModel
    {
        public List<ViolatorViewModel> Data { get; set; }
        public PageModel PageModel { get; set; }
    }
}
