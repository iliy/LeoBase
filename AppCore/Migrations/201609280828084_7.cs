namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "DocumentType_DocumentTypeID1", "dbo.DocumentTypes");
            DropIndex("dbo.Documents", new[] { "DocumentType_DocumentTypeID1" });
            RenameColumn(table: "dbo.Documents", name: "DocumentType_DocumentTypeID1", newName: "DocumentType_Name");
            DropPrimaryKey("dbo.DocumentTypes");
            AlterColumn("dbo.Documents", "DocumentType_Name", c => c.String(maxLength: 128));
            AlterColumn("dbo.DocumentTypes", "DocumentTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.DocumentTypes", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DocumentTypes", "Name");
            CreateIndex("dbo.Documents", "DocumentType_Name");
            AddForeignKey("dbo.Documents", "DocumentType_Name", "dbo.DocumentTypes", "Name");
            DropColumn("dbo.Documents", "DocumentType_DocumentTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "DocumentType_DocumentTypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Documents", "DocumentType_Name", "dbo.DocumentTypes");
            DropIndex("dbo.Documents", new[] { "DocumentType_Name" });
            DropPrimaryKey("dbo.DocumentTypes");
            AlterColumn("dbo.DocumentTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.DocumentTypes", "DocumentTypeID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Documents", "DocumentType_Name", c => c.Int());
            AddPrimaryKey("dbo.DocumentTypes", "DocumentTypeID");
            RenameColumn(table: "dbo.Documents", name: "DocumentType_Name", newName: "DocumentType_DocumentTypeID1");
            CreateIndex("dbo.Documents", "DocumentType_DocumentTypeID1");
            AddForeignKey("dbo.Documents", "DocumentType_DocumentTypeID1", "dbo.DocumentTypes", "DocumentTypeID");
        }
    }
}
