namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back07_StaticHtml_PictureInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PictureInfoes", "PicGroupID", c => c.Guid(nullable: false));
            AddColumn("dbo.StaticHtmls", "PicGroupID", c => c.Guid(nullable: false));
            DropColumn("dbo.StaticHtmls", "PicID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaticHtmls", "PicID", c => c.Guid(nullable: false));
            DropColumn("dbo.StaticHtmls", "PicGroupID");
            DropColumn("dbo.PictureInfoes", "PicGroupID");
        }
    }
}
