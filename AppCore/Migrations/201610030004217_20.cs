namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID", "dbo.Protocols");
            DropIndex("dbo.ProtocolAboutViolationOrganisations", new[] { "Protocol_ProtocolID" });
            AlterColumn("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID");
            AddForeignKey("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID", "dbo.Protocols");
            DropIndex("dbo.ProtocolAboutViolationOrganisations", new[] { "Protocol_ProtocolID" });
            AlterColumn("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID", c => c.Int());
            CreateIndex("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID");
            AddForeignKey("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
        }
    }
}
