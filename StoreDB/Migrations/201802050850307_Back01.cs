namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuTreeRoots", "UrlIcon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuTreeRoots", "UrlIcon"); 
        }
    }
}
