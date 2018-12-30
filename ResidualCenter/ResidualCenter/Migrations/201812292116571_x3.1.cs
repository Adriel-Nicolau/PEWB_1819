namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x31 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceRequests", "Adress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceRequests", "Adress", c => c.String());
        }
    }
}
