using AvenueOne.Models;
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
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            //table

            //key
            HasKey(p => p.Id);

            //property
            int nameLength = 100;
            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(nameLength);

            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(nameLength);

            Property(p=> p.FullName)
                .IsRequired()
                .HasMaxLength(nameLength)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, 
                                                                                                new IndexAnnotation(new IndexAttribute("FullName") { IsUnique = true }));

            Property(p => p.MaidenName)
                .HasMaxLength(nameLength);

            Property(p => p.MiddleName)
                .HasMaxLength(nameLength);

            //relatioships
            HasOptional(p => p.User)
            .WithOptionalPrincipal(u => u.Person);
        }
    }
}
