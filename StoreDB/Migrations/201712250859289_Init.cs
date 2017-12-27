namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        { 
            
            DropTable("dbo.TestAdd01");
        }
        
        public override void Down()
        {
            DropTable("dbo.TestAdd01");
        }
    }
}
