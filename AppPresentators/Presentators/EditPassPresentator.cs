using AppData.Entities;
using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using System.Windows.Forms;
using AppPresentators.Views;
using AppPresentators.Components;
using AppData.Contexts;

namespace AppPresentators.Presentators
{
    public interface IEditPassPresentator: IComponentPresentator
    {
        Pass Pass { get; set; }
    }

    public class EditPassPresentator : IEditPassPresentator
    {
        private IMainView _parent;

        private IApplicationFactory _appFactory;

        private IEditPassControl _control;

        public EditPassPresentator(IMainView main, IApplicationFactory appFactory)
        {
            _parent = main;
            _appFactory = appFactory;
            _control = _appFactory.GetComponent<IEditPassControl>();
            _control.Save += Save;
        }

        private void Save()
        {
            if (!ValidateData()) return;
            
            using(var db = new PassedContext())
            {
                var pass = db.Passes.FirstOrDefault(p => p.PassID == _control.Pass.PassID);
                
                if(pass == null)
                {
                    db.Passes.Add(_control.Pass);
                }else
                {
                    pass.DocumentType = _control.Pass.DocumentType;
                    pass.FirstName = _control.Pass.FirstName;
                    pass.MiddleName = _control.Pass.MiddleName;
                    pass.Number = _control.Pass.Number;
                    pass.PassClosed = _control.Pass.PassClosed;
                    pass.PassGiven = _control.Pass.PassGiven;
                    pass.SecondName = _control.Pass.SecondName;
                    pass.Serial = _control.Pass.Serial;
                    pass.WhenIssued = _control.Pass.WhenIssued;
                    pass.WhoIssued = _control.Pass.WhoIssued;
                }
                try {
                    db.SaveChanges();
                    _control.ShowMessage("Пропуск успешно сохранен.");

                    if (ShowForResult)
                    {
                        if (SendResult != null)
                        {
                            SendResult(ResultType, pass);
                        }
                    }
                } catch (Exception e)
                {
                    _control.ShowError("При сохранение пропуска возникли ошибки. Обратитесь к разработчику.");
                }
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(_control.Pass.FirstName))
            {
                _control.ShowMessage("Укажите фамилию");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_control.Pass.SecondName))
            {
                _control.ShowMessage("Укажите имя");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_control.Pass.MiddleName))
            {
                _control.ShowMessage("Укажите отчество");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_control.Pass.DocumentType) || ConfigApp.DocumentTypesList.FirstOrDefault(d => d.Name == _control.Pass.DocumentType) == null)
            {
                _control.ShowMessage("Выберите тип документа");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_control.Pass.Number))
            {
                _control.ShowMessage("Укажите номер документа");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_control.Pass.Serial))
            {
                _control.ShowMessage("Укажите серию документа");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_control.Pass.WhoIssued))
            {
               _control.ShowMessage("Укажите кем был выдан документ");
                return false;
            }

            if(_control.Pass.PassGiven.Year <= 1900)
            {
                _control.ShowMessage("Неверно указана дата выдачи пропуска");
                return false;
            }

            if(_control.Pass.PassGiven >= _control.Pass.PassClosed)
            {
                _control.ShowMessage("Дата выдачи пропуска должна быть раньше даты закрытия пропуска");
                return false;
            }

            if(_control.Pass.WhenIssued.Year <= 1900)
            {
                _control.ShowMessage("Неверно указана дата выдачи документа");
                return false;
            }

            return true;
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
        public bool ShowForResult { get; set; }

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
