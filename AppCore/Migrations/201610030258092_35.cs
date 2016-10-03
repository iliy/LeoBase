namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "PersoneID", c => c.Int(nullable: false));
            DropColumn("dbo.Employers", "ProtocolID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employers", "ProtocolID", c => c.Int(nullable: false));
            DropColumn("dbo.Employers", "PersoneID");
        }
    }
}
