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
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Six = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FamilyRole",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 20, storeType: "nvarchar"),
                        Level = c.Int(nullable: false),
                        Six = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfile", "ContactProfileID", "dbo.ContactProfile");
            DropIndex("dbo.UserProfile", new[] { "ContactProfileID" });
            DropTable("dbo.FamilyRole");
            DropTable("dbo.ContactProfile");
            DropTable("dbo.UserProfile");
        }
    }
}
