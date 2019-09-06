using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Converters
{
    public class UserConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value is string)
            {
                string[] parts = ( (string)value).Split(new char[] { ',' });
                IUser user = new User();
                user.Id = parts[0].Length > 0 ? parts[0]: null;
                user.Username = parts[1].Length > 0 ? parts[1] : null;
                user.Password = parts[2].Length > 0 ? parts[2] : null;
                user.IsAdmin = Convert.ToBoolean(parts[3]);
                return user;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if(destinationType == typeof(string))
            {
                IUser user = value as User;
                return string.Format($"{user.Id},{user.Username},{user.Password},{user.IsAdmin}");
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
