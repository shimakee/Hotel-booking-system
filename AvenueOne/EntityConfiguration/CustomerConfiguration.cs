﻿using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.EntityConfiguration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            //table

            //keys
            HasKey(u => u.Id);

            //property

            //relationships
            HasRequired(c => c.Person)
                .WithOptional(p=>p.Customer)
                .Map(c=> c.MapKey("PersonId"));

        }
    }
}