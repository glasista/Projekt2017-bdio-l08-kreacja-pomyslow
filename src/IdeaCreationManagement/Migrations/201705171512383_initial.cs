namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeOfChange = c.DateTime(nullable: false),
                        StateId = c.Int(nullable: false),
                        AuthorOfChangeId = c.String(maxLength: 128),
                        StudentRead = c.Boolean(nullable: false),
                        EmployeeRead = c.Boolean(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorOfChangeId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId)
                .Index(t => t.AuthorOfChangeId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FieldOfStudyId = c.Int(),
                        OrganizationalUnitId = c.Int(),
                        CategoryId = c.Int(),
                        Surname = c.String(),
                        StudentNumber = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.FieldOfStudies", t => t.FieldOfStudyId)
                .ForeignKey("dbo.OrganizationalUnits", t => t.OrganizationalUnitId)
                .Index(t => t.FieldOfStudyId)
                .Index(t => t.OrganizationalUnitId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FieldOfStudies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrganizationalUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AuthorId = c.String(maxLength: 128),
                        AssigneeId = c.String(maxLength: 128),
                        Type = c.Int(nullable: false),
                        AverageGrade = c.Single(nullable: false),
                        AverageUsefulness = c.Single(nullable: false),
                        AverageDifficulty = c.Single(nullable: false),
                        AverageIngenuity = c.Single(nullable: false),
                        StateId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssigneeId)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.AuthorId)
                .Index(t => t.AssigneeId)
                .Index(t => t.StateId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FilePath = c.String(),
                        Size = c.Long(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        UsefulnessValue = c.Int(nullable: false),
                        DifficultyValue = c.Int(nullable: false),
                        Ingenuity = c.Int(nullable: false),
                        RaterId = c.String(maxLength: 128),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RaterId)
                .Index(t => t.RaterId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Grades", "RaterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Files", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Alerts", "StateId", "dbo.States");
            DropForeignKey("dbo.Alerts", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "StateId", "dbo.States");
            DropForeignKey("dbo.Projects", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Projects", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "AssigneeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Alerts", "AuthorOfChangeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "OrganizationalUnitId", "dbo.OrganizationalUnits");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "FieldOfStudyId", "dbo.FieldOfStudies");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Grades", new[] { "ProjectId" });
            DropIndex("dbo.Grades", new[] { "RaterId" });
            DropIndex("dbo.Files", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "CategoryId" });
            DropIndex("dbo.Projects", new[] { "StateId" });
            DropIndex("dbo.Projects", new[] { "AssigneeId" });
            DropIndex("dbo.Projects", new[] { "AuthorId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUsers", new[] { "OrganizationalUnitId" });
            DropIndex("dbo.AspNetUsers", new[] { "FieldOfStudyId" });
            DropIndex("dbo.Alerts", new[] { "ProjectId" });
            DropIndex("dbo.Alerts", new[] { "AuthorOfChangeId" });
            DropIndex("dbo.Alerts", new[] { "StateId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Grades");
            DropTable("dbo.Files");
            DropTable("dbo.States");
            DropTable("dbo.Projects");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.OrganizationalUnits");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FieldOfStudies");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Alerts");
        }
    }
}
