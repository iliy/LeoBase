namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persones", "WasBeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Persones", "WasBeUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persones", "WasBeUpdated");
            DropColumn("dbo.Persones", "WasBeCreated");
        }
    }
}
