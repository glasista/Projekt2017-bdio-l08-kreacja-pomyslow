namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filecontent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "DataType", c => c.String());
            AddColumn("dbo.Files", "Content", c => c.Binary());
            DropColumn("dbo.Files", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "FilePath", c => c.String());
            DropColumn("dbo.Files", "Content");
            DropColumn("dbo.Files", "DataType");
        }
    }
}
