namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entities", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Entities", "UserId");
            AddForeignKey("dbo.Entities", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entities", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Entities", new[] { "UserId" });
            DropColumn("dbo.Entities", "UserId");
        }
    }
}
