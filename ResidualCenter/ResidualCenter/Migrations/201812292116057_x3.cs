namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "Adress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "Adress");
        }
    }
}
