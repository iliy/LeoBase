using AppData.Abstract;
using AppData.Abstract.Protocols;
using AppData.Contexts;
using AppData.Entities;
using AppData.Infrastructure;
using AppPresentators.Factorys;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Services
{
    public interface IViolationService
    {
        ViolationSearchModel SearchModel { get; set; }
        ViolationOrderModel OrderModel { get; set; }
        PageModel PageModel { get; set; }
        List<ViolationViewModel> GetViolations(bool loadProtocols = false);
        bool SaveViolation(ViolationViewModel violation);
        bool AddProtocolForViolation(ViolationViewModel violation, IProtocol protocol);
    }

    public class ViolationSearchModel
    {
        public int ViolaotionID { get; set; }

        public DateTime DateFixed { get; set; } = new DateTime(1, 1, 1);
        public CompareValue CompareDateFixed { get; set; } = CompareValue.EQUAL;

        public DateTime DateSave { get; set; } = new DateTime(1, 1, 1);
        public CompareValue CompareDateSave { get; set; } = CompareValue.EQUAL;

        public DateTime DateUpdate { get; set; } = new DateTime(1, 1, 1);
        public CompareValue CompareDateUpdate { get; set; } = CompareValue.EQUAL;

        public int EmplyerID { get; set; }

        public int ViolatorID { get; set; }

        public string Description { get; set; }
        public CompareString CompareDescription { get; set; } = CompareString.EQUAL;

        public MSquare SquareCoordinat { get; set; }

    }

    public enum ViolationOrderProperties
    {
        WAS_BE_CREATED = 1,
        WAS_BE_UPDATED = 2
    }

    public class ViolationOrderModel
    {
        public ViolationOrderProperties OrderProperties { get; set; }
        public OrderType OrderType { get; set; }
    }

    public class ViolationService : IViolationService
    {
        public ViolationSearchModel SearchModel { get; set; }
        public ViolationOrderModel OrderModel { get; set; }
        public PageModel PageModel { get; set; }

        #region Добавление и обновление правонарушений
        public ViolationService()
        {
        }

        public bool AddViolation(ViolationViewModel violation)
        {
            if (violation == null)
                throw new ArgumentNullException("Нарушение не определено");

            Violation violationSave = new Violation
            {
                DateSave = DateTime.Now,
                DateUpdate = DateTime.Now,
                Date = violation.Date,
                Description = violation.Description,
                E = violation.E,
                N = violation.N
            };

            if (violation.Images != null)
            {
                violationSave.Images = violation.Images.Select(v => new ViolationImage
                {
                    Image = v
                }).ToList();
            }

            //if (violation.ViolationType != null)
            //{
            //    violationSave.ViolationType = new ViolationType
            //    {
            //        Name = violationSave.ViolationType.Name
            //    };
            //}

            var repo = RepositoryesFactory.GetInstance().Get<IViolationRepository>();

            return repo.AddViolation(violationSave);
        }

        public bool UpdateViolation(ViolationViewModel violation)
        {
            if (violation == null)
                throw new ArgumentNullException("Нарушение не определено");

            Violation violationForUpdate = new Violation
            {
                Date = violation.Date,
                DateUpdate = DateTime.Now,
                Description = violation.Description,
                E = violation.E,
                N = violation.N,
                ViolationID = violation.ViolationID
            };

            if (violation.Images != null)
            {
                violationForUpdate.Images = violation.Images.Select(v => new ViolationImage
                {
                    Image = v
                }).ToList();
            }

            if (violation.ViolationType != null)
            {
                violationForUpdate.ViolationType = new ViolationType {
                    Name = violation.ViolationType
                };
            }

            var repo = RepositoryesFactory.GetInstance().Get<IViolationRepository>();

            return repo.UpdateViolation(violationForUpdate);
        }

        public bool SaveViolation(ViolationViewModel violation)
        {
            return violation.ViolationID == 0 ? AddViolation(violation) : UpdateViolation(violation);
        }

        public bool AddProtocolForViolation(ViolationViewModel violation, IProtocol protocol)
        {
            if (violation == null || protocol == null)
                throw new ArgumentNullException("Не все аргументы заданы");

            protocol.Protocol.ViolationID = violation.ViolationID;
            
            return ProtocolFactory.SaveProtocol(protocol);
        }
        
        #endregion

        public List<ViolationViewModel> GetViolations(bool loadProtocols = false)
        {
            var repo = RepositoryesFactory.GetInstance().Get<IViolationRepository>();

            var answer = repo.Violations.SearchByModel(SearchModel, OrderModel, PageModel);

            List<ViolationViewModel> result = new List<ViolationViewModel>();

            if (answer != null && answer.Count() != 0)

                if (loadProtocols)
                {
                    result = answer.Select(a => new ViolationViewModel
                    {
                        ViolationID = a.ViolationID,
                        Date = a.Date,
                        Description = a.Description,
                        E = a.E,
                        N = a.N,
                        ViolationType = "Административное"//a.ViolationType
                    }).ToList();
                    foreach (var violation in result)
                    {
                        var protocols = ProtocolFactory.GetViolationProtocols(violation.ViolationID);
                        if(protocols != null ) {
                            violation.Protocols = protocols.Select(p =>
                                                    new VModels.Protocols.ProtocolViewModel
                                                    {
                                                        DateCreatedProtocol = p.DateCreatedProtocol,
                                                        PlaceCreatedProtocol = p.PlaceCreatedProtocol,
                                                        PlaceCreatedProtocolE = p.PlaceCreatedProtocolE,
                                                        PlaceCreatedProtocolN = p.PlaceCreatedProtocolN,
                                                        ProtocolID = p.ProtocolID,
                                                        ProtocolTypeID = p.ProtocolTypeID,
                                                        WitnessFIO_1 = p.WitnessFIO_1,
                                                        WitnessFIO_2 = p.WitnessFIO_2,
                                                        WitnessLive_1 = p.WitnessLive_1,
                                                        WitnessLive_2 = p.WitnessLive_2,
                                                        ProtocolTypeName = ProtocolFactory.ProtocolTypes[p.ProtocolTypeID]
                                                    }).ToList();
                        }else
                        {
                            violation.Protocols = new List<VModels.Protocols.ProtocolViewModel>();
                        }
                    }
                }
                else {
                    result = answer.Select(a => new ViolationViewModel
                    {
                        ViolationID = a.ViolationID,
                        Date = a.Date,
                        Description = a.Description,
                        E = a.E,
                        N = a.N,
                        ViolationType = "Административное"//a.ViolationType
                    }).ToList();
                }

            return result;
        }




    }

    public static class ViolationTools
    {
        public static IQueryable<Violation> SearchByModel(this IQueryable<Violation> vt, ViolationSearchModel search, ViolationOrderModel order, PageModel page)
        {
            if (search == null) throw new ArgumentNullException("Модель поиска должна быть задана");

            if (search.ViolaotionID != 0)
                return vt.Where(v => v.ViolationID == search.ViolaotionID);

            vt = vt.SearchByDate(search.DateFixed, search.CompareDateFixed);

            vt = vt.SearchByDate(search.DateSave, search.CompareDateSave);

            vt = vt.SearchByDescription(search.Description, search.CompareDescription);

            vt = vt.SearchByCoordinats(search.SquareCoordinat);

            vt = vt.SearchByEmployer(search.EmplyerID);

            

            return vt;
        }

        public static IQueryable<Violation> OrderByModel(this IQueryable<Violation> vt, ViolationOrderModel order)
        {

            return vt;
        }

        public static IQueryable<Violation> SearchByEmployer(this IQueryable<Violation> vt, int employerId)
        {
            if (employerId <= 0) return vt;
            
            var pr = RepositoryesFactory.GetInstance().Get<IProtocolRepository>().Protocols;

            var violationIds = pr.Where(p => p.EmployerID == employerId).Select(p => p.ViolationID).ToList();//.Distinct();

            vt = vt.Where(v => violationIds.Contains(v.ViolationID));

            return vt;
        }

        public static IQueryable<Violation> SearchByCoordinats(this IQueryable<Violation> vt, MSquare square)
        {
            if (square == null || square.Point1 == null || square.Point2 == null) return vt;

            if (square.Point1.X > square.Point2.X)
            {
                double b = square.Point1.X;
                square.Point1.X = square.Point2.X;
                square.Point2.X = b;
            }

            if (square.Point1.Y > square.Point2.Y)
            {
                double b = square.Point1.Y;
                square.Point1.Y = square.Point2.Y;
                square.Point2.Y = b;
            }

            return vt.Where(v => v.E >= square.Point1.X &&
                                 v.N >= square.Point1.Y &&
                                 v.E <= square.Point2.X &&
                                 v.N <= square.Point2.Y);
        }

        /// <summary>
        /// Поиск по описанию
        /// </summary>
        /// <param name="vt"></param>
        /// <param name="description"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static IQueryable<Violation> SearchByDescription(this IQueryable<Violation> vt, string description, CompareString compare)
        {
            if (description == null || string.IsNullOrEmpty(description.Trim())) return vt;

            if (compare == CompareString.CONTAINS)
                return vt.Where(v => v.Description.Contains(description));

            return vt.Where(v => v.Description.Equals(description));
        }


        /// <summary>
        /// Поиск по дате
        /// </summary>
        /// <param name="vt"></param>
        /// <param name="date"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static IQueryable<Violation> SearchByDate(this IQueryable<Violation> vt, DateTime date, CompareValue compare)
        {
            if (date == null || date.Year == 1) return vt;

            switch (compare)
            {
                case CompareValue.EQUAL:
                    return vt.Where(v => v.Date == date);
                case CompareValue.LESS:
                    return vt.Where(v => v.Date <= date);
                case CompareValue.MORE:
                    return vt.Where(v => v.Date >= date);
            }
            return vt;
        }
    }
}
