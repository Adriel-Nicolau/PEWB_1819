namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x222d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "Location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "Location");
        }
    }
}
