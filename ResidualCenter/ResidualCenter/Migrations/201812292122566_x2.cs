namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceRequestStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ServiceRequests", "ServiceRequestStatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceRequests", "ServiceRequestStatusID");
            AddForeignKey("dbo.ServiceRequests", "ServiceRequestStatusID", "dbo.ServiceRequestStatus", "ID", cascadeDelete: true);
            DropColumn("dbo.ServiceTypes", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceTypes", "Description", c => c.String());
            DropForeignKey("dbo.ServiceRequests", "ServiceRequestStatusID", "dbo.ServiceRequestStatus");
            DropIndex("dbo.ServiceRequests", new[] { "ServiceRequestStatusID" });
            DropColumn("dbo.ServiceRequests", "ServiceRequestStatusID");
            DropTable("dbo.ServiceRequestStatus");
        }
    }
}
