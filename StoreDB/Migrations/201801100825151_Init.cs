namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        { 
            CreateTable(
                "dbo.MenuTrees",
                c => new
                    {
                        MenuID = c.String(nullable: false, maxLength: 128),
                        MenuName = c.String(),
                        MenuOrder = c.Int(nullable: false),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        TRootID = c.String(),
                    })
                .PrimaryKey(t => t.MenuID);
            
            CreateTable(
                "dbo.MenuTreeRoots",
                c => new
                    {
                        TRootID = c.String(nullable: false, maxLength: 128),
                        TRootName = c.String(),
                        TRootOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TRootID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MenuTreeRoots");
            DropTable("dbo.MenuTrees");
        }
    }
}
