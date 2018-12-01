namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Contact = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Adress = c.String(nullable: false),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "ServiceEntityRelationalID", "dbo.ServiceEntityRelationals");
            DropForeignKey("dbo.Reviews", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.Services", "ServiceTypeID", "dbo.ServiceTypes");
            DropForeignKey("dbo.ServiceEntityRelationals", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.EquipmentServices", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.EquipmentServices", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "EquipmentTypeID", "dbo.EquipmentTypes");
            DropForeignKey("dbo.ServiceEntityRelationals", "Entity_ID", "dbo.Entities");
            DropForeignKey("dbo.Entities", "LocationID", "dbo.Locations");
            DropIndex("dbo.EquipmentServices", new[] { "Service_ID" });
            DropIndex("dbo.EquipmentServices", new[] { "Equipment_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "ServiceEntityRelationalID" });
            DropIndex("dbo.Reviews", new[] { "EntityID" });
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeID" });
            DropIndex("dbo.Services", new[] { "ServiceTypeID" });
            DropIndex("dbo.ServiceEntityRelationals", new[] { "Entity_ID" });
            DropIndex("dbo.ServiceEntityRelationals", new[] { "ServiceID" });
            DropIndex("dbo.Entities", new[] { "LocationID" });
            DropTable("dbo.EquipmentServices");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceEntityRelationals");
            DropTable("dbo.Locations");
            DropTable("dbo.Entities");
        }
    }
}
