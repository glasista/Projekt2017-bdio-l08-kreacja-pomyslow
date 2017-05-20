using IdeaCreationManagement.Models;
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

            context.States.AddOrUpdate(
                s => s.Name,
                new State() { Name = "oczekuj¹cy" },
                new State() { Name = "odrzucony" },
                new State() { Name = "przydzielony" },
                new State() { Name = "zrealizowany" },
                new State() { Name = "niezrealizowany" },
                new State() { Name = "rozwi¹zany" },
                new State() { Name = "nierozwi¹zany" }
            );

            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Stan ³azienek", Type = ProjectType.Problem },
                new Category() { Name = "Stan pod³óg", Type = ProjectType.Problem },
                new Category() { Name = "Organizacja studiów", Type = ProjectType.Idea },
                new Category() { Name = "Organizacja dziekanatu", Type = ProjectType.Idea }
            );

            context.FieldsOfStudies.AddOrUpdate(
                f => f.Name,
                new FieldOfStudy() { Name = "Informatyka" },
                new FieldOfStudy() { Name = "Zarz¹dzanie" }
            );

            context.OrganizationalUnits.AddOrUpdate(
                o => o.Name,
                new OrganizationalUnit() { Name = "Wydzia³ informatyki" },
                new OrganizationalUnit() { Name = "Wydzia³ zarz¹dzania" }
            );

            context.Roles.AddOrUpdate(
                r => r.Name,
                new IdentityRole("student"),
                new IdentityRole("employee"),
                new IdentityRole("admin")
            );

            //do dodania:
            //Category, Claims, Logins, OrganizationalU…, Roles
            context.Users.AddOrUpdate(
                r => r.Email,
                new User {
                    FieldOfStudyId = 1,
                    OrganizationalUnitId = 1,
                    Surname = "Kowalski",
                    StudentNumber = 10010,
                    Email = "email1@mail.com",
                    EmailConfirmed = false,
                    PasswordHash = "AJbhCiDRViukYOcL9E05michPMIBGfOr2Bsj4zkkJRpLbDe5y1b4Gd922uCBevfzFA==",  //Has³o6zn
                    SecurityStamp = "c1c09534-086f-448f-95c4-e775ad6d8cd7",
                    PhoneNumber = "465675485",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "Jan",

                },
                new User
                { 
                    FieldOfStudyId = 1,
                    OrganizationalUnitId = 1,
                    Surname = "Polak",
                    StudentNumber = 10011,
                    Email = "email2@mail.com",
                    EmailConfirmed = false,
                    PasswordHash = "AJbhCiDRViukYOcL9E05michPMIBGfOr2Bsj4zkkJRpLbDe5y1b4Gd922uCBevfzFA==",  //Has³o6zn
                    SecurityStamp = "c1c09534-086f-448f-95c4-e775ad6d8cd7",
                    PhoneNumber = "987876765",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "Adam"
                }
            );

            context.SaveChanges();

            context.Projects.AddOrUpdate(
                r => r.Id,
                new Project
                {
                    Title = "Tytu³1",
                    Description = "Opis 1",
                    AuthorId = context.Users.ToArray()[0].Id,
                    AssigneeId = context.Users.ToArray()[1].Id,
                    Type = ProjectType.Idea,
                    StateId = 3,
                    CategoryId = 1
                },
                new Project
                {
                    Title = "Tytu³2",
                    Description = "Opis 2",
                    AuthorId = context.Users.ToArray()[0].Id,
                    AssigneeId = context.Users.ToArray()[1].Id,
                    Type = ProjectType.Idea,
                    StateId = 3,
                    CategoryId = 1
                }
            );


        }
    }
}
