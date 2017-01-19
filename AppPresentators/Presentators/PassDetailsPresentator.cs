using AppData.Entities;
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

namespace AppPresentators.Presentators
{
    public interface IPassDeatailsPresentator : IComponentPresentator
    {
        Pass Pass { get; set; }
    }
    public class PassDetailsPresentator : IPassDeatailsPresentator
    {
        private IPassDetailsControl _control;
        private IMainView _parent;
        private IApplicationFactory _appFactory;

        public PassDetailsPresentator(IMainView main, IApplicationFactory appFactory)
        {
            _parent = main;
            _appFactory = appFactory;
            _control = _appFactory.GetComponent<IPassDetailsControl>();
            _control.MakeReport += MakeReport;
        }

        private void MakeReport()
        {

        }

        public Pass Pass
        {
            get
            {
                return _control.Pass;
            }

            set
            {
                _control.Pass = value;
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
        }
    }
}
