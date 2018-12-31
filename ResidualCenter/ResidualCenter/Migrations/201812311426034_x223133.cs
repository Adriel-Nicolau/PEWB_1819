namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x223133 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reviews", "Seen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "Seen", c => c.Boolean(nullable: false));
        }
    }
}
