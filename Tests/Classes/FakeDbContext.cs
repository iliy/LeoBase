using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Classes
{
    public class FakeDbContext:DbContext
    {
        public IQueryable<Persone> Persones { get; set; }
        public IQueryable<PersoneAddress> Addresses { get; set; }
        public IQueryable<Document> Documents { get; set; }
        public IQueryable<DocumentType> DocumentsType { get; set; }
        public IQueryable<Phone> Phones { get; set; }
        public IQueryable<EmploeyrPosition> Positions { get; set; }
        public IQueryable<Manager> Managers { get; set; }
        public IQueryable<Violation> Violations { get; set; }
        public IQueryable<ViolationType> ViolationsType { get; set; }
        public IQueryable<Violator> Violators { get; set; }
        public IQueryable<Employer> Employers { get; set; }
        public IQueryable<ViolationImage> ViolationImages { get; set; }


        ///Протоколы
        public IQueryable<Protocol> Protocols { get; set; }

        public IQueryable<DefinitionAboutViolation> DefinitionsAboutViolation { get; set; }

        /// <summary>
        /// Протоклы об аресте
        /// </summary>
        public IQueryable<ProtocolAboutArrest> ProtocolsAboutArrest { get; set; }
        /// <summary>
        /// Протоколы о доставление
        /// </summary>
        public IQueryable<ProtocolAboutBringing> ProtocolsAboutBringing { get; set; }
        /// <summary>
        /// Протоколы о личном досмотре и досмотре вещей, находящихся при физическом лице
        /// </summary>
        public IQueryable<ProtocolAboutInspection> ProtocolsAboutInspection { get; set; }
        /// <summary>
        /// Протокол о досмотре транспортного средства
        /// </summary>
        public IQueryable<ProtocolAboutInspectionAuto> ProtocolsAboutInspectionAuto { get; set; }
        /// <summary>
        /// Протокол об осмотре принадлежащих юридическому лицу или индивидуальному предпринимателю помещений, территорий
        /// и находящихся там вещей и документов
        /// </summary>
        public IQueryable<ProtocolAboutInspectionOrganisation> ProtocolsAboutInspectionOrganisation { get; set; }
        /// <summary>
        /// Протокол об административном правонарушение для юридического лица
        /// </summary>
        public IQueryable<ProtocolAboutViolationOrganisation> ProtocolsAboutViolationOrganisation { get; set; }
        /// <summary>
        /// Протокол об административном правонарушение для физического лица
        /// </summary>
        public IQueryable<ProtocolAboutViolationPersone> ProtocolsAboutViolationPersone { get; set; }
        /// <summary>
        /// Протокол об изъятие
        /// </summary>
        public IQueryable<ProtocolAboutWithdraw> ProtocolsAboutWithdraw { get; set; }
        /// <summary>
        /// Предписание об устранение нарушений законодательства в сфере
        /// природопользования и охраны окружающей среды
        /// </summary>
        public IQueryable<Injunction> Injunctions { get; set; }

        /// <summary>
        /// Пункт предписания
        /// </summary>
        public IQueryable<InjunctionItem> InjunctionItems { get; set; }
        /// <summary>
        /// Юридические лица
        /// </summary>
        public IQueryable<Organisation> Organisations { get; set; }
        /// <summary>
        /// Постановление по делу об административном правонарушение
        /// </summary>
        public IQueryable<RulingForViolation> RulingsForViolation { get; set; }
        /// <summary>
        /// Типы протоколов
        /// </summary>
        public IQueryable<ProtocolType> ProtocolTypes { get; set; }
    }
}
