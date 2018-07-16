namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back14_AddAspNetUserFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreateUser", c => c.String());
            AddColumn("dbo.AspNetUsers", "UpdateUser", c => c.String());
            AddColumn("dbo.AspNetUsers", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "sort");
            DropColumn("dbo.AspNetUsers", "Status");
            DropColumn("dbo.AspNetUsers", "UpdateUser");
            DropColumn("dbo.AspNetUsers", "CreateUser");
        }
    }
}
