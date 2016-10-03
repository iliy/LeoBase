namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "DocumentType_Name", "dbo.DocumentTypes");
            DropForeignKey("dbo.Persones", "Position_PositionID1", "dbo.EmploeyrPositions");
            DropIndex("dbo.Persones", new[] { "Position_PositionID1" });
            DropIndex("dbo.Documents", new[] { "DocumentType_Name" });
            DropPrimaryKey("dbo.DocumentTypes");
            AddColumn("dbo.Documents", "Document_DocumentTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.DocumentTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.DocumentTypes", "DocumentTypeID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DocumentTypes", "DocumentTypeID");
            DropColumn("dbo.Persones", "Position_PositionID1");
            DropColumn("dbo.Documents", "DocumentType_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "DocumentType_Name", c => c.String(maxLength: 128));
            AddColumn("dbo.Persones", "Position_PositionID1", c => c.Int());
            DropPrimaryKey("dbo.DocumentTypes");
            AlterColumn("dbo.DocumentTypes", "DocumentTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.DocumentTypes", "Name", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Documents", "Document_DocumentTypeID");
            AddPrimaryKey("dbo.DocumentTypes", "Name");
            CreateIndex("dbo.Documents", "DocumentType_Name");
            CreateIndex("dbo.Persones", "Position_PositionID1");
            AddForeignKey("dbo.Persones", "Position_PositionID1", "dbo.EmploeyrPositions", "PositionID");
            AddForeignKey("dbo.Documents", "DocumentType_Name", "dbo.DocumentTypes", "Name");
        }
    }
}
