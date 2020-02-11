using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities.Tools
{
    public class TimeSpanBeforeToday : ValidationAttribute
    {
        private DateTime Date;
        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public TimeSpanBeforeToday()
        {
            //this.Year = year;
            //this.Month = month;
            //this.Day = day;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if(value == null)
                    return ValidationResult.Success;

                Date = DateTime.Today;
                Date.AddDays(Seconds);
                Date.AddDays(Minutes);
                Date.AddDays(Hours);
                Date.AddDays(Days);
                Date.AddMonths(Months);
                Date.AddYears(Years);


                if ((DateTime)value < Date)
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
