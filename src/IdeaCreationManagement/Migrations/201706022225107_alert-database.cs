namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alertdatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alerts", "TimeOfChange", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alerts", "TimeOfChange");
        }
    }
}
