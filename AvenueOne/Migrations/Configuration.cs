namespace AvenueOne.Migrations
{
    using AvenueOne.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AvenueOne.Persistence.Repositories.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AvenueOne.Persistence.Repositories.PlutoContext context)
        {
            Person person = new Person() { FirstName = "Kenneth", LastName = "De Leon" };
            Person person2 = new Person() { FirstName = "Dinah", LastName = "Hong" };
            User user = new User("shimakee", "shimakee", true);
            User user2 = new User("dinah", "dinah", false);
            user.Person = person;
            user2.Person = person2;

            //clean database
            context.Database.ExecuteSqlCommand("DELETE FROM PEOPLE");
            context.Database.ExecuteSqlCommand("DELETE FROM USERS");

            //insert
            context.Users.AddRange(new List<User>() { user, user2 });

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
        }
    }
}
