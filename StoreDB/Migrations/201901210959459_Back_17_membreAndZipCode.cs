namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back_17_membreAndZipCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ZipCodeID", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "City");
            DropColumn("dbo.Members", "County");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "County", c => c.String(maxLength: 4));
            AddColumn("dbo.Members", "City", c => c.String(maxLength: 4));
            DropColumn("dbo.Members", "ZipCodeID");
        }
    }
}
