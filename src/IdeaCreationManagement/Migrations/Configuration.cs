using IdeaCreationManagement.Models;

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
                new State() { Name = "oczekuj�cy" },
                new State() { Name = "odrzucony" },
                new State() { Name = "przydzielony" },
                new State() { Name = "zrealizowany" },
                new State() { Name = "niezrealizowany" },
                new State() { Name = "rozwi�zany" },
                new State() { Name = "nierozwi�zany" }
            );

            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Stan �azienek", Type = ProjectType.Problem },
                new Category() { Name = "Stan pod��g", Type = ProjectType.Problem },
                new Category() { Name = "Organizacja studi�w", Type = ProjectType.Idea },
                new Category() { Name = "Organizacja dziekanatu", Type = ProjectType.Idea }
            );

            context.FieldsOfStudies.AddOrUpdate(
                f => f.Name,
                new FieldOfStudy() { Name = "Informatyka" },
                new FieldOfStudy() { Name = "Zarz�dzanie" }
            );

            context.OrganizationalUnits.AddOrUpdate(
                o => o.Name,
                new OrganizationalUnit() { Name = "Wydzia� informatyki" },
                new OrganizationalUnit() { Name = "Wydzia� zarz�dzania" }
            );
        }
    }
}
