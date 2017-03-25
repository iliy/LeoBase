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
using AppPresentators.Infrastructure.Orders;

namespace AppPresentators.Presentators
{
    public class AdminViolationTablePresentator : IAdminViolationTablePresentator, IOrderPage
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

        public Infrastructure.Orders.OrderType OrderType
        {
            get
            {
                return Infrastructure.Orders.OrderType.TABLE;
            }
        }

        private string _orderDirPath;

        public string OrderDirPath
        {
            get
            {
                return _orderDirPath;
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

            _view.BuildReport += BuildReport;

            _view.UpdateTable += () => UpdateData(_view.OrederModel, _view.SearchModel, _view.PageModel);
        }


        private static PageModel _page;

        private static AdminViolationSearchModel _search;

        private static AdminViolationOrderModel _order;

        private void BuildReport()
        {
            var mainView = _appFactory.GetMainView();

            _page = _view.PageModel;
            _search = _view.SearchModel;
            _order = _view.OrederModel;

            mainView.MakeOrder(this);
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

        public void BuildOrder(IOrderBuilder orderBuilder, OrderConfigs configs)
        {
            //_view.PageModel, _view.OrederModel, _view.SearchModel

            List<AdminViolationRowModel> data = null;// _service.GetTableData(page, order, search);
            
            if (!configs.TableConfig.ConsiderFilter) _search = null;

            if (!configs.TableConfig.CurrentPageOnly)
                _page = new PageModel()
                {
                    CurentPage = 1,
                    ItemsOnPage = 1000000
                };

            data = _service.GetTableData(_page, _order, _search);

            string[] headers = new[]
            {
                "Нарушитель",
                "ФИО сотрудника",
                "Координаты",
                "Рассмотрение",
                "Нарушение",
                "Дата оплаты",
                "Сумма наложения",
                "Сумма взыскания",
                "Отправление",
                "Отправлено судебным приставам",
                "Извещение",
                "Повестка по статье 20.25"
            };

            orderBuilder.StartTable("violations table", headers);

            foreach (var row in data)
            {
                string[] cells = new string[headers.Length];
                cells[0] = row.ViolatorInfo;
                cells[1] = row.EmployerInfo;
                cells[2] = row.Coordinates;
                cells[3] = row.Consideration.ToShortDateString();
                cells[4] = row.Violation;
                cells[5] = row.DatePaymant;
                cells[6] = row.SumViolation.ToString();
                cells[7] = row.SumRecovery.ToString();
                cells[8] = row.InformationAboutSending;
                cells[9] = row.DateSentBailiff;
                cells[10] = row.InformationAboutNotice;
                cells[11] = row.InformationAbout2025;

                RowColor color = RowColor.DEFAULT;

                if (row.SumRecovery == row.SumViolation)
                {
                    color = RowColor.GREEN;
                }
                else if (row.SumRecovery < row.SumViolation && (DateTime.Now - row.Consideration).Days > 70)
                {
                    color = RowColor.RED;
                }
                else if ((DateTime.Now - row.Consideration).Days < 70)
                {
                    color = RowColor.YELLOW;
                }

                orderBuilder.WriteRow(cells, color);
            }

            orderBuilder.EndTable("violations table");

            _orderDirPath = orderBuilder.Save();
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
