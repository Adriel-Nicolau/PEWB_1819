namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x33 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "EquipmentStateID", "dbo.EquipmentStates");
            DropForeignKey("dbo.Equipments", "EquipmentTypeID", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "ServiceTypeID", "dbo.ServiceTypes");
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeID" });
            DropIndex("dbo.Equipments", new[] { "EquipmentStateID" });
            DropIndex("dbo.Equipments", new[] { "ServiceTypeID" });
            DropTable("dbo.Equipments");
            DropTable("dbo.EquipmentStates");
            DropTable("dbo.EquipmentTypes");
        }
        
        public override void Down()
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
                "dbo.EquipmentStates",
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Equipments", "ServiceTypeID");
            CreateIndex("dbo.Equipments", "EquipmentStateID");
            CreateIndex("dbo.Equipments", "EquipmentTypeID");
            AddForeignKey("dbo.Equipments", "ServiceTypeID", "dbo.ServiceTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "EquipmentTypeID", "dbo.EquipmentTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "EquipmentStateID", "dbo.EquipmentStates", "ID", cascadeDelete: true);
        }
    }
}
