using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using System.Windows.Forms;
using AppPresentators.Components;
using AppPresentators.Views;
using AppData.Contexts;
using AppData.Entities;
using System.ComponentModel;
using AppPresentators.VModels;

namespace AppPresentators.Presentators
{
    public interface IPassesPresentator: IComponentPresentator
    {

    }
    public class PassesPresentator : IPassesPresentator
    {
        private IPassesTableControl _control;
        private IApplicationFactory _appFactory;
        private IMainView _parent;
        private BackgroundWorker _pageLoader;

        public PassesPresentator(IMainView main, IApplicationFactory appFactory)
        {
            _parent = main;

            _appFactory = appFactory;

            _control = appFactory.GetComponent<IPassesTableControl>();

            _control.EditPass += EditPass;

            _control.MakeReport += MakeReport;

            _control.RemovePass += RemovePass;

            _control.AddPass += AddPass;

            _control.UpdateTable += UpdateTable;

            _control.ShowDetailsPass += ShowDetails;

            _pageLoader = new BackgroundWorker();

            _pageLoader.DoWork += PageLoading;

            _pageLoader.RunWorkerCompleted += LoadComplete;

            UpdateTable();
        }

        private void ShowDetails(int id)
        {
            using(var db = new PassedContext())
            {
                var pass = db.Passes.FirstOrDefault(p => p.PassID == id);

                if(pass == null)
                {
                    _control.ShowError("Пропуск не найден");
                    return;
                }

                var mainPresentator = _appFactory.GetMainPresentator();

                var detailsPresentator = _appFactory.GetPresentator<IPassDeatailsPresentator>();

                detailsPresentator.Pass = pass;

                mainPresentator.ShowComponentForResult(this, detailsPresentator, ResultTypes.DETAILS_PASS);
            }
        }

        private void LoadComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            var response = (LoadDataPassResponseModel)e.Result;

            _control.DataSource = response.Data;
            
            _control.PageModel = response.PageModel;

            _control.LoadEnd();

        }

