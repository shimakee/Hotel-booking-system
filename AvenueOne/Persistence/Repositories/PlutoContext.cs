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
        }
    }
}
