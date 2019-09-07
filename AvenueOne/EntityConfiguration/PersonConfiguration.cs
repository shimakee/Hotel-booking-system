using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.EntityConfiguration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            //table

            //key
            HasKey(p => p.Id);

            //property
            Property(p => p.FirstName)
                .IsRequired();
            Property(p => p.LastName)
                .IsRequired();
            //relatioships
            HasRequired(p => p.User)
                .WithRequiredDependent(u => u.Person);
        }
    }
}
