namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1231324142214 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Entities", new[] { "ServiceRequest_ID" });
            RenameColumn(table: "dbo.ServiceRequests", name: "ServiceRequest_ID", newName: "Entity_ID");
            AddColumn("dbo.ServiceRequests", "EntityID", c => c.String(nullable: false));
            CreateIndex("dbo.ServiceRequests", "Entity_ID");
            DropColumn("dbo.Entities", "ServiceRequest_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Entities", "ServiceRequest_ID", c => c.Int());
            DropIndex("dbo.ServiceRequests", new[] { "Entity_ID" });
            DropColumn("dbo.ServiceRequests", "EntityID");
            RenameColumn(table: "dbo.ServiceRequests", name: "Entity_ID", newName: "ServiceRequest_ID");
            CreateIndex("dbo.Entities", "ServiceRequest_ID");
        }
    }
}
