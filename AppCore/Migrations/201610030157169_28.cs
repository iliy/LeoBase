namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Protocols", "Employer_UserID", "dbo.Persones");
            DropForeignKey("dbo.Employers", "Persone_UserID", "dbo.Persones");
            DropForeignKey("dbo.Employers", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Violators", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Violators", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropForeignKey("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropIndex("dbo.Protocols", new[] { "Employer_UserID" });
            DropIndex("dbo.Employers", new[] { "Persone_UserID" });
            DropIndex("dbo.Employers", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Violators", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Violators", new[] { "ViolatorDocument_DocumentID" });
            DropIndex("dbo.ProtocolAboutBringings", new[] { "ViolatorDocument_DocumentID" });
            RenameColumn(table: "dbo.Protocols", name: "Violation_ViolationID", newName: "ViolationID");
            RenameIndex(table: "dbo.Protocols", name: "IX_Violation_ViolationID", newName: "IX_ViolationID");
            AddColumn("dbo.Protocols", "EmployerID", c => c.Int(nullable: false));
            AddColumn("dbo.Employers", "EmployerID", c => c.Int(nullable: false));
            AddColumn("dbo.Employers", "ViolationID", c => c.Int(nullable: false));
            AddColumn("dbo.Violators", "PersoneID", c => c.Int(nullable: false));
            AddColumn("dbo.Violators", "ViolationID", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolAboutBringings", "ViolatorDocumentID", c => c.Int(nullable: false));
            DropColumn("dbo.Protocols", "Employer_UserID");
            DropColumn("dbo.Employers", "Persone_UserID");
            DropColumn("dbo.Employers", "Violation_ViolationID");
            DropColumn("dbo.Violators", "Violation_ViolationID");
            DropColumn("dbo.Violators", "ViolatorDocument_DocumentID");
            DropColumn("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID", c => c.Int(nullable: false));
            AddColumn("dbo.Violators", "ViolatorDocument_DocumentID", c => c.Int());
            AddColumn("dbo.Violators", "Violation_ViolationID", c => c.Int());
            AddColumn("dbo.Employers", "Violation_ViolationID", c => c.Int());
            AddColumn("dbo.Employers", "Persone_UserID", c => c.Int());
            AddColumn("dbo.Protocols", "Employer_UserID", c => c.Int(nullable: false));
            DropColumn("dbo.ProtocolAboutBringings", "ViolatorDocumentID");
            DropColumn("dbo.Violators", "ViolationID");
            DropColumn("dbo.Violators", "PersoneID");
            DropColumn("dbo.Employers", "ViolationID");
            DropColumn("dbo.Employers", "EmployerID");
            DropColumn("dbo.Protocols", "EmployerID");
            RenameIndex(table: "dbo.Protocols", name: "IX_ViolationID", newName: "IX_Violation_ViolationID");
            RenameColumn(table: "dbo.Protocols", name: "ViolationID", newName: "Violation_ViolationID");
            CreateIndex("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.Violators", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.Violators", "Violation_ViolationID");
            CreateIndex("dbo.Employers", "Violation_ViolationID");
            CreateIndex("dbo.Employers", "Persone_UserID");
            CreateIndex("dbo.Protocols", "Employer_UserID");
            AddForeignKey("dbo.ProtocolAboutBringings", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID", cascadeDelete: true);
            AddForeignKey("dbo.Violators", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            AddForeignKey("dbo.Violators", "Violation_ViolationID", "dbo.Violations", "ViolationID");
            AddForeignKey("dbo.Employers", "Violation_ViolationID", "dbo.Violations", "ViolationID");
            AddForeignKey("dbo.Employers", "Persone_UserID", "dbo.Persones", "UserID");
            AddForeignKey("dbo.Protocols", "Employer_UserID", "dbo.Persones", "UserID", cascadeDelete: true);
        }
    }
}
