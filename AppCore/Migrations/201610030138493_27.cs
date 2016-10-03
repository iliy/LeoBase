namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Violators", "Persone_UserID", "dbo.Persones");
            DropIndex("dbo.Violators", new[] { "Persone_UserID" });
            AddColumn("dbo.Violators", "ViolatorDocument_DocumentID", c => c.Int());
            CreateIndex("dbo.Violators", "ViolatorDocument_DocumentID");
            AddForeignKey("dbo.Violators", "ViolatorDocument_DocumentID", "dbo.Documents", "DocumentID");
            DropColumn("dbo.Violators", "Persone_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Violators", "Persone_UserID", c => c.Int());
            DropForeignKey("dbo.Violators", "ViolatorDocument_DocumentID", "dbo.Documents");
            DropIndex("dbo.Violators", new[] { "ViolatorDocument_DocumentID" });
            DropColumn("dbo.Violators", "ViolatorDocument_DocumentID");
            CreateIndex("dbo.Violators", "Persone_UserID");
            AddForeignKey("dbo.Violators", "Persone_UserID", "dbo.Persones", "UserID");
        }
    }
}
