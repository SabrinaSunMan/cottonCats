namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MenuTrees", "ControllerName", c => c.String(maxLength: 20));
            AlterColumn("dbo.MenuTrees", "ActionName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuTrees", "ActionName", c => c.String(maxLength: 12));
            AlterColumn("dbo.MenuTrees", "ControllerName", c => c.String(maxLength: 12));
        }
    }
}
