namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "ProtocolID", c => c.Int(nullable: false));
            AddColumn("dbo.Violators", "ProtocolID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Violators", "ProtocolID");
            DropColumn("dbo.Employers", "ProtocolID");
        }
    }
}
