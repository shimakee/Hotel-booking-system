using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.CustomDataAnnotations
{
    public class AfterDate : ValidationAttribute
    {
        public DateTime Date { get; set; }

        private string _property;
        public AfterDate(string property)
        {
            Date = DateTime.Today;
            _property = property;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo instanceProperty = validationContext.ObjectType.GetProperty(_property);
            //no need to validate if there is no property to refer to
            if (instanceProperty == null)
                throw new NullReferenceException($"No property of {_property} exists.");
                //return new ValidationResult($"There is no property with the name {_property}");


            object instance = validationContext.ObjectInstance;
            object instanceValue = instanceProperty.GetValue(instance, null);
            //no need to validate if there is no value to check condition
            if (instanceValue == null)
                return new ValidationResult($"Property {_property} is null or has no value.");
            //throw new NullReferenceException($"Property of {_property} has no value or is null.");

            try
            {
                Date = (DateTime)instanceValue;

                if ((DateTime)value > Date)
                    return ValidationResult.Success;
                return new ValidationResult($"Date must be before {Date.Day}, {Date.Month}, {Date.Year}.");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
