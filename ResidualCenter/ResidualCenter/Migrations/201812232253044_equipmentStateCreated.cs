namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class equipmentStateCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentStates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Equipments", "EquipmentStateID", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "EquipmentStateID");
            AddForeignKey("dbo.Equipments", "EquipmentStateID", "dbo.EquipmentStates", "ID", cascadeDelete: true);
            DropColumn("dbo.Equipments", "Using");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "Using", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Equipments", "EquipmentStateID", "dbo.EquipmentStates");
            DropIndex("dbo.Equipments", new[] { "EquipmentStateID" });
            DropColumn("dbo.Equipments", "EquipmentStateID");
            DropTable("dbo.EquipmentStates");
        }
    }
}
