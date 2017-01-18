using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.VModels.Persons;
using System.Windows.Forms;
using AppPresentators.Infrastructure;
using AppPresentators.Components;
using AppPresentators.Services;
using AppData.Infrastructure;
using AppData.Abstract;
using AppData.Entities;

namespace AppPresentators.Presentators
{
    public class SavePersonPresentator : ISaveEmployerPresentator
    {
        private IApplicationFactory _appFactory;
        private ISavePersonControl _control;
        public bool ShowForResult { get; set; }

        public ResultTypes ResultType { get; set; }
        public void SetResult(ResultTypes resultType, object data)
        {
            // Может быть гдето понадобиться вызывать окно для возврата значений
        }

        public List<Control> TopControls
        {
            get
            {
                return _control.TopControls;
            }
        }

        public event SendResult SendResult;

        public IPersoneViewModel Persone {
            get {
                if (_control != null) return _control.Persone;
                return null;
            }
            set
            {
                if (_control != null)
                {
                    if(value.UserID <= 0)
                    {
                        _control.Persone = value;
                    }else
                    {
                        var persone = _service.GetPerson(value.UserID, true);
                        _control.Persone = persone;
                    }
                }
            }
        }
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

        private IPersonesService _service;

        public SavePersonPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;
            _control = appFactory.GetComponent<ISavePersonControl>();
            _service = _appFactory.GetService<IPersonesService>();

            _control.SavePersone += () =>
            {
                var persone = _control.Persone;
                if (persone != null)
                {
                    SavePersone(persone);
                }
            };
        }

        public void FastSearch(string message){}

        public Control RenderControl()
        {
            return _control.GetControl();
        }

        public void SavePersone(IPersoneViewModel persone)
        {
            bool result = false;
            
            try { 
                if(persone.UserID <= 0)
                {
                    int userId = _service.AddNewPersone(persone);
                    if (userId > 0) result = true;
                }else
                {
                    result = _service.UpdatePersone(persone);
                }
            }catch(ArgumentException e)
            {
                _control.ShowMessage(e.Message);
                return;
            }

            if (result)
            {
                string message = persone.IsEmploeyr ? "Сотрудник успешно сохранен!" : "Нарушитель успешно сохранен!";
                _control.ShowMessage(message);
                if (ShowForResult)
                {
                    if(SendResult != null)
                    {
                        SendResult(ResultType, result);
                    }
                }
            }else
            {
                string message = persone.IsEmploeyr ? "Возникли ошибки при сохранение сотрудника!" : "Возникли ошибки при сохранение нарушителя!";
                _control.ShowMessage(message);
            }
        }

    }
}
