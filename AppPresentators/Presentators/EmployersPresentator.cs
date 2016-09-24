﻿using AppPresentators.Components;
using AppPresentators.Infrastructure;
using AppPresentators.Presentators.Interfaces;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using AppPresentators.Services;
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Presentators
{
    public class EmployersPresentator : IEmployersPresentator
    {
        private IApplicationFactory _appFactory;
        private IEmployersTableControl _control;
        private IPersonesService _service;

        public EmployersPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _service = _appFactory.GetService<IPersonesService>();

            _control = _appFactory.GetComponent<IEmployersTableControl>();

            _control.UpdateTable += () => GetPersones(_control.PageModel, _control.SearchModel, _control.OrderModel);
        }

        public void GetPersones(PageModel pageModel, PersonsSearchModel searchModel, PersonsOrderModel orderModel)
        {
            if (pageModel == null)
                pageModel = new PageModel
                {
                    ItemsOnPage = 10,
                    CurentPage = 1
                };

            if (searchModel == null)
                searchModel = new PersonsSearchModel();

            searchModel.IsEmployer = true;
            
            _service.PageModel = pageModel;


        }

        public Control RenderControl()
        {
            return _control.GetControl();
        }
    }
}
