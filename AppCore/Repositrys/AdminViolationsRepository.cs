using AppData.Contexts;
using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositrys
{
    public interface IAdminViolationRepository
    {
        AdminViolation SaveViolation(AdminViolation violation);
        AdminViolation GetViolation(int violationID);
        bool RemoveViolation(AdminViolation violation);
        bool RemoveViolation(int violationID);
        IQueryable<AdminViolation> Violations(LeoBaseContext context);
        List<AdminViolation> Violations();
    }
    public class AdminViolationsRepository : IAdminViolationRepository
    {
        public AdminViolation GetViolation(int violationID)
        {
            using(var db = new LeoBaseContext())
            {
                var violations = db.AdminViolations.Include("Employer")
                                         .Include("ViolatorOrganisation")
                                         .Include("ViolatorPersone")
                                         .Include("ViolatorDocument")
                                         .Include("Images")
                                         .Where(v => v.ViolationID == violationID).ToList();

                return violations.Count != 0 ? violations[0] : null;
            }
        }
        public bool RemoveViolation(int violationID)
        {
            using(var db = new LeoBaseContext())
            {
                var deletedViolation = db.AdminViolations.FirstOrDefault(v => v.ViolationID == violationID);

                if (deletedViolation == null) return false;

                var violationImages = db.ViolationImages.Where(i => i.AdminViolation.ViolationID == violationID);

                if(violationImages != null)
                    db.ViolationImages.RemoveRange(violationImages);

                db.AdminViolations.Remove(deletedViolation);

                db.SaveChanges();

                return true;
            }
        }

        public bool RemoveViolation(AdminViolation violation)
        {
            return RemoveViolation(violation.ViolationID);
        }



        public AdminViolation SaveViolation(AdminViolation violation)
        {
            if (violation == null) throw new ArgumentNullException();

            using (var db = new LeoBaseContext()) { 
                using(var transaction = db.Database.BeginTransaction()) {

                    if (violation.Employer != null && violation.Employer.UserID > 0)
                    {
                        violation.Employer = db.Persones.FirstOrDefault(p => p.UserID == violation.Employer.UserID);
                    }

                    if (violation.ViolatorPersone != null && violation.ViolatorPersone.UserID > 0)
                    {
                        violation.ViolatorPersone = db.Persones.FirstOrDefault(p => p.UserID == violation.ViolatorPersone.UserID);
                    }

                    if (violation.ViolatorDocument != null && violation.ViolatorDocument.DocumentID > 0)
                    {
                        violation.ViolatorDocument = db.Documents.FirstOrDefault(d => d.DocumentID == violation.ViolatorDocument.DocumentID);
                    }

                    if (violation.ViolatorOrganisation != null && violation.ViolatorOrganisation.OrganisationID > 0)
                    {
                        violation.ViolatorOrganisation = db.Organisations.FirstOrDefault(o => o.OrganisationID == violation.ViolatorOrganisation.OrganisationID);
                    }

                    if (violation.ViolationID <= 0)
                    {
                        violation.DateSave = DateTime.Now;
                        violation.DateUpdate = DateTime.Now;
                        db.AdminViolations.Add(violation);
                    }
                    else
                    {
                        var findedViolation = db.AdminViolations.FirstOrDefault(v => v.ViolationID == violation.ViolationID);

                        if (findedViolation == null) return null;

                        findedViolation.Employer = violation.Employer;
                        findedViolation.ViolatorDocument = violation.ViolatorDocument;
                        findedViolation.ViolatorOrganisation = violation.ViolatorOrganisation;
                        findedViolation.ViolatorPersone = violation.ViolatorPersone;

                        findedViolation.Consideration = violation.Consideration;
                        findedViolation.CreatedProtocolE = violation.CreatedProtocolE;
                        findedViolation.CreatedProtocolN = violation.CreatedProtocolN;
                        findedViolation.DateAgenda2025 = violation.DateAgenda2025;
                        findedViolation.DateCreatedProtocol = violation.DateCreatedProtocol;
                        findedViolation.DateNotice = violation.DateNotice;
                        findedViolation.DatePaymant = violation.DatePaymant;
                        findedViolation.DateReceiving = violation.DateReceiving;
                        findedViolation.DateSave = violation.DateSave;
                        findedViolation.DateSent = violation.DateSent;
                        findedViolation.DateSentBailiff = violation.DateSentBailiff;
                        findedViolation.DateUpdate = DateTime.Now;
                        findedViolation.DateViolation = violation.DateViolation;
                        findedViolation.FindedGunsHuntingAndFishing = violation.FindedGunsHuntingAndFishing;
                        findedViolation.FindedNatureManagementProducts = violation.FindedNatureManagementProducts;
                        findedViolation.FindedWeapons = violation.FindedWeapons;
                        findedViolation.GotPersonaly = violation.GotPersonaly;
                        findedViolation.KOAP = violation.KOAP;
                        findedViolation.NumberNotice = violation.NumberNotice;
                        findedViolation.NumberSent = violation.NumberSent;
                        findedViolation.NumberSentBailigg = violation.NumberSentBailigg;
                        findedViolation.PlaceCreatedProtocol = violation.PlaceCreatedProtocol;
                        findedViolation.PlaceViolation = violation.PlaceViolation;
                        findedViolation.RulingNumber = violation.RulingNumber;
                        findedViolation.SumRecovery = violation.SumRecovery;
                        findedViolation.SumViolation = violation.SumViolation;
                        findedViolation.Violation = violation.Violation;
                        findedViolation.ViolationDescription = violation.ViolationDescription;
                        findedViolation.ViolationE = violation.ViolationE;
                        findedViolation.ViolationN = violation.ViolationN;
                        findedViolation.WasAgenda2025 = violation.WasAgenda2025;
                        findedViolation.WasNotice = violation.WasNotice;
                        findedViolation.WasPaymant = violation.WasPaymant;
                        findedViolation.WasReceiving = violation.WasReceiving;
                        findedViolation.WasSent = violation.WasSent;
                        findedViolation.WasSentBailiff = violation.WasSentBailiff;
                        findedViolation.WitnessFIO_1 = violation.WitnessFIO_1;
                        findedViolation.WitnessFIO_2 = violation.WitnessFIO_2;
                        findedViolation.WitnessLive_1 = violation.WitnessLive_1;
                        findedViolation.WitnessLive_2 = violation.WitnessLive_2;


                        if (violation.Images != null) {
                            
                            db.ViolationImages.RemoveRange(db.ViolationImages.Where(i => i.AdminViolation.ViolationID == violation.ViolationID));

                            foreach (var image in violation.Images)
                            {
                                image.AdminViolation = findedViolation;
                                db.ViolationImages.Add(image);
                            }
                        }
                    }
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                    }catch(Exception e)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            }
            return violation;
        }

        public List<AdminViolation> Violations()
        {
            using (var db = new LeoBaseContext()) return db.AdminViolations.ToList();
        }

        public IQueryable<AdminViolation> Violations(LeoBaseContext context = null)
        {
            if (context != null) return context.AdminViolations;

            using (var db = new LeoBaseContext()) return db.AdminViolations;
        }
    }
}
