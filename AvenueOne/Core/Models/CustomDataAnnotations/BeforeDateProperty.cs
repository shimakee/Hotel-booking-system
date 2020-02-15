using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.CustomDataAnnotations
{
    public class BeforeDateProperty : ValidationAttribute
    {
        private DateTime _date;
        public int Year { get; set; }
        public string Property { get; set; }
        public BeforeDateProperty()
        {
                _date = DateTime.Today;
            //Property = property;
        }

        public BeforeDateProperty(string property)
            : this()
        {
            Property = property;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo instanceProperty = validationContext.ObjectType.GetProperty(Property);
            //no need to validate if there is no property to refer to
            if (instanceProperty == null)
                throw new NullReferenceException($"No property of {Property} exists.");
            //return new ValidationResult($"There is no property with the name {_property}");


            object instance = validationContext.ObjectInstance;
            object instanceValue = instanceProperty.GetValue(instance, null);
            //no need to validate if there is no value to check condition
            if (instanceValue == null)
                return ValidationResult.Success;
            //return new ValidationResult($"Property {_property} is null or has no value.");

            try
            {
                _date = (DateTime)instanceValue;

                if ((DateTime)value < _date)
                    return ValidationResult.Success;
                return new ValidationResult($"Date must be before {_date.Day}, {_date.Month}, {_date.Year}.");

            }
            catch (Exception ex)
            {
                new ValidationResult(ex.Message);
                throw;
            }
        }

    }
}
