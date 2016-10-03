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
    public class PersonesRepository : IPersoneRepository
    {
        public int Count
        {
            get
            {
                return Persons.Count();
            }
        }

        public IQueryable<Persone> Persons
        {
            get
            {
                var dbContext = new LeoBaseContext();

                return dbContext.Persones;
            }
        }

        public int AddPersone(Persone persone)
        {
            using (var dbContext = new LeoBaseContext()) { 
                dbContext.Persones.Add(persone);
                dbContext.SaveChanges();
            }
            return persone.UserID;
        }

        public bool Remove(int id)
        {
            using(var db = new LeoBaseContext())
            {
                var persone = db.Persones
                                .Include("Address")
                                .Include("Phones")
                                .Include("Documents")
                                .FirstOrDefault(p => p.UserID == id);

                if (persone != null)
                {
                    var addresses = db.Addresses.Where(p => p.Persone.UserID == persone.UserID);
                    var documents = db.Documents.Where(p => p.Persone.UserID == persone.UserID);
                    var phones = db.Phones.Where(p => p.Persone.UserID == persone.UserID);

                    if (addresses != null)
                        db.Addresses.RemoveRange(addresses);

                    if (documents != null)
                        db.Documents.RemoveRange(documents);

                    if (phones != null)
                        db.Phones.RemoveRange(phones);

                    db.Persones.Remove(persone);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool Update(Persone persone)
        {
            using(var db = new LeoBaseContext())
            {
                var uPersone = db.Persones.FirstOrDefault(p => p.UserID == persone.UserID);
                //var uDocuments = db.Documents.Where(p => p.Persone.UserID == persone.UserID);
                //var uPhones = db.Documents.Where(p => p.Persone.UserID == persone.UserID);
                //var uAddresses = db.Addresses.Where(p => p.Persone.UserID == persone.UserID);

                if(persone.Address != null) {
                    List<int> adrIds = new List<int>();

                    foreach (var address in persone.Address)
                    {
                        var adrSearch = db.Addresses.FirstOrDefault(a => a.AddressID == address.AddressID);

                        if (adrSearch != null)
                        {
                            adrSearch.Country = address.Country;
                            adrSearch.Subject = address.Subject;
                            adrSearch.Area = address.Area;
                            adrSearch.City = address.City;
                            adrSearch.Street = address.Street;
                            adrSearch.HomeNumber = address.HomeNumber;
                            adrSearch.Flat = address.Flat;
                            adrSearch.Note = address.Note;
                        }
                        else
                        {
                            address.Persone = uPersone;
                            db.Addresses.Add(address);
                        }

                        adrIds.Add(address.AddressID);
                    }
                    
                    var addressesForRemove = db.Addresses.Where(a => a.Persone.UserID == persone.UserID && !adrIds.Contains(a.AddressID));

                    db.Addresses.RemoveRange(addressesForRemove);
                }

                if(persone.Documents != null)
                {
                    List<int> docIds = new List<int>();

                    foreach(var document in persone.Documents)
                    {
                        var docSearch = db.Documents.FirstOrDefault(d => d.DocumentID == document.DocumentID);

                        if(docSearch != null)
                        {
                            docSearch.Document_DocumentTypeID = document.Document_DocumentTypeID;
                            docSearch.Serial = document.Serial;
                            docSearch.Number = document.Number;
                            docSearch.IssuedBy = document.IssuedBy;
                            docSearch.WhenIssued = document.WhenIssued;
                            docSearch.CodeDevision = document.CodeDevision;
                        }else
                        {
                            document.Persone = uPersone;
                            db.Documents.Add(document);
                        }

                        docIds.Add(document.DocumentID);
                    }

                    var documentsForDelete = db.Documents.Where(d => d.Persone.UserID == persone.UserID && !docIds.Contains(d.DocumentID));

                    db.Documents.RemoveRange(documentsForDelete);
                }

                if(persone.Phones != null)
                {
                    List<int> phoneIds = new List<int>();

                    foreach(var phone in persone.Phones)
                    {
                        var phoneSearch = db.Phones.FirstOrDefault(p => p.PhoneID == phone.PhoneID);

                        if(phoneSearch != null)
                        {
                            phoneSearch.PhoneNumber = phone.PhoneNumber;
                        }else
                        {
                            phone.Persone = uPersone;
                            db.Phones.Add(phone);
                        }

                        phoneIds.Add(phone.PhoneID);
                    }

                    var phonesForDelete = db.Phones.Where(p => p.Persone.UserID == uPersone.UserID && !phoneIds.Contains(p.PhoneID));

                    db.Phones.RemoveRange(phonesForDelete);
                }

                uPersone.FirstName = persone.FirstName;
                uPersone.SecondName = persone.SecondName;
                uPersone.MiddleName = persone.MiddleName;
                uPersone.PlaceOfBirth = persone.PlaceOfBirth;
                uPersone.DateBirthday = persone.DateBirthday;
                uPersone.Image = persone.Image;
                uPersone.IsEmploeyr = persone.IsEmploeyr;
                uPersone.Position_PositionID = persone.Position_PositionID;
                uPersone.WasBeUpdated = DateTime.Now;
                
                db.SaveChanges();
            }
            return true;
        }
    }
}
