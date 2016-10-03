namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Injunctions", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.Organisations", "Representative_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutArrests", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutBringings", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutInspections", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutInspectionAutoes", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutViolationPersones", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutWithdraws", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.RulingForViolations", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.Protocols", "Employer_UserID", "dbo.Persones");
            DropForeignKey("dbo.Protocols", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.ProtocolAboutArrests", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutBringings", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutInspections", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.RulingForViolations", "Protocol_ProtocolID", "dbo.Protocols");
            DropIndex("dbo.Injunctions", new[] { "Violator_UserID" });
            DropIndex("dbo.Protocols", new[] { "Employer_UserID" });
            DropIndex("dbo.Protocols", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Organisations", new[] { "Representative_UserID" });
            DropIndex("dbo.ProtocolAboutArrests", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutArrests", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutBringings", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutBringings", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutInspections", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutInspections", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutInspectionAutoes", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutInspectionAutoes", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutInspectionOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutViolationOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutViolationPersones", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutViolationPersones", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutWithdraws", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutWithdraws", new[] { "Violator_UserID" });
            DropIndex("dbo.RulingForViolations", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.RulingForViolations", new[] { "Violator_UserID" });
            CreateTable(
                "dbo.DefinitionAboutViolations",
                c => new
                    {
                        DefinitionAboutViolationID = c.Int(nullable: false, identity: true),
                        FindedWeapons = c.String(),
                        FindedAmmunitions = c.String(),
                        FindedGunsHuntingAndFishing = c.String(),
                        FindedNatureManagementProducts = c.String(),
                        FindedDocuments = c.String(),
                        KOAP = c.String(),
                        FixingMethods = c.String(),
                        Organisation_OrganisationID = c.Int(),
                        Protocol_ProtocolID = c.Int(nullable: false),
                        ViolatorDocument_DocumentID = c.Int(),
                    })
                .PrimaryKey(t => t.DefinitionAboutViolationID)
                .ForeignKey("dbo.Organisations", t => t.Organisation_OrganisationID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.ViolatorDocument_DocumentID)
                .Index(t => t.Organisation_OrganisationID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.ViolatorDocument_DocumentID);
            
            AddColumn("dbo.Injunctions", "ViolatorDocument_DocumentID", c => c.Int());
            AddColumn("dbo.Protocols", "WitnessFIO_1", c => c.String());
            AddColumn("dbo.Protocols", "WitnessLive_1", c => c.String());
            AddColumn("dbo.Protocols", "WitnessFIO_2", c => c.String());
            AddColumn("dbo.Protocols", "WitnessLive_2", c => c.String());
            AddColumn("dbo.Organisations", "RepresentativeDocument_DocumentID", c => c.Int());
            AddColumn("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID", c => c.Int());
            AddColumn("dbo.ProtocolAboutInspectionOrganisations", "Protocol_ProtocolID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.RulingForViolations", "ViolatorDocument_DocumentID", c => c.Int());
            AlterColumn("dbo.Protocols", "Employer_UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Protocols", "Violation_ViolationID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutArrests", "Protocol_ProtocolID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutBringings", "Protocol_ProtocolID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutInspections", "Protocol_ProtocolID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID", c => c.Int(nullable: false));
            AlterColumn("dbo.RulingForViolations", "Protocol_ProtocolID", c => c.Int(nullable: false));
            CreateIndex("dbo.Organisations", "RepresentativeDocument_DocumentID");
            CreateIndex("dbo.Protocols", "Employer_UserID");
            CreateIndex("dbo.Protocols", "Violation_ViolationID");
            CreateIndex("dbo.Injunctions", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutArrests", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutBringings", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutInspections", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID");
            CreateIndex("dbo.ProtocolAboutInspectionOrganisations", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID");
            CreateIndex("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.RulingForViolations", "Protocol_ProtocolID");
            CreateIndex("dbo.RulingForViolations", "ViolatorDocument_DocumentID");
            AddForeignKey("dbo.Organisations", "RepresentativeDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.Injunctions", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.RulingForViolations", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.Protocols", "Employer_UserID", "dbo.Persones", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Protocols", "Violation_ViolationID", "dbo.Violations", "ViolationID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutArrests", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutBringings", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutInspections", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            AddForeignKey("dbo.RulingForViolations", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
            DropColumn("dbo.Injunctions", "Violator_UserID");
            DropColumn("dbo.Protocols", "WitnessFIO");
            DropColumn("dbo.Protocols", "WitnessLive");
            DropColumn("dbo.Organisations", "Representative_UserID");
            DropColumn("dbo.ProtocolAboutArrests", "Violator_UserID");
            DropColumn("dbo.ProtocolAboutBringings", "Violator_UserID");
            DropColumn("dbo.ProtocolAboutInspections", "Violator_UserID");
            DropColumn("dbo.ProtocolAboutInspectionAutoes", "Violator_UserID");
            DropColumn("dbo.ProtocolAboutViolationPersones", "Violator_UserID");
            DropColumn("dbo.ProtocolAboutWithdraws", "Violator_UserID");
            DropColumn("dbo.RulingForViolations", "Violator_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RulingForViolations", "Violator_UserID", c => c.Int());
            AddColumn("dbo.ProtocolAboutWithdraws", "Violator_UserID", c => c.Int());
            AddColumn("dbo.ProtocolAboutViolationPersones", "Violator_UserID", c => c.Int());
            AddColumn("dbo.ProtocolAboutInspectionAutoes", "Violator_UserID", c => c.Int());
            AddColumn("dbo.ProtocolAboutInspections", "Violator_UserID", c => c.Int());
            AddColumn("dbo.ProtocolAboutBringings", "Violator_UserID", c => c.Int());
            AddColumn("dbo.ProtocolAboutArrests", "Violator_UserID", c => c.Int());
            AddColumn("dbo.Organisations", "Representative_UserID", c => c.Int());
            AddColumn("dbo.Protocols", "WitnessLive", c => c.String());
            AddColumn("dbo.Protocols", "WitnessFIO", c => c.String());
            AddColumn("dbo.Injunctions", "Violator_UserID", c => c.Int());
            DropForeignKey("dbo.RulingForViolations", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutInspections", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutBringings", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutArrests", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.Protocols", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Protocols", "Employer_UserID", "dbo.Persones");
            DropForeignKey("dbo.RulingForViolations", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.Injunctions", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.DefinitionAboutViolations", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.DefinitionAboutViolations", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.DefinitionAboutViolations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.Organisations", "RepresentativeDocument_DocumentID", "dbo.Documents");
            DropIndex("dbo.RulingForViolations", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.RulingForViolations", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutWithdraws", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutWithdraws", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutViolationPersones", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutViolationPersones", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutViolationOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutInspectionOrganisations", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutInspectionOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutInspectionAutoes", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutInspectionAutoes", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutInspections", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutInspections", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutBringings", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutBringings", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutArrests", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutArrests", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.Injunctions", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.Protocols", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Protocols", new[] { "Employer_UserID" });
            DropIndex("dbo.Organisations", new[] { "RepresentativeDocument_DocumentID" });
            DropIndex("dbo.DefinitionAboutViolations", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.DefinitionAboutViolations", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.DefinitionAboutViolations", new[] { "Organisation_OrganisationID" });
            AlterColumn("dbo.RulingForViolations", "Protocol_ProtocolID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutInspections", "Protocol_ProtocolID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutBringings", "Protocol_ProtocolID", c => c.Int());
            AlterColumn("dbo.ProtocolAboutArrests", "Protocol_ProtocolID", c => c.Int());
            AlterColumn("dbo.Protocols", "Violation_ViolationID", c => c.Int());
            AlterColumn("dbo.Protocols", "Employer_UserID", c => c.Int());
            DropColumn("dbo.RulingForViolations", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutInspectionOrganisations", "Protocol_ProtocolID");
            DropColumn("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID");
            DropColumn("dbo.Organisations", "RepresentativeDocument_DocumentID");
            DropColumn("dbo.Protocols", "WitnessLive_2");
            DropColumn("dbo.Protocols", "WitnessFIO_2");
            DropColumn("dbo.Protocols", "WitnessLive_1");
            DropColumn("dbo.Protocols", "WitnessFIO_1");
            DropColumn("dbo.Injunctions", "ViolatorDocument_DocumentID");
            DropTable("dbo.DefinitionAboutViolations");
            CreateIndex("dbo.RulingForViolations", "Violator_UserID");
            CreateIndex("dbo.RulingForViolations", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutWithdraws", "Violator_UserID");
            CreateIndex("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutViolationPersones", "Violator_UserID");
            CreateIndex("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID");
            CreateIndex("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID");
            CreateIndex("dbo.ProtocolAboutInspectionAutoes", "Violator_UserID");
            CreateIndex("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutInspections", "Violator_UserID");
            CreateIndex("dbo.ProtocolAboutInspections", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutBringings", "Violator_UserID");
            CreateIndex("dbo.ProtocolAboutBringings", "Protocol_ProtocolID");
            CreateIndex("dbo.ProtocolAboutArrests", "Violator_UserID");
            CreateIndex("dbo.ProtocolAboutArrests", "Protocol_ProtocolID");
            CreateIndex("dbo.Organisations", "Representative_UserID");
            CreateIndex("dbo.Protocols", "Violation_ViolationID");
            CreateIndex("dbo.Protocols", "Employer_UserID");
            CreateIndex("dbo.Injunctions", "Violator_UserID");
            AddForeignKey("dbo.RulingForViolations", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
            AddForeignKey("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
            AddForeignKey("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
            AddForeignKey("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID");
            AddForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID");
            AddForeignKey("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
            AddForeignKey("dbo.ProtocolAboutInspections", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
            AddForeignKey("dbo.ProtocolAboutBringings", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
            AddForeignKey("dbo.ProtocolAboutArrests", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
            AddForeignKey("dbo.Protocols", "Violation_ViolationID", "dbo.Violations", "ViolationID");
            AddForeignKey("dbo.Protocols", "Employer_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.RulingForViolations", "Violator_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.ProtocolAboutWithdraws", "Violator_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.ProtocolAboutViolationPersones", "Violator_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.ProtocolAboutInspectionAutoes", "Violator_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.ProtocolAboutInspections", "Violator_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.ProtocolAboutBringings", "Violator_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.ProtocolAboutArrests", "Violator_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.Organisations", "Representative_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.Injunctions", "Violator_UserID", "dbo.Persones", "UserID");
        }
    }
}
