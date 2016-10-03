namespace AppData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _31 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Employers");
            AddColumn("dbo.Employers", "PersoneID", c => c.Int(nullable: false));
            AlterColumn("dbo.Employers", "EmployerID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Employers", "EmployerID");
            DropColumn("dbo.Employers", "EmploerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employers", "EmploerID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Employers");
            AlterColumn("dbo.Employers", "EmployerID", c => c.Int(nullable: false));
            DropColumn("dbo.Employers", "PersoneID");
            AddPrimaryKey("dbo.Employers", "EmploerID");
        }
    }
}
