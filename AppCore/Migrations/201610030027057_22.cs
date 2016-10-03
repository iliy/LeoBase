namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Violations", "DateSave", c => c.DateTime(nullable: false));
            AddColumn("dbo.Violations", "DateUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Violations", "DateUpdate");
            DropColumn("dbo.Violations", "DateSave");
        }
    }
}
