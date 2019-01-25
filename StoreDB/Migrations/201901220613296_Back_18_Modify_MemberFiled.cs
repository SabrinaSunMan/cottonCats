namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back_18_Modify_MemberFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Password", c => c.String(maxLength: 100));
            DropColumn("dbo.Members", "PasswordHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "PasswordHash", c => c.String());
            DropColumn("dbo.Members", "Password");
        }
    }
}
