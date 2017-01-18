using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using System.Windows.Forms;
using AppPresentators.Components;
using AppData.Contexts;

namespace AppPresentators.Presentators
{
    public interface IViolatorDetailsPresentator: IComponentPresentator
    {
        ViolatorDetailsModel Violator { get; set; }
    }

    public class ViolatorDetailsPresentator : IViolatorDetailsPresentator
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
                return _control.TopControls;
            }
        }

        public ViolatorDetailsModel Violator
        {
            get
            {
                return _control.Violator;
            }

            set
            {
                _control.Violator = value;
            }
        }

        public event SendResult SendResult;

        private IViolatorDetailsControl _control;

        private IApplicationFactory _appFactory;

        public ViolatorDetailsPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _control = _appFactory.GetComponent<IViolatorDetailsControl>();

            _control.ShowDetailsViolation += ShowDetailsViolation;
        }

        private void ShowDetailsViolation(int id)
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

        public void FastSearch(string message)
        {

        }

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {

        }
    }
}
