namespace Sawa2ly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectEdit3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Status", c => c.Int());
            AlterColumn("dbo.Projects", "Price", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Projects", "Status", c => c.Int(nullable: false));
        }
    }
}
