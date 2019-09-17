using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reflection;

namespace AvenueOne.Models
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class Person : BaseObservableModel, IPerson
    {
        private string _id;
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

        public Person()
        {
            this.Id = GenerateId();
        }

        #region Properties
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
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

        ////reference
        public virtual User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        //for XML
        //<Id></Id><FirstName></FirstName><MiddleName></MiddleName><LastName></LastName><MaidenName></MaidenName><Suffix></Suffix><Gender></Gender><CivilStatus></CivilStatus><Nationality></Nationality><BirthDate></BirthDate>


        #region MethodsAndOverrides

        public IPerson CopyPropertyValues()
        {
            return CopyPropertyValues(new Person());
        }
        public IPerson CopyPropertyValues(IPerson person)
        {
            List<PropertyInfo> propertyList = typeof(IPerson).GetProperties().Where(u => u.CanWrite && u.CanRead).ToList();

            foreach (PropertyInfo info in propertyList)
            {
                if (typeof(IPerson).GetProperty(info.Name) != null)
                    info.SetValue(person, info.GetValue(this));
            }
            return person;
        }
        private bool HasContent(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && !string.IsNullOrEmpty(content);
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
                this.CivilStatus.GetHashCode() ^
                (this.Nationality == null ? 0 : this.Nationality.GetHashCode()) ^
                (this.BirthDate == null ? 0 : this.BirthDate.GetHashCode());
        }
        #endregion
    }
}
