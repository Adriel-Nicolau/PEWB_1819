namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class equipmentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipmentServices", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentServices", "Service_ID", "dbo.Services");
            DropIndex("dbo.EquipmentServices", new[] { "Equipment_ID" });
            DropIndex("dbo.EquipmentServices", new[] { "Service_ID" });
            AddColumn("dbo.Equipments", "Service_ID", c => c.Int());
            CreateIndex("dbo.Equipments", "Service_ID");
            AddForeignKey("dbo.Equipments", "Service_ID", "dbo.Services", "ID");
            DropTable("dbo.EquipmentServices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EquipmentServices",
                c => new
                    {
                        Equipment_ID = c.Int(nullable: false),
                        Service_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Equipment_ID, t.Service_ID });
            
            DropForeignKey("dbo.Equipments", "Service_ID", "dbo.Services");
            DropIndex("dbo.Equipments", new[] { "Service_ID" });
            DropColumn("dbo.Equipments", "Service_ID");
            CreateIndex("dbo.EquipmentServices", "Service_ID");
            CreateIndex("dbo.EquipmentServices", "Equipment_ID");
            AddForeignKey("dbo.EquipmentServices", "Service_ID", "dbo.Services", "ID", cascadeDelete: true);
            AddForeignKey("dbo.EquipmentServices", "Equipment_ID", "dbo.Equipments", "ID", cascadeDelete: true);
        }
    }
}
