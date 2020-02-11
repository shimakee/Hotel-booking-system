using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reflection;
using AvenueOne.Utilities.Tools;
using AvenueOne.Core.Models.CustomDataAnnotations;

namespace AvenueOne.Models
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class Person : BaseObservableModel<Person>, IPerson
    {
        #region Constructors

            public Person()
                :base()
            {
            }

            public Person(string firstname)
                : this()
            {
                this.FirstName = firstname;
            }

            public Person (string firstname, string lastname)
                : this(firstname)
            {
                this.LastName = lastname;
            }
        #endregion

        #region Properties

        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _maidenName;
        private string _suffix;
        private GenderType _gender;
        private CivilStatusType _civilStatus;
        private string _nationality;
        private DateTime? _birthDate;
        private User _user;
        private Customer _customer;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }
        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }
        public string MaidenName
        {
            get { return _maidenName; }
            set
            {
                _maidenName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }
        public string Suffix
        {
            get { return _suffix; }
            set
            {
                _suffix = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");

            }
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
                if (FullName != value)
                    OnPropertyChanged();
            }
        }
        public GenderType Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged();
                OnPropertyChanged("IsMaiden");
                OnPropertyChanged("WasMaiden");
                OnPropertyChanged("MaidenName");
            }
        }
        public CivilStatusType CivilStatus
        {
            get { return _civilStatus; }
            set
            {
                _civilStatus = value;
                OnPropertyChanged();
                OnPropertyChanged("IsMaiden");
                OnPropertyChanged("WasMaiden");
                OnPropertyChanged("MaidenName");
            }
        }
        public string Nationality
        {
            get { return _nationality; }
            set
            {
                _nationality = value;
                OnPropertyChanged();
            }
        }

        [TimeSpanBeforeToday(Years =18)]
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Tools
        private bool HasContent(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && !string.IsNullOrEmpty(content);
        }
        public byte[] GenderValues { get { return (byte[])Enum.GetValues(typeof(GenderType)); } }
        public byte[] CivilStatusValues { get { return (byte[])Enum.GetValues(typeof(CivilStatusType)); } }
        public bool IsMaiden
        {
            get
            {
                return CivilStatus == CivilStatusType.Single && Gender == GenderType.Female;
            }
        }
        public bool WasMaiden
        {
            get
            {
                return CivilStatus != CivilStatusType.Single && Gender == GenderType.Female;
            }
        }
        #endregion

        #region Reference
            public virtual User User
            {
                get { return _user; }
                set
                {
                    _user = value;
                    OnPropertyChanged();
                }
            }

            public virtual Customer Customer
            {
                get { return _customer; }
                set
                {
                    _customer = value;
                    OnPropertyChanged();
                }
            }
        #endregion


        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Person))
                return false;

            Person person = (Person)obj;

            return this.Id == person.Id &&
                this.FullName == person.FullName &&
                this.Gender == person.Gender &&
                this.BirthDate.GetValueOrDefault().Year == person.BirthDate.GetValueOrDefault().Year &&
                this.BirthDate.GetValueOrDefault().Month == person.BirthDate.GetValueOrDefault().Month &&
                this.BirthDate.GetValueOrDefault().Day == person.BirthDate.GetValueOrDefault().Day;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
                //this.FullName.GetHashCode() ^
                //this.Gender.GetHashCode() ^
                //this.CivilStatus.GetHashCode() ^
                //(this.Nationality == null ? 0 : this.Nationality.GetHashCode()) ^
                //(this.BirthDate == null ? 0 : this.BirthDate.GetHashCode());
        }
        #endregion
    }
}
