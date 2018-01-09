namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.NLog_Error");
            AddColumn("dbo.NLog_Error", "LogId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.NLog_Error", "CreateDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.NLog_Error", "Host", c => c.String());
            AddColumn("dbo.NLog_Error", "Result", c => c.String());
            AddColumn("dbo.NLog_Error", "SaveData", c => c.String());
            AddColumn("dbo.NLog_Error", "LogLevel", c => c.String());
            AddColumn("dbo.NLog_Error", "Data_Action", c => c.String());
            AddColumn("dbo.NLog_Error", "Orignal_Page", c => c.String());
            AddColumn("dbo.NLog_Error", "Statement", c => c.String());
            AddColumn("dbo.NLog_Error", "ControllersName", c => c.String());
            AddColumn("dbo.NLog_Error", "ActionName", c => c.String());
            AddPrimaryKey("dbo.NLog_Error", "LogId");
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.NLog_Error", "ActionName");
            DropColumn("dbo.NLog_Error", "ControllersName");
            DropColumn("dbo.NLog_Error", "Statement");
            DropColumn("dbo.NLog_Error", "Orignal_Page");
            DropColumn("dbo.NLog_Error", "Data_Action");
            DropColumn("dbo.NLog_Error", "LogLevel");
            DropColumn("dbo.NLog_Error", "SaveData");
            DropColumn("dbo.NLog_Error", "Result");
            DropColumn("dbo.NLog_Error", "Host");
            DropColumn("dbo.NLog_Error", "CreateDateTime");
            DropColumn("dbo.NLog_Error", "LogId");
            AddPrimaryKey("dbo.NLog_Error", "NLogId");
        }
    }
}
