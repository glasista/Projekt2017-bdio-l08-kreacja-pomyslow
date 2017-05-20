namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projecttime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Time");
        }
    }
}
