namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class password : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PasswordHashed", c => c.String());
            DropColumn("dbo.Grades", "AverageGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grades", "AverageGrade", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "PasswordHashed");
        }
    }
}
