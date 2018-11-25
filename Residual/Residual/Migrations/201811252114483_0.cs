namespace Residual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntityTypeID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Contact = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Adress = c.String(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EntityTypes", t => t.EntityTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.EntityTypeID)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.EntityTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ServiceTypeID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeID, cascadeDelete: true)
                .Index(t => t.ServiceTypeID);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EquipmentTypeID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Using = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeID, cascadeDelete: true)
                .Index(t => t.EquipmentTypeID);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntityID = c.Int(nullable: false),
                        ServiceEntityRelationalID = c.Int(nullable: false),
                        Content = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.EntityID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceEntityRelationals", t => t.ServiceEntityRelationalID, cascadeDelete: true)
                .Index(t => t.EntityID)
                .Index(t => t.ServiceEntityRelationalID);
            
            CreateTable(
                "dbo.RoleActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.EquipmentServices",
                c => new
                    {
                        Equipment_ID = c.Int(nullable: false),
                        Service_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Equipment_ID, t.Service_ID })
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_ID, cascadeDelete: true)
                .Index(t => t.Equipment_ID)
                .Index(t => t.Service_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleActions", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Reviews", "ServiceEntityRelationalID", "dbo.ServiceEntityRelationals");
            DropForeignKey("dbo.Reviews", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.Services", "ServiceTypeID", "dbo.ServiceTypes");
            DropForeignKey("dbo.ServiceEntityRelationals", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.EquipmentServices", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.EquipmentServices", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "EquipmentTypeID", "dbo.EquipmentTypes");
            DropForeignKey("dbo.ServiceEntityRelationals", "Entity_ID", "dbo.Entities");
            DropForeignKey("dbo.Entities", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Entities", "EntityTypeID", "dbo.EntityTypes");
            DropForeignKey("dbo.EntityTypes", "RoleID", "dbo.Roles");
            DropIndex("dbo.EquipmentServices", new[] { "Service_ID" });
            DropIndex("dbo.EquipmentServices", new[] { "Equipment_ID" });
            DropIndex("dbo.RoleActions", new[] { "RoleID" });
            DropIndex("dbo.Reviews", new[] { "ServiceEntityRelationalID" });
            DropIndex("dbo.Reviews", new[] { "EntityID" });
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeID" });
            DropIndex("dbo.Services", new[] { "ServiceTypeID" });
            DropIndex("dbo.ServiceEntityRelationals", new[] { "Entity_ID" });
            DropIndex("dbo.ServiceEntityRelationals", new[] { "ServiceID" });
            DropIndex("dbo.EntityTypes", new[] { "RoleID" });
            DropIndex("dbo.Entities", new[] { "LocationID" });
            DropIndex("dbo.Entities", new[] { "EntityTypeID" });
            DropTable("dbo.EquipmentServices");
            DropTable("dbo.RoleActions");
            DropTable("dbo.Reviews");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceEntityRelationals");
            DropTable("dbo.Locations");
            DropTable("dbo.Roles");
            DropTable("dbo.EntityTypes");
            DropTable("dbo.Entities");
        }
    }
}
