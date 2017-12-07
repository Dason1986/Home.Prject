namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_2_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SerialNumberManagement",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Code = c.String(unicode: false),
                        CurrentNumber = c.Int(nullable: false),
                        SerialNumberFormat = c.String(unicode: false),
                        IsCustom = c.Boolean(nullable: false),
                        CustomClass = c.String(unicode: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "PhotoFace",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContactId = c.Guid(nullable: false),
                        PhotoId = c.Guid(nullable: false),
                        Location = c.String(unicode: false),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("ContactProfile", t => t.ContactId)
                .ForeignKey("Photo", t => t.PhotoId)
                .Index(t => t.ContactId)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "FileLogicTree",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        Name = c.String(unicode: false),
                        NodeType = c.Int(nullable: false),
                        FileId = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FileLogicTree", t => t.ParentId)
                .ForeignKey("FileInfo", t => t.FileId)
                .Index(t => t.ParentId)
                .Index(t => t.FileId);
            
            AddColumn("FileInfo", "FileStatue", c => c.Int(nullable: false));
            AddColumn("FileInfo", "Sequence", c => c.String(unicode: false));
            DropColumn("FileInfoExtend", "Sequence");
            this.ExSqlUp();
        }
        
        public override void Down()
        {
            AddColumn("FileInfoExtend", "Sequence", c => c.String(unicode: false));
            DropForeignKey("FileLogicTree", "FileId", "FileInfo");
            DropForeignKey("FileLogicTree", "ParentId", "FileLogicTree");
            DropForeignKey("PhotoFace", "PhotoId", "Photo");
            DropForeignKey("PhotoFace", "ContactId", "ContactProfile");
            DropIndex("FileLogicTree", new[] { "FileId" });
            DropIndex("FileLogicTree", new[] { "ParentId" });
            DropIndex("PhotoFace", new[] { "PhotoId" });
            DropIndex("PhotoFace", new[] { "ContactId" });
            DropColumn("FileInfo", "Sequence");
            DropColumn("FileInfo", "FileStatue");
            DropTable("FileLogicTree");
            DropTable("PhotoFace");
            DropTable("SerialNumberManagement");
            this.ExSqlDown();
        }
    }
}
