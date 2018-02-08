namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back04 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Account");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Account", c => c.String(nullable: false));
        }
    }
}
