namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _40 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Protocols", "Huiny");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocols", "Huiny", c => c.Int(nullable: false));
        }
    }
}