        private void PageLoading(object sender, DoWorkEventArgs e)
        {
            var loadModel = (LoadDataPassModel)e.Argument;

            using (var db = new PassedContext())
            {
                var passes = db.Passes.AsQueryable();

                if (loadModel.SearchModel != null && !loadModel.SearchModel.IsEmptySearchModel())
                {
                    var sm = loadModel.SearchModel;

                    if (!string.IsNullOrWhiteSpace(sm.FIO))
                    {
                        string[] spliter = sm.FIO.Split(new[] { ' ' });

                        if (spliter.Length == 3)
                        {
                            string fn = spliter[0];
                            string sn = spliter[1];
                            string mn = spliter[2];

                            passes = passes.Where(p => p.FirstName == fn && p.SecondName == sn && p.MiddleName.Contains(mn));
                        }
                        else if (spliter.Length == 2)
                        {
                            string fn = spliter[0];
                            string sn = spliter[1];

                            passes = passes.Where(p => p.FirstName == fn && p.SecondName.Contains(sn));
                        }
                        else if (spliter.Length == 1)
                        {
                            string fn = spliter[0];

                            passes = passes.Where(p => p.FirstName.Contains(fn));
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(sm.DocumentType))
                    {
                        passes = passes.Where(p => p.DocumentType.Contains(sm.DocumentType));
                    }

                    if (!string.IsNullOrWhiteSpace(sm.Serial))
                    {
                        passes = passes.Where(p => p.Serial == sm.Serial);
                    }

                    if (!string.IsNullOrWhiteSpace(sm.Number))
                    {
                        passes = passes.Where(p => p.Number == sm.Number);
                    }

                    if (!string.IsNullOrWhiteSpace(sm.IssuedBy))
                    {
                        passes = passes.Where(p => p.WhoIssued.Contains(sm.IssuedBy));
                    }

                    switch (sm.CompareWhenClosed)
                    {
                        case VModels.CompareValue.EQUAL:
                            passes = passes.Where(p => p.PassClosed == sm.WhenClosed);
                            break;
                        case VModels.CompareValue.LESS:
                            passes = passes.Where(p => p.PassClosed < sm.WhenClosed);
                            break;
                        case VModels.CompareValue.MORE:
                            passes = passes.Where(p => p.PassClosed > sm.WhenClosed);
                            break;
                    }

                    switch (sm.CompareWhenGived)
                    {
                        case VModels.CompareValue.EQUAL:
                            passes = passes.Where(p => p.PassGiven == sm.WhenGived);
                            break;
                        case VModels.CompareValue.LESS:
                            passes = passes.Where(p => p.PassGiven < sm.WhenGived);
                            break;
                        case VModels.CompareValue.MORE:
                            passes = passes.Where(p => p.PassGiven > sm.WhenGived);
                            break;
                    }

                    switch (sm.CompareWhenIssued)
                    {
                        case VModels.CompareValue.EQUAL:
                            passes = passes.Where(p => p.WhenIssued == sm.WhenIssued);
                            break;
                        case VModels.CompareValue.LESS:
                            passes = passes.Where(p => p.WhenIssued < sm.WhenIssued);
                            break;
                        case VModels.CompareValue.MORE:
                            passes = passes.Where(p => p.WhenIssued > sm.WhenIssued);
                            break;
                    }
                }

                if (loadModel.OrderModel.OrderType != VModels.OrderType.NONE)
                    switch (loadModel.OrderModel.OrderProperties)
                    {
                        case VModels.PassesOrderProperties.BY_DATE_CLOSED:
                            passes = loadModel.OrderModel.OrderType == VModels.OrderType.ASC 
                                ? passes.OrderBy(p => p.PassClosed) 
                                : passes.OrderByDescending(p => p.PassClosed);
                            break;
                        case VModels.PassesOrderProperties.BY_DATE_GIVED:
                            passes = loadModel.OrderModel.OrderType == VModels.OrderType.ASC 
                                ? passes.OrderBy(p => p.PassGiven) 
                                : passes.OrderByDescending(p => p.PassGiven);
                            break;
                        case VModels.PassesOrderProperties.BY_FIO:
                            passes = loadModel.OrderModel.OrderType == VModels.OrderType.ASC 
                                ? passes.OrderBy(p => p.FirstName) 
                                : passes.OrderByDescending(p => p.FirstName);
                            break;
                    }

                List<Pass> result = null;

                if (loadModel.PageModel != null && loadModel.PageModel.CurentPage != -1)
                {
                    result = passes.Skip(loadModel.PageModel.ItemsOnPage * (loadModel.PageModel.CurentPage - 1)).Take(loadModel.PageModel.ItemsOnPage).ToList();
                }
                else
                {
                    result = passes.ToList();
                }

                //_control.DataSource = result;
                LoadDataPassResponseModel response = new LoadDataPassResponseModel();

                response.Data = result;
                response.PageModel = loadModel.PageModel;

                e.Result = response;
            }

        }

        private void UpdateTable()
        {
            if (_pageLoader.IsBusy) return;

            var page = _control.PageModel;
            var order = _control.OrderModel;
            var search = _control.SearchModel;

            if (_control == null)
                page = new PageModel
                {
                    ItemsOnPage = 10,
                    CurentPage = 1
                };

            var request = new LoadDataPassModel
            {
                PageModel = page,
                OrderModel = order,
                SearchModel = search
            };

            _control.LoadStart();

            _pageLoader.RunWorkerAsync(request);

            return;
            
        }

        private void AddPass()
        {
            var pass = new Pass
            {
                PassID = -1,
                DocumentType = ConfigApp.DocumentTypesList.FirstOrDefault().Name,
                PassGiven = DateTime.Now,
                PassClosed = DateTime.Now.AddYears(1),
                WhenIssued = DateTime.Now.AddYears(-2)
            };

            var mainPresentator = _appFactory.GetMainPresentator();

            var savePassPresentator = _appFactory.GetPresentator<IEditPassPresentator>();

            savePassPresentator.Pass = pass;

            mainPresentator.ShowComponentForResult(this, savePassPresentator, ResultTypes.SAVE_PASS);

        }
        
        private void EditPass(int id)
        {
            using(var db = new PassedContext())
            {
                var pass = db.Passes.FirstOrDefault(p => p.PassID == id);

                if (pass == null)
                {
                    _control.ShowError("Пропуск не найден");
                    return;
                }

                var mainPresentator = _appFactory.GetMainPresentator();

                var editPassPresentator = _appFactory.GetPresentator<IEditPassPresentator>();

                editPassPresentator.Pass = pass;

                mainPresentator.ShowComponentForResult(this, editPassPresentator, ResultTypes.EDIT_PASS);
            }
        }

        private void RemovePass(int id)
        {
            if (_control.ShowDialog("Вы уверены что хотите удалить эту запись?"))
            {
                using(var db = new PassedContext())
                {
                    var pass = db.Passes.FirstOrDefault(p => p.PassID == id);

                    if (pass != null)
                    {
                        db.Passes.Remove(pass);

                        db.SaveChanges();
                    }
                }
            }
        }

        private void MakeReport(object obj)
        {

        }

        public ResultTypes ResultType { get; set; }

        public bool ShowFastSearch
        {
            get
            {
                return true;
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
                return _control.TopControls;
            }
        }

        public event SendResult SendResult;

        public void FastSearch(string message)
        {
            if (_pageLoader.IsBusy) return;
            
            var page = _control.PageModel;
            var order = _control.OrderModel;
            var search = new PassesSearchModel
            {
                FIO = message
            };

            if (_control == null)
                page = new PageModel
                {
                    ItemsOnPage = 10,
                    CurentPage = 1
                };

            var request = new LoadDataPassModel
            {
                PageModel = page,
                OrderModel = order,
                SearchModel = search
            };

            _control.LoadStart();

            _pageLoader.RunWorkerAsync(request);

            return;

        }

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            if(resultType == ResultTypes.SAVE_PASS || resultType == ResultTypes.EDIT_PASS)
            {
                UpdateTable();
            }
        }
    }

    public class LoadDataPassModel
    {
        public PageModel PageModel { get; set; }
        public PassesOrderModel OrderModel { get; set; }
        public PassesSearchModel SearchModel { get; set; }
    }

    public class LoadDataPassResponseModel
    {
        public List<Pass> Data { get; set; }
        public PageModel PageModel { get; set; }
    }
}
