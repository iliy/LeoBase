using AppData.Abstract;
using AppData.Abstract.Protocols;
using AppData.Contexts;
using AppData.Entities;
using AppData.Infrastructure;
using AppPresentators.VModels.Persons;
using AppPresentators.VModels.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Factorys
{
    public enum ProtocolsType
    {
        /// <summary>
        /// Протокол о доставление
        /// </summary>
        PROTOCOL_ABOUT_BRINGING = 1,
        /// <summary>
        /// Протокол о личном досмотре
        /// </summary>
        PROTOCOL_ABOUT_INSPECTION = 2,
        /// <summary>
        /// Протокол о досмотре транспортного средства
        /// </summary>
        PROTOCOL_ABOUT_INSPECTION_AUTO = 3,
        /// <summary>
        /// Протокол об изъятии
        /// </summary>
        PROTOCOL_ABOUT_WITHDRAW = 4,
        /// <summary>
        /// Протокол об осмотре территорий и зданий принадлежащих организации
        /// </summary>
        PROTOCOL_ABOUT_INSPECTION_ORGANISATION = 5,
        /// <summary>
        /// Протокол об аресте
        /// </summary>
        PROTOCOL_ABOUT_ARREST = 6,
        /// <summary>
        /// Определение
        /// </summary>
        DEFINITION = 7,
        /// <summary>
        /// Протокол об административном правонарушение для физического лица
        /// </summary>
        PROTOCOL_ABOUT_VIOLATION_PERSONE = 8,
        /// <summary>
        /// Протокол об административном правонарушение для юридического лица
        /// </summary>
        PROTOCOL_ABOUT_VIOLATION_ORGANISATION = 9,
        /// <summary>
        /// Постановление
        /// </summary>
        RULING_FOR_VIOLATION = 10,
        /// <summary>
        /// Предписание
        /// </summary>
        INJUCTION = 11
    }

    public class ProtocolFactory
    {

        #region Сохранение протоколов
        public static bool SaveProtocolAboutBringing(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutBringing))
                throw new ArgumentException("Протокол не является протоколом о доставление.");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction()) {
                    try
                    {
                        var protocolAboutBringing = (ProtocolAboutBringing)protocol;

                        var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == protocolAboutBringing.ViolatorDocumentID);

                        if (violatorDocument == null) return false;

                        var violatorEntity = db.Persones
                                                .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                        if (violatorEntity == null) return false;

                        Violator violator = new Violator
                        {
                            PersoneID = violatorEntity.UserID,
                            ViolationID = protocol.Protocol.ViolationID,
                            Protocol = protocol.Protocol
                        };

                        db.ProtocolsAboutBringing.Add(protocolAboutBringing);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }

        public static bool SaveProtocolAboutInspection(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutInspection))
                throw new ArgumentException("Протокол не является протоколом о досмотре.");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var protocolAboutInspection = (ProtocolAboutInspection)protocol;

                        var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == protocolAboutInspection.ViolatorDocumentID);

                        if (violatorDocument == null) return false;

                        var violatorEntity = db.Persones
                                                .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                        if (violatorEntity == null) return false;

                        Violator violator = new Violator
                        {
                            PersoneID = violatorEntity.UserID,
                            ViolationID = protocol.Protocol.ViolationID,
                            Protocol = protocol.Protocol
                        };

                        db.ProtocolsAboutInspection.Add(protocolAboutInspection);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }

        public static bool SaveProtocolAboutInspectAuto(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutInspectionAuto))
                throw new ArgumentException("Протокол не является протоколом досмотра машины");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var protocolAboutInspectionAuto = (ProtocolAboutInspectionAuto)protocol;

                        var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == protocolAboutInspectionAuto.ViolatorDocumentID);

                        if (violatorDocument == null) return false;

                        var violatorEntity = db.Persones
                                                .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                        if (violatorEntity == null) return false;

                        Violator violator = new Violator
                        {
                            PersoneID = violatorEntity.UserID,
                            ViolationID = protocol.Protocol.ViolationID,
                            Protocol = protocol.Protocol
                        };

                        db.ProtocolsAboutInspectionAuto.Add(protocolAboutInspectionAuto);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }

        public static bool SaveProtocolAboutWithdraw(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutWithdraw))
                throw new ArgumentException("Протокол не является протоколом о изъятии.");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var protocolAboutWithdraw = (ProtocolAboutWithdraw)protocol;

                        var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == protocolAboutWithdraw.ViolatorDocumentID);

                        if (violatorDocument == null) return false;

                        var violatorEntity = db.Persones
                                                .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                        if (violatorEntity == null) return false;

                        Violator violator = new Violator
                        {
                            PersoneID = violatorEntity.UserID,
                            ViolationID = protocol.Protocol.ViolationID,
                            Protocol = protocol.Protocol
                        };

                        db.ProtocolsAboutWithdraw.Add(protocolAboutWithdraw);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }

        public static bool SaveProtocolAboutInspectionOrganisation(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutInspectionOrganisation))
                throw new ArgumentException("Протокол не является протоколом осмотра организации");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        ProtocolAboutInspectionOrganisation protocolAboutInspectionOrganisation = (ProtocolAboutInspectionOrganisation)protocol;

                        Organisation organisation = db.Organisations.FirstOrDefault(o => o.OrganisationID == protocolAboutInspectionOrganisation.OrganisationID);

                        if (organisation == null) return false;

                        Violator violator = new Violator
                        {
                            ViolatorType = "Организация",
                            ViolationID = protocol.Protocol.ViolationID,
                            PersoneID = organisation.OrganisationID,
                            Protocol = protocol.Protocol
                        };

                        db.Violators.Add(violator);

                        db.ProtocolsAboutInspectionOrganisation.Add(protocolAboutInspectionOrganisation);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }

        public static bool SaveProtocolAboutArrest(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutArrest))
                throw new ArgumentException("Протокол не является протоколом об аресте");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var protocolAboutArrest = (ProtocolAboutArrest)protocol;

                        var violatorDocument = db.Documents
                                                 .Include("Persone")
                                                 .FirstOrDefault(d => d.DocumentID == protocolAboutArrest.ViolatorDocumentID);

                        if (violatorDocument == null) return false;

                        var violatorEntity = db.Persones
                                                .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                        if (violatorEntity == null) return false;

                        Violator violator = new Violator
                        {
                            PersoneID = violatorEntity.UserID,
                            ViolationID = protocol.Protocol.ViolationID,
                            Protocol = protocol.Protocol
                        };

                        db.ProtocolsAboutArrest.Add(protocolAboutArrest);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }

        public static bool SaveDefinition(IProtocol protocol)
        {
            if (!(protocol is DefinitionAboutViolation))
                throw new ArgumentException("Документ не является определением о возбуждение административного расследования.");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var definition = (DefinitionAboutViolation)protocol;

                        Violator violator;

                        if (definition.OrganisationID != 0)
                        {
                            var organisation = db.Organisations.FirstOrDefault(o => o.OrganisationID == definition.OrganisationID);

                            if (organisation == null) return false;

                            violator = new Violator
                            {
                                ViolationID = protocol.Protocol.ViolationID,
                                PersoneID = organisation.OrganisationID,
                                ViolatorType = "Организация",
                                Protocol = protocol.Protocol
                            };
                        } else if (definition.ViolatorDocumentID != 0)
                        {
                            var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == definition.ViolatorDocumentID);

                            if (violatorDocument == null) return false;

                            var violatorEntity = db.Persones
                                                    .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                            if (violatorEntity == null) return false;

                            violator = new Violator
                            {
                                PersoneID = violatorEntity.UserID,
                                ViolationID = protocol.Protocol.ViolationID,
                                Protocol = protocol.Protocol
                            };
                        }
                        else
                        {
                            return false;
                        }

                        db.Violators.Add(violator);

                        db.DefinitionsAboutViolation.Add(definition);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }

        public static bool SaveProtocolAboutViolationPersone(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutViolationPersone))
                throw new ArgumentException("Протокол не является протоколом об административном правонарушение гражданина");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var protocolAboutViolation = (ProtocolAboutViolationPersone)protocol;

                        var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == protocolAboutViolation.ViolatorDocumentID);

                        if (violatorDocument == null) return false;

                        var violatorEntity = db.Persones
                                                .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                        if (violatorEntity == null) return false;

                        Violator violator = new Violator
                        {
                            PersoneID = violatorEntity.UserID,
                            ViolationID = protocol.Protocol.ViolationID,
                            Protocol = protocol.Protocol
                        };

                        db.ProtocolsAboutViolationPersone.Add(protocolAboutViolation);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }

        public static bool SaveProtocolAboutViolationOrganisation(IProtocol protocol)
        {
            if (!(protocol is ProtocolAboutViolationOrganisation))
                throw new ArgumentException("Протокол не является протоколом об административном правонарушение организации");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var protocolAboutViolationOrganisation = (ProtocolAboutViolationOrganisation)protocol;

                        var organisation = db.Organisations.FirstOrDefault(o => o.OrganisationID == protocolAboutViolationOrganisation.OrganisationID);

                        if (organisation == null) return false;

                        Violator violator = new Violator
                        {
                            ViolationID = protocol.Protocol.ViolationID,
                            PersoneID = organisation.OrganisationID,
                            ViolatorType = "Организация",
                            Protocol = protocol.Protocol
                        };

                        db.Violators.Add(violator);

                        db.ProtocolsAboutViolationOrganisation.Add(protocolAboutViolationOrganisation);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }

            }
            return false;
        }

        public static bool SaveRulingForViolation(IProtocol protocol)
        {
            if (!(protocol is RulingForViolation))
                throw new AccessViolationException("Документ не является постановлением");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var ruling = (RulingForViolation)protocol;

                        Violator violator;

                        if (ruling.OrganisationID != 0)
                        {
                            var organisation = db.Organisations.FirstOrDefault(o => o.OrganisationID == ruling.OrganisationID);

                            if (organisation == null) return false;

                            violator = new Violator
                            {
                                ViolationID = protocol.Protocol.ViolationID,
                                PersoneID = organisation.OrganisationID,
                                ViolatorType = "Организация",
                                Protocol = protocol.Protocol
                            };
                        }
                        else if (ruling.ViolatorDocumentID != 0)
                        {
                            var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == ruling.ViolatorDocumentID);

                            if (violatorDocument == null) return false;

                            var violatorEntity = db.Persones
                                                    .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                            if (violatorEntity == null) return false;

                            violator = new Violator
                            {
                                PersoneID = violatorEntity.UserID,
                                ViolationID = protocol.Protocol.ViolationID
                            };
                        }
                        else
                        {
                            return false;
                        }

                        db.RulingsForViolation.Add(ruling);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }

        public static bool SaveInjunction(IProtocol protocol)
        {
            if (!(protocol is Injunction))
                throw new ArgumentException("Документ не является предписанием");

            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var injuction = (Injunction)protocol;

                        var violatorDocument = db.Documents
                                                 .FirstOrDefault(d => d.DocumentID == injuction.ViolatorDocumentID);

                        if (violatorDocument == null) return false;

                        var violatorEntity = db.Persones
                                                .FirstOrDefault(p => p.UserID == violatorDocument.Persone.UserID);

                        if (violatorEntity == null) return false;

                        Violator violator = new Violator
                        {
                            PersoneID = violatorEntity.UserID,
                            ViolationID = protocol.Protocol.ViolationID,
                            Protocol = protocol.Protocol
                        };

                        db.Injunctions.Add(injuction);

                        db.Violators.Add(violator);

                        db.SaveChanges();

                        transaction.Commit();

                        return true;
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Сохранение протокола
        /// </summary>
        /// <param name="protocol">Протокол</param>
        /// <returns></returns>
        public static bool SaveProtocol(IProtocol protocol)
        {
            switch (protocol.Protocol.ProtocolTypeID)
            {
                case (int)ProtocolsType.PROTOCOL_ABOUT_BRINGING:
                    return SaveProtocolAboutBringing(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION:
                    return SaveProtocolAboutInspection(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_AUTO:
                    return SaveProtocolAboutInspectAuto(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_WITHDRAW:
                    return SaveProtocolAboutWithdraw(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_ORGANISATION:
                    return SaveProtocolAboutInspectionOrganisation(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_ARREST:
                    return SaveProtocolAboutArrest(protocol);
                case (int)ProtocolsType.DEFINITION:
                    return SaveDefinition(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_PERSONE:
                    return SaveProtocolAboutViolationPersone(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_ORGANISATION:
                    return SaveProtocolAboutViolationOrganisation(protocol);
                case (int)ProtocolsType.RULING_FOR_VIOLATION:
                    return SaveRulingForViolation(protocol);
                case (int)ProtocolsType.INJUCTION:
                    return SaveInjunction(protocol);
            }

            return false;
        }
        #endregion

        #region Получение протоколов и их детализации

        /// <summary>
        /// Получение протокола по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Protocol GetProtocol(int id)
        {
            using (var db = new LeoBaseContext())
            {
                var protocol = db.Protocols.FirstOrDefault(p => p.ProtocolID == id);

                return protocol;
            }
        }

        /// <summary>
        /// Получение всех протоколов по правонарушению
        /// </summary>
        /// <param name="id">Идентификатор правонарушения</param>
        /// <returns></returns>
        public static List<Protocol> GetViolationProtocols(int id)
        {
            List<Protocol> result = new List<Protocol>();

            using (var db = new LeoBaseContext())
            {
                var protocols = db.Protocols.Where(p => p.ViolationID == id);

                foreach (var protocol in protocols)
                {
                    result.Add(protocol);
                }
            }

            return result;
        }

        public static DefinitionAboutViolation GetDefinition(Protocol protocol)
        {
            using (var db = new LeoBaseContext())
            {
                var definition = db.DefinitionsAboutViolation.Include("Protocol")
                                                             .FirstOrDefault(d => d.Protocol.ProtocolID == protocol.ProtocolID);

                return definition;
            }
        }

        public static Injunction GetInjuctuion(Protocol protocol)
        {
            using (var db = new LeoBaseContext())
            {
                var injunctuion = db.Injunctions.Include("Protocol")
                                                .Include("InjuctionsItem")
                                                .FirstOrDefault(i => i.Protocol.ProtocolID == protocol.ProtocolID);

                return injunctuion;
            }
        }

        public static ProtocolAboutArrest GetProtocolAboutArrest(Protocol protocol)
        {
            using (var db = new LeoBaseContext())
            {
                var protocolAboutArrest = db.ProtocolsAboutArrest.Include("Protocol").FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);
                return protocolAboutArrest;
            }
        }

        public static ProtocolAboutBringing GetProtocolAboutBringing(Protocol protocol)
        {
            using (var db = new LeoBaseContext())
            {
                var protocolAboutBringing = db.ProtocolsAboutBringing.Include("Protocol").FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);
                return protocolAboutBringing;
            }
        }

        public static ProtocolAboutInspection GetProtocolAboutInspection(Protocol protocol)
        {
            using (var db = new LeoBaseContext())
            {
                var protocolAboutInspection = db.ProtocolsAboutInspection
                                                    .Include("Protocol")
                                                    .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);

                return protocolAboutInspection;
            }
        }

        public static ProtocolAboutInspectionAuto GetProtocolAboutInspectionAuto(Protocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                var protocolAboutInspectionAuto = db.ProtocolsAboutInspectionAuto
                                                    .Include("Protocol")
                                                    .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);

                return protocolAboutInspectionAuto;
            }
        }

        public static ProtocolAboutInspectionOrganisation GetProtocolAboutInspectionOrganisation(Protocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                var protocolAboutInspectionOrganisation = db.ProtocolsAboutInspectionOrganisation
                                                            .Include("Protocol")
                                                            .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);

                return protocolAboutInspectionOrganisation;
            }
        }

        public static ProtocolAboutViolationOrganisation GetProtocolAboutViolationOrganisation(Protocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                var protocolAboutViolationOrganisation = db.ProtocolsAboutViolationOrganisation
                                                           .Include("Protocol")
                                                           .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);

                return protocolAboutViolationOrganisation;
            }
        }

        public static ProtocolAboutViolationPersone GetProtocolAboutViolationPersone(Protocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                var protocolAboutViolationPersone = db.ProtocolsAboutViolationPersone
                                                      .Include("Protocol")
                                                      .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);

                return protocolAboutViolationPersone;
            }
        }

        public static ProtocolAboutWithdraw GetProtocolAboutWithdraw(Protocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                var protocolAboutWithdraw = db.ProtocolsAboutWithdraw
                                              .Include("Protocol")
                                              .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);

                return protocolAboutWithdraw;
            }
        }

        public static RulingForViolation GetRulingForViolation(Protocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                var rulingForViolation = db.RulingsForViolation
                                            .Include("Protocol")
                                            .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.ProtocolID);

                return rulingForViolation;
            }
        }

        public static IProtocol GetProtocolDetails(Protocol protocol)
        {
            switch (protocol.ProtocolTypeID)
            {
                case (int)ProtocolsType.DEFINITION:
                    return GetDefinition(protocol);
                case (int)ProtocolsType.INJUCTION:
                    return GetInjuctuion(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_ARREST:
                    return GetProtocolAboutArrest(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_BRINGING:
                    return GetProtocolAboutBringing(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION:
                    return GetProtocolAboutInspection(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_AUTO:
                    return GetProtocolAboutInspectionAuto(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_ORGANISATION:
                    return GetProtocolAboutInspectionOrganisation(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_ORGANISATION:
                    return GetProtocolAboutViolationOrganisation(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_PERSONE:
                    return GetProtocolAboutViolationPersone(protocol);
                case (int)ProtocolsType.PROTOCOL_ABOUT_WITHDRAW:
                    return GetProtocolAboutWithdraw(protocol);
                case (int)ProtocolsType.RULING_FOR_VIOLATION:
                    return GetRulingForViolation(protocol);
            }

            return null; 
        }

        public static IProtocol GetProtocolDetails(int id)
        {
            Protocol protocol = GetProtocol(id);
            
            if (protocol == null) return null;

            return GetProtocolDetails(protocol);
        }
        #endregion

        #region Удаление протоколов и нарушений
        private static bool RemoveProtocol(int id, LeoBaseContext db)
        {
            var violators = db.Violators.Where(v => v.Protocol.ProtocolID == id);

            var protocol = db.Protocols.FirstOrDefault(p => p.ProtocolID == id);

            if (protocol == null) return false;

            switch (protocol.ProtocolTypeID)
            {
                case (int)ProtocolsType.INJUCTION:
                    var injenction = db.Injunctions.Include("InjuctionsItem")
                                               .FirstOrDefault(i => i.Protocol.ProtocolID == id);

                    if (injenction != null) { 
                        db.InjunctionItems.RemoveRange(injenction.InjuctionsItem);

                        db.Injunctions.Remove(injenction);
                    }
                    break;
                case (int)ProtocolsType.DEFINITION:
                    var definition = db.DefinitionsAboutViolation
                                        .FirstOrDefault(d => d.Protocol.ProtocolID == id);

                    if (definition != null) 
                        db.DefinitionsAboutViolation.Remove(definition);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_ARREST:
                    var protocolAboutArrest = db.ProtocolsAboutArrest
                                                .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutArrest != null)
                        db.ProtocolsAboutArrest.Remove(protocolAboutArrest);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_BRINGING:
                    var protocolAboutBringing = db.ProtocolsAboutBringing
                                                  .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutBringing != null) 
                        db.ProtocolsAboutBringing.Remove(protocolAboutBringing);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION:
                    var protocolAboutInspection = db.ProtocolsAboutInspection
                                                    .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutInspection != null)
                        db.ProtocolsAboutInspection.Remove(protocolAboutInspection);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_AUTO:
                    var protocolAboutInspectionAuto = db.ProtocolsAboutInspectionAuto
                                                        .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutInspectionAuto != null)
                        db.ProtocolsAboutInspectionAuto.Remove(protocolAboutInspectionAuto);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_ORGANISATION:
                    var protocolAboutInspectionOrganisation = db.ProtocolsAboutInspectionOrganisation
                                                                .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutInspectionOrganisation != null) 
                        db.ProtocolsAboutInspectionOrganisation.Remove(protocolAboutInspectionOrganisation);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_ORGANISATION:
                    var protocolAboutViolationOrganisation = db.ProtocolsAboutViolationOrganisation
                                                                .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutViolationOrganisation != null)
                        db.ProtocolsAboutViolationOrganisation.Remove(protocolAboutViolationOrganisation);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_PERSONE:
                    var protocolAboutViolationPersone = db.ProtocolsAboutViolationPersone
                                                          .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutViolationPersone != null)
                        db.ProtocolsAboutViolationPersone.Remove(protocolAboutViolationPersone);
                    break;
                case (int)ProtocolsType.PROTOCOL_ABOUT_WITHDRAW:
                    var protocolAboutWithdraw = db.ProtocolsAboutWithdraw
                                                  .FirstOrDefault(p => p.Protocol.ProtocolID == id);

                    if (protocolAboutWithdraw != null)
                        db.ProtocolsAboutWithdraw.Remove(protocolAboutWithdraw);
                    break;
                case (int)ProtocolsType.RULING_FOR_VIOLATION:
                    var ruling = db.RulingsForViolation
                                    .FirstOrDefault(r => r.Protocol.ProtocolID == id);

                    if (ruling != null)
                        db.RulingsForViolation.Remove(ruling);
                    break;
                default:
                    return false;
            }

            db.Violators.RemoveRange(violators);

            db.Protocols.Remove(protocol);

            return true;
        }

        public static bool RemoveProtocol(int id)
        {
            using (var db = new LeoBaseContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (!RemoveProtocol(id, db)) return false;

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
        
        public static bool RemoveViolation(int violationID, LeoBaseContext db)
        {
            var violation = db.Violations
                                          .FirstOrDefault(v => v.ViolationID == violationID);

            if (violation == null) return false;

            bool result = RemoveViolationProtocols(violationID, db);

            if (!result) return false;

            db.Violations.Remove(violation);

            return true;
        }

        

        public static bool RemoveViolation(int violationID)
        {
            using(var db = new LeoBaseContext())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (!RemoveViolation(violationID, db)) return false;

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

        public static bool RemoveViolationProtocols(int violationID, LeoBaseContext db)
        {
            List<int> protocolsIds = db.Protocols.Where(p => p.ViolationID == violationID)
                                       .Select(p => p.ProtocolID)
                                       .ToList();

            if (protocolsIds == null) return false;
            
            foreach (var idP in protocolsIds)
                if (!RemoveProtocol(idP, db)) return false;

            return true;
        }

        public static bool RemoveViolationProtocols(int violationID)
        {
            List<int> protocolsIds = new List<int>();

            using(var db = new LeoBaseContext())
            {
                using(var transaction = db.Database.BeginTransaction()) {
                    try {
                        return RemoveViolationProtocols(violationID, db);
                    }catch(Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }

            return false;
        }
        #endregion

        #region Обновление протоколов и нарушений

        public static bool UpdateDefinition(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is DefinitionAboutViolation))
                throw new ArgumentException("Документ не является определением о возбуждение административного расследования");

            var definition = (DefinitionAboutViolation)protocol;

            var definitionUpdate = db.DefinitionsAboutViolation.FirstOrDefault(d => d.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (definitionUpdate == null) return false;

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator != null)
            {
                violator.ViolationID = definition.Protocol.ViolationID;

                if (definition.OrganisationID != 0)
                {
                    var organisation = db.Organisations.FirstOrDefault(o => o.OrganisationID == definition.OrganisationID);

                    if (organisation == null) return false;

                    violator.PersoneID = organisation.OrganisationID;

                    violator.ViolatorType = "Организация";

                    violator.ViolationID = definition.Protocol.ViolationID;
                }
                else if (definition.ViolatorDocumentID != 0)
                {
                    var document = db.Documents.Include("Persone").FirstOrDefault(d => d.DocumentID == definition.ViolatorDocumentID);

                    if (document == null) return false;

                    violator.PersoneID = document.Persone.UserID;

                    violator.ViolatorType = "Гражданин";

                    violator.ViolationID = definition.Protocol.ViolationID;
                }

            }


            definitionUpdate.FindedAmmunitions = definition.FindedAmmunitions;

            definitionUpdate.FindedDocuments = definition.FindedDocuments;

            definitionUpdate.FindedGunsHuntingAndFishing = definition.FindedGunsHuntingAndFishing;

            definitionUpdate.FindedNatureManagementProducts = definition.FindedNatureManagementProducts;

            definitionUpdate.FindedWeapons = definition.FindedWeapons;

            definitionUpdate.FixingMethods = definition.FixingMethods;

            definitionUpdate.KOAP = definition.FixingMethods;

            definitionUpdate.OrganisationID = definition.OrganisationID;

            definitionUpdate.ViolatorDocumentID = definition.ViolatorDocumentID;
            

            return true;
        }

        public static bool UpdateInjunction(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is Injunction))
                throw new ArgumentException("Документ не является предписанием");

            var injunction = (Injunction)protocol;

            var injunctionUpdate = db.Injunctions.Include("InjuctionsItem")
                                                 .FirstOrDefault(i => i.InjunctionID == injunction.InjunctionID);

            if (injunctionUpdate == null)
                return false;

            if(injunction.InjuctionsItem != null)
            {
                var injunctionItemsForRemove = injunctionUpdate.InjuctionsItem;

                if(injunctionItemsForRemove != null)
                    db.InjunctionItems.RemoveRange(injunctionItemsForRemove);

                injunctionUpdate.InjuctionsItem = injunction.InjuctionsItem;
            }

            var document = db.Documents.Include("Persone").FirstOrDefault(d => d.DocumentID == injunction.ViolatorDocumentID);

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if(document != null && violator != null)
            {
                violator.PersoneID = document.Persone.UserID;

                violator.ViolationID = injunction.Protocol.ViolationID;
            }

            injunctionUpdate.ActInspectionDate = injunction.ActInspectionDate;

            injunctionUpdate.ActInspectionNumber = injunction.ActInspectionNumber;

            injunctionUpdate.InjunctionInfo = injunction.InjunctionInfo;

            injunctionUpdate.ViolatorDocumentID = injunction.ViolatorDocumentID;


            return true;
        }

        public static bool UpdateProtocolAboutArrest(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutArrest))
                throw new ArgumentException("Протокол не является протоколом об аресте");

            var protocolAboutArrest = (ProtocolAboutArrest)protocol;

            var protocolAboutArrestUpdate = db.ProtocolsAboutArrest.FirstOrDefault(p => p.ProtocolAboutArrestID == protocolAboutArrest.ProtocolAboutArrestID);

            if (protocolAboutArrestUpdate == null) return false;

            var document = db.Documents.Include("Persone").FirstOrDefault(d => d.DocumentID == protocolAboutArrest.ViolatorDocumentID);

            if (document == null) return false;

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;

            violator.PersoneID = document.Persone.UserID;

            violator.ViolationID = protocol.Protocol.ViolationID;


            protocolAboutArrestUpdate.AboutCar = protocolAboutArrest.AboutCar;

            protocolAboutArrestUpdate.AboutOtherThings = protocolAboutArrest.AboutOtherThings;

            protocolAboutArrestUpdate.AboutViolator = protocolAboutArrest.AboutViolator;

            protocolAboutArrestUpdate.FixingMethods = protocolAboutArrest.FixingMethods;

            protocolAboutArrestUpdate.ThingsWasTransfer = protocolAboutArrest.ThingsWasTransfer;

            protocolAboutArrestUpdate.ViolatorDocumentID = protocolAboutArrest.ViolatorDocumentID;


            return true;
        }

        public static bool UpdateProtocolAboutBringing(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutBringing))
                throw new ArgumentException("Протокол не является протоколом о доставление.");

            var protocolAboutBringing = (ProtocolAboutBringing)protocol;

            var protocolAboutBringingUpdate = db.ProtocolsAboutBringing.FirstOrDefault(p => p.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (protocolAboutBringingUpdate == null) return false;


            var document = db.Documents.Include("Persone").FirstOrDefault(d => d.DocumentID == protocolAboutBringing.ViolatorDocumentID);

            if (document == null) return false;

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;

            violator.ViolationID = protocol.Protocol.ViolationID;

            violator.PersoneID = document.Persone.UserID;


            protocolAboutBringingUpdate.FindedAmmunitions = protocolAboutBringing.FindedAmmunitions;

            protocolAboutBringingUpdate.FindedDocuments = protocolAboutBringing.FindedDocuments;

            protocolAboutBringingUpdate.FindedGunsHuntingAndFishing = protocolAboutBringing.FindedGunsHuntingAndFishing;

            protocolAboutBringingUpdate.FindedNatureManagementProducts = protocolAboutBringing.FindedNatureManagementProducts;

            protocolAboutBringingUpdate.FindedWeapons = protocolAboutBringing.FindedWeapons;

            protocolAboutBringingUpdate.FixingMethods = protocolAboutBringing.FixingMethods;

            protocolAboutBringingUpdate.ViolatorDocumentID = protocolAboutBringing.ViolatorDocumentID;

            protocolAboutBringingUpdate.WithdrawAmmunitions = protocolAboutBringing.WithdrawAmmunitions;

            protocolAboutBringingUpdate.WithdrawDocuments = protocolAboutBringing.WithdrawDocuments;

            protocolAboutBringingUpdate.WithdrawGunsHuntingAndFishing = protocolAboutBringing.WithdrawGunsHuntingAndFishing;

            protocolAboutBringingUpdate.WithdrawNatureManagementProducts = protocolAboutBringing.WithdrawNatureManagementProducts;

            protocolAboutBringingUpdate.WithdrawWeapons = protocolAboutBringing.WithdrawWeapons;
            
            return true;
        }

        public static bool UpdateProtocolAboutInspection(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutInspection))
                throw new ArgumentException("Протокол не является протоколом о личном досмотре");

            var protocolAboutInspection = (ProtocolAboutInspection)protocol;

            var protocolAboutInspectionUpdate = db.ProtocolsAboutInspection.FirstOrDefault(p => p.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            var document = db.Documents
                             .Include("Persone")
                             .FirstOrDefault(d => d.DocumentID == protocolAboutInspection.ViolatorDocumentID);

            if (document == null) return false;

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;

            violator.ViolationID = protocol.Protocol.ViolationID;

            violator.PersoneID = document.Persone.UserID;


            protocolAboutInspectionUpdate.FindedAmmunitions = protocolAboutInspection.FindedAmmunitions;

            protocolAboutInspectionUpdate.FindedDocuments = protocolAboutInspection.FindedDocuments;

            protocolAboutInspectionUpdate.FindedGunsHuntingAndFishing = protocolAboutInspection.FindedGunsHuntingAndFishing;

            protocolAboutInspectionUpdate.FindedNatureManagementProducts = protocolAboutInspection.FindedNatureManagementProducts;

            protocolAboutInspectionUpdate.FindedWeapons = protocolAboutInspection.FindedWeapons;

            protocolAboutInspectionUpdate.FixingMethods = protocolAboutInspection.FixingMethods;

            protocolAboutInspectionUpdate.ViolatorDocumentID = protocolAboutInspection.ViolatorDocumentID;


            return true;
        }

        public static bool UpdateProtocolAboutInspectionAuto(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutInspectionAuto))
                throw new ArgumentException("Протокол не ялвяется протоколом о досмотре транспортного средства");

            var protocolAboutInspectionAuto = (ProtocolAboutInspectionAuto)protocol;

            var protocolAboutInspectionAutoUpdate = db.ProtocolsAboutInspectionAuto
                                                      .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            var document = db.Documents.Include("Persone")
                                       .FirstOrDefault(d => d.DocumentID == protocolAboutInspectionAuto.ViolatorDocumentID);

            if (document == null) return false;

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;

            violator.ViolationID = protocol.Protocol.ViolationID;

            violator.PersoneID = document.Persone.UserID;


            protocolAboutInspectionAutoUpdate.FindedAmmunitions = protocolAboutInspectionAuto.FindedAmmunitions;

            protocolAboutInspectionAutoUpdate.FindedDocuments = protocolAboutInspectionAuto.FindedDocuments;

            protocolAboutInspectionAutoUpdate.FindedGunsHuntingAndFishing = protocolAboutInspectionAuto.FindedGunsHuntingAndFishing;

            protocolAboutInspectionAutoUpdate.FindedNatureManagementProducts = protocolAboutInspectionAuto.FindedNatureManagementProducts;

            protocolAboutInspectionAutoUpdate.FindedWeapons = protocolAboutInspectionAuto.FindedWeapons;

            protocolAboutInspectionAutoUpdate.FixingMethods = protocolAboutInspectionAuto.FixingMethods;

            protocolAboutInspectionAutoUpdate.InformationAbouCar = protocolAboutInspectionAuto.InformationAbouCar;

            protocolAboutInspectionAutoUpdate.ViolatorDocumentID = protocolAboutInspectionAuto.ViolatorDocumentID;


            return true;
        }

        public static bool UpdateProtocolAboutInspectionOrganisation(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutInspectionOrganisation))
                throw new ArgumentException("Протокол не является протоколом осмтра оргнизации");

            var protocolAboutInspectionOrganisation = (ProtocolAboutInspectionOrganisation)protocol;

            var protocolAboutInspectionOrganisationUpdate = db.ProtocolsAboutInspectionOrganisation
                                                              .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);
            
            if (violator == null) return false;

            violator.PersoneID = protocolAboutInspectionOrganisation.OrganisationID;

            violator.ViolationID = protocolAboutInspectionOrganisation.Protocol.ViolationID;


            protocolAboutInspectionOrganisationUpdate.FindedAmmunitions = protocolAboutInspectionOrganisation.FindedAmmunitions;

            protocolAboutInspectionOrganisationUpdate.FindedDocuments = protocolAboutInspectionOrganisation.FindedDocuments;

            protocolAboutInspectionOrganisationUpdate.FindedGunsHuntingAndFishing = protocolAboutInspectionOrganisation.FindedGunsHuntingAndFishing;

            protocolAboutInspectionOrganisationUpdate.FindedNatureManagementProducts = protocolAboutInspectionOrganisation.FindedNatureManagementProducts;

            protocolAboutInspectionOrganisationUpdate.FindedWeapons = protocolAboutInspectionOrganisation.FindedWeapons;

            protocolAboutInspectionOrganisationUpdate.FixingMethods = protocolAboutInspectionOrganisation.FixingMethods;

            protocolAboutInspectionOrganisationUpdate.InspectionTerritoryes = protocolAboutInspectionOrganisation.InspectionTerritoryes;

            protocolAboutInspectionOrganisationUpdate.OrganisationID = protocolAboutInspectionOrganisation.OrganisationID;
            
            return true;
        }

        public static bool UpdateProtocolAboutViolationOrganisation(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutViolationOrganisation))
                throw new ArgumentException("Протокол не является протоколом за административное правонарушение организацией");

            var protocolAboutViolationOrganisation = (ProtocolAboutViolationOrganisation)protocol;

            var protocolAboutViolationOrganisationUpdate = db.ProtocolsAboutViolationOrganisation
                                                             .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;

            violator.ViolationID = protocol.Protocol.ViolationID;

            violator.PersoneID = protocolAboutViolationOrganisation.OrganisationID;


            protocolAboutViolationOrganisationUpdate.Description = protocolAboutViolationOrganisation.Description;

            protocolAboutViolationOrganisationUpdate.KOAP = protocolAboutViolationOrganisation.KOAP;

            protocolAboutViolationOrganisationUpdate.OrganisationID = protocolAboutViolationOrganisation.OrganisationID;

            protocolAboutViolationOrganisationUpdate.ViolationTime = protocolAboutViolationOrganisation.ViolationTime;


            return true;
        }

        public static bool UpdateProtocolAboutViolationPersone(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutViolationPersone))
                throw new ArgumentException("Документ не является протоколом о административном правонарушение");

            var protocolAboutViolation = (ProtocolAboutViolationPersone)protocol;

            var protocolAboutViolationUpdate = db.ProtocolsAboutViolationPersone
                                                       .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            var document = db.Documents.Include("Persone")
                                       .FirstOrDefault(d => d.DocumentID == protocolAboutViolation.ViolatorDocumentID);

            if (document == null) return false;

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;


            violator.PersoneID = document.Persone.UserID;

            violator.ViolationID = protocol.Protocol.ViolationID;


            protocolAboutViolationUpdate.FindedGunsHuntingAndFishing = protocolAboutViolation.FindedGunsHuntingAndFishing;

            protocolAboutViolationUpdate.FindedNatureManagementProducts = protocolAboutViolation.FindedNatureManagementProducts;

            protocolAboutViolationUpdate.FindedWeapons = protocolAboutViolation.FindedWeapons;

            protocolAboutViolationUpdate.KOAP = protocolAboutViolation.KOAP;

            protocolAboutViolationUpdate.ViolationDate = protocolAboutViolation.ViolationDate;

            protocolAboutViolationUpdate.ViolationDescription = protocolAboutViolation.ViolationDescription;

            protocolAboutViolationUpdate.ViolatorDocumentID = protocolAboutViolation.ViolatorDocumentID;


            return true;
        }

        public static bool UpdateProtocolAboutWithdraw(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is ProtocolAboutWithdraw))
                throw new ArgumentException("Документ не является протоколом об изъятии");

            var protocolAboutWithdraw = (ProtocolAboutWithdraw)protocol;

            var protocolAboutWithdrawUpdate = db.ProtocolsAboutWithdraw
                                                .FirstOrDefault(p => p.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            var document = db.Documents.Include("Persone")
                                       .FirstOrDefault(d => d.DocumentID == protocolAboutWithdraw.ViolatorDocumentID);

            if (document == null) return false;

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;

            violator.PersoneID = document.Persone.UserID;

            violator.ViolationID = protocol.Protocol.ViolationID;


            protocolAboutWithdrawUpdate.FixingMethods = protocolAboutWithdraw.FixingMethods;

            protocolAboutWithdrawUpdate.ViolatorDocumentID = protocolAboutWithdraw.ViolatorDocumentID;

            protocolAboutWithdrawUpdate.WithdrawAmmunitions = protocolAboutWithdraw.WithdrawAmmunitions;

            protocolAboutWithdrawUpdate.WithdrawDocuments = protocolAboutWithdraw.WithdrawDocuments;

            protocolAboutWithdrawUpdate.WithdrawGunsHuntingAndFishing = protocolAboutWithdraw.WithdrawGunsHuntingAndFishing;

            protocolAboutWithdrawUpdate.WithdrawNatureManagementProducts = protocolAboutWithdraw.WithdrawNatureManagementProducts;

            protocolAboutWithdrawUpdate.WithdrawWeapons = protocolAboutWithdraw.WithdrawWeapons;
        

            return true;
        }

        public static bool UpdateRuling(IProtocol protocol, LeoBaseContext db)
        {
            if (!(protocol is RulingForViolation))
                throw new ArgumentException("Документ не является постановлением по делу об административном правонарушение");

            var ruling = (RulingForViolation)protocol;

            var rulingUpdate = db.RulingsForViolation.FirstOrDefault(r => r.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            var violator = db.Violators.FirstOrDefault(v => v.Protocol.ProtocolID == protocol.Protocol.ProtocolID);

            if (violator == null) return false;

            if (ruling.OrganisationID != 0)
            {
                violator.PersoneID = ruling.OrganisationID;

                violator.ViolatorType = "Организация";

                violator.ViolationID = protocol.Protocol.ViolationID;
            }
            else if (ruling.ViolatorDocumentID != 0)
            {
                var document = db.Documents.Include("Persone")
                                           .FirstOrDefault(p => p.DocumentID == ruling.ViolatorDocumentID);

                if (document == null) return false;

                violator.PersoneID = document.Persone.UserID;

                violator.ViolatorType = "Гражданин";

                violator.ViolationID = protocol.Protocol.ViolationID;
            }
            else
            {
                return false;
            }


            rulingUpdate.AboutArrest = ruling.AboutArrest;

            rulingUpdate.BankDetails = ruling.BankDetails;

            rulingUpdate.Damage = ruling.Damage;

            rulingUpdate.Fine = ruling.Fine;

            rulingUpdate.FixingDate = ruling.FixingDate;

            rulingUpdate.FixingInfo = ruling.FixingInfo;

            rulingUpdate.KOAP = ruling.KOAP;

            rulingUpdate.Number = ruling.Number;

            rulingUpdate.OrganisationID = ruling.OrganisationID;

            rulingUpdate.Products = ruling.Products;

            rulingUpdate.ProductsPrice = ruling.ProductsPrice;

            rulingUpdate.ViolatorDocumentID = ruling.ViolatorDocumentID;


            return true;
        }

        public static bool UpdateProtocol(IProtocol protocol)
        {
            using(var db = new LeoBaseContext())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Обновление общей информации по протоколу
                        var updatedProtocol = db.Protocols.FirstOrDefault(p => p.ProtocolID == protocol.Protocol.ProtocolID);

                        if (updatedProtocol == null) return false;

                        // Обновление общей информации по протоколу
                        updatedProtocol.PlaceCreatedProtocol = protocol.Protocol.PlaceCreatedProtocol;
                        updatedProtocol.EmployerID = protocol.Protocol.EmployerID;
                        updatedProtocol.DateUpdate = DateTime.Now;
                        updatedProtocol.DateSave = protocol.Protocol.DateSave;
                        updatedProtocol.DateCreatedProtocol = protocol.Protocol.DateCreatedProtocol;
                        updatedProtocol.PlaceCreatedProtocolE = protocol.Protocol.PlaceCreatedProtocolE;
                        updatedProtocol.PlaceCreatedProtocolN = protocol.Protocol.PlaceCreatedProtocolN;
                        updatedProtocol.ViolationID = protocol.Protocol.ViolationID;
                        updatedProtocol.WitnessFIO_1 = protocol.Protocol.WitnessFIO_1;
                        updatedProtocol.WitnessFIO_2 = protocol.Protocol.WitnessFIO_2;
                        updatedProtocol.WitnessLive_1 = protocol.Protocol.WitnessLive_1;
                        updatedProtocol.WitnessLive_2 = protocol.Protocol.WitnessLive_2;

                        bool result = true;
                        
                        switch (updatedProtocol.ProtocolTypeID)
                        {
                            case (int)ProtocolsType.DEFINITION:
                                result = UpdateDefinition(protocol, db);
                                break;
                            case (int)ProtocolsType.INJUCTION:
                                result = UpdateInjunction(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_ARREST:
                                result = UpdateProtocolAboutArrest(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_BRINGING:
                                result = UpdateProtocolAboutBringing(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION:
                                result = UpdateProtocolAboutInspection(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_AUTO:
                                result = UpdateProtocolAboutInspectionAuto(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_INSPECTION_ORGANISATION:
                                result = UpdateProtocolAboutInspectionOrganisation(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_ORGANISATION:
                                result = UpdateProtocolAboutViolationOrganisation(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_VIOLATION_PERSONE:
                                result = UpdateProtocolAboutViolationPersone(protocol, db);
                                break;
                            case (int)ProtocolsType.PROTOCOL_ABOUT_WITHDRAW:
                                result = UpdateProtocolAboutWithdraw(protocol, db);
                                break;
                            case (int)ProtocolsType.RULING_FOR_VIOLATION:
                                result = UpdateRuling(protocol, db);
                                break;
                            default:
                                return false;
                        }

                        if (!result) return false;

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

        #endregion
    }
}
