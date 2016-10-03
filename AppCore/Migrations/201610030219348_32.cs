namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _32 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employers", "ProtocolID");
            DropColumn("dbo.Violators", "ProtocolID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Violators", "ProtocolID", c => c.Int(nullable: false));
            AddColumn("dbo.Employers", "ProtocolID", c => c.Int(nullable: false));
        }
    }
}
