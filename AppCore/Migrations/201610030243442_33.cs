namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "ProtocolID", c => c.Int(nullable: false));
            DropColumn("dbo.Employers", "PersoneID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employers", "PersoneID", c => c.Int(nullable: false));
            DropColumn("dbo.Employers", "ProtocolID");
        }
    }
}
