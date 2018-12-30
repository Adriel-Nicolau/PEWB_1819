namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entities", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.Entities", new[] { "ServiceRequest_ID" });
            CreateTable(
                "dbo.ServiceRequestEntities",
                c => new
                    {
                        ServiceRequest_ID = c.Int(nullable: false),
                        Entity_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceRequest_ID, t.Entity_ID })
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequest_ID, cascadeDelete: true)
                .ForeignKey("dbo.Entities", t => t.Entity_ID, cascadeDelete: true)
                .Index(t => t.ServiceRequest_ID)
                .Index(t => t.Entity_ID);
            
            DropColumn("dbo.Entities", "ServiceRequest_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Entities", "ServiceRequest_ID", c => c.Int());
            DropForeignKey("dbo.ServiceRequestEntities", "Entity_ID", "dbo.Entities");
            DropForeignKey("dbo.ServiceRequestEntities", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.ServiceRequestEntities", new[] { "Entity_ID" });
            DropIndex("dbo.ServiceRequestEntities", new[] { "ServiceRequest_ID" });
            DropTable("dbo.ServiceRequestEntities");
            CreateIndex("dbo.Entities", "ServiceRequest_ID");
            AddForeignKey("dbo.Entities", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
    }
}
