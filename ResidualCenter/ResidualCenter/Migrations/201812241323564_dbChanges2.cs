namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChanges2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "ServiceType_ID", "dbo.ServiceTypes");
            DropIndex("dbo.Equipments", new[] { "ServiceType_ID" });
            RenameColumn(table: "dbo.Equipments", name: "ServiceType_ID", newName: "ServiceTypeID");
            AlterColumn("dbo.Equipments", "ServiceTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "ServiceTypeID");
            AddForeignKey("dbo.Equipments", "ServiceTypeID", "dbo.ServiceTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "ServiceTypeID", "dbo.ServiceTypes");
            DropIndex("dbo.Equipments", new[] { "ServiceTypeID" });
            AlterColumn("dbo.Equipments", "ServiceTypeID", c => c.Int());
            RenameColumn(table: "dbo.Equipments", name: "ServiceTypeID", newName: "ServiceType_ID");
            CreateIndex("dbo.Equipments", "ServiceType_ID");
            AddForeignKey("dbo.Equipments", "ServiceType_ID", "dbo.ServiceTypes", "ID");
        }
    }
}
