﻿using System.Data.Entity;
using System.Web.DynamicData;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaCreationManagement.Models
{
    public class AppContext : IdentityDbContext<User>
    {
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FieldOfStudy> FieldsOfStudies { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<State> States { get; set; }      
        public AppContext()
            : base("SqlServerFile", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>()
                .HasRequired(x => x.State)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.StateId)
                .WillCascadeOnDelete(false);

            builder.Entity<Alert>()
                .HasRequired(x => x.AuthorOfChange)
                .WithMany()
                .WillCascadeOnDelete(true);
        }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}
