namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
			
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContactProfileID = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactProfile", t => t.ContactProfileID)
                .Index(t => t.ContactProfileID);
            
            CreateTable(
                "dbo.ContactProfile",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(unicode: false),
                        Six = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactRelation",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Line = c.Int(nullable: false),
                        LeftRoleId = c.Guid(nullable: false),
                        RightRoleId = c.Guid(nullable: false),
                        Range = c.Int(nullable: false),
                        Remark = c.String(maxLength: 100, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactRole", t => t.LeftRoleId)
                .ForeignKey("dbo.ContactRole", t => t.RightRoleId)
                .Index(t => t.LeftRoleId)
                .Index(t => t.RightRoleId);
            
            CreateTable(
                "dbo.ContactRole",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
						Six = c.Int(nullable: false),
						Level = c.Int(nullable: false),
                        Remark = c.String(maxLength: 20, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactRelationRight",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Left = c.Guid(nullable: false),
                        Right = c.Guid(nullable: false),
                        RelationID = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactRelation", t => t.RelationID)
                .Index(t => t.RelationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactRelationRight", "RelationID", "dbo.ContactRelation");
            DropForeignKey("dbo.ContactRelation", "RightRoleId", "dbo.ContactRole");
            DropForeignKey("dbo.ContactRelation", "LeftRoleId", "dbo.ContactRole");
            DropForeignKey("dbo.UserProfile", "ContactProfileID", "dbo.ContactProfile");
            DropIndex("dbo.ContactRelationRight", new[] { "RelationID" });
            DropIndex("dbo.ContactRelation", new[] { "RightRoleId" });
            DropIndex("dbo.ContactRelation", new[] { "LeftRoleId" });
            DropIndex("dbo.UserProfile", new[] { "ContactProfileID" });
            DropTable("dbo.ContactRelationRight");
            DropTable("dbo.ContactRole");
            DropTable("dbo.ContactRelation");
            DropTable("dbo.ContactProfile");
            DropTable("dbo.UserProfile");
        }
    }
}
