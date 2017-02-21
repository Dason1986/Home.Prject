namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemParameter",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Description = c.String(maxLength: 255, storeType: "nvarchar"),
                    Group = c.String(maxLength: 50, storeType: "nvarchar"),
                    IsReadOnly = c.Boolean(nullable: false),
                    Key = c.String(maxLength: 50, storeType: "nvarchar"),
                    Value = c.String(maxLength: 255, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.UserProfile",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    ContactProfileID = c.Guid(nullable: false),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
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
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
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
                    Remark = c.String(maxLength: 100, storeType: "nvarchar"),
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
                    FileName = c.String(maxLength: 100, storeType: "nvarchar"),
                    FullPath = c.String(maxLength: 255, storeType: "nvarchar"),
                    FileSize = c.Long(nullable: false),
                    MD5 = c.String(maxLength: 32, storeType: "nvarchar"),
                    Extension = c.String(maxLength: 50, storeType: "nvarchar"),
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
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileInfo", t => t.FileID)
                .ForeignKey("dbo.Album", t => t.AlbumID)
                .ForeignKey("dbo.FileInfo", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.FileID)
                .Index(t => t.AlbumID);

            CreateTable(
                "dbo.PhotoAttribute",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    PhotoID = c.Guid(nullable: false),
                    AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                    AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                    BitValue = c.Binary(),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photo", t => t.PhotoID)
                .Index(t => t.PhotoID);

            CreateTable(
                "dbo.Album",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Name = c.String(maxLength: 50, storeType: "nvarchar"),
                    Remark = c.String(maxLength: 50, storeType: "nvarchar"),
                    RecordingDate = c.DateTime(precision: 0),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.PhotoFingerprint",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    PhotoID = c.Guid(nullable: false),
                    Fingerprint = c.Binary(),
                    Algorithm = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photo", t => t.PhotoID)
                .Index(t => t.PhotoID);

            CreateTable(
                "dbo.PhotoSimilar",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    RightPhotoID = c.Guid(nullable: false),
                    LeftPhotoID = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photo", t => t.LeftPhotoID)
                .ForeignKey("dbo.Photo", t => t.RightPhotoID)
                .Index(t => t.RightPhotoID)
                .Index(t => t.LeftPhotoID);

            CreateTable(
                "dbo.AssetsItem",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    SnCode = c.String(unicode: false),
                    Name = c.String(unicode: false),
                    IsPublic = c.Boolean(nullable: false),
                    ContactID = c.Guid(nullable: false),
                    OrderID = c.Guid(nullable: false),
                    ProductID = c.Guid(nullable: false),
                    IsBroken = c.Boolean(nullable: false),
                    BrokenDate = c.DateTime(nullable: false, precision: 0),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactProfile", t => t.ContactID)
                .ForeignKey("dbo.PurchaseOrder", t => t.OrderID)
                .ForeignKey("dbo.ProductItem", t => t.ProductID)
                .Index(t => t.ContactID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);

            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    PayType = c.Int(nullable: false),
                    PayAmout_Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PayAmout_CurrencyType = c.Int(nullable: false),
                    OrderUserID = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserProfile", t => t.OrderUserID)
                .Index(t => t.OrderUserID);

            CreateTable(
                "dbo.PurchaseLineItem",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    ProductID = c.Guid(nullable: false),
                    Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Price_Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Price_CurrencyType = c.Int(nullable: false),
                    OrderID = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PurchaseOrder", t => t.OrderID)
                .ForeignKey("dbo.ProductItem", t => t.ProductID)
                .Index(t => t.ProductID)
                .Index(t => t.OrderID);

            CreateTable(
                "dbo.ProductItem",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Name = c.String(maxLength: 50, storeType: "nvarchar"),
                    Modle = c.String(maxLength: 50, storeType: "nvarchar"),
                    Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    BarCode = c.String(maxLength: 50, storeType: "nvarchar"),
                    Tag = c.String(maxLength: 50, storeType: "nvarchar"),
                    Company = c.String(maxLength: 50, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ProductAttachment",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    ProductID = c.Guid(nullable: false),
                    FileID = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileInfo", t => t.FileID)
                .ForeignKey("dbo.ProductItem", t => t.ProductID)
                .Index(t => t.ProductID)
                .Index(t => t.FileID);

            InitSql();
        }

        public override void Down()
        {
            DropForeignKey("dbo.AssetsItem", "ProductID", "dbo.ProductItem");
            DropForeignKey("dbo.AssetsItem", "OrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrder", "OrderUserID", "dbo.UserProfile");
            DropForeignKey("dbo.PurchaseLineItem", "ProductID", "dbo.ProductItem");
            DropForeignKey("dbo.ProductAttachment", "ProductID", "dbo.ProductItem");
            DropForeignKey("dbo.ProductAttachment", "FileID", "dbo.FileInfo");
            DropForeignKey("dbo.PurchaseLineItem", "OrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.AssetsItem", "ContactID", "dbo.ContactProfile");
            DropForeignKey("dbo.PhotoSimilar", "RightPhotoID", "dbo.Photo");
            DropForeignKey("dbo.PhotoSimilar", "LeftPhotoID", "dbo.Photo");
            DropForeignKey("dbo.PhotoFingerprint", "PhotoID", "dbo.Photo");
            DropForeignKey("dbo.Photo", "ID", "dbo.FileInfo");
            DropForeignKey("dbo.Photo", "AlbumID", "dbo.Album");
            DropForeignKey("dbo.Photo", "FileID", "dbo.FileInfo");
            DropForeignKey("dbo.PhotoAttribute", "PhotoID", "dbo.Photo");
            DropForeignKey("dbo.UserProfile", "ContactProfileID", "dbo.ContactProfile");
            DropIndex("dbo.ProductAttachment", new[] { "FileID" });
            DropIndex("dbo.ProductAttachment", new[] { "ProductID" });
            DropIndex("dbo.PurchaseLineItem", new[] { "OrderID" });
            DropIndex("dbo.PurchaseLineItem", new[] { "ProductID" });
            DropIndex("dbo.PurchaseOrder", new[] { "OrderUserID" });
            DropIndex("dbo.AssetsItem", new[] { "ProductID" });
            DropIndex("dbo.AssetsItem", new[] { "OrderID" });
            DropIndex("dbo.AssetsItem", new[] { "ContactID" });
            DropIndex("dbo.PhotoSimilar", new[] { "LeftPhotoID" });
            DropIndex("dbo.PhotoSimilar", new[] { "RightPhotoID" });
            DropIndex("dbo.PhotoFingerprint", new[] { "PhotoID" });
            DropIndex("dbo.PhotoAttribute", new[] { "PhotoID" });
            DropIndex("dbo.Photo", new[] { "AlbumID" });
            DropIndex("dbo.Photo", new[] { "FileID" });
            DropIndex("dbo.Photo", new[] { "ID" });
            DropIndex("dbo.UserProfile", new[] { "ContactProfileID" });
            DropTable("dbo.ProductAttachment");
            DropTable("dbo.ProductItem");
            DropTable("dbo.PurchaseLineItem");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.AssetsItem");
            DropTable("dbo.PhotoSimilar");
            DropTable("dbo.PhotoFingerprint");
            DropTable("dbo.Album");
            DropTable("dbo.PhotoAttribute");
            DropTable("dbo.Photo");
            DropTable("dbo.FileInfo");
            DropTable("dbo.FamilyRole");
            DropTable("dbo.ContactRelation");
            DropTable("dbo.ContactProfile");
            DropTable("dbo.UserProfile");
            DropTable("dbo.SystemParameter");
        }
    }
}