namespace Residual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "ServiceID", "dbo.Services");
            DropIndex("dbo.Reviews", new[] { "ServiceID" });
            AddColumn("dbo.Actions", "RoleID", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "ServiceEntityRelationalID", c => c.Int(nullable: false));
            CreateIndex("dbo.Actions", "RoleID");
            CreateIndex("dbo.Reviews", "ServiceEntityRelationalID");
            AddForeignKey("dbo.Actions", "RoleID", "dbo.Roles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "ServiceEntityRelationalID", "dbo.ServiceEntityRelationals", "ID", cascadeDelete: true);
            DropColumn("dbo.Actions", "IDrole");
            DropColumn("dbo.Reviews", "ServiceID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "ServiceID", c => c.Int(nullable: false));
            AddColumn("dbo.Actions", "IDrole", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reviews", "ServiceEntityRelationalID", "dbo.ServiceEntityRelationals");
            DropForeignKey("dbo.Actions", "RoleID", "dbo.Roles");
            DropIndex("dbo.Reviews", new[] { "ServiceEntityRelationalID" });
            DropIndex("dbo.Actions", new[] { "RoleID" });
            DropColumn("dbo.Reviews", "ServiceEntityRelationalID");
            DropColumn("dbo.Actions", "RoleID");
            CreateIndex("dbo.Reviews", "ServiceID");
            AddForeignKey("dbo.Reviews", "ServiceID", "dbo.Services", "ID", cascadeDelete: true);
        }
    }
}
