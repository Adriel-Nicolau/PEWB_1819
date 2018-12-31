namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2221q : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidueTypes", "Unit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResidueTypes", "Unit");
        }
    }
}
