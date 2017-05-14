﻿using System.Data.Entity;
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
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}
