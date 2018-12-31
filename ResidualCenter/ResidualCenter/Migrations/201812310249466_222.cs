namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _222 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceRequests", "Quantity", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceRequests", "Quantity", c => c.String());
        }
    }
}
