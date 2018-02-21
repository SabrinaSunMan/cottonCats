namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogError : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NLog_Error", "UserId", c => c.String());
            DropColumn("dbo.NLog_Error", "Orignal_Page");
            DropColumn("dbo.NLog_Error", "Statement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NLog_Error", "Statement", c => c.String());
            AddColumn("dbo.NLog_Error", "Orignal_Page", c => c.String());
            DropColumn("dbo.NLog_Error", "UserId");
        }
    }
}
