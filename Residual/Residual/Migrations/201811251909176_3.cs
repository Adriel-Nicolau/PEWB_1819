namespace Residual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            AddColumn("dbo.Equipments", "EquipmentTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "EquipmentTypeID");
            AddForeignKey("dbo.Equipments", "EquipmentTypeID", "dbo.EquipmentTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentServices", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.EquipmentServices", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "EquipmentTypeID", "dbo.EquipmentTypes");
            DropIndex("dbo.EquipmentServices", new[] { "Service_ID" });
            DropIndex("dbo.EquipmentServices", new[] { "Equipment_ID" });
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeID" });
            DropColumn("dbo.Equipments", "EquipmentTypeID");
            DropTable("dbo.EquipmentServices");
            DropTable("dbo.EquipmentTypes");
        }
    }
}
