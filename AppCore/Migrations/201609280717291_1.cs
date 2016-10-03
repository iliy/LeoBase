namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersoneAddresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false),
                        Subject = c.String(),
                        Area = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HomeNumber = c.String(),
                        Flat = c.String(),
                        Note = c.String(),
                        Persone_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Persones", t => t.Persone_UserID)
                .Index(t => t.Persone_UserID);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentID = c.Int(nullable: false, identity: true),
                        Serial = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        IssuedBy = c.String(nullable: false),
                        WhenIssued = c.DateTime(nullable: false),
                        CodeDevision = c.String(),
                        DocumentType_DocumentTypeID = c.Int(),
                        Persone_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentID)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentType_DocumentTypeID)
                .ForeignKey("dbo.Persones", t => t.Persone_UserID)
                .Index(t => t.DocumentType_DocumentTypeID)
                .Index(t => t.Persone_UserID);
            
            CreateTable(
                "dbo.DocumentTypes",
                c => new
                    {
                        DocumentTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentTypeID);
            
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
                "dbo.Persones",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        IsEmploeyr = c.Boolean(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        DateBirthday = c.DateTime(nullable: false),
                        Image = c.Binary(storeType: "image"),
                        Position_PositionID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.EmploeyrPositions", t => t.Position_PositionID)
                .Index(t => t.Position_PositionID);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        PhoneID = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        Persone_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.PhoneID)
                .ForeignKey("dbo.Persones", t => t.Persone_UserID)
                .Index(t => t.Persone_UserID);
            
            CreateTable(
                "dbo.EmploeyrPositions",
                c => new
                    {
                        PositionID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PositionID);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerID = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ManagerID);
            
            CreateTable(
                "dbo.ViolationImages",
                c => new
                    {
                        ViolationImageID = c.Int(nullable: false, identity: true),
                        Image = c.Binary(storeType: "image"),
                        Violation_ViolationID = c.Int(),
                    })
                .PrimaryKey(t => t.ViolationImageID)
                .ForeignKey("dbo.Violations", t => t.Violation_ViolationID)
                .Index(t => t.Violation_ViolationID);
            
            CreateTable(
                "dbo.Violations",
                c => new
                    {
                        ViolationID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        N = c.Double(nullable: false),
                        E = c.Double(nullable: false),
                        Description = c.String(),
                        ViolationType_ViolationTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ViolationID)
                .ForeignKey("dbo.ViolationTypes", t => t.ViolationType_ViolationTypeID)
                .Index(t => t.ViolationType_ViolationTypeID);
            
            CreateTable(
                "dbo.ViolationTypes",
                c => new
                    {
                        ViolationTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ViolationTypeID);
            
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
            DropForeignKey("dbo.Violations", "ViolationType_ViolationTypeID", "dbo.ViolationTypes");
            DropForeignKey("dbo.ViolationImages", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Employers", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Employers", "Persone_UserID", "dbo.Persones");
            DropForeignKey("dbo.Persones", "Position_PositionID", "dbo.EmploeyrPositions");
            DropForeignKey("dbo.Phones", "Persone_UserID", "dbo.Persones");
            DropForeignKey("dbo.Documents", "Persone_UserID", "dbo.Persones");
            DropForeignKey("dbo.PersoneAddresses", "Persone_UserID", "dbo.Persones");
            DropForeignKey("dbo.Documents", "DocumentType_DocumentTypeID", "dbo.DocumentTypes");
            DropIndex("dbo.Violators", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Violators", new[] { "Persone_UserID" });
            DropIndex("dbo.Violations", new[] { "ViolationType_ViolationTypeID" });
            DropIndex("dbo.ViolationImages", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Phones", new[] { "Persone_UserID" });
            DropIndex("dbo.Persones", new[] { "Position_PositionID" });
            DropIndex("dbo.Employers", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Employers", new[] { "Persone_UserID" });
            DropIndex("dbo.Documents", new[] { "Persone_UserID" });
            DropIndex("dbo.Documents", new[] { "DocumentType_DocumentTypeID" });
            DropIndex("dbo.PersoneAddresses", new[] { "Persone_UserID" });
            DropTable("dbo.Violators");
            DropTable("dbo.ViolationTypes");
            DropTable("dbo.Violations");
            DropTable("dbo.ViolationImages");
            DropTable("dbo.Managers");
            DropTable("dbo.EmploeyrPositions");
            DropTable("dbo.Phones");
            DropTable("dbo.Persones");
            DropTable("dbo.Employers");
            DropTable("dbo.DocumentTypes");
            DropTable("dbo.Documents");
            DropTable("dbo.PersoneAddresses");
        }
    }
}
