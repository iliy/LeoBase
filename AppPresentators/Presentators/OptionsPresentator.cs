using AppPresentators.Presentators.Interfaces.ComponentPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.Infrastructure;
using System.Windows.Forms;
using AppPresentators.Components;
using AppData.Contexts;
using AppPresentators.Views;

namespace AppPresentators.Presentators
{
    public interface IOptionsPresentator: IComponentPresentator
    {
           
    }

    public class OptionsPresentator : IOptionsPresentator
    {
     
        #region Interface Realisation
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
                return _control != null ? _control.TopControls : new List<Control>();
            }
        }

        public event SendResult SendResult;

        public void FastSearch(string message)
        {
        }

        public Control RenderControl()
        {
            return _control != null ? _control.GetControl() : null;
        }

        public void SetResult(ResultTypes resultType, object data)
        {
        }

        #endregion

        private IOptionsControl _control;

        private IApplicationFactory _appFactory;

        private IMainView _main;

        public OptionsPresentator(IMainView mainView, IApplicationFactory appFactory)
        {
            _main = mainView;

            _appFactory = appFactory;

            _control = _appFactory.GetComponent<IOptionsControl>();

            _control.RemoveDocumentType += RemoveDocumentType;

            _control.SaveDocumentType += SaveDocumentType;

            _control.UpdateCurrentManager += UpdateCurrentManager;

            _control.AddManager += AddNewManager;

            _control.RemoveManager += RemoveManager;

            _control.UpdateManager += UpdateManager;

            _control.GetManagers += GetManagers;

            if (ConfigApp.CurrentManager.Role.Equals("admin"))
            {
                _control.DocumentTypes = ConfigApp.DocumentTypesList;

                _control.UpdateManagerTable();
            }
        }

        private List<AppData.Entities.Manager> GetManagers()
        {
            using(var db = new LeoBaseContext())
            {
                var managers = db.Managers.Where(m => !m.Role.Equals("admin"));

                return managers != null ? managers.ToList() : new List<AppData.Entities.Manager>();
            }
        }

        private void UpdateManager(AppData.Entities.Manager obj)
        {
            using(var db = new LeoBaseContext())
            {
                var manager = db.Managers.FirstOrDefault(m => m.ManagerID == obj.ManagerID);

                if(manager == null)
                {
                    _control.ShowError("Пользователь не найден");
                    return;
                }

                manager.Password = obj.Password;

                db.SaveChanges();
            }
        }

        private void RemoveManager(AppData.Entities.Manager obj)
        {
            using(var db = new LeoBaseContext())
            {
                var manager = db.Managers.FirstOrDefault(m => m.ManagerID == obj.ManagerID);

                if(manager == null)
                {
                    _control.ShowError("Пользователь не найден");
                    return;
                }

                db.Managers.Remove(manager);

                db.SaveChanges();
            }
        }

        private void AddNewManager(AppData.Entities.Manager obj)
        {
            using(var db = new LeoBaseContext())
            {
                db.Managers.Add(obj);
                db.SaveChanges();
            }
        }

        private void UpdateCurrentManager(string login, string oldPassword, string newPassword)
        {
            if (!ConfigApp.CurrentManager.Password.Equals(oldPassword))
            {
                _control.ShowError("Старый пароль указан не верно!");

                return;
            }

            using(var db = new LeoBaseContext())
            {
                var findedManager = db.Managers.FirstOrDefault(m => m.Login.Equals(login) && m.ManagerID != ConfigApp.CurrentManager.ManagerID);

                if(findedManager != null)
                {
                    _control.ShowError("Логин уже используется");

                    return;
                }

                var manager = db.Managers.FirstOrDefault(m => m.ManagerID == ConfigApp.CurrentManager.ManagerID);

                if(manager == null)
                {
                    _control.ShowError("Пользователь не найден.");

                    return;
                }
                
                manager.Login = login;

                manager.Password = newPassword;

                db.SaveChanges();

                _control.ShowMessage("Изменения успешно сохранены");
            }
        }

        /// <summary>
        /// Сохранение типа документов
        /// </summary>
        /// <param name="name">Имя типа документов</param>
        /// <param name="id">Идентификатор типа документов</param>
        private void SaveDocumentType(string name, int id = -1)
        {
            using(var db = new LeoBaseContext())
            {
                var docType = db.DocumentsType.FirstOrDefault(d => d.Name.Equals(name));

                if (docType != null)
                {
                    _control.ShowError("Тип документов с таким именем уже существует.");

                    return;
                }

                if (id == -1)
                {
                    db.DocumentsType.Add(new AppData.Entities.DocumentType
                    {
                        Name = name
                    });
                }else
                {
                    var dt = db.DocumentsType.FirstOrDefault(d => d.DocumentTypeID == id);

                    dt.Name = name;
                }

                db.SaveChanges();

                ConfigApp.DocumentTypesWasUpdated = true;

                _control.DocumentTypes = db.DocumentsType.ToList();

                _control.ShowMessage("Тип документа успешно сохранен");
            }
        }

        /// <summary>
        /// Удаление типа документов
        /// </summary>
        /// <param name="id"></param>
        private void RemoveDocumentType(int id)
        {
            using(var db = new LeoBaseContext())
            {
                var documents = db.Documents.Where(d => d.Document_DocumentTypeID == id);

                if (documents != null && documents.Count() != 0)
                {
                    _control.ShowError("Удалить тип документов невозможно, имеются связанные с ним записи в базе данных.");
                    return;
                }

                var dt = db.DocumentsType.FirstOrDefault(d => d.DocumentTypeID == id);

                if(dt != null)
                {
                    try { 
                        db.DocumentsType.Remove(dt);

                        db.SaveChanges();

                        _control.ShowMessage("Тип документов успешно удален");

                    }catch(Exception e)
                    {
                        _control.ShowMessage("Тип документов удалить не удалось. Если ошибка будет повторяться, обратитесь к разработчику.");

                        return;
                    }
                }

                ConfigApp.DocumentTypesWasUpdated = true;

                _control.DocumentTypes = db.DocumentsType.ToList();
            }


        }
    }
}
