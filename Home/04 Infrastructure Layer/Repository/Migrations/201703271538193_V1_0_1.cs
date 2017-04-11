namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class V1_0_1 : DbMigration
    {
        public override void Up()
        {
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
                "dbo.FamilyRelation",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    LeftRoleId = c.Guid(nullable: false),
                    RightRoleId = c.Guid(nullable: false),
                    Remark = c.String(maxLength: 100, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FamilyRole", t => t.LeftRoleId)
                .ForeignKey("dbo.FamilyRole", t => t.RightRoleId)
                .Index(t => t.LeftRoleId)
                .Index(t => t.RightRoleId);

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
                "dbo.UserProfile",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    ContactProfileID = c.Guid(nullable: false),
                    StaffNo = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
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
                "dbo.SystemParameter",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Description = c.String(maxLength: 255, storeType: "nvarchar"),
                    ParameterGroup = c.String(maxLength: 50, storeType: "nvarchar"),
                    IsReadOnly = c.Boolean(nullable: false),
                    ParameterKey = c.String(maxLength: 50, storeType: "nvarchar"),
                    ParameterValue = c.String(maxLength: 255, storeType: "nvarchar"),
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
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductItem", t => t.ProductID)
                .Index(t => t.ProductID);

            CreateTable(
                "dbo.ProductItem",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Name = c.String(maxLength: 50, storeType: "nvarchar"),
                    Modle = c.String(maxLength: 50, storeType: "nvarchar"),
                    Value_Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_CurrencyType = c.Int(nullable: false),
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
                "dbo.WordInfo",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    PagesCount = c.Int(nullable: false),
                    CharCount = c.Int(nullable: false),
                    ChineseCount = c.Int(nullable: false),
                    ImageCount = c.Int(nullable: false),
                    LinkCount = c.Int(nullable: false),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.WordAttribute",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    OwnerID = c.Guid(nullable: false),
                    AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                    AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.WordInfo", t => t.OwnerID)
                .Index(t => t.OwnerID);

            CreateTable(
                "dbo.WordObjectElement",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    ElementType = c.Int(nullable: false),
                    ObjectBuffer = c.Binary(),
                    ObjectContent = c.String(unicode: false),
                    OwnerID = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.WordInfo", t => t.OwnerID)
                .Index(t => t.OwnerID);

            CreateTable(
                "dbo.PDFInfo",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    PagesCount = c.Int(nullable: false),
                    CharCount = c.Int(nullable: false),
                    ChineseCount = c.Int(nullable: false),
                    ImageCount = c.Int(nullable: false),
                    LinkCount = c.Int(nullable: false),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.PDFAttribute",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    OwnerID = c.Guid(nullable: false),
                    AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                    AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PDFInfo", t => t.OwnerID)
                .Index(t => t.OwnerID);

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
                "dbo.Photo",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    FileID = c.Guid(nullable: false),
                    AlbumID = c.Guid(nullable: false),
                    Tags = c.String(maxLength: 100, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileInfo", t => t.ID)
                .ForeignKey("dbo.FileInfo", t => t.FileID)
                .ForeignKey("dbo.Album", t => t.AlbumID)
                .Index(t => t.ID)
                .Index(t => t.FileID)
                .Index(t => t.AlbumID);

            CreateTable(
                "dbo.PhotoAttribute",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    OwnerID = c.Guid(nullable: false),
                    AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                    AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photo", t => t.OwnerID)
                .Index(t => t.OwnerID);

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
                    SourceType = c.Int(nullable: false),
                    EngineID = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StorageEngine", t => t.EngineID)
                .Index(t => t.EngineID);

            CreateTable(
                "dbo.StorageEngine",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Root = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                    SettingID = c.Guid(nullable: false),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StorageEngineSetting", t => t.SettingID)
                .Index(t => t.SettingID);

            CreateTable(
                "dbo.StorageEngineSetting",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Host = c.String(maxLength: 200, storeType: "nvarchar"),
                    Uid = c.String(maxLength: 200, storeType: "nvarchar"),
                    Pwd = c.String(maxLength: 200, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.FileInfoExtend",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    Comments = c.String(unicode: false),
                    KeyWords = c.String(unicode: false),
                    FileID = c.Guid(nullable: false),
                    BarCodeBuffer = c.Binary(),
                    QRCodeBuffer = c.Binary(),
                    BarCode = c.String(unicode: false),
                    QRCode = c.String(unicode: false),
                    Sequence = c.String(unicode: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileInfo", t => t.ID)
                .Index(t => t.ID);

            CreateTable(
                "dbo.FileAttribute",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    OwnerID = c.Guid(nullable: false),
                    AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                    AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                    Modified = c.DateTime(nullable: false, precision: 0),
                    ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    StatusCode = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileInfoExtend", t => t.OwnerID)
                .Index(t => t.OwnerID);

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
                    BrokenDate = c.DateTime(precision: 0),
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
            InitSql();
        }

        public override void Down()
        {
            DropForeignKey("dbo.AssetsItem", "ProductID", "dbo.ProductItem");
            DropForeignKey("dbo.AssetsItem", "OrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrder", "OrderUserID", "dbo.UserProfile");
            DropForeignKey("dbo.PurchaseLineItem", "ProductID", "dbo.ProductItem");
            DropForeignKey("dbo.PurchaseLineItem", "OrderID", "dbo.PurchaseOrder");
            DropForeignKey("dbo.AssetsItem", "ContactID", "dbo.ContactProfile");
            DropForeignKey("dbo.PhotoSimilar", "RightPhotoID", "dbo.Photo");
            DropForeignKey("dbo.PhotoSimilar", "LeftPhotoID", "dbo.Photo");
            DropForeignKey("dbo.PhotoFingerprint", "PhotoID", "dbo.Photo");
            DropForeignKey("dbo.Photo", "AlbumID", "dbo.Album");
            DropForeignKey("dbo.Photo", "FileID", "dbo.FileInfo");
            DropForeignKey("dbo.Photo", "ID", "dbo.FileInfo");
            DropForeignKey("dbo.FileInfoExtend", "ID", "dbo.FileInfo");
            DropForeignKey("dbo.FileAttribute", "OwnerID", "dbo.FileInfoExtend");
            DropForeignKey("dbo.FileInfo", "EngineID", "dbo.StorageEngine");
            DropForeignKey("dbo.StorageEngine", "SettingID", "dbo.StorageEngineSetting");
            DropForeignKey("dbo.PhotoAttribute", "OwnerID", "dbo.Photo");
            DropForeignKey("dbo.PDFAttribute", "OwnerID", "dbo.PDFInfo");
            DropForeignKey("dbo.WordObjectElement", "OwnerID", "dbo.WordInfo");
            DropForeignKey("dbo.WordAttribute", "OwnerID", "dbo.WordInfo");
            DropForeignKey("dbo.ProductAttachment", "ProductID", "dbo.ProductItem");
            DropForeignKey("dbo.UserProfile", "ContactProfileID", "dbo.ContactProfile");
            DropForeignKey("dbo.FamilyRelation", "RightRoleId", "dbo.FamilyRole");
            DropForeignKey("dbo.FamilyRelation", "LeftRoleId", "dbo.FamilyRole");
            DropIndex("dbo.PurchaseLineItem", new[] { "OrderID" });
            DropIndex("dbo.PurchaseLineItem", new[] { "ProductID" });
            DropIndex("dbo.PurchaseOrder", new[] { "OrderUserID" });
            DropIndex("dbo.AssetsItem", new[] { "ProductID" });
            DropIndex("dbo.AssetsItem", new[] { "OrderID" });
            DropIndex("dbo.AssetsItem", new[] { "ContactID" });
            DropIndex("dbo.PhotoSimilar", new[] { "LeftPhotoID" });
            DropIndex("dbo.PhotoSimilar", new[] { "RightPhotoID" });
            DropIndex("dbo.PhotoFingerprint", new[] { "PhotoID" });
            DropIndex("dbo.FileAttribute", new[] { "OwnerID" });
            DropIndex("dbo.FileInfoExtend", new[] { "ID" });
            DropIndex("dbo.StorageEngine", new[] { "SettingID" });
            DropIndex("dbo.FileInfo", new[] { "EngineID" });
            DropIndex("dbo.PhotoAttribute", new[] { "OwnerID" });
            DropIndex("dbo.Photo", new[] { "AlbumID" });
            DropIndex("dbo.Photo", new[] { "FileID" });
            DropIndex("dbo.Photo", new[] { "ID" });
            DropIndex("dbo.PDFAttribute", new[] { "OwnerID" });
            DropIndex("dbo.WordObjectElement", new[] { "OwnerID" });
            DropIndex("dbo.WordAttribute", new[] { "OwnerID" });
            DropIndex("dbo.ProductAttachment", new[] { "ProductID" });
            DropIndex("dbo.UserProfile", new[] { "ContactProfileID" });
            DropIndex("dbo.FamilyRelation", new[] { "RightRoleId" });
            DropIndex("dbo.FamilyRelation", new[] { "LeftRoleId" });
            DropTable("dbo.PurchaseLineItem");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.AssetsItem");
            DropTable("dbo.PhotoSimilar");
            DropTable("dbo.PhotoFingerprint");
            DropTable("dbo.FileAttribute");
            DropTable("dbo.FileInfoExtend");
            DropTable("dbo.StorageEngineSetting");
            DropTable("dbo.StorageEngine");
            DropTable("dbo.FileInfo");
            DropTable("dbo.PhotoAttribute");
            DropTable("dbo.Photo");
            DropTable("dbo.Album");
            DropTable("dbo.PDFAttribute");
            DropTable("dbo.PDFInfo");
            DropTable("dbo.WordObjectElement");
            DropTable("dbo.WordAttribute");
            DropTable("dbo.WordInfo");
            DropTable("dbo.ProductItem");
            DropTable("dbo.ProductAttachment");
            DropTable("dbo.SystemParameter");
            DropTable("dbo.UserProfile");
            DropTable("dbo.FamilyRole");
            DropTable("dbo.FamilyRelation");
            DropTable("dbo.ContactProfile");
        }
    }
}
