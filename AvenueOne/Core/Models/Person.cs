using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AvenueOne.Models
{
    public class Person : IPerson
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public string Suffix { get; set; }
        public GenderType Gender { get; set; }
        public CivilStatusType CivilStatus { get; set; }
        public string Nationality { get; set; }
        public DateTime? BirthDate { get; set; }
        public virtual User User { get; set; }

        public Person()
        {
            this.Id = GenerateId();
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

                if (HasContent(Suffix))
                    fullname.Append($" {Suffix}");

                return fullname.ToString();
            }
            set
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

                if (HasContent(Suffix))
                    fullname.Append($" {Suffix}");

                //assign
                if (FullName != value)
                    FullName = fullname.ToString();
            }
        }

        private bool HasContent(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && !string.IsNullOrEmpty(content);
        }

        private string GenerateId()
        {
            return GenerateId(32);
        }

        private string GenerateId(int length)
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Person))
                return false;
            
            Person person = (Person)obj;
            
            return this.FullName == person.FullName &&
                this.Gender == person.Gender &&
                this.BirthDate.GetValueOrDefault().Year == person.BirthDate.GetValueOrDefault().Year &&
                this.BirthDate.GetValueOrDefault().Month == person.BirthDate.GetValueOrDefault().Month &&
                this.BirthDate.GetValueOrDefault().Day == person.BirthDate.GetValueOrDefault().Day;
        }

        public override int GetHashCode()
        {
            return
                this.FullName.GetHashCode() ^
                this.Gender.GetHashCode() ^
                this.CivilStatus.GetHashCode()^
                (this.Nationality == null ? 0 : this.Nationality.GetHashCode()) ^
                (this.BirthDate == null ? 0 : this.BirthDate.GetHashCode());
        }
    }
}
