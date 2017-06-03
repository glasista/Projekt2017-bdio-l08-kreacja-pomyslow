namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passwordHashed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PasswordHashed", c => c.String());
            AddColumn("dbo.Grades", "AverageGrade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "AverageGrade");
            DropColumn("dbo.AspNetUsers", "PasswordHashed");
        }
    }
}
