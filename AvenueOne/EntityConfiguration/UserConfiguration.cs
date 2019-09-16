using AvenueOne.Interfaces;
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
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            //table

            //keys
            HasKey(u => u.Id);

            //property
            Property(u => u.Username).IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, 
                                                    new IndexAnnotation(new IndexAttribute("Username") { IsUnique = true }));
            Property(u => u.Password).IsRequired()
                .HasMaxLength(255);

            Ignore(u => u.PasswordConfirm);

            //relationships
            HasOptional(u => u.Person)
                .WithOptionalDependent(p => p.User)
                .Map(u=> u.MapKey("PersonId"));
        }
    }
}
