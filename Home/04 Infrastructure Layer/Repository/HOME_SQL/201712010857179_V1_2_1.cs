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
            
            AddColumn("dbo.FileInfo", "IsDuplicate", c => c.Boolean(nullable: false));
            ExSqlUp();
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoFace", "PhotoId", "dbo.Photo");
            DropForeignKey("dbo.PhotoFace", "ContactId", "dbo.ContactProfile");
            DropIndex("dbo.PhotoFace", new[] { "PhotoId" });
            DropIndex("dbo.PhotoFace", new[] { "ContactId" });
            DropColumn("dbo.FileInfo", "IsDuplicate");
            DropTable("dbo.PhotoFace");
            DropTable("dbo.SerialNumberManagement");
        }
    }
}
