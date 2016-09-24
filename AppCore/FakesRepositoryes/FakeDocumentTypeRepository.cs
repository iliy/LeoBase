using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;

namespace AppData.FakesRepositoryes
{
    public class FakeDocumentTypeRepository : IDocumentTypeRepository
    {
        private List<DocumentType> _docTypes = new List<DocumentType>
            {
                new DocumentType
                {
                    DocumentTypeID = 1,
                    Name = "Паспорт"
                },
                new DocumentType
                {
                    DocumentTypeID = 2,
                    Name = "Водительские права"
                }
            };

        public IQueryable<DocumentType> DocumentTypes
        {
            get
            {
                return _docTypes.AsQueryable();
            }
        }
    }
}
