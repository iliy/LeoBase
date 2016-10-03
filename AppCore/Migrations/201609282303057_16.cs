namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employers",
                c => new
                    {
                        EmploerID = c.Int(nullable: false, identity: true),
                        Persone_UserID = c.Int(),
                        Violation_ViolationID = c.Int(),
                    })
                .PrimaryKey(t => t.EmploerID)
                .ForeignKey("dbo.Persones", t => t.Persone_UserID)
                .ForeignKey("dbo.Violations", t => t.Violation_ViolationID)
                .Index(t => t.Persone_UserID)
                .Index(t => t.Violation_ViolationID);
            
            CreateTable(
                "dbo.Violators",
                c => new
                    {
                        ViolatorID = c.Int(nullable: false, identity: true),
                        Persone_UserID = c.Int(),
                        Violation_ViolationID = c.Int(),
                    })
                .PrimaryKey(t => t.ViolatorID)
                .ForeignKey("dbo.Persones", t => t.Persone_UserID)
                .ForeignKey("dbo.Violations", t => t.Violation_ViolationID)
                .Index(t => t.Persone_UserID)
                .Index(t => t.Violation_ViolationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Violators", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Violators", "Persone_UserID", "dbo.Persones");
            DropForeignKey("dbo.Employers", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Employers", "Persone_UserID", "dbo.Persones");
            DropIndex("dbo.Violators", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Violators", new[] { "Persone_UserID" });
            DropIndex("dbo.Employers", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Employers", new[] { "Persone_UserID" });
            DropTable("dbo.Violators");
            DropTable("dbo.Employers");
        }
    }
}
