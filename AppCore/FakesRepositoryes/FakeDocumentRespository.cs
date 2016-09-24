using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;

namespace AppData.FakesRepositoryes
{
    public class FakeDocumentRespository : IDocumentRepository
    {
        private List<Document> _documents = new List<Document>
            {
                new Document
                {
                    DocumentID = 1,
                    DocumentTypeID = 1,
                    UserID = 1,
                    Serial = "111",
                    Number = "123",
                    IssuedBy = "Выдан 1",
                    WhenIssued = new DateTime(2016, 11, 11, 0, 0, 0),//"11.11.2016",
                    CodeDevision = "234"
                },
                new Document
                {
                    DocumentID = 2,
                    DocumentTypeID = 1,
                    UserID = 2,
                    Serial = "212",
                    Number = "126",
                    IssuedBy = "Выдан 2",
                    WhenIssued = new DateTime(2015, 11, 11, 0, 0, 0)//"11.11.2015"
                },
                new Document
                {
                    DocumentID = 3,
                    DocumentTypeID = 2,
                    UserID = 2,
                    Serial = "322",
                    Number = "125",
                    IssuedBy = "Выдан 3",
                    WhenIssued = new DateTime(2015, 11, 12, 0, 0, 0)//"11.12.2015"
                },
                new Document
                {
                    DocumentID = 4,
                    DocumentTypeID = 1,
                    UserID = 3,
                    Serial = "413",
                    Number = "127",
                    IssuedBy = "Выдан 2",
                    WhenIssued = new DateTime(2016, 11, 5, 0, 0, 0),//"5.11.2016",
                    CodeDevision = "2341"
                },
                new Document
                {
                    DocumentID = 5,
                    DocumentTypeID = 2,
                    UserID = 4,
                    Serial = "524",
                    Number = "223",
                    IssuedBy = "Выдан 3",
                    WhenIssued = new DateTime(2012, 11, 11, 0, 0, 0)//"11.11.2012"
                },
                new Document
                {
                    DocumentID = 6,
                    DocumentTypeID = 1,
                    UserID = 5,
                    Serial = "524",
                    Number = "12312",
                    IssuedBy = "Выдан 8",
                    WhenIssued = new DateTime(2011, 7, 9, 0, 0, 0)//"09.7.2011"
                }
            };
        public IQueryable<Document> Documents
        {
            get
            {
                return _documents.AsQueryable();
            }
        }
    }
}
