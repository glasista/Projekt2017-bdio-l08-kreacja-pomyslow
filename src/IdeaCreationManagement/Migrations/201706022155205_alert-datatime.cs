namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alertdatatime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Alerts", "TimeOfChange");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alerts", "TimeOfChange", c => c.DateTime(nullable: false));
        }
    }
}
