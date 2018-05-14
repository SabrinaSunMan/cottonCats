namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back06_StaticHtml_PictureInfo : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HtmlSubjects");
            AddColumn("dbo.PictureInfoes", "FileExtension", c => c.String());
            AlterColumn("dbo.HtmlSubjects", "SubjectID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StaticHtmls", "SubjectID", c => c.String());
            AddPrimaryKey("dbo.HtmlSubjects", "SubjectID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.HtmlSubjects");
            AlterColumn("dbo.StaticHtmls", "SubjectID", c => c.Guid(nullable: false));
            AlterColumn("dbo.HtmlSubjects", "SubjectID", c => c.Guid(nullable: false));
            DropColumn("dbo.PictureInfoes", "FileExtension");
            AddPrimaryKey("dbo.HtmlSubjects", "SubjectID");
        }
    }
}
