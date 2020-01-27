using AvenueOne.Persistence.Repositories;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AvenueOne.Services;
using System.Linq;
using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using System.Data.Entity.Validation;

namespace AvenueOne.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<AvenueOne.Persistence.Repositories.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
        }

        protected override void Seed(PlutoContext context)
        {
            //clean database
            context.Database.ExecuteSqlCommand("DELETE FROM AMENITIES");
            context.Database.ExecuteSqlCommand("DELETE FROM ROOMTYPES");
            context.Database.ExecuteSqlCommand("DELETE FROM ROOMS");
            context.Database.ExecuteSqlCommand("DELETE FROM USERS");
            context.Database.ExecuteSqlCommand("DELETE FROM CUSTOMERS");
            context.Database.ExecuteSqlCommand("DELETE FROM PEOPLE");

            #region Seed Users
            Person person = new Person() { FirstName = "Kenneth", LastName = "De Leon" };
            Person person2 = new Person() { FirstName = "Dinah", LastName = "Hong" };
            person.BirthDate = new DateTime(1980, 10, 10);
            person2.BirthDate = new DateTime(1918, 12, 18);

            User user = new User("shimakee", "shimakee", true);
            User user2 = new User("dinah", "dinah", false);

            user.Person = person;
            user.Password = HashService.Hash(user.Password);
            user.PasswordConfirm = user.Password;
            user2.Person = person2;
            user2.Password = HashService.Hash(user2.Password);
            user2.PasswordConfirm = user2.Password;

            context.Users.AddRange(new List<User>() { user, user2 });
            #endregion

            #region Seed Customers
            Person person3 = new Person() { FirstName = "Darius", LastName = "De Leon" };
            Person person4 = new Person() { FirstName = "Kristof", LastName = "De Leon" };
            person3.BirthDate = new DateTime(1989, 10, 18);
            person4.BirthDate = new DateTime(1988, 12, 31);


            Customer customer = new Customer();
            Customer customer2 = new Customer();

            customer.Person = person3;
            customer2.Person = person4;

            context.Customers.AddRange(new List<Customer>() { customer, customer2 });
            #endregion

            #region Seed Amenities
            Amenities amenity = new Amenities("Pool");
            Amenities amenity2 = new Amenities("TV");
            Amenities amenity3 = new Amenities("Internet");
            Amenities amenity4 = new Amenities("Aircondition");

            context.Amenities.AddRange(new List<Amenities>() { amenity, amenity2, amenity3, amenity4 });
            //context.SaveChanges();
            #endregion

            #region Seed RoomType
            RoomType roomType = new RoomType("Standard", 100, RateType.Daily);
            RoomType roomType2 = new RoomType("Deluxe", 200, RateType.Daily);
            RoomType roomType3 = new RoomType("Single", 350, RateType.Daily);
            RoomType roomType4 = new RoomType("Matrimonial", 630, RateType.Daily);
            RoomType roomType5 = new RoomType("Suite", 270, RateType.Daily);


            roomType.Amenities.Add(amenity);
            roomType.Amenities.Add(amenity2);
            roomType.Amenities.Add(amenity3);
            roomType.Amenities.Add(amenity4);

            roomType2.Amenities.Add(amenity);
            roomType2.Amenities.Add(amenity2);
            roomType2.Amenities.Add(amenity3);


            context.RoomType.AddRange(new List<RoomType>() { roomType, roomType2, roomType3, roomType4, roomType5 });
            //context.SaveChanges();
            #endregion

            #region Seed Room

            Room room = new Room("Some", 1, 1);
            Room room2 = new Room("Something", 2, 1);
            Room room3 = new Room("Someone", 3, 1);
            Room room4 = new Room("Somewhere", 4, 1);
            Room room5 = new Room("Somehow", 5, 1);
            room.RoomType = roomType;
            room2.RoomType = roomType2;
            room3.RoomType = roomType3;
            room4.RoomType = roomType4;
            room5.RoomType = roomType5;

            context.Room.AddRange(new List<Room>() { room, room2, room3, room4, room5 });
            //context.SaveChanges();
            #endregion
            //insert
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
