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
using AppPresentators.Views;
using System.ComponentModel;
using AppPresentators.VModels;
using System.Threading;
using AppData.Contexts;

namespace AppPresentators.Presentators
{
    public class AdminViolationTablePresentator : IAdminViolationTablePresentator
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
                return true;
            }
        }

        public List<Control> TopControls
        {
            get
            {
                return _view.TopControls;
            }
        }

        public event SendResult SendResult;

        private IApplicationFactory _appFactory;
        private IAdminViolationControl _view;
        private IAdminViolationService _service;
        private IMainView _mainView;
        private BackgroundWorker _pageLoader;

        public AdminViolationTablePresentator(IMainView mainView, IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _view = _appFactory.GetComponent<IAdminViolationControl>();

            _service = _appFactory.GetService<IAdminViolationService>();

            _pageLoader = new BackgroundWorker();

            _pageLoader.DoWork += PageLoading;

            _pageLoader.RunWorkerCompleted += PageLoaded;

            _mainView = mainView;

            _view.AddViolation += AddViolation;

            _view.EditViolation += EditViolation;

            _view.ShowDetailsViolation += ShowDetaiolsForViolation;

            _view.RemoveViolation += RemoveViolation;

            _view.UpdateTable += () => UpdateData(_view.OrederModel, _view.SearchModel, _view.PageModel);
        }

        private void PageLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            var response = (LoadDataViolationResponseModel)e.Result;

            _view.DataContext = response.Data;

            _view.PageModel = response.PageModel;

            _view.LoadEnd();
        }

        private void PageLoading(object sender, DoWorkEventArgs e)
        {
            var requst = (LoadDataViolationRequestModel)e.Argument;

            var order = requst.OrderModel;
            var search = requst.SearchModel;
            var page = requst.PageModel;
            
            var data = _service.GetTableData(page, order, search);

            var response = new LoadDataViolationResponseModel();

            response.Data = data;

            response.PageModel = _service.PageModel;

            e.Result = response;
        }

        private void RemoveViolation(int id)
        {
            _service.RemoveViolation(id);
            Update();
        }

        private void ShowDetaiolsForViolation(int id)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var detailsViolatorPresentator = _appFactory.GetPresentator<IViolationDetailsPresentator>();

            //var violation = _service.GetViolation(id);
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

        private void EditViolation(int id)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var saveViolatorPresentator = _appFactory.GetPresentator<ISaveAdminViolationPresentatar>();

            //var violation = _service.GetViolation(id);
            using(var db = new LeoBaseContext())
            {
                saveViolatorPresentator.Violation = db.AdminViolations.Include("Employer")
                                                     .Include("ViolatorOrganisation")
                                                     .Include("ViolatorPersone")
                                                     .Include("ViolatorDocument")
                                                     .Include("Images")
                                                     .FirstOrDefault(v => v.ViolationID == id);

            }


            mainPresentator.ShowComponentForResult(this, saveViolatorPresentator, ResultTypes.UPDATE_VIOLATION);
        }

        private void AddViolation()
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            var saveViolatorPresentator = _appFactory.GetPresentator<ISaveAdminViolationPresentatar>();
            
            mainPresentator.ShowComponentForResult(this, saveViolatorPresentator, ResultTypes.ADD_VIOLATION);
        }

        private void UpdateData()
        {
            var tableData = _service.GetTableData(_view.PageModel, _view.OrederModel, _view.SearchModel);
            _view.DataContext = tableData;
        }

        private void UpdateData(AdminViolationOrderModel order, AdminViolationSearchModel search, PageModel page)
        {
            if (_pageLoader.IsBusy) return;

            if (page == null)
                page = new PageModel
                {
                    ItemsOnPage = 10,
                    CurentPage = 1
                };

            var request = new LoadDataViolationRequestModel
            {
                PageModel = page,
                OrderModel = order,
                SearchModel = search
            };

            _view.LoadStart();

            _pageLoader.RunWorkerAsync(request);
        }

        public void FastSearch(string message)
        {
            var service = new AdminViolationService();

            UpdateData(_view.OrederModel,  new AdminViolationSearchModel
            {
                FastSearchString = message
            }, _view.PageModel);
        }

        public Control RenderControl()
        {
            UpdateData(_view.OrederModel, _view.SearchModel, _view.PageModel);

            return _view.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            //throw new NotImplementedException();
        }

        public void Update()
        {
            UpdateData();
        }
    }

    public class LoadDataViolationRequestModel
    {
        public AdminViolationOrderModel OrderModel { get; set; }
        public AdminViolationSearchModel SearchModel { get; set; }
        public PageModel PageModel { get; set; }
    }

    public class LoadDataViolationResponseModel
    {
        public List<AdminViolationRowModel> Data { get; set; }
        public PageModel PageModel { get; set; }
    }
}
