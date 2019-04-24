namespace Sawa2ly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectrequestmd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectRequestsMDs", "CustomerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProjectRequestsMDs", "CustomerId");
            AddForeignKey("dbo.ProjectRequestsMDs", "CustomerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectRequestsMDs", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectRequestsMDs", new[] { "CustomerId" });
            DropColumn("dbo.ProjectRequestsMDs", "CustomerId");
        }
    }
}
