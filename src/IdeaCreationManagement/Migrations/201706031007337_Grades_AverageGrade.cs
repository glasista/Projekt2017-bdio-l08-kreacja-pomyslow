namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Grades_AverageGrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "AverageGrade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "AverageGrade");
        }
    }
}
