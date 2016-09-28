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
            
            CreateTable(
                "dbo.FileInfo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FileName = c.String(unicode: false),
                        FullPath = c.String(unicode: false),
                        FileSize = c.Long(nullable: false),
                        MD5 = c.String(maxLength: 32, storeType: "nvarchar"),
                        Extension = c.String(maxLength: 50, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 50, storeType: "nvarchar"),
                        RecordingDate = c.DateTime(precision: 0),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FileID = c.Guid(nullable: false),
                        AlbumID = c.Guid(nullable: false),
                        Tags = c.String(maxLength: 100, storeType: "nvarchar"),
                        PhotoType = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileInfo", t => t.FileID)
                .ForeignKey("dbo.Album", t => t.AlbumID)
                .Index(t => t.FileID)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "dbo.PhtotAttribute",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PhotoID = c.Guid(nullable: false),
                        AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                        AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photo", t => t.PhotoID)
                .Index(t => t.PhotoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "AlbumID", "dbo.Album");
            DropForeignKey("dbo.Photo", "FileID", "dbo.FileInfo");
            DropForeignKey("dbo.PhtotAttribute", "PhotoID", "dbo.Photo");
            DropForeignKey("dbo.UserProfile", "ContactProfileID", "dbo.ContactProfile");
            DropIndex("dbo.PhtotAttribute", new[] { "PhotoID" });
            DropIndex("dbo.Photo", new[] { "AlbumID" });
            DropIndex("dbo.Photo", new[] { "FileID" });
            DropIndex("dbo.UserProfile", new[] { "ContactProfileID" });
            DropTable("dbo.PhtotAttribute");
            DropTable("dbo.Photo");
            DropTable("dbo.Album");
            DropTable("dbo.FileInfo");
            DropTable("dbo.FamilyRole");
            DropTable("dbo.ContactProfile");
            DropTable("dbo.UserProfile");
        }
    }
}
