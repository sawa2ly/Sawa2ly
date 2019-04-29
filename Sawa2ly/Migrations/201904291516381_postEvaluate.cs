namespace Sawa2ly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postEvaluate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Date = c.DateTime(storeType: "date"),
                        ProjectId = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ProjectId)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.TraineeEvaluates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
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
            DropForeignKey("dbo.TraineeEvaluates", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TraineeEvaluates", "MTSID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TraineeEvaluates", new[] { "MTSID" });
            DropIndex("dbo.TraineeEvaluates", new[] { "ProjectId" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "ProjectId" });
            DropTable("dbo.TraineeEvaluates");
            DropTable("dbo.Posts");
        }
    }
}
