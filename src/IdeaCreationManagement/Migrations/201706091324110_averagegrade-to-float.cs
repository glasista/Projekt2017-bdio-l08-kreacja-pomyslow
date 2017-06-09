namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class averagegradetofloat : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Grades", "AverageGrade");
            AddColumn("dbo.Grades", "AverageGrade", c => c.Single(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Grades", "AverageGrade");
            AddColumn("dbo.Grades", "AverageGrade", c => c.Int(nullable: false));
        }
    }
}
