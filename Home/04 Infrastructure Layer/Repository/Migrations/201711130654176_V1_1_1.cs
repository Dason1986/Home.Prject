namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_1_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MessageLogEntities",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        MessageEntityID = c.Guid(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Message_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("MessageEntity", t => t.Message_ID)
                .ForeignKey("MessageEntity", t => t.MessageEntityID)
                .Index(t => t.MessageEntityID)
                .Index(t => t.Message_ID);
            
            CreateTable(
                "MessageEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Content = c.String(nullable: false, unicode: false),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ContactProfile",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Six = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FamilyRelation",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        LeftRoleId = c.Guid(nullable: false),
                        RightRoleId = c.Guid(nullable: false),
                        Remark = c.String(maxLength: 100, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FamilyRole", t => t.LeftRoleId)
                .ForeignKey("FamilyRole", t => t.RightRoleId)
                .Index(t => t.LeftRoleId)
                .Index(t => t.RightRoleId);
            
            CreateTable(
                "FamilyRole",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 20, storeType: "nvarchar"),
                        Level = c.Int(nullable: false),
                        Six = c.Int(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "UserProfile",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContactProfileID = c.Guid(nullable: false),
                        StaffNo = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("ContactProfile", t => t.ContactProfileID)
                .Index(t => t.ContactProfileID);
            
            CreateTable(
                "SystemParameter",
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
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "DomainEventArgsLog",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IsExecuted = c.Boolean(nullable: false),
                        HasError = c.Boolean(nullable: false),
                        ErrorMsg = c.String(unicode: false),
                        DomainEventType = c.String(unicode: false),
                        DomainEvent = c.String(unicode: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ScheduleJob",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(unicode: false),
                        ScheduleJobClass = c.String(unicode: false),
                        ScheduleCronExpression = c.String(unicode: false),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "LogEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CallSite = c.String(unicode: false),
                        Date = c.String(unicode: false),
                        Exception = c.String(unicode: false),
                        Level = c.String(unicode: false),
                        Logger = c.String(unicode: false),
                        MachineName = c.String(unicode: false),
                        Message = c.String(unicode: false),
                        StackTrace = c.String(unicode: false),
                        Thread = c.String(unicode: false),
                        Username = c.String(unicode: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ScheduleJobLog",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ScheduleId = c.Guid(nullable: false),
                        ElapsedTime = c.Time(nullable: false, precision: 0),
                        HasError = c.Boolean(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("ScheduleJob", t => t.ScheduleId)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "ProductAttachment",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("ProductItem", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "ProductItem",
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
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "WordInfo",
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
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "WordAttribute",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                        AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("WordInfo", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "WordObjectElement",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ElementType = c.Int(nullable: false),
                        ObjectBuffer = c.Binary(),
                        ObjectContent = c.String(unicode: false),
                        OwnerID = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("WordInfo", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "PDFInfo",
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
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "PDFAttribute",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                        AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("PDFInfo", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "Album",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 50, storeType: "nvarchar"),
                        RecordingDate = c.DateTime(precision: 0),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Photo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FileID = c.Guid(nullable: false),
                        AlbumID = c.Guid(nullable: false),
                        Tags = c.String(maxLength: 100, storeType: "nvarchar"),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FileInfo", t => t.ID)
                .ForeignKey("FileInfo", t => t.FileID)
                .ForeignKey("Album", t => t.AlbumID)
                .Index(t => t.ID)
                .Index(t => t.FileID)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "PhotoAttribute",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                        AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Photo", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "FileInfo",
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
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("StorageEngine", t => t.EngineID)
                .Index(t => t.EngineID);
            
            CreateTable(
                "StorageEngine",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Root = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        SettingID = c.Guid(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("StorageEngineSetting", t => t.SettingID)
                .Index(t => t.SettingID);
            
            CreateTable(
                "StorageEngineSetting",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Host = c.String(maxLength: 200, storeType: "nvarchar"),
                        Uid = c.String(maxLength: 200, storeType: "nvarchar"),
                        Pwd = c.String(maxLength: 200, storeType: "nvarchar"),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FileInfoExtend",
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
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FileInfo", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "FileAttribute",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        AttKey = c.String(maxLength: 50, storeType: "nvarchar"),
                        AttValue = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FileInfoExtend", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "PhotoFingerprint",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PhotoID = c.Guid(nullable: false),
                        Fingerprint = c.Binary(),
                        Algorithm = c.Int(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Photo", t => t.PhotoID)
                .Index(t => t.PhotoID);
            
            CreateTable(
                "PhotoSimilar",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RightPhotoID = c.Guid(nullable: false),
                        LeftPhotoID = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Photo", t => t.LeftPhotoID)
                .ForeignKey("Photo", t => t.RightPhotoID)
                .Index(t => t.RightPhotoID)
                .Index(t => t.LeftPhotoID);
            
            CreateTable(
                "AssetsItem",
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
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("ContactProfile", t => t.ContactID)
                .ForeignKey("PurchaseOrder", t => t.OrderID)
                .ForeignKey("ProductItem", t => t.ProductID)
                .Index(t => t.ContactID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "PurchaseOrder",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PayType = c.Int(nullable: false),
                        PayAmout_Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayAmout_CurrencyType = c.Int(nullable: false),
                        OrderUserID = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("UserProfile", t => t.OrderUserID)
                .Index(t => t.OrderUserID);
            
            CreateTable(
                "PurchaseLineItem",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_CurrencyType = c.Int(nullable: false),
                        OrderID = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("PurchaseOrder", t => t.OrderID)
                .ForeignKey("ProductItem", t => t.ProductID)
                .Index(t => t.ProductID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "MailMessageLogEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        To = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Cc = c.String(maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("MessageLogEntities", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "PhoneMessageLogEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("MessageLogEntities", t => t.ID)
                .Index(t => t.ID);
            InitSql();
        }
        
        public override void Down()
        {
            DropForeignKey("PhoneMessageLogEntity", "ID", "MessageLogEntities");
            DropForeignKey("MailMessageLogEntity", "ID", "MessageLogEntities");
            DropForeignKey("AssetsItem", "ProductID", "ProductItem");
            DropForeignKey("AssetsItem", "OrderID", "PurchaseOrder");
            DropForeignKey("PurchaseOrder", "OrderUserID", "UserProfile");
            DropForeignKey("PurchaseLineItem", "ProductID", "ProductItem");
            DropForeignKey("PurchaseLineItem", "OrderID", "PurchaseOrder");
            DropForeignKey("AssetsItem", "ContactID", "ContactProfile");
            DropForeignKey("PhotoSimilar", "RightPhotoID", "Photo");
            DropForeignKey("PhotoSimilar", "LeftPhotoID", "Photo");
            DropForeignKey("PhotoFingerprint", "PhotoID", "Photo");
            DropForeignKey("Photo", "AlbumID", "Album");
            DropForeignKey("Photo", "FileID", "FileInfo");
            DropForeignKey("Photo", "ID", "FileInfo");
            DropForeignKey("FileInfoExtend", "ID", "FileInfo");
            DropForeignKey("FileAttribute", "OwnerID", "FileInfoExtend");
            DropForeignKey("FileInfo", "EngineID", "StorageEngine");
            DropForeignKey("StorageEngine", "SettingID", "StorageEngineSetting");
            DropForeignKey("PhotoAttribute", "OwnerID", "Photo");
            DropForeignKey("PDFAttribute", "OwnerID", "PDFInfo");
            DropForeignKey("WordObjectElement", "OwnerID", "WordInfo");
            DropForeignKey("WordAttribute", "OwnerID", "WordInfo");
            DropForeignKey("ProductAttachment", "ProductID", "ProductItem");
            DropForeignKey("ScheduleJobLog", "ScheduleId", "ScheduleJob");
            DropForeignKey("UserProfile", "ContactProfileID", "ContactProfile");
            DropForeignKey("FamilyRelation", "RightRoleId", "FamilyRole");
            DropForeignKey("FamilyRelation", "LeftRoleId", "FamilyRole");
            DropForeignKey("MessageLogEntities", "MessageEntityID", "MessageEntity");
            DropForeignKey("MessageLogEntities", "Message_ID", "MessageEntity");
            DropIndex("PhoneMessageLogEntity", new[] { "ID" });
            DropIndex("MailMessageLogEntity", new[] { "ID" });
            DropIndex("PurchaseLineItem", new[] { "OrderID" });
            DropIndex("PurchaseLineItem", new[] { "ProductID" });
            DropIndex("PurchaseOrder", new[] { "OrderUserID" });
            DropIndex("AssetsItem", new[] { "ProductID" });
            DropIndex("AssetsItem", new[] { "OrderID" });
            DropIndex("AssetsItem", new[] { "ContactID" });
            DropIndex("PhotoSimilar", new[] { "LeftPhotoID" });
            DropIndex("PhotoSimilar", new[] { "RightPhotoID" });
            DropIndex("PhotoFingerprint", new[] { "PhotoID" });
            DropIndex("FileAttribute", new[] { "OwnerID" });
            DropIndex("FileInfoExtend", new[] { "ID" });
            DropIndex("StorageEngine", new[] { "SettingID" });
            DropIndex("FileInfo", new[] { "EngineID" });
            DropIndex("PhotoAttribute", new[] { "OwnerID" });
            DropIndex("Photo", new[] { "AlbumID" });
            DropIndex("Photo", new[] { "FileID" });
            DropIndex("Photo", new[] { "ID" });
            DropIndex("PDFAttribute", new[] { "OwnerID" });
            DropIndex("WordObjectElement", new[] { "OwnerID" });
            DropIndex("WordAttribute", new[] { "OwnerID" });
            DropIndex("ProductAttachment", new[] { "ProductID" });
            DropIndex("ScheduleJobLog", new[] { "ScheduleId" });
            DropIndex("UserProfile", new[] { "ContactProfileID" });
            DropIndex("FamilyRelation", new[] { "RightRoleId" });
            DropIndex("FamilyRelation", new[] { "LeftRoleId" });
            DropIndex("MessageLogEntities", new[] { "Message_ID" });
            DropIndex("MessageLogEntities", new[] { "MessageEntityID" });
            DropTable("PhoneMessageLogEntity");
            DropTable("MailMessageLogEntity");
            DropTable("PurchaseLineItem");
            DropTable("PurchaseOrder");
            DropTable("AssetsItem");
            DropTable("PhotoSimilar");
            DropTable("PhotoFingerprint");
            DropTable("FileAttribute");
            DropTable("FileInfoExtend");
            DropTable("StorageEngineSetting");
            DropTable("StorageEngine");
            DropTable("FileInfo");
            DropTable("PhotoAttribute");
            DropTable("Photo");
            DropTable("Album");
            DropTable("PDFAttribute");
            DropTable("PDFInfo");
            DropTable("WordObjectElement");
            DropTable("WordAttribute");
            DropTable("WordInfo");
            DropTable("ProductItem");
            DropTable("ProductAttachment");
            DropTable("ScheduleJobLog");
            DropTable("LogEntity");
            DropTable("ScheduleJob");
            DropTable("DomainEventArgsLog");
            DropTable("SystemParameter");
            DropTable("UserProfile");
            DropTable("FamilyRole");
            DropTable("FamilyRelation");
            DropTable("ContactProfile");
            DropTable("MessageEntity");
            DropTable("MessageLogEntities");
        }
    }
}
