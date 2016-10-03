namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersoneAddresses", "Persone_UserID", "dbo.Persones");
            DropIndex("dbo.PersoneAddresses", new[] { "Persone_UserID" });
            AlterColumn("dbo.PersoneAddresses", "Persone_UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.PersoneAddresses", "Persone_UserID");
            AddForeignKey("dbo.PersoneAddresses", "Persone_UserID", "dbo.Persones", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersoneAddresses", "Persone_UserID", "dbo.Persones");
            DropIndex("dbo.PersoneAddresses", new[] { "Persone_UserID" });
            AlterColumn("dbo.PersoneAddresses", "Persone_UserID", c => c.Int());
            CreateIndex("dbo.PersoneAddresses", "Persone_UserID");
            AddForeignKey("dbo.PersoneAddresses", "Persone_UserID", "dbo.Persones", "UserID");
        }
    }
}
