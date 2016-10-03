namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Violators", "Protocol_ProtocolID", c => c.Int());
            CreateIndex("dbo.Violators", "Protocol_ProtocolID");
            AddForeignKey("dbo.Violators", "Protocol_ProtocolID", "dbo.Protocols", "ProtocolID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Violators", "Protocol_ProtocolID", "dbo.Protocols");
            DropIndex("dbo.Violators", new[] { "Protocol_ProtocolID" });
            DropColumn("dbo.Violators", "Protocol_ProtocolID");
        }
    }
}
