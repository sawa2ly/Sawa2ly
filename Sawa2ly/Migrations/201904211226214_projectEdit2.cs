namespace Sawa2ly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectEdit2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "StartDate", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Projects", "EndDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "EndDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Projects", "StartDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
