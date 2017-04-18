namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_0_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageLogEntities",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        MessageEntityID = c.Guid(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                        Message_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MessageEntity", t => t.Message_ID)
                .ForeignKey("dbo.MessageEntity", t => t.MessageEntityID)
                .Index(t => t.MessageEntityID)
                .Index(t => t.Message_ID);
            
            CreateTable(
                "dbo.MessageEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Content = c.String(nullable: false, unicode: false),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ModifiedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        StatusCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MailMessageLogEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        To = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Cc = c.String(maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MessageLogEntities", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.PhoneMessageLogEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MessageLogEntities", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneMessageLogEntity", "ID", "dbo.MessageLogEntities");
            DropForeignKey("dbo.MailMessageLogEntity", "ID", "dbo.MessageLogEntities");
            DropForeignKey("dbo.MessageLogEntities", "MessageEntityID", "dbo.MessageEntity");
            DropForeignKey("dbo.MessageLogEntities", "Message_ID", "dbo.MessageEntity");
            DropIndex("dbo.PhoneMessageLogEntity", new[] { "ID" });
            DropIndex("dbo.MailMessageLogEntity", new[] { "ID" });
            DropIndex("dbo.MessageLogEntities", new[] { "Message_ID" });
            DropIndex("dbo.MessageLogEntities", new[] { "MessageEntityID" });
            DropTable("dbo.PhoneMessageLogEntity");
            DropTable("dbo.MailMessageLogEntity");
            DropTable("dbo.MessageEntity");
            DropTable("dbo.MessageLogEntities");
        }
    }
}
