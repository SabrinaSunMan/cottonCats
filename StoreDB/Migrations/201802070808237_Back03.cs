namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Back03 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Members");
            DropPrimaryKey("dbo.MenuSideLists");
            DropPrimaryKey("dbo.MenuTrees");
            DropPrimaryKey("dbo.MenuTreeRoots");
            AlterColumn("dbo.Members", "MemberID", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newID()"));
            AlterColumn("dbo.MenuSideLists", "MenuSideListID", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newID()"));
            AlterColumn("dbo.MenuTrees", "MenuID", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newID()"));
            AlterColumn("dbo.MenuTreeRoots", "TRootID", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newID()"));
            AddPrimaryKey("dbo.Members", "MemberID");
            AddPrimaryKey("dbo.MenuSideLists", "MenuSideListID");
            AddPrimaryKey("dbo.MenuTrees", "MenuID");
            AddPrimaryKey("dbo.MenuTreeRoots", "TRootID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MenuTreeRoots");
            DropPrimaryKey("dbo.MenuTrees");
            DropPrimaryKey("dbo.MenuSideLists");
            DropPrimaryKey("dbo.Members");
            AlterColumn("dbo.MenuTreeRoots", "TRootID", c => c.Guid(nullable: false));
            AlterColumn("dbo.MenuTrees", "MenuID", c => c.Guid(nullable: false));
            AlterColumn("dbo.MenuSideLists", "MenuSideListID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Members", "MemberID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.MenuTreeRoots", "TRootID");
            AddPrimaryKey("dbo.MenuTrees", "MenuID");
            AddPrimaryKey("dbo.MenuSideLists", "MenuSideListID");
            AddPrimaryKey("dbo.Members", "MemberID");
        }
    }
}
