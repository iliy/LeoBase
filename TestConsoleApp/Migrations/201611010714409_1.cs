namespace TestConsoleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persones",
                c => new
                    {
                        PersoneID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.PersoneID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Persones");
        }
    }
}
