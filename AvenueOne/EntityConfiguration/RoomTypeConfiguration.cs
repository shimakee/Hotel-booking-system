using AvenueOne.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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
                .HasMaxLength(maxLength)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                                                                new IndexAnnotation(new IndexAttribute("Name") { IsUnique = true }));
            Property(r => r.Rate)
                .IsRequired();
            Property(r => r.RateType)
                .IsRequired();

            //relationships
            HasMany(r => r.Amenities);
                //.WithMany(a=> a.RoomTypes);
        }
    }
}
