using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities.Tools
{
    public class Generator
    {
        public static string GenerateId()
        {
            return GenerateId(32);
        }

        public static string GenerateId(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("Id length cannot be less than 1 in length.");

            decimal d = length / 32;
            int repeat = (int)Math.Floor(d);
            StringBuilder Id = new StringBuilder();

            if (repeat > 0)
                for (int i = 0; i < repeat; i++)
                {
                    Id.Append(Guid.NewGuid().ToString("N"));
                }

            return Id.ToString(0, length);
        }
    }
}
