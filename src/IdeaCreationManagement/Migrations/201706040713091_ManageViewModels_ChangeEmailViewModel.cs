namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManageViewModels_ChangeEmailViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PasswordHashed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PasswordHashed");
        }
    }
}
