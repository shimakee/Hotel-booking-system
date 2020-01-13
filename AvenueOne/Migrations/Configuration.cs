namespace AvenueOne.Migrations
{
    using AvenueOne.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using AvenueOne.Services;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AvenueOne.Persistence.Repositories.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AvenueOne.Persistence.Repositories.PlutoContext context)
        {
            //clean database
            context.Database.ExecuteSqlCommand("DELETE FROM USERS");
            context.Database.ExecuteSqlCommand("DELETE FROM CUSTOMERS");
            context.Database.ExecuteSqlCommand("DELETE FROM PEOPLE");

            Person person = new Person() { FirstName = "Kenneth", LastName = "De Leon" };
            Person person2 = new Person() { FirstName = "Dinah", LastName = "Hong" };
            Person person3 = new Person() { FirstName = "Darius", LastName = "De Leon" };
            Person person4 = new Person() { FirstName = "Kristof", LastName = "De Leon" };
            person.BirthDate = new DateTime(1980, 10, 10);
            person2.BirthDate = new DateTime(1918, 12, 18);
            person3.BirthDate = new DateTime(1989, 10, 18);
            person4.BirthDate = new DateTime(1988, 12, 31);
            User user = new User("shimakee", "shimakee", true);
            User user2 = new User("dinah", "dinah", false);

            Customer customer = new Customer();
            Customer customer2 = new Customer();

            customer.Person = person3;
            customer2.Person = person4;

            user.Person = person;
            user.Password = HashService.Hash(user.Password);
            user.PasswordConfirm = user.Password;
            user2.Person = person2;
            user2.Password = HashService.Hash(user2.Password);
            user2.PasswordConfirm = user2.Password;

            //insert
            context.Users.AddRange(new List<User>() { user, user2 });
            context.Customers.AddRange(new List<Customer>() { customer, customer2 });
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
