namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Persones", "Position_PositionID", "dbo.EmploeyrPositions");
            DropForeignKey("dbo.Documents", "DocumentType_DocumentTypeID", "dbo.DocumentTypes");
            DropIndex("dbo.Persones", new[] { "Position_PositionID" });
            DropIndex("dbo.Documents", new[] { "DocumentType_DocumentTypeID" });
            AddColumn("dbo.Persones", "Position_PositionID1", c => c.Int());
            AddColumn("dbo.Documents", "DocumentType_DocumentTypeID1", c => c.Int());
            AlterColumn("dbo.Persones", "Position_PositionID", c => c.Int(nullable: false));
            AlterColumn("dbo.Documents", "DocumentType_DocumentTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Persones", "Position_PositionID1");
            CreateIndex("dbo.Documents", "DocumentType_DocumentTypeID1");
            AddForeignKey("dbo.Persones", "Position_PositionID1", "dbo.EmploeyrPositions", "PositionID");
            AddForeignKey("dbo.Documents", "DocumentType_DocumentTypeID1", "dbo.DocumentTypes", "DocumentTypeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "DocumentType_DocumentTypeID1", "dbo.DocumentTypes");
            DropForeignKey("dbo.Persones", "Position_PositionID1", "dbo.EmploeyrPositions");
            DropIndex("dbo.Documents", new[] { "DocumentType_DocumentTypeID1" });
            DropIndex("dbo.Persones", new[] { "Position_PositionID1" });
            AlterColumn("dbo.Documents", "DocumentType_DocumentTypeID", c => c.Int());
            AlterColumn("dbo.Persones", "Position_PositionID", c => c.Int());
            DropColumn("dbo.Documents", "DocumentType_DocumentTypeID1");
            DropColumn("dbo.Persones", "Position_PositionID1");
            CreateIndex("dbo.Documents", "DocumentType_DocumentTypeID");
            CreateIndex("dbo.Persones", "Position_PositionID");
            AddForeignKey("dbo.Documents", "DocumentType_DocumentTypeID", "dbo.DocumentTypes", "DocumentTypeID");
            AddForeignKey("dbo.Persones", "Position_PositionID", "dbo.EmploeyrPositions", "PositionID");
        }
    }
}
