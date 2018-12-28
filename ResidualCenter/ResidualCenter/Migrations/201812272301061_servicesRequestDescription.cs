namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servicesRequestDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "Description");
        }
    }
}
