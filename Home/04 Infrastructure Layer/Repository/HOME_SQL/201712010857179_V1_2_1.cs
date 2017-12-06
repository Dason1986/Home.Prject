namespace Home.Repository.HOME_SQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_2_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SerialNumberManagement",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Code = c.String(),
                        CurrentNumber = c.Int(nullable: false),
                        SerialNumberFormat = c.String(),
                        IsCustom = c.Boolean(nullable: false),
                        CustomClass = c.String(),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhotoFace",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContactId = c.Guid(nullable: false),
                        PhotoId = c.Guid(nullable: false),
                        Location = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 20),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContactProfile", t => t.ContactId)
                .ForeignKey("dbo.Photo", t => t.PhotoId)
                .Index(t => t.ContactId)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.FileLogicTree",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        Name = c.String(),
                        NodeType = c.Int(nullable: false),
                        FileId = c.Guid(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileLogicTree", t => t.ParentId)
                .ForeignKey("dbo.FileInfo", t => t.FileId)
                .Index(t => t.ParentId)
                .Index(t => t.FileId);
            
            AddColumn("dbo.FileInfo", "FileStatue", c => c.Int(nullable: false));
            AddColumn("dbo.FileInfo", "Sequence", c => c.String());
            DropColumn("dbo.FileInfoExtend", "Sequence");
            this.ExSqlUp();
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileInfoExtend", "Sequence", c => c.String());
            DropForeignKey("dbo.FileLogicTree", "FileId", "dbo.FileInfo");
            DropForeignKey("dbo.FileLogicTree", "ParentId", "dbo.FileLogicTree");
            DropForeignKey("dbo.PhotoFace", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.PhotoFace", "ContactId", "dbo.ContactProfile");
            DropIndex("dbo.FileLogicTree", new[] { "FileId" });
            DropIndex("dbo.FileLogicTree", new[] { "ParentId" });
            DropIndex("dbo.PhotoFace", new[] { "PhotoId" });
            DropIndex("dbo.PhotoFace", new[] { "ContactId" });
            DropColumn("dbo.FileInfo", "Sequence");
            DropColumn("dbo.FileInfo", "FileStatue");
            DropTable("dbo.FileLogicTree");
            DropTable("dbo.PhotoFace");
            DropTable("dbo.SerialNumberManagement");
        }
    }
}
