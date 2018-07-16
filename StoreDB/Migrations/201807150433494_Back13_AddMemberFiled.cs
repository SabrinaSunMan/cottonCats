namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back13_AddMemberFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Address", c => c.String());
            AddColumn("dbo.Members", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.Members", "City", c => c.String());
            AddColumn("dbo.Members", "County", c => c.String());
            AddColumn("dbo.Members", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Members", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Members", "CreateUser", c => c.String());
            AddColumn("dbo.Members", "UpdateUser", c => c.String());
            AddColumn("dbo.Members", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Members", "sort", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "sort");
            DropColumn("dbo.Members", "Status");
            DropColumn("dbo.Members", "UpdateUser");
            DropColumn("dbo.Members", "CreateUser");
            DropColumn("dbo.Members", "UpdateTime");
            DropColumn("dbo.Members", "CreateTime");
            DropColumn("dbo.Members", "County");
            DropColumn("dbo.Members", "City");
            DropColumn("dbo.Members", "Birthday");
            DropColumn("dbo.Members", "Address");
        }
    }
}
