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
using AppPresentators.VModels;
using System.ComponentModel;
using System.Threading;

namespace AppPresentators.Presentators
{
    public class ViolationTablePresentator : IViolationTablePresentator
    {
        public ResultTypes ResultType { get; set; }

        public bool ShowFastSearch
        {
            get { return true; }
        }

        public bool ShowForResult { get; set; }

        public bool ShowSearch { get
            {
                return true;
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

        private IApplicationFactory _appFactory;
        private IViolationTableControl _control;
        private IViolationService _service;

        private BackgroundWorker _pageLoader;

        public ViolationTablePresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;
            _service = _appFactory.GetService<IViolationService>();
            _control = _appFactory.GetComponent<IViolationTableControl>();

            _control.RemoveViolation      += ViolationAction;
            _control.UpdateViolation      += ViolationAction;
            _control.ShowDetailsViolation += ViolationAction;

            _control.AddNewViolation      += () => ViolationAction(ViolationActionType.ADD_NEW_VIOLATION, null);

            _control.UpdateData           += () => Update(_control.SearchModel, _control.OrderModel, _control.PageModel);

            _pageLoader = new BackgroundWorker();

            _pageLoader.DoWork += PageLoading;

            _pageLoader.RunWorkerCompleted += PageLoaded;
        }

        private void PageLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            //var result = (LoadDataViolationResponseModel)e.Result;

            //if (result == null) return;

            //_control.Violations = result.Data;

            //_control.PageModel = result.PageModel;

            //_control.LoadEnd();
        }

        private void PageLoading(object sender, DoWorkEventArgs e)
        {
            //Thread.Sleep(2000);
            //var request = (LoadDataViolationRequestModel)e.Argument;

            //var order = request.OrderModel;
            //var page = request.PageModel;
            //var search = request.SearchModel;

            //_service.OrderModel = order;
            //_service.PageModel = page;
            //_service.SearchModel = search;

            //var data = _service.GetViolations();

            //if (data == null)
            //    data = new List<ViolationViewModel>();

            //var result = new LoadDataViolationResponseModel();

            //result.Data = data;
            //result.PageModel = _service.PageModel;

            //e.Result = result;
        }

        private void ViolationAction(ViolationActionType type, ViolationViewModel violation)
        {
            var mainPresentator = _appFactory.GetMainPresentator();
            IComponentPresentator presentator = null;
            ResultTypes rt = ResultTypes.NONE;

            switch (type)
            {
                case ViolationActionType.DETAILS:

                    rt = ResultTypes.UPDATE_VIOLATION;
                    break;
                case ViolationActionType.UPDATE:

                    rt = ResultTypes.UPDATE_VIOLATION;
                    break;
                case ViolationActionType.SHOW_ON_MAP:

                    rt = ResultTypes.SHOW_ON_MAP;
                    break;
                case ViolationActionType.ADD_NEW_VIOLATION:
                    presentator = _appFactory.GetPresentator<IAddOrUpdateViolation>();
                    rt = ResultTypes.ADD_VIOLATION;
                    break;
                case ViolationActionType.REMOVE:

                    return;
            }

            mainPresentator.ShowComponentForResult(this, presentator, rt);
        }

        public void FastSearch(string message)
        {
            Update(new ViolationSearchModel(), _control.OrderModel, _control.PageModel);
        }

        public Control RenderControl()
        {
            Update(_control.SearchModel, _control.OrderModel, _control.PageModel);

            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            Update(_control.SearchModel, _control.OrderModel, _control.PageModel);
        }

        public void Update(ViolationSearchModel search, ViolationOrderModel order, PageModel page)
        {
            //var request = new LoadDataViolationRequestModel
            ////{
            ////    SearchModel = search,
            ////    OrderModel = order,
            ////    PageModel = page
            //};

            //_control.LoadStart();

            //_pageLoader.RunWorkerAsync(request);
        }
    }

    //public class LoadDataViolationRequestModel
    //{
    //    public PageModel PageModel { get; set; }

    //    public ViolationOrderModel OrderModel { get; set; }

    //    public ViolationSearchModel SearchModel { get; set; }
    //}

    //public class LoadDataViolationResponseModel
    //{
    //    public List<ViolationViewModel> Data { get; set; }
    //    public PageModel PageModel { get; set; }
    //}
}
