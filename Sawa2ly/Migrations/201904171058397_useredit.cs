namespace Sawa2ly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useredit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserImageUrl");
        }
    }
}
