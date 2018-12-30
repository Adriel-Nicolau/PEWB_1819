namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x : DbMigration
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
                        UserId = c.String(nullable: false, maxLength: 128),
                        LocationID = c.Int(nullable: false),
                        ServiceRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequest_ID)
                .Index(t => t.UserId)
                .Index(t => t.LocationID)
                .Index(t => t.ServiceRequest_ID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EquipmentTypeID = c.Int(nullable: false),
                        EquipmentStateID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        ServiceTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EquipmentStates", t => t.EquipmentStateID, cascadeDelete: true)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeID, cascadeDelete: true)
                .Index(t => t.EquipmentTypeID)
                .Index(t => t.EquipmentStateID)
                .Index(t => t.ServiceTypeID);
            
            CreateTable(
                "dbo.EquipmentStates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ResidueTypes",
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
                        ServiceRequestID = c.Int(nullable: false),
                        Content = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Entities", t => t.EntityID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequestID, cascadeDelete: true)
                .Index(t => t.EntityID)
                .Index(t => t.ServiceRequestID);
            
            CreateTable(
                "dbo.ServiceRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ServiceTypeID = c.Int(nullable: false),
                        Status = c.String(),
                        Quantity = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        ResidueTypeID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ResidueTypes", t => t.ResidueTypeID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeID, cascadeDelete: true)
                .Index(t => t.ServiceTypeID)
                .Index(t => t.ResidueTypeID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "ServiceRequestID", "dbo.ServiceRequests");
            DropForeignKey("dbo.ServiceRequests", "ServiceTypeID", "dbo.ServiceTypes");
            DropForeignKey("dbo.ServiceRequests", "ResidueTypeID", "dbo.ResidueTypes");
            DropForeignKey("dbo.Entities", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropForeignKey("dbo.Reviews", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.Equipments", "ServiceTypeID", "dbo.ServiceTypes");
            DropForeignKey("dbo.Equipments", "EquipmentTypeID", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "EquipmentStateID", "dbo.EquipmentStates");
            DropForeignKey("dbo.Entities", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Entities", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ServiceRequests", new[] { "ResidueTypeID" });
            DropIndex("dbo.ServiceRequests", new[] { "ServiceTypeID" });
            DropIndex("dbo.Reviews", new[] { "ServiceRequestID" });
            DropIndex("dbo.Reviews", new[] { "EntityID" });
            DropIndex("dbo.Equipments", new[] { "ServiceTypeID" });
            DropIndex("dbo.Equipments", new[] { "EquipmentStateID" });
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Entities", new[] { "ServiceRequest_ID" });
            DropIndex("dbo.Entities", new[] { "LocationID" });
            DropIndex("dbo.Entities", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ServiceRequests");
            DropTable("dbo.Reviews");
            DropTable("dbo.ResidueTypes");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.EquipmentStates");
            DropTable("dbo.Equipments");
            DropTable("dbo.Locations");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Entities");
        }
    }
}
