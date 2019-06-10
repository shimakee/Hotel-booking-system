using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Models
{
    public class PersonModel : IPersonModel
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
