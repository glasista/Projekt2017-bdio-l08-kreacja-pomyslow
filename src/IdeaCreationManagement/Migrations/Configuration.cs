using System.Web.Razor.Generator;
﻿using System.Web.Razor.Generator;
using IdeaCreationManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaCreationManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdeaCreationManagement.Models.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IdeaCreationManagement.Models.AppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var states = new[]
            {
                new State() {Name = "oczekujący"},
                new State() {Name = "odrzucony"},
                new State() {Name = "przydzielony"},
                new State() {Name = "zrealizowany"},
                new State() {Name = "niezrealizowany"},
                new State() {Name = "rozwiązany"},
                new State() {Name = "nierozwiązany"}
            };

            context.States.AddOrUpdate(
                s => s.Name,
                states
            );

            var categories = new[]
            {
                new Category() { Name = "Stan łazienek", Type = ProjectType.Problem },
                new Category() { Name = "Stan podłóg", Type = ProjectType.Problem },
                new Category() { Name = "Organizacja studiów", Type = ProjectType.Pomysł },
                new Category() { Name = "Organizacja dziekanatu", Type = ProjectType.Pomysł }
            };

            context.Categories.AddOrUpdate(
                c => c.Name,
                categories
            );

            var fields = new[]
            {
                new FieldOfStudy() {Name = "Informatyka"},
                new FieldOfStudy() {Name = "Zarządzanie"}
            };

            context.FieldsOfStudies.AddOrUpdate(
                f => f.Name,
                fields
            );

            var organizations = new[]
            {
                new OrganizationalUnit() {Name = "Wydział informatyki"},
                new OrganizationalUnit() {Name = "Wydział zarządzania"}
            };

            context.OrganizationalUnits.AddOrUpdate(
                o => o.Name,
                organizations
            );

            var student = new IdentityRole("student");
            var employee = new IdentityRole("employee");
            var admin = new IdentityRole("admin");

            context.Roles.AddOrUpdate(
                r => r.Name,
                student,
                employee,
                admin
            );
            context.SaveChanges();

            var studentMail = "student@mail.com";
            var employeeMail = "pracownik@mail.com";
            var adminMail = "admin@mail.com";

            var user1 = context.Users.SingleOrDefault(x => x.UserName == studentMail);
            var user2 = context.Users.SingleOrDefault(x => x.UserName == employeeMail);
            var user3 = context.Users.SingleOrDefault(x => x.UserName == adminMail);

            var hasher = new PasswordHasher();
            var pass = "qwerty";
            var passHash = hasher.HashPassword(pass);
            

            if (user1 == null)
            {
                user1 = new User
                {
                    FieldOfStudyId = fields[0].Id,
                    Surname = "Kowalski",
                    StudentNumber = 10010,
                    Email = studentMail,
                    EmailConfirmed = true,
                    PasswordHash = passHash,
                    SecurityStamp = new Guid().ToString(),
                    UserName = studentMail,
                    Name = "Jan",
                };
                user1.Roles.Add(new IdentityUserRole() { RoleId = student.Id, UserId = user1.Id});
                context.Users.Add(user1);
            }

            if (user2 == null)
            {
                user2 = new User
                {
                    OrganizationalUnitId = organizations[0].Id,
                    Surname = "Polak",
                    Email = employeeMail,
                    EmailConfirmed = true,
                    PasswordHash = passHash,
                    SecurityStamp = new Guid().ToString(),
                    Name = "Adam",
                    UserName = employeeMail,
                    CategoryId = categories[1].Id,
                };
                user2.Roles.Add(new IdentityUserRole() { RoleId = employee.Id, UserId = user2.Id});
                context.Users.Add(user2);
            }

            if (user3 == null)
            {
                user3 = new User
                {
                    Surname = "Niepolak",
                    Email = adminMail,
                    EmailConfirmed = true,
                    PasswordHash = passHash,
                    SecurityStamp = new Guid().ToString(),
                    Name = "Admin",
                    UserName = adminMail,
                };
                user3.Roles.Add(new IdentityUserRole() { RoleId = student.Id, UserId = user3.Id });
                user3.Roles.Add(new IdentityUserRole() { RoleId = employee.Id, UserId = user3.Id });
                user3.Roles.Add(new IdentityUserRole() { RoleId = admin.Id, UserId = user3.Id });
                context.Users.Add(user3);
            }

            context.SaveChanges();

            var projects = new Project[]
            {
                new Project
                {
                    Title = "Tytuł 1",
                    Description = "Opis 1",
                    AuthorId = user1.Id,
                    AssigneeId = user2.Id,
                    Type = ProjectType.Problem,
                    StateId = states[0].Id,
                    CategoryId = categories[0].Id,
                    Time = DateTime.Now
                },
                new Project
                {
                    Title = "Tytuł 2",
                    Description = "Opis 2",
                    AuthorId = user1.Id,
                    Type = ProjectType.Problem,
                    StateId = states[0].Id,
                    CategoryId = categories[1].Id,
                    Time = DateTime.Now
                },
                new Project
                {
                    Title = "Tytuł 3",
                    Description = "Opis 3",
                    AssigneeId = user2.Id,
                    Type = ProjectType.Pomysł,
                    StateId = states[0].Id,
                    CategoryId = categories[2].Id,
                    Time = DateTime.Now
                },
                new Project
                {
                    Title = "Tytuł 4",
                    Description = "Opis 4",
                    Type = ProjectType.Pomysł,
                    StateId = states[0].Id,
                    CategoryId = categories[3].Id,
                    Time = DateTime.Now
                },
            };

            context.Users.AddOrUpdate(
                r => r.UserName,
                user1, user2);

            context.SaveChanges();

            context.Projects.AddOrUpdate(
                r => r.Title,
                projects
            );

            context.SaveChanges();
        }
    }
}
