namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20181218整理TableGuid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Activities");
            DropPrimaryKey("dbo.Groups");
            AddColumn("dbo.MenuSideLists", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MenuSideLists", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MenuSideLists", "CreateUser", c => c.String());
            AddColumn("dbo.MenuSideLists", "UpdateUser", c => c.String());
            AddColumn("dbo.MenuSideLists", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.MenuSideLists", "sort", c => c.Int(nullable: false));
            AlterColumn("dbo.Activities", "ActivityID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Groups", "GroupID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Activities", "ActivityID");
            AddPrimaryKey("dbo.Groups", "GroupID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Groups");
            DropPrimaryKey("dbo.Activities");
            AlterColumn("dbo.Groups", "GroupID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Activities", "ActivityID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.MenuSideLists", "sort");
            DropColumn("dbo.MenuSideLists", "Status");
            DropColumn("dbo.MenuSideLists", "UpdateUser");
            DropColumn("dbo.MenuSideLists", "CreateUser");
            DropColumn("dbo.MenuSideLists", "UpdateTime");
            DropColumn("dbo.MenuSideLists", "CreateTime");
            AddPrimaryKey("dbo.Groups", "GroupID");
            AddPrimaryKey("dbo.Activities", "ActivityID");
        }
    }
}
