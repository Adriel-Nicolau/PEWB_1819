namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x222 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ServiceRequests", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceRequests", "Status", c => c.String());
        }
    }
}
