namespace Residual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "EntityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "EntityID");
            CreateIndex("dbo.Reviews", "ServiceID");
            AddForeignKey("dbo.Reviews", "EntityID", "dbo.Entities", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "ServiceID", "dbo.Services", "ID", cascadeDelete: true);
            DropColumn("dbo.Services", "InProgress");
            DropColumn("dbo.Reviews", "EntityReviwerID");
            DropColumn("dbo.Reviews", "EntityReviwedID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "EntityReviwedID", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "EntityReviwerID", c => c.Int(nullable: false));
            AddColumn("dbo.Services", "InProgress", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Reviews", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Reviews", "EntityID", "dbo.Entities");
            DropIndex("dbo.Reviews", new[] { "ServiceID" });
            DropIndex("dbo.Reviews", new[] { "EntityID" });
            DropColumn("dbo.Reviews", "EntityID");
        }
    }
}
