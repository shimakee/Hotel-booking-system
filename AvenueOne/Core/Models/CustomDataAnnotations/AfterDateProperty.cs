using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.CustomDataAnnotations
{
    public class AfterDateProperty : ValidationAttribute
    {
        private DateTime _date;

        public string Property { get; set; }

        public AfterDateProperty()
        {
            _date = DateTime.Today;
        }
        public AfterDateProperty(string property)
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
            //throw new NullReferenceException($"Property of {_property} has no value or is null.");

            try
            {
                _date = (DateTime)instanceValue;

                if ((DateTime)value > _date)
                    return ValidationResult.Success;
                return new ValidationResult($"Date must be after {_date.Day}, {_date.Month}, {_date.Year}.");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
