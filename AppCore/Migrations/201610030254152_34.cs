namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _34 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DefinitionAboutViolations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.DefinitionAboutViolations", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.Injunctions", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.RulingForViolations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.RulingForViolations", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropIndex("dbo.DefinitionAboutViolations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.DefinitionAboutViolations", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.Injunctions", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutArrests", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutInspections", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutInspectionAutoes", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutInspectionOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutViolationOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutViolationPersones", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutWithdraws", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.RulingForViolations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.RulingForViolations", new[] { "ViolatorDocument_DocumentID" });
            AddColumn("dbo.DefinitionAboutViolations", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.DefinitionAboutViolations", "OrganisationID", c => c.Int(nullable: false));
            AddColumn("dbo.Injunctions", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutArrests", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutInspections", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutInspectionOrganisations", "OrganisationID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutViolationOrganisations", "OrganisationID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutViolationPersones", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutWithdraws", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.RulingForViolations", "ViolatorDocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.RulingForViolations", "OrganisationID", c => c.Int(nullable: false));
            DropColumn("dbo.DefinitionAboutViolations", "Organisation_OrganisationID");
            DropColumn("dbo.DefinitionAboutViolations", "ViolatorDocument_DocumentID");
            DropColumn("dbo.Injunctions", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID");
            DropColumn("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID");
            DropColumn("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID");
            DropColumn("dbo.RulingForViolations", "Organisation_OrganisationID");
            DropColumn("dbo.RulingForViolations", "ViolatorDocument_DocumentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RulingForViolations", "ViolatorDocument_DocumentID", c => c.Int());
            AddColumn("dbo.RulingForViolations", "Organisation_OrganisationID", c => c.Int());
            AddColumn("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID", c => c.Int());
            AddColumn("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.Injunctions", "ViolatorDocument_DocumentID", c => c.Int());
            AddColumn("dbo.DefinitionAboutViolations", "ViolatorDocument_DocumentID", c => c.Int());
            AddColumn("dbo.DefinitionAboutViolations", "Organisation_OrganisationID", c => c.Int());
            DropColumn("dbo.RulingForViolations", "OrganisationID");
            DropColumn("dbo.RulingForViolations", "ViolatorDocumentID");
            DropColumn("dbo.ProtocolAboutWithdraws", "ViolatorDocumentID");
            DropColumn("dbo.ProtocolAboutViolationPersones", "ViolatorDocumentID");
            DropColumn("dbo.ProtocolAboutViolationOrganisations", "OrganisationID");
            DropColumn("dbo.ProtocolAboutInspectionOrganisations", "OrganisationID");
            DropColumn("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocumentID");
            DropColumn("dbo.ProtocolAboutInspections", "ViolatorDocumentID");
            DropColumn("dbo.ProtocolAboutArrests", "ViolatorDocumentID");
            DropColumn("dbo.Injunctions", "ViolatorDocumentID");
            DropColumn("dbo.DefinitionAboutViolations", "OrganisationID");
            DropColumn("dbo.DefinitionAboutViolations", "ViolatorDocumentID");
            CreateIndex("dbo.RulingForViolations", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.RulingForViolations", "Organisation_OrganisationID");
            CreateIndex("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID");
            CreateIndex("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID");
            CreateIndex("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.Injunctions", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.DefinitionAboutViolations", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.DefinitionAboutViolations", "Organisation_OrganisationID");
            AddForeignKey("dbo.RulingForViolations", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.RulingForViolations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID");
            AddForeignKey("dbo.ProtocolAboutWithdraws", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutViolationPersones", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutInspectionAutoes", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.ProtocolAboutInspections", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.ProtocolAboutArrests", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.Injunctions", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.DefinitionAboutViolations", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.DefinitionAboutViolations", "Organisation_OrganisationID", "dbo.Organisations", "OrganisationID");
        }
    }
}
