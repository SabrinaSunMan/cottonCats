namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back11_MenuTreeAddStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuTrees", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuTrees", "Status");
        }
    }
}
