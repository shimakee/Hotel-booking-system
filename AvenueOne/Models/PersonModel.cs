using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AvenueOne.Models
{
    public class PersonModel : IPerson
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

        private bool HasContent(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && !string.IsNullOrEmpty(content);
        }

        public string FullName
        {
            get
            {
                StringBuilder fullname = new StringBuilder();
                if (HasContent(FirstName))
                    fullname.Append(FirstName);

                if (HasContent(MiddleName))
                    fullname.Append($" {MiddleName}");

                if (HasContent(MaidenName) && Gender == GenderType.Female)
                    fullname.Append($" {MaidenName}");

                if (HasContent(LastName))
                    fullname.Append($" {LastName}");

                return fullname.ToString();
            }
            set
            {
                if (FullName != value)
                    FullName = $"{FirstName} {LastName}";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is PersonModel))
                return false;

            PersonModel person = (PersonModel)obj;

            return this.FirstName == person.FirstName &&
                this.MiddleName == person.MiddleName &&
                this.LastName == person.LastName &&
                this.MaidenName == person.MaidenName &&
                this.Gender == person.Gender &&
                this.CivilStatus == person.CivilStatus &&
                this.Nationality == person.Nationality &&
                this.BirthDate == person.BirthDate;
        }

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() ^
                this.MiddleName.GetHashCode() ^
                this.LastName.GetHashCode() ^
                this.MaidenName.GetHashCode() ^
                this.Gender.GetHashCode() ^
                this.CivilStatus.GetHashCode() ^
                this.Nationality.GetHashCode() ^
                this.BirthDate.GetHashCode();
        }
    }
}
