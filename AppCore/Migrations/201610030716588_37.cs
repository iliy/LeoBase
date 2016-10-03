namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _37 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProtocolAboutViolationOrganisations", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProtocolAboutViolationOrganisations", "Description");
        }
    }
}
