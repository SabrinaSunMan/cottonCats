namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back15_ModifyMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "PasswordHash", c => c.String());
            AlterColumn("dbo.Members", "Birthday", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Birthday", c => c.DateTime(nullable: false));
            DropColumn("dbo.Members", "PasswordHash");
        }
    }
}
