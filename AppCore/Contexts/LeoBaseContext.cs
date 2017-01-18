using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Contexts
{
    public class LeoBaseContext : DbContext
    {

        public LeoBaseContext():base("DataSource='LeoDataBase.sdf';Password='abc45166bca'") {

        }

        public static void InitDefaultValue()
        {
            using (var context = new LeoBaseContext())
            {
                if (context.Managers.Count() == 0)
                {
                    context.Managers.AddRange(new List<Manager> {
                        new Manager { Login = "admin", Password = "admin", Role = "admin" },
                        new Manager { Login = "user", Password = "user", Role = "user" }
                        }
                    );
                }

                if (context.ProtocolTypes.Count() == 0)
                {
                    context.ProtocolTypes.AddRange(new List<ProtocolType> {
                        new ProtocolType { Name = "Протоклы об аресте" },
                        new ProtocolType { Name = "Протоколы о доставление" },
                        new ProtocolType { Name = "Протоколы о личном досмотре и досмотре вещей, находящихся при физическом лице" },
                        new ProtocolType { Name = "Протокол о досмотре транспортного средства" },
                        new ProtocolType { Name = "Протокол об осмотре принадлежащих юридическому лицу или индивидуальному предпринимателю помещений, территорий и находящихся там вещей и документов" },
                        new ProtocolType { Name = "Протокол об административном правонарушение для юридического лица" },
                        new ProtocolType { Name = "Протокол об административном правонарушение для физического лица" },
                        new ProtocolType { Name = "Протокол об изъятие" },
                        new ProtocolType { Name = "Предписание об устранение нарушений законодательства в сфере природопользования и охраны окружающей среды" },
                        new ProtocolType { Name = "Постановление по делу об административном правонарушение" },
                        new ProtocolType { Name = "Определение о возбуждение дела об административном правонарушение и проведение административного расследования" }}
                        );
                }

                if (context.Positions.Count() == 0)
                {
                    context.Positions.AddRange(new List<EmploeyrPosition> {
                        new EmploeyrPosition { Name = "Государственный инспектор" },
                        new EmploeyrPosition { Name = "Участковый государственный инспектор" },
                        new EmploeyrPosition { Name = "Старший государственный инспектор" }
                        }
                        );
                }

                if (context.DocumentsType.Count() == 0)
                {
                    context.DocumentsType.AddRange(new List<DocumentType> {
                        new DocumentType { Name = "Водительское удостоверение" },
                        new DocumentType { Name = "Паспорт" }
                        }
                        );
                }
                context.SaveChanges();
            }
        }


        public DbSet<Persone> Persones { get; set; }
        public DbSet<PersoneAddress> Addresses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentsType { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<EmploeyrPosition> Positions { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<ViolationType> ViolationsType { get; set; }
        public DbSet<Violator> Violators { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<ViolationImage> ViolationImages { get; set; }
        public DbSet<AdminViolation> AdminViolations { get; set; }

        ///Протоколы
        public DbSet<Protocol> Protocols { get; set; }

        public DbSet<DefinitionAboutViolation> DefinitionsAboutViolation { get; set; }

        /// <summary>
        /// Протоклы об аресте
        /// </summary>
        public DbSet<ProtocolAboutArrest> ProtocolsAboutArrest { get; set; }
        /// <summary>
        /// Протоколы о доставление
        /// </summary>
        public DbSet<ProtocolAboutBringing> ProtocolsAboutBringing { get; set; }
        /// <summary>
        /// Протоколы о личном досмотре и досмотре вещей, находящихся при физическом лице
        /// </summary>
        public DbSet<ProtocolAboutInspection> ProtocolsAboutInspection { get; set; }
        /// <summary>
        /// Протокол о досмотре транспортного средства
        /// </summary>
        public DbSet<ProtocolAboutInspectionAuto> ProtocolsAboutInspectionAuto { get; set; }
        /// <summary>
        /// Протокол об осмотре принадлежащих юридическому лицу или индивидуальному предпринимателю помещений, территорий
        /// и находящихся там вещей и документов
        /// </summary>
        public DbSet<ProtocolAboutInspectionOrganisation> ProtocolsAboutInspectionOrganisation { get; set; }
        /// <summary>
        /// Протокол об административном правонарушение для юридического лица
        /// </summary>
        public DbSet<ProtocolAboutViolationOrganisation> ProtocolsAboutViolationOrganisation { get; set; }
        /// <summary>
        /// Протокол об административном правонарушение для физического лица
        /// </summary>
        public DbSet<ProtocolAboutViolationPersone> ProtocolsAboutViolationPersone { get; set; }
        /// <summary>
        /// Протокол об изъятие
        /// </summary>
        public DbSet<ProtocolAboutWithdraw> ProtocolsAboutWithdraw { get; set; }
        /// <summary>
        /// Предписание об устранение нарушений законодательства в сфере
        /// природопользования и охраны окружающей среды
        /// </summary>
        public DbSet<Injunction> Injunctions { get; set; }

        /// <summary>
        /// Пункт предписания
        /// </summary>
        public DbSet<InjunctionItem> InjunctionItems { get; set; }
        /// <summary>
        /// Юридические лица
        /// </summary>
        public DbSet<Organisation> Organisations { get; set; }
        /// <summary>
        /// Постановление по делу об административном правонарушение
        /// </summary>
        public DbSet<RulingForViolation> RulingsForViolation { get; set; }
        /// <summary>
        /// Типы протоколов
        /// </summary>
        public DbSet<ProtocolType> ProtocolTypes { get; set; }


    }
}
