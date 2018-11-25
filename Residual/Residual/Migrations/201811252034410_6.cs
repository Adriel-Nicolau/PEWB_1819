namespace Residual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceEntities", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.ServiceEntities", "Entity_ID", "dbo.Entities");
            DropIndex("dbo.ServiceEntities", new[] { "Service_ID" });
            DropIndex("dbo.ServiceEntities", new[] { "Entity_ID" });
            CreateTable(
                "dbo.ServiceEntityRelationals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntityID = c.String(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        Status = c.String(),
                        Entity_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.Entity_ID)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ServiceID)
                .Index(t => t.Entity_ID);
            
            DropTable("dbo.ServiceEntities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceEntities",
                c => new
                    {
                        Service_ID = c.Int(nullable: false),
                        Entity_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ID, t.Entity_ID });
            
            DropForeignKey("dbo.ServiceEntityRelationals", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.ServiceEntityRelationals", "Entity_ID", "dbo.Entities");
            DropIndex("dbo.ServiceEntityRelationals", new[] { "Entity_ID" });
            DropIndex("dbo.ServiceEntityRelationals", new[] { "ServiceID" });
            DropTable("dbo.ServiceEntityRelationals");
            CreateIndex("dbo.ServiceEntities", "Entity_ID");
            CreateIndex("dbo.ServiceEntities", "Service_ID");
            AddForeignKey("dbo.ServiceEntities", "Entity_ID", "dbo.Entities", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ServiceEntities", "Service_ID", "dbo.Services", "ID", cascadeDelete: true);
        }
    }
}
