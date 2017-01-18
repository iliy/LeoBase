using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys.Violations
{
    public class ViolationRepository : IViolationRepository
    {
        public IQueryable<Violation> Violations
        {
            get
            {
                var context = new LeoBaseContext();
                return context.Violations;
            }
        }

        public bool AddViolation(Violation violation)
        {
            using(var db = new LeoBaseContext())
            {
                db.Violations.Add(violation);

                db.SaveChanges();

                return true;
            }
        }

        public bool UpdateViolation(Violation violation)
        {
            using(var db = new LeoBaseContext())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        var violationForSave = db.Violations
                                                    .Include("Images")
                                                    .FirstOrDefault(v => v.ViolationID == violation.ViolationID);

                        if (violationForSave == null) return false;

                        if (violation.Images != null)
                        {
                            // Если изображения не пустые, то удаляем старые из базы, и сохраняем новые
                            var imagesForRemove = violationForSave.Images;

                            db.ViolationImages.RemoveRange(imagesForRemove);

                            violationForSave.Images = violation.Images;
                        }

                        

                        violationForSave.Date = violation.Date;

                        violationForSave.DateUpdate = DateTime.Now;

                        violationForSave.Description = violation.Description;

                        violationForSave.E = violation.E;

                        violationForSave.N = violation.N;

                        violationForSave.ViolationType = violation.ViolationType;

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    }catch(Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }
    }
}
