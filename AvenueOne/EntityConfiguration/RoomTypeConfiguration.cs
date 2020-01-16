using AvenueOne.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.EntityConfiguration
{
    public class RoomTypeConfiguration : EntityTypeConfiguration<RoomType>
    {
        public RoomTypeConfiguration()
        {
            int maxLength = 50;
            //table

            //keys
            HasKey(r => r.Id);

            //property
            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(maxLength); ;

            //relationships
            HasMany(r => r.Amenities);
                //.WithMany(a=> a.RoomTypes);
        }
    }
}
