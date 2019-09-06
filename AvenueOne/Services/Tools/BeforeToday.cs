using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities.Tools
{
    public class BeforeToday : RequiredAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            try
            {
                DateTime date = (DateTime)value;
                return date <= DateTime.Today;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
