namespace StoreDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        AddressContent = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        studentID = c.Int(nullable: false, identity: true),
                        studentName = c.String(),
                        addressInfo_AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.studentID)
                .ForeignKey("dbo.Addresses", t => t.addressInfo_AddressID)
                .Index(t => t.addressInfo_AddressID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Account = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(maxLength: 10),
                        AspNetUsers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id)
                .Index(t => t.AspNetUsers_Id);
             
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 10),
                        Age = c.Int(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MemberID);
            
            CreateTable(
                "dbo.MenuSideLists",
                c => new
                    {
                        MenuSideListID = c.String(nullable: false, maxLength: 128),
                        MenuID = c.Guid(nullable: false),
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MenuSideListID);
            
            CreateTable(
                "dbo.MenuTrees",
                c => new
                    {
                        MenuID = c.Guid(nullable: false),
                        MenuName = c.String(maxLength: 10),
                        MenuOrder = c.Int(nullable: false),
                        ControllerName = c.String(maxLength: 12),
                        ActionName = c.String(maxLength: 12),
                        TRootID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MenuID);
            
            CreateTable(
                "dbo.MenuTreeRoots",
                c => new
                    {
                        TRootID = c.Guid(nullable: false),
                        TRootName = c.String(maxLength: 10),
                        TRootOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TRootID);
            
            CreateTable(
                "dbo.NLog_Error",
                c => new
                    {
                        LogId = c.String(nullable: false, maxLength: 128),
                        CreateDateTime = c.DateTime(nullable: false),
                        Host = c.String(),
                        Result = c.String(),
                        SaveData = c.String(),
                        LogLevel = c.String(),
                        Data_Action = c.String(),
                        Orignal_Page = c.String(),
                        Statement = c.String(),
                        ControllersName = c.String(),
                        ActionName = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Groups", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Students", "addressInfo_AddressID", "dbo.Addresses");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Groups", new[] { "AspNetUsers_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Students", new[] { "addressInfo_AddressID" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.NLog_Error");
            DropTable("dbo.MenuTreeRoots");
            DropTable("dbo.MenuTrees");
            DropTable("dbo.MenuSideLists");
            DropTable("dbo.Members"); 
            DropTable("dbo.Groups");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Students");
            DropTable("dbo.Addresses");
        }
    }
}
