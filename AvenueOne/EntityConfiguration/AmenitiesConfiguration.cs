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
    public class AmenitiesConfiguration : EntityTypeConfiguration<Amenities>
    {
        public AmenitiesConfiguration()
        {
            int maxLength = 50;
            //table

            //keys
            HasKey(a => a.Id);

            //property
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(maxLength)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                                                                new IndexAnnotation(new IndexAttribute("Name") { IsUnique = true })); ; ;

            //relationships
        }
    }
}
