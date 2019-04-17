namespace Sawa2ly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ProjectImageUrl = c.String(),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        MDID = c.String(maxLength: 128),
                        ProjectModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.MDID)
                .ForeignKey("dbo.ProjectModules", t => t.ProjectModuleId)
                .Index(t => t.CustomerId)
                .Index(t => t.MDID)
                .Index(t => t.ProjectModuleId);
            
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
            
            CreateTable(
                "dbo.ProjectRequestsMDs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        MDID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MDID)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.MDID);
            
            CreateTable(
                "dbo.ProjectsJoinRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        SenderId = c.String(maxLength: 128),
                        RecieverId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RecieverId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.ProjectId)
                .Index(t => t.SenderId)
                .Index(t => t.RecieverId);
            
            CreateTable(
                "dbo.ProjectTrainees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        MTSID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MTSID)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.MTSID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTrainees", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectTrainees", "MTSID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectsJoinRequests", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectsJoinRequests", "RecieverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectsJoinRequests", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectRequestsMDs", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectRequestsMDs", "MDID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "ProjectModuleId", "dbo.ProjectModules");
            DropForeignKey("dbo.Projects", "MDID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectTrainees", new[] { "MTSID" });
            DropIndex("dbo.ProjectTrainees", new[] { "ProjectId" });
            DropIndex("dbo.ProjectsJoinRequests", new[] { "RecieverId" });
            DropIndex("dbo.ProjectsJoinRequests", new[] { "SenderId" });
            DropIndex("dbo.ProjectsJoinRequests", new[] { "ProjectId" });
            DropIndex("dbo.ProjectRequestsMDs", new[] { "MDID" });
            DropIndex("dbo.ProjectRequestsMDs", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ProjectModuleId" });
            DropIndex("dbo.Projects", new[] { "MDID" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropTable("dbo.ProjectTrainees");
            DropTable("dbo.ProjectsJoinRequests");
            DropTable("dbo.ProjectRequestsMDs");
            DropTable("dbo.ProjectModules");
            DropTable("dbo.Projects");
        }
    }
}
