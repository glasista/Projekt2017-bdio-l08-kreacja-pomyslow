namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kasowaniealertow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Alerts", "AuthorOfChangeId", "dbo.AspNetUsers");
            DropIndex("dbo.Alerts", new[] { "AuthorOfChangeId" });
            AlterColumn("dbo.Alerts", "AuthorOfChangeId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Alerts", "AuthorOfChangeId");
            AddForeignKey("dbo.Alerts", "AuthorOfChangeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alerts", "AuthorOfChangeId", "dbo.AspNetUsers");
            DropIndex("dbo.Alerts", new[] { "AuthorOfChangeId" });
            AlterColumn("dbo.Alerts", "AuthorOfChangeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Alerts", "AuthorOfChangeId");
            AddForeignKey("dbo.Alerts", "AuthorOfChangeId", "dbo.AspNetUsers", "Id");
        }
    }
}
