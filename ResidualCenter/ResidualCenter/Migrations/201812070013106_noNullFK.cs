namespace ResidualCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noNullFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entities", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Entities", new[] { "UserId" });
            AlterColumn("dbo.Entities", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Entities", "UserId");
            AddForeignKey("dbo.Entities", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entities", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Entities", new[] { "UserId" });
            AlterColumn("dbo.Entities", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Entities", "UserId");
            AddForeignKey("dbo.Entities", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
