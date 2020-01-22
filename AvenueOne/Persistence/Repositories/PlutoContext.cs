using AvenueOne.Core.Models;
using AvenueOne.EntityConfiguration;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Persistence.Repositories
{
    public class PlutoContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<Room> Room { get; set; }

        public PlutoContext()
            :base("name=LocalConnection")
        {

        }

        public PlutoContext(string connectionName)
            :base($"name={connectionName}")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new AmenitiesConfiguration());
            modelBuilder.Configurations.Add(new RoomTypeConfiguration());
        }
    }
}
