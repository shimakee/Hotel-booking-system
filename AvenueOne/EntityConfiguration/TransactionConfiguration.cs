using AvenueOne.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.EntityConfiguration
{
    public class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
            int maxLength = 50;
            //table

            //keys
            HasKey(a => a.Id);

            //property

            //relationships
            HasRequired(t => t.Customer);

            HasRequired(t => t.Employee);

            HasMany(t => t.Bookings)
                .WithRequired(b=> b.Transaction);
        }
    }
}
