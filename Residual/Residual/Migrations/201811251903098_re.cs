namespace Residual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class re : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "Employee_ID", "dbo.Entities");
            DropForeignKey("dbo.Entities", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.Services", "Entity_ID", "dbo.Entities");
            DropIndex("dbo.Entities", new[] { "Service_ID" });
            DropIndex("dbo.Services", new[] { "Employee_ID" });
            DropIndex("dbo.Services", new[] { "Entity_ID" });
            CreateTable(
                "dbo.ServiceEntities",
                c => new
                    {
                        Service_ID = c.Int(nullable: false),
                        Entity_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ID, t.Entity_ID })
                .ForeignKey("dbo.Services", t => t.Service_ID, cascadeDelete: true)
                .ForeignKey("dbo.Entities", t => t.Entity_ID, cascadeDelete: true)
                .Index(t => t.Service_ID)
                .Index(t => t.Entity_ID);
            
            DropColumn("dbo.Entities", "Service_ID");
            DropColumn("dbo.Services", "Employee_ID");
            DropColumn("dbo.Services", "Entity_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "Entity_ID", c => c.Int());
            AddColumn("dbo.Services", "Employee_ID", c => c.Int());
            AddColumn("dbo.Entities", "Service_ID", c => c.Int());
            DropForeignKey("dbo.ServiceEntities", "Entity_ID", "dbo.Entities");
            DropForeignKey("dbo.ServiceEntities", "Service_ID", "dbo.Services");
            DropIndex("dbo.ServiceEntities", new[] { "Entity_ID" });
            DropIndex("dbo.ServiceEntities", new[] { "Service_ID" });
            DropTable("dbo.ServiceEntities");
            CreateIndex("dbo.Services", "Entity_ID");
            CreateIndex("dbo.Services", "Employee_ID");
            CreateIndex("dbo.Entities", "Service_ID");
            AddForeignKey("dbo.Services", "Entity_ID", "dbo.Entities", "ID");
            AddForeignKey("dbo.Entities", "Service_ID", "dbo.Services", "ID");
            AddForeignKey("dbo.Services", "Employee_ID", "dbo.Entities", "ID");
        }
    }
}
