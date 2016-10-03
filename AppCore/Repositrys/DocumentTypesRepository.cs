using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys
{
    public class DocumentTypesRepository : IDocumentTypeRepository
    {
        public IQueryable<DocumentType> DocumentTypes
        {
            get
            {
                var db = new LeoBaseContext();
                return db.DocumentsType;
            }
        }
    }
}
