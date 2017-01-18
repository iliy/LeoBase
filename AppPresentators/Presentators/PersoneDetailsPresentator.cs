using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using AppPresentators.VModels.Persons;
using System.Windows.Forms;
using AppPresentators.Services;
using AppPresentators.Components;

namespace AppPresentators.Presentators
{
    public class PersoneDetailsPresentator : IPersoneDetailsPresentator
    {
        private IPersoneViewModel _persone;
        public IPersoneViewModel Persone {
            get {
                return _persone;
            }
            set
            {
                _persone = value;
                UpdatePersone();
            }
        }
        public ResultTypes ResultType { get; set; }

        public bool ShowFastSearch { get; set; }
        public bool ShowForResult { get; set; }

        public bool ShowSearch { get; set; }

        public List<Control> TopControls
        {
            get
            {
                return _control.TopControls;
            }
        }
        
        public event SendResult SendResult;

        private IApplicationFactory _appFactory;
        private IPersonesService _service;
        private IPersoneDetailsControl _control;

        public PersoneDetailsPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;
            _service = _appFactory.GetService<IPersonesService>();
            _control = _appFactory.GetComponent<IPersoneDetailsControl>();

        }

        public void FastSearch(string message){}

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            if(resultType == ResultTypes.UPDATE_PERSONE)
            {
                UpdatePersone();
            }
        }

        public void UpdatePersone()
        {
            int userId = Persone.UserID;

            _persone = _service.GetPerson(userId, true);

            _control.Persone = Persone;

            var violationsService = _appFactory.GetService<IViolationService>();

            if (Persone.IsEmploeyr) {
                violationsService.SearchModel = new ViolationSearchModel
                {
                    EmplyerID = Persone.UserID
                };
            }

            var violations = violationsService.GetViolations(true);

            _control.Violations = violations;
        }
    }
}
