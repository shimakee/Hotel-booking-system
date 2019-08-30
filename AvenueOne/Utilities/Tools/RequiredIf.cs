using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities.Tools
{


    public class RequiredIf : RequiredAttribute
    {
        public string Property { get; private set; }
        
        public RequiredIf(string property)
        {
            Property = property;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool condition;
            object instance = validationContext.ObjectInstance;
            PropertyInfo instanceProperty = validationContext.ObjectType.GetProperty(Property);
            //no need to validate if there is no property to refer to
            if (instanceProperty == null)
                return ValidationResult.Success;

            object instanceValue = instanceProperty.GetValue(instance, null);
            //no need to validate if there is no value to check condition
            if (instanceValue == null)
                return ValidationResult.Success;

            try
            {
                condition = (bool)instanceValue;
                string stringValue = (string)value;

                if (condition == true && String.IsNullOrWhiteSpace(stringValue))
                    return new ValidationResult("required."); ;
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
