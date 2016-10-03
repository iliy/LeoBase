namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocols", "DateSave", c => c.DateTime(nullable: false));
            AddColumn("dbo.Protocols", "DateUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocols", "DateUpdate");
            DropColumn("dbo.Protocols", "DateSave");
        }
    }
}
