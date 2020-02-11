using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.CustomDataAnnotations
{
    public class BetweenDate : ValidationAttribute
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        //private string _property;
        //public BetweenDate(string property)
        public BetweenDate()
        {
            //Date = DateTime.Today;
            //_property = property;
            MinDate = DateTime.MinValue;
            MaxDate = DateTime.MaxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //object instance = validationContext.ObjectInstance;
            //PropertyInfo instanceProperty = validationContext.ObjectType.GetProperty(_property);
            ////no need to validate if there is no property to refer to
            //if (instanceProperty == null)
            //    return new ValidationResult($"There is no property with the name {_property}");


            //object instanceValue = instanceProperty.GetValue(instance, null);
            ////no need to validate if there is no value to check condition
            //if (instanceValue == null)
            //    return new ValidationResult($"Property {_property} is null or has no value.");

            try
            {
                //Date = (DateTime)instanceValue;

                //if ((DateTime)value < Date)
                //    return ValidationResult.Success;
                //return new ValidationResult($"Date must be before {Date.Day}, {Date.Month}, {Date.Year}.");

                DateTime dateValue = (DateTime)value;

                if (dateValue > MinDate && dateValue < MaxDate)
                    return ValidationResult.Success;
                return new ValidationResult($"Date must be between {MinDate.ToString("d")} - {MaxDate.ToString("d")}.");

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
