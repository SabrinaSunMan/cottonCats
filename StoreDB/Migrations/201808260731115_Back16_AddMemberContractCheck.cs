namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back16_AddMemberContractCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ContractCheck", c => c.Boolean(nullable: false));
            AddColumn("dbo.Members", "EmailCheck", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "EmailCheck");
            DropColumn("dbo.Members", "ContractCheck");
        }
    }
}
