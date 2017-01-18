using AppPresentators.Presentators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System.Windows.Forms;
using AppPresentators.Components;
using AppPresentators.Views;
using AppPresentators.VModels.Map;
using AppData.Contexts;
using AppData.Entities;

namespace AppPresentators.Presentators
{
    public class MapPresentator : IMapPresentator
    {
        public ResultTypes ResultType { get; set; }

        public bool ShowFastSearch
        {
            get
            {
                return false;
            }
        }
        public bool ShowSearch
        {
            get
            {
                return false;
            }
        }

        public bool ShowForResult { get; set; }


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

        private IMapControl _control;
        private IMainView _parent;
        private IApplicationFactory _appFactory;

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
        }


        public MapPresentator(IMainView main, IApplicationFactory appFactory)
        {
            _parent = main;
            _appFactory = appFactory;

            _control = _appFactory.GetComponent<IMapControl>();

            _control.FilterViolations += FilteringViolation;

            _control.OpenViolation += OpenDetailsViolation;
        }

        private void OpenDetailsViolation(int id)
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


            mainPresentator.ShowComponentForResult(this, detailsViolatorPresentator, ResultTypes.UPDATE_VIOLATION);
        }

        private void FilteringViolation(MapSearchModel searchModel)
        {
            using(var db = new LeoBaseContext())
            {
                var violations = db.AdminViolations.Where(v => v.DateViolation >= searchModel.DateFrom && v.DateViolation <= searchModel.DateTo
                                                    && v.ViolationE <= searchModel.MapRegion.E
                                                    && v.ViolationN <= searchModel.MapRegion.N
                                                    && v.ViolationE >= searchModel.MapRegion.W
                                                    && v.ViolationN >= searchModel.MapRegion.S);

                var listViolations = violations == null ? new List<AdminViolation>() : violations.ToList();

                _control.DataSource = listViolations;
            }
            //throw new NotImplementedException();
        }
    }
}
