using AvenueOne.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.EntityConfiguration
{
    public class BookingConfiguration : EntityTypeConfiguration<Booking>
    {
        public BookingConfiguration()
        {
            int maxLength = 50;
            //table

            //keys
            HasKey(b => b.Id);

            //property
            Property(a => a.AmountTotal)
                .IsRequired();
            Property(a => a.Status)
                .IsRequired();

            //relationships
            HasRequired(a => a.Room);
                //.WithMany(r => r.Bookings);
        }
    }
}
