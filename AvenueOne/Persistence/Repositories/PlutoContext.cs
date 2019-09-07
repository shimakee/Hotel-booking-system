using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Persistence.Repositories
{
    public class PlutoContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<IPerson> People { get; set; }

        public PlutoContext()
            :base("name=DefaultConnection")
        {

        }

        public PlutoContext(string connectionName)
            :base($"name={connectionName}")
        {

        }
    }
}
