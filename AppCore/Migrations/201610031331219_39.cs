namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocols", "Huiny", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocols", "Huiny");
        }
    }
}
