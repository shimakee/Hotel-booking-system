using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Models
{
    public class PersonModel //need to interface this
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public GenderType Gender { get; set; }
        public CivilStatusType CivilStatus { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }

        public enum GenderType
        {
            Male = 1,
            Female = 2
        }

        public enum CivilStatusType
        {
            Single = 1,
            Married = 2,
            Widowed = 3,
            Annuled = 4,
            Divorced = 5
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
            set
            {
                if (FullName != value)
                    FullName = $"{FirstName} {LastName}";
            }
        }
    }
}
