namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_3_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("WordObjectElement", "OwnerID", "WordInfo");
            DropForeignKey("PDFAttribute", "OwnerID", "PDFInfo");
            DropIndex("WordObjectElement", new[] { "OwnerID" });
            DropIndex("PDFAttribute", new[] { "OwnerID" });
            AddColumn("WordInfo", "Summary_Author", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("WordInfo", "Summary_Subject", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("WordInfo", "Summary_Title", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("WordInfo", "Summary_Keyworks", c => c.String(maxLength: 255, storeType: "nvarchar"));
            AddColumn("WordInfo", "Summary_Content", c => c.String(unicode: false));
            AddColumn("WordInfo", "OffileFileType", c => c.Int(nullable: false));
            AddColumn("WordInfo", "FileID", c => c.Guid(nullable: false));
            CreateIndex("WordInfo", "ID");
            CreateIndex("WordInfo", "FileID");
            AddForeignKey("WordInfo", "ID", "FileInfo", "ID");
            AddForeignKey("WordInfo", "FileID", "FileInfo", "ID");
            DropColumn("WordInfo", "PagesCount");
            DropColumn("WordInfo", "CharCount");
            DropColumn("WordInfo", "ChineseCount");
            DropColumn("WordInfo", "ImageCount");
            DropColumn("WordInfo", "LinkCount");
            DropTable("WordObjectElement");
            DropTable("PDFInfo");
            DropTable("PDFAttribute");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            AddColumn("WordInfo", "LinkCount", c => c.Int(nullable: false));
            AddColumn("WordInfo", "ImageCount", c => c.Int(nullable: false));
            AddColumn("WordInfo", "ChineseCount", c => c.Int(nullable: false));
            AddColumn("WordInfo", "CharCount", c => c.Int(nullable: false));
            AddColumn("WordInfo", "PagesCount", c => c.Int(nullable: false));
            DropForeignKey("WordInfo", "FileID", "FileInfo");
            DropForeignKey("WordInfo", "ID", "FileInfo");
            DropIndex("WordInfo", new[] { "FileID" });
            DropIndex("WordInfo", new[] { "ID" });
            DropColumn("WordInfo", "FileID");
            DropColumn("WordInfo", "OffileFileType");
            DropColumn("WordInfo", "Summary_Content");
            DropColumn("WordInfo", "Summary_Keyworks");
            DropColumn("WordInfo", "Summary_Title");
            DropColumn("WordInfo", "Summary_Subject");
            DropColumn("WordInfo", "Summary_Author");
            CreateIndex("PDFAttribute", "OwnerID");
            CreateIndex("WordObjectElement", "OwnerID");
            AddForeignKey("PDFAttribute", "OwnerID", "PDFInfo", "ID");
            AddForeignKey("WordObjectElement", "OwnerID", "WordInfo", "ID");
        }
    }
}
