using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AvenueOne.Utilities.Tools
{
    public class RequiredEnum : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            Type enumType = value.GetType();

            return enumType.IsEnum && Enum.IsDefined(enumType, value);
        }
    }
}
