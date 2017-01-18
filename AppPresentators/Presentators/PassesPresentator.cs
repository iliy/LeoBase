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
        }

        private void UpdateTable()
        {
            using(var db = new PassedContext())
            {
                var passes = db.Passes.AsQueryable();

                if(_control.SearchModel != null && !_control.SearchModel.IsEmptySearchModel())
                {
                    var sm = _control.SearchModel;

                    if (!string.IsNullOrWhiteSpace(sm.FIO))
                    {
                        string[] spliter = sm.FIO.Split(new[] { ' ' });

                        if (spliter.Length == 3)
                        {
                            passes = passes.Where(p => p.FirstName == spliter[0] && p.SecondName == spliter[1] && p.MiddleName.Contains(spliter[2]));
                        } else if (spliter.Length == 2)
                        {
                            passes = passes.Where(p => p.FirstName == spliter[0] && p.SecondName.Contains(spliter[1]));
                        }else if(spliter.Length == 1)
                        {
                            passes = passes.Where(p => p.FirstName.Contains(spliter[0]));
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
                            passes = passes.Where(p => p.PassClosed <= sm.WhenClosed);
                            break;
                        case VModels.CompareValue.MORE:
                            passes = passes.Where(p => p.PassClosed >= sm.WhenClosed);
                            break;
                    }

                    switch (sm.CompareWhenGived)
                    {
                        case VModels.CompareValue.EQUAL:
                            passes = passes.Where(p => p.PassGiven == sm.WhenGived);
                            break;
                        case VModels.CompareValue.LESS:
                            passes = passes.Where(p => p.PassGiven <= sm.WhenGived);
                            break;
                        case VModels.CompareValue.MORE:
                            passes = passes.Where(p => p.PassGiven >= sm.WhenGived);
                            break;
                    }

                    switch (sm.CompareWhenIssued)
                    {
                        case VModels.CompareValue.EQUAL:
                            passes = passes.Where(p => p.WhenIssued == sm.WhenIssued);
                            break;
                        case VModels.CompareValue.LESS:
                            passes = passes.Where(p => p.WhenIssued <= sm.WhenIssued);
                            break;
                        case VModels.CompareValue.MORE:
                            passes = passes.Where(p => p.WhenIssued >= sm.WhenIssued);
                            break;
                    }
                }

                if(_control.OrderModel.OrderType != VModels.OrderType.NONE)
                    switch (_control.OrderModel.OrderProperties)
                    {
                        case VModels.PassesOrderProperties.BY_DATE_CLOSED:
                            passes = _control.OrderModel.OrderType == VModels.OrderType.ASC ? passes.OrderBy(p => p.PassClosed) : passes.OrderByDescending(p => p.PassClosed);
                            break;
                        case VModels.PassesOrderProperties.BY_DATE_GIVED:
                            passes = _control.OrderModel.OrderType == VModels.OrderType.ASC ? passes.OrderBy(p => p.PassGiven) : passes.OrderByDescending(p => p.PassGiven);
                            break;
                        case VModels.PassesOrderProperties.BY_FIO:
                            passes = _control.OrderModel.OrderType == VModels.OrderType.ASC ? passes.OrderBy(p => p.FirstName) : passes.OrderByDescending(p => p.FirstName);
                            break;
                    }

                List<Pass> result = null;

                if(_control.PageModel != null && _control.PageModel.CurentPage != -1)
                {
                    result = passes.Skip(_control.PageModel.ItemsOnPage * (_control.PageModel.CurentPage - 1)).Take(_control.PageModel.ItemsOnPage).ToList();
                }else
                {
                    result = passes.ToList();
                }

                _control.DataSource = result;
            }

        }

        private void AddPass()
        {

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

        private void EditPass(int obj)
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
