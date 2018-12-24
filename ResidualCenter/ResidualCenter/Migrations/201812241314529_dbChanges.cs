namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceEntityRelationals", "Entity_ID", "dbo.Entities");
            DropForeignKey("dbo.Equipments", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.ServiceEntityRelationals", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Reviews", "ServiceEntityRelationalID", "dbo.ServiceEntityRelationals");
            DropIndex("dbo.ServiceEntityRelationals", new[] { "ServiceID" });
            DropIndex("dbo.ServiceEntityRelationals", new[] { "Entity_ID" });
            DropIndex("dbo.Equipments", new[] { "Service_ID" });
            DropIndex("dbo.Reviews", new[] { "ServiceEntityRelationalID" });
            CreateTable(
                "dbo.ServiceRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntityID = c.String(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        Status = c.String(),
                        Quantity = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        ResidueTypeID = c.Int(nullable: false),
                        Entity_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.Entity_ID)
                .ForeignKey("dbo.ResidueTypes", t => t.ResidueTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ServiceID)
                .Index(t => t.ResidueTypeID)
                .Index(t => t.Entity_ID);
            
            CreateTable(
                "dbo.ResidueTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Equipments", "ServiceType_ID", c => c.Int());
            AddColumn("dbo.Reviews", "ServiceRequestID", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "ServiceType_ID");
            CreateIndex("dbo.Reviews", "ServiceRequestID");
            AddForeignKey("dbo.Equipments", "ServiceType_ID", "dbo.ServiceTypes", "ID");
            AddForeignKey("dbo.Reviews", "ServiceRequestID", "dbo.ServiceRequests", "ID", cascadeDelete: true);
            DropColumn("dbo.Equipments", "Service_ID");
            DropColumn("dbo.Reviews", "ServiceEntityRelationalID");
            DropTable("dbo.ServiceEntityRelationals");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Reviews", "ServiceEntityRelationalID", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "Service_ID", c => c.Int());
            DropForeignKey("dbo.Reviews", "ServiceRequestID", "dbo.ServiceRequests");
            DropForeignKey("dbo.ServiceRequests", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Equipments", "ServiceType_ID", "dbo.ServiceTypes");
            DropForeignKey("dbo.ServiceRequests", "ResidueTypeID", "dbo.ResidueTypes");
            DropForeignKey("dbo.ServiceRequests", "Entity_ID", "dbo.Entities");
            DropIndex("dbo.Reviews", new[] { "ServiceRequestID" });
            DropIndex("dbo.Equipments", new[] { "ServiceType_ID" });
            DropIndex("dbo.ServiceRequests", new[] { "Entity_ID" });
            DropIndex("dbo.ServiceRequests", new[] { "ResidueTypeID" });
            DropIndex("dbo.ServiceRequests", new[] { "ServiceID" });
            DropColumn("dbo.Reviews", "ServiceRequestID");
            DropColumn("dbo.Equipments", "ServiceType_ID");
            DropTable("dbo.ResidueTypes");
            DropTable("dbo.ServiceRequests");
            CreateIndex("dbo.Reviews", "ServiceEntityRelationalID");
            CreateIndex("dbo.Equipments", "Service_ID");
            CreateIndex("dbo.ServiceEntityRelationals", "Entity_ID");
            CreateIndex("dbo.ServiceEntityRelationals", "ServiceID");
            AddForeignKey("dbo.Reviews", "ServiceEntityRelationalID", "dbo.ServiceEntityRelationals", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ServiceEntityRelationals", "ServiceID", "dbo.Services", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "Service_ID", "dbo.Services", "ID");
            AddForeignKey("dbo.ServiceEntityRelationals", "Entity_ID", "dbo.Entities", "ID");
        }
    }
}
