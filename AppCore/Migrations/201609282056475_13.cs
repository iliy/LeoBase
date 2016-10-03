namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persones", "PlaceWork", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persones", "PlaceWork");
        }
    }
}
