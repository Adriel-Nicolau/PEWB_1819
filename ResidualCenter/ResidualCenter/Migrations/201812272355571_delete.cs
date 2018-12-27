namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "ServiceTypeID", "dbo.ServiceTypes");
            DropForeignKey("dbo.ServiceRequests", "ServiceID", "dbo.Services");
            DropIndex("dbo.ServiceRequests", new[] { "ServiceID" });
            DropIndex("dbo.Services", new[] { "ServiceTypeID" });
            AddColumn("dbo.ServiceRequests", "ServiceTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.ServiceTypes", "Description", c => c.String());
            CreateIndex("dbo.ServiceRequests", "ServiceTypeID");
            AddForeignKey("dbo.ServiceRequests", "ServiceTypeID", "dbo.ServiceTypes", "ID", cascadeDelete: true);
            DropColumn("dbo.ServiceRequests", "ServiceID");
            DropTable("dbo.Services");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ServiceTypeID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ServiceRequests", "ServiceID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ServiceRequests", "ServiceTypeID", "dbo.ServiceTypes");
            DropIndex("dbo.ServiceRequests", new[] { "ServiceTypeID" });
            DropColumn("dbo.ServiceTypes", "Description");
            DropColumn("dbo.ServiceRequests", "ServiceTypeID");
            CreateIndex("dbo.Services", "ServiceTypeID");
            CreateIndex("dbo.ServiceRequests", "ServiceID");
            AddForeignKey("dbo.ServiceRequests", "ServiceID", "dbo.Services", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Services", "ServiceTypeID", "dbo.ServiceTypes", "ID", cascadeDelete: true);
        }
    }
}
