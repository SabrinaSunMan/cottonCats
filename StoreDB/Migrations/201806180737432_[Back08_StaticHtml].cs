namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back08_StaticHtml : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.StaticHtmls");
            AlterColumn("dbo.StaticHtmls", "StaticID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.StaticHtmls", "StaticID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.StaticHtmls");
            AlterColumn("dbo.StaticHtmls", "StaticID", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.StaticHtmls", "StaticID");
        }
    }
}
