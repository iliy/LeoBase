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

        public EmployersPresentator(IMainView main, IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _mainView = main;

            _service = _appFactory.GetService<IPersonesService>();

            _control = _appFactory.GetComponent<IEmployersTableControl>();

            _control.UpdateTable += () => GetPersones(_control.PageModel, _control.SearchModel, _control.OrderModel, _control.DocumentSearchModel);
            
            GetPersones(_control.PageModel, _control.SearchModel, _control.OrderModel, _control.DocumentSearchModel);
        }

        public void GetPersones(PageModel pageModel, PersonsSearchModel searchModel, PersonsOrderModel orderModel, DocumentSearchModel documentSearchModel)
        {
            if (!_mainView.Enabled) return;

            _mainView.StartTask();
            
            if (pageModel == null)
                pageModel = new PageModel
                {
                    ItemsOnPage = 10,
                    CurentPage = 1
                };

            if (searchModel == null)
                searchModel = new PersonsSearchModel();

            if (documentSearchModel == null)
                documentSearchModel = new DocumentSearchModel();

            if (orderModel == null)
                orderModel = new PersonsOrderModel();

            searchModel.IsEmployer = true;
            
            _service.PageModel = pageModel;
            _service.DocumentSearchModel = documentSearchModel;
            _service.SearchModel = searchModel;
            _service.OrderModel = orderModel;

            _control.Data = _service.GetPersons();

            _control.PageModel = _service.PageModel;

            _control.Update();

            _mainView.EndTask();
        }

        public Control RenderControl()
        {
            return _control.GetControl();
        }
    }
}
