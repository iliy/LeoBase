using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Components
{
    public interface IOptionsControl: UIComponent
    {
        /// <summary>
        /// Удаление типа документа (1 arg - id типа документа), если тип документов с заданным id используется, то запрет на удаление.
        /// </summary>
        event Action<int> RemoveDocumentType;

        /// <summary>
        /// Добавление/обновление типа документов (1 arg - название типа документа, 2 arg - id типа документа, если -1, то создание нового)
        /// </summary>
        event Action<string, int> SaveDocumentType;

        /// <summary>
        /// Обновить текущего пользователя (1 arg - новый логин, 2 arg - старый пароль, 3 arg - новый пароль)
        /// </summary>
        event Action<string, string, string> UpdateCurrentManager;

        
        List<DocumentType> DocumentTypes { get; set; }

        List<EmploeyrPosition> Positions { get; set; }

        void ShowError(string message);

        void ShowMessage(string message);
    }
}
