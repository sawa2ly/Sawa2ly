namespace Sawa2ly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectEdit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "ProjectModuleId", "dbo.ProjectModules");
            DropIndex("dbo.Projects", new[] { "ProjectModuleId" });
            AddColumn("dbo.Projects", "MTLID", c => c.String(maxLength: 128));
            AddColumn("dbo.Projects", "StartDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Projects", "EndDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Projects", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Price", c => c.Double(nullable: false));
            CreateIndex("dbo.Projects", "MTLID");
            AddForeignKey("dbo.Projects", "MTLID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Projects", "ProjectModuleId");
            DropTable("dbo.ProjectModules");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        Status = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Projects", "ProjectModuleId", c => c.Int());
            DropForeignKey("dbo.Projects", "MTLID", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "MTLID" });
            DropColumn("dbo.Projects", "Price");
            DropColumn("dbo.Projects", "Status");
            DropColumn("dbo.Projects", "EndDate");
            DropColumn("dbo.Projects", "StartDate");
            DropColumn("dbo.Projects", "MTLID");
            CreateIndex("dbo.Projects", "ProjectModuleId");
            AddForeignKey("dbo.Projects", "ProjectModuleId", "dbo.ProjectModules", "Id");
        }
    }
}
