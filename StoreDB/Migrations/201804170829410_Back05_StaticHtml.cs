namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back05_StaticHtml : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HtmlSubjects",
                c => new
                    {
                        SubjectID = c.Guid(nullable: false),
                        SubjectName = c.String(maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateUser = c.String(),
                        Status = c.Boolean(nullable: false),
                        sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.PictureInfoes",
                c => new
                    {
                        PicID = c.Guid(nullable: false),
                        PictureName = c.String(maxLength: 20),
                        PictureUrl = c.String(maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateUser = c.String(),
                        Status = c.Boolean(nullable: false),
                        sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PicID);
            
            CreateTable(
                "dbo.StaticHtmls",
                c => new
                    {
                        StaticID = c.Guid(nullable: false, identity: true),
                        SubjectID = c.Guid(nullable: false),
                        PicID = c.Guid(nullable: false),
                        HtmlContext = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateUser = c.String(),
                        Status = c.Boolean(nullable: false),
                        sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaticID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StaticHtmls");
            DropTable("dbo.PictureInfoes");
            DropTable("dbo.HtmlSubjects");
        }
    }
}
