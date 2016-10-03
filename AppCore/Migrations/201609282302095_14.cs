namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InjunctionItems",
                c => new
                    {
                        InjunctionItemID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Deedline = c.String(),
                        BaseOrders = c.String(),
                        Injunction_InjunctionID = c.Int(),
                    })
                .PrimaryKey(t => t.InjunctionItemID)
                .ForeignKey("dbo.Injunctions", t => t.Injunction_InjunctionID)
                .Index(t => t.Injunction_InjunctionID);
            
            CreateTable(
                "dbo.Injunctions",
                c => new
                    {
                        InjunctionID = c.Int(nullable: false, identity: true),
                        ActInspectionDate = c.DateTime(nullable: false),
                        ActInspectionNumber = c.String(),
                        InjunctionInfo = c.String(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.InjunctionID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
            CreateTable(
                "dbo.Protocols",
                c => new
                    {
                        ProtocolID = c.Int(nullable: false, identity: true),
                        ProtocolTypeID = c.Int(nullable: false),
                        DateCreatedProtocol = c.DateTime(nullable: false),
                        PlaceCreatedProtocol = c.String(),
                        PlaceCreatedProtocolN = c.Double(nullable: false),
                        PlaceCreatedProtocolE = c.Double(nullable: false),
                        WitnessFIO = c.String(),
                        WitnessLive = c.String(),
                        Employer_UserID = c.Int(),
                        Violation_ViolationID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Employer_UserID)
                .ForeignKey("dbo.Violations", t => t.Violation_ViolationID)
                .Index(t => t.Employer_UserID)
                .Index(t => t.Violation_ViolationID);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        OrganisationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Format = c.String(),
                        Info = c.String(),
                        Representative_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.OrganisationID)
                .ForeignKey("dbo.Persones", t => t.Representative_UserID)
                .Index(t => t.Representative_UserID);
            
            CreateTable(
                "dbo.ProtocolAboutArrests",
                c => new
                    {
                        ProtocolAboutArrestID = c.Int(nullable: false, identity: true),
                        AboutViolator = c.String(),
                        AboutCar = c.String(),
                        AboutOtherThings = c.String(),
                        ThingsWasTransfer = c.String(),
                        FixingMethods = c.String(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutArrestID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
            CreateTable(
                "dbo.ProtocolAboutBringings",
                c => new
                    {
                        ProtocolAboutBringingID = c.Int(nullable: false, identity: true),
                        FindedWeapons = c.String(),
                        FindedAmmunitions = c.String(),
                        FindedGunsHuntingAndFishing = c.String(),
                        FindedNatureManagementProducts = c.String(),
                        FindedDocuments = c.String(),
                        WithdrawWeapons = c.String(),
                        WithdrawAmmunitions = c.String(),
                        WithdrawGunsHuntingAndFishing = c.String(),
                        WithdrawNatureManagementProducts = c.String(),
                        WithdrawDocuments = c.String(),
                        FixingMethods = c.String(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutBringingID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
            CreateTable(
                "dbo.ProtocolAboutInspections",
                c => new
                    {
                        ProtocolAboutInspectionID = c.Int(nullable: false, identity: true),
                        FindedWeapons = c.String(),
                        FindedAmmunitions = c.String(),
                        FindedGunsHuntingAndFishing = c.String(),
                        FindedNatureManagementProducts = c.String(),
                        FindedDocuments = c.String(),
                        FixingMethods = c.String(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutInspectionID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
            CreateTable(
                "dbo.ProtocolAboutInspectionAutoes",
                c => new
                    {
                        ProtocolAboutInspectionAutoID = c.Int(nullable: false, identity: true),
                        InformationAbouCar = c.String(),
                        FindedWeapons = c.String(),
                        FindedAmmunitions = c.String(),
                        FindedGunsHuntingAndFishing = c.String(),
                        FindedNatureManagementProducts = c.String(),
                        FindedDocuments = c.String(),
                        FixingMethods = c.String(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutInspectionAutoID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
            CreateTable(
                "dbo.ProtocolAboutInspectionOrganisations",
                c => new
                    {
                        ProtocolAboutInspectionOrganisationID = c.Int(nullable: false, identity: true),
                        InspectionTerritoryes = c.String(),
                        FindedWeapons = c.String(),
                        FindedAmmunitions = c.String(),
                        FindedGunsHuntingAndFishing = c.String(),
                        FindedNatureManagementProducts = c.String(),
                        FindedDocuments = c.String(),
                        FixingMethods = c.String(),
                        Organisation_OrganisationID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutInspectionOrganisationID)
                .ForeignKey("dbo.Organisations", t => t.Organisation_OrganisationID)
                .Index(t => t.Organisation_OrganisationID);
            
            CreateTable(
                "dbo.ProtocolAboutViolationOrganisations",
                c => new
                    {
                        ProtocolAboutViolationOrganisationID = c.Int(nullable: false, identity: true),
                        ViolationTime = c.DateTime(nullable: false),
                        KOAP = c.String(),
                        Organisation_OrganisationID = c.Int(),
                        Protocol_ProtocolID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutViolationOrganisationID)
                .ForeignKey("dbo.Organisations", t => t.Organisation_OrganisationID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .Index(t => t.Organisation_OrganisationID)
                .Index(t => t.Protocol_ProtocolID);
            
            CreateTable(
                "dbo.ProtocolAboutViolationPersones",
                c => new
                    {
                        ProtocolAboutViolationPersoneID = c.Int(nullable: false, identity: true),
                        ViolationDate = c.DateTime(nullable: false),
                        ViolationDescription = c.String(),
                        KOAP = c.String(),
                        FindedNatureManagementProducts = c.String(),
                        FindedWeapons = c.String(),
                        FindedGunsHuntingAndFishing = c.String(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutViolationPersoneID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
            CreateTable(
                "dbo.ProtocolAboutWithdraws",
                c => new
                    {
                        ProtocolAboutWithdrawID = c.Int(nullable: false, identity: true),
                        WithdrawWeapons = c.String(),
                        WithdrawAmmunitions = c.String(),
                        WithdrawGunsHuntingAndFishing = c.String(),
                        WithdrawNatureManagementProducts = c.String(),
                        WithdrawDocuments = c.String(),
                        FixingMethods = c.String(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ProtocolAboutWithdrawID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
            CreateTable(
                "dbo.ProtocolTypes",
                c => new
                    {
                        ProtocolTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProtocolTypeID);
            
            CreateTable(
                "dbo.RulingForViolations",
                c => new
                    {
                        RulingForViolationID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        FixingDate = c.DateTime(nullable: false),
                        FixingInfo = c.String(),
                        KOAP = c.String(),
                        Fine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Damage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Products = c.String(),
                        ProductsPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AboutArrest = c.String(),
                        BankDetails = c.String(),
                        Organisation_OrganisationID = c.Int(),
                        Protocol_ProtocolID = c.Int(),
                        Violator_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.RulingForViolationID)
                .ForeignKey("dbo.Organisations", t => t.Organisation_OrganisationID)
                .ForeignKey("dbo.Protocols", t => t.Protocol_ProtocolID)
                .ForeignKey("dbo.Persones", t => t.Violator_UserID)
                .Index(t => t.Organisation_OrganisationID)
                .Index(t => t.Protocol_ProtocolID)
                .Index(t => t.Violator_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RulingForViolations", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.RulingForViolations", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.RulingForViolations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutWithdraws", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutWithdraws", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutViolationPersones", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutViolationPersones", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutViolationOrganisations", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutViolationOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutInspectionOrganisations", "Organisation_OrganisationID", "dbo.Organisations");
            DropForeignKey("dbo.ProtocolAboutInspectionAutoes", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutInspectionAutoes", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutInspections", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutInspections", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutBringings", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutBringings", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolAboutArrests", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.ProtocolAboutArrests", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.Organisations", "Representative_UserID", "dbo.Persones");
            DropForeignKey("dbo.Injunctions", "Violator_UserID", "dbo.Persones");
            DropForeignKey("dbo.Injunctions", "Protocol_ProtocolID", "dbo.Protocols");
            DropForeignKey("dbo.Protocols", "Violation_ViolationID", "dbo.Violations");
            DropForeignKey("dbo.Protocols", "Employer_UserID", "dbo.Persones");
            DropForeignKey("dbo.InjunctionItems", "Injunction_InjunctionID", "dbo.Injunctions");
            DropIndex("dbo.RulingForViolations", new[] { "Violator_UserID" });
            DropIndex("dbo.RulingForViolations", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.RulingForViolations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutWithdraws", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutWithdraws", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutViolationPersones", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutViolationPersones", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutViolationOrganisations", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutViolationOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutInspectionOrganisations", new[] { "Organisation_OrganisationID" });
            DropIndex("dbo.ProtocolAboutInspectionAutoes", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutInspectionAutoes", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutInspections", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutInspections", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutBringings", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutBringings", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.ProtocolAboutArrests", new[] { "Violator_UserID" });
            DropIndex("dbo.ProtocolAboutArrests", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.Organisations", new[] { "Representative_UserID" });
            DropIndex("dbo.Protocols", new[] { "Violation_ViolationID" });
            DropIndex("dbo.Protocols", new[] { "Employer_UserID" });
            DropIndex("dbo.Injunctions", new[] { "Violator_UserID" });
            DropIndex("dbo.Injunctions", new[] { "Protocol_ProtocolID" });
            DropIndex("dbo.InjunctionItems", new[] { "Injunction_InjunctionID" });
            DropTable("dbo.RulingForViolations");
            DropTable("dbo.ProtocolTypes");
            DropTable("dbo.ProtocolAboutWithdraws");
            DropTable("dbo.ProtocolAboutViolationPersones");
            DropTable("dbo.ProtocolAboutViolationOrganisations");
            DropTable("dbo.ProtocolAboutInspectionOrganisations");
            DropTable("dbo.ProtocolAboutInspectionAutoes");
            DropTable("dbo.ProtocolAboutInspections");
            DropTable("dbo.ProtocolAboutBringings");
            DropTable("dbo.ProtocolAboutArrests");
            DropTable("dbo.Organisations");
            DropTable("dbo.Protocols");
            DropTable("dbo.Injunctions");
            DropTable("dbo.InjunctionItems");
        }
    }
}
