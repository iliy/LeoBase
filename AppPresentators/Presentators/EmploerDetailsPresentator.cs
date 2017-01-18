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
    public interface IEmployerDetailsPresentator : IComponentPresentator
    {
        EmployerDetailsModel Employer { get; set; }
    }

    public class EmploerDetailsPresentator : IEmployerDetailsPresentator
    {
        private IEmployerDetailsControl _control;
        private IApplicationFactory _appFactory;

        public EmployerDetailsModel Employer
        {
            get
            {
                return _control.Employer;
            }

            set
            {
                _control.Employer = value;
            }
        }

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

        public event SendResult SendResult;

        public void FastSearch(string message)
        {
        }

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            EmployerDetailsModel newEmployer = data as EmployerDetailsModel;

            if (newEmployer == null) return;

            if(resultType == ResultTypes.UPDATE_PERSONE) Employer = newEmployer;
        }

        public EmploerDetailsPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _control = _appFactory.GetComponent<IEmployerDetailsControl>();

            _control.ShowViolationDetails += ShowViolationDetails;
        }

        private void ShowViolationDetails(int id)
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
            
            mainPresentator.ShowComponentForResult(this, detailsViolatorPresentator, ResultTypes.DETAILS_VIOLATION);
        }
    }
}
