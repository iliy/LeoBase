namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Persones", "TestProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Persones", "TestProperty", c => c.String(nullable: false));
        }
    }
}