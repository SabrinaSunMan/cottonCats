namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back11_ZipCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        County = c.String(),
                        PostalCode = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        CitySort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ZipCodes");
        }
    }
}
