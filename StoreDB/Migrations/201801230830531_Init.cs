namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.MemberID);
            
            CreateTable(
                "dbo.MenuSideLists",
                c => new
                    {
                        MenuSideListID = c.String(nullable: false, maxLength: 128),
                        MenuID = c.String(),
                        Id = c.String(),
                    })
                .PrimaryKey(t => t.MenuSideListID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MenuSideLists");
            DropTable("dbo.Members");
            DropTable("dbo.Groups");
        }
    }
}
