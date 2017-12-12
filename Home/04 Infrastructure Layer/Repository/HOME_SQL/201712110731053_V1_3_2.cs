namespace Home.Repository.HOME_SQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_3_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WordObjectElement", "OwnerID", "dbo.WordInfo");
            DropForeignKey("dbo.PDFAttribute", "OwnerID", "dbo.PDFInfo");
            DropIndex("dbo.WordObjectElement", new[] { "OwnerID" });
            DropIndex("dbo.PDFAttribute", new[] { "OwnerID" });
            AddColumn("dbo.WordInfo", "Summary_Author", c => c.String(maxLength: 50));
            AddColumn("dbo.WordInfo", "Summary_Subject", c => c.String(maxLength: 50));
            AddColumn("dbo.WordInfo", "Summary_Title", c => c.String(maxLength: 50));
            AddColumn("dbo.WordInfo", "Summary_Keyworks", c => c.String(maxLength: 255));
            AddColumn("dbo.WordInfo", "Summary_Content", c => c.String());
            AddColumn("dbo.WordInfo", "OffileFileType", c => c.Int(nullable: false));
            AddColumn("dbo.WordInfo", "FileID", c => c.Guid(nullable: false));
            CreateIndex("dbo.WordInfo", "ID");
            CreateIndex("dbo.WordInfo", "FileID");
            AddForeignKey("dbo.WordInfo", "ID", "dbo.FileInfo", "ID");
            AddForeignKey("dbo.WordInfo", "FileID", "dbo.FileInfo", "ID");
            DropColumn("dbo.WordInfo", "PagesCount");
            DropColumn("dbo.WordInfo", "CharCount");
            DropColumn("dbo.WordInfo", "ChineseCount");
            DropColumn("dbo.WordInfo", "ImageCount");
            DropColumn("dbo.WordInfo", "LinkCount");
            DropTable("dbo.WordObjectElement");
            DropTable("dbo.PDFInfo");
            DropTable("dbo.PDFAttribute");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PDFAttribute",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        AttKey = c.String(maxLength: 50),
                        AttValue = c.String(maxLength: 255),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 20),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 20),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WordObjectElement",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ElementType = c.Int(nullable: false),
                        ObjectBuffer = c.Binary(),
                        ObjectContent = c.String(),
                        OwnerID = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.WordInfo", "LinkCount", c => c.Int(nullable: false));
            AddColumn("dbo.WordInfo", "ImageCount", c => c.Int(nullable: false));
            AddColumn("dbo.WordInfo", "ChineseCount", c => c.Int(nullable: false));
            AddColumn("dbo.WordInfo", "CharCount", c => c.Int(nullable: false));
            AddColumn("dbo.WordInfo", "PagesCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.WordInfo", "FileID", "dbo.FileInfo");
            DropForeignKey("dbo.WordInfo", "ID", "dbo.FileInfo");
            DropIndex("dbo.WordInfo", new[] { "FileID" });
            DropIndex("dbo.WordInfo", new[] { "ID" });
            DropColumn("dbo.WordInfo", "FileID");
            DropColumn("dbo.WordInfo", "OffileFileType");
            DropColumn("dbo.WordInfo", "Summary_Content");
            DropColumn("dbo.WordInfo", "Summary_Keyworks");
            DropColumn("dbo.WordInfo", "Summary_Title");
            DropColumn("dbo.WordInfo", "Summary_Subject");
            DropColumn("dbo.WordInfo", "Summary_Author");
            CreateIndex("dbo.PDFAttribute", "OwnerID");
            CreateIndex("dbo.WordObjectElement", "OwnerID");
            AddForeignKey("dbo.PDFAttribute", "OwnerID", "dbo.PDFInfo", "ID");
            AddForeignKey("dbo.WordObjectElement", "OwnerID", "dbo.WordInfo", "ID");
        }
    }
}
