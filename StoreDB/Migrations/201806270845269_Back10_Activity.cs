namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back10_Activity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "PicGroupID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "PicGroupID");
        }
    }
}
