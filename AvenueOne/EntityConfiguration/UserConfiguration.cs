using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            //table

            //keys
            HasKey(u => u.Id);

            //property
            Property(u => u.Username).IsRequired();
            Property(u => u.Password).IsRequired();

            //relationships
            HasRequired(u => u.Person)
                .WithRequiredPrincipal(p=>p.User);
        }
    }
}
