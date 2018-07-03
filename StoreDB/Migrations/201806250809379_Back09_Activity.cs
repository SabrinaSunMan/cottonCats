namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back09_Activity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityID = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TitleName = c.String(maxLength: 20),
                        HtmlContext = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateUser = c.String(),
                        Status = c.Boolean(nullable: false),
                        sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Activities");
        }
    }
}
