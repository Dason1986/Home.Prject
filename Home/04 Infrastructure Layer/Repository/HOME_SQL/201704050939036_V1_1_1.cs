namespace Home.Repository.HOME_SQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_1_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileAttributes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        AttKey = c.String(maxLength: 50),
                        AttValue = c.String(maxLength: 255),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 20),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 20),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileInfoExtend", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            AddColumn("dbo.FileInfoExtend", "KeyWords", c => c.String());
            AddColumn("dbo.FileInfoExtend", "BarCodeBuffer", c => c.Binary());
            AddColumn("dbo.FileInfoExtend", "QRCodeBuffer", c => c.Binary());
            AddColumn("dbo.FileInfoExtend", "BarCode", c => c.String());
            AddColumn("dbo.FileInfoExtend", "QRCode", c => c.String());
            AddColumn("dbo.FileInfoExtend", "Sequence", c => c.String());
            DropColumn("dbo.FileInfoExtend", "SourceType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileInfoExtend", "SourceType", c => c.Int(nullable: false));
            DropForeignKey("dbo.FileAttributes", "OwnerID", "dbo.FileInfoExtend");
            DropIndex("dbo.FileAttributes", new[] { "OwnerID" });
            DropColumn("dbo.FileInfoExtend", "Sequence");
            DropColumn("dbo.FileInfoExtend", "QRCode");
            DropColumn("dbo.FileInfoExtend", "BarCode");
            DropColumn("dbo.FileInfoExtend", "QRCodeBuffer");
            DropColumn("dbo.FileInfoExtend", "BarCodeBuffer");
            DropColumn("dbo.FileInfoExtend", "KeyWords");
            DropTable("dbo.FileAttributes");
        }
    }
}
