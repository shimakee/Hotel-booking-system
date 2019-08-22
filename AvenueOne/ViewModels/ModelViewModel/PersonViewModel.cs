using AvenueOne.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.ModelViewModel
{
    public class PersonViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public IPerson Person { get; private set; }
        public byte[] GenderValues { get { return (byte[])Enum.GetValues(typeof(GenderType)); } }
        public byte[] CivilStatusValues { get { return (byte[])Enum.GetValues(typeof(CivilStatusType)); } }

        public PersonViewModel(IPerson person)
        {
            this.Person = person;
        }

        public bool IsMaiden
        {
            get { return Person.Gender == GenderType.Female && Person.CivilStatus != CivilStatusType.Single; }
        }

        #region Properties
        [Required]
        public string Id
        {
            get { return Person.Id; }
            set
            {
                Person.Id = value;
                OnPropertyChanged();
            }
        }
        
        [Required(ErrorMessage = "required")]
        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "alphanumeric only.")]
        public string FirstName
        {
            get { return Person.FirstName; }
            set
            {
                //ValidateProperty(value, "FirstName");
                Person.FirstName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }


        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid format.")]
        public string MiddleName
        {
            get { return Person.MiddleName; }
            set
            {
                Person.MiddleName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }


        [Required(ErrorMessage = "required")]
        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid format.")]
        public string LastName
        {
            get { return Person.LastName; }
            set
            {
                Person.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid format.")]
        public string MaidenName
        {
            get { return Person.MaidenName; }
            set
            {
                Person.MaidenName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }
        
        public string FullName
        {
            get { return Person.FullName; }
            set
            {
                if (value != Person.FullName)
                    OnPropertyChanged();
            }
        }
        
        [RequiredEnum(ErrorMessage = "required")]
        [EnumDataType(typeof(GenderType))]
        public GenderType Gender
        {
            get { return Person.Gender; }
            set
            {
                Person.Gender = value;
                OnPropertyChanged();
                OnPropertyChanged("IsMaiden");
                OnPropertyChanged("MaidenName");
            }
        }

        [RequiredEnum(ErrorMessage = "required")]
        [EnumDataType(typeof(CivilStatusType))]
        public CivilStatusType CivilStatus
        {
            get { return Person.CivilStatus; }
            set
            {
                Person.CivilStatus = value;
                OnPropertyChanged();
                OnPropertyChanged("IsMaiden");
                OnPropertyChanged("MaidenName");
            }
        }

        [StringLength(30, ErrorMessage = "must be between 5 to 30 characters in length.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid username format.")]
        public string Nationality
        {
            get { return Person.Nationality; }
            set
            {
                Person.Nationality = value;
                OnPropertyChanged();
            }
        }


        [DataType(DataType.Date)]
        public DateTime? BirthDate
        {
            get { return Person.BirthDate; }
            set
            {
                Person.BirthDate = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region IDataErrorInfo
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        string IDataErrorInfo.this[string property]
        {
            get
            {
                return ValidateProperty(property);
            }
        }
        #endregion

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Validation
        private string ValidateProperty(string property)
        {
            var context = new ValidationContext(this, null, null) { MemberName = property };
            var result = new List<ValidationResult>();
            var value = this.GetType().GetProperty(property).GetValue(this);

            bool isValid = Validator.TryValidateProperty(value, context, result);

            //special case for maiden name as it needs to be both female, and not single before it is required.
            if (CivilStatus != CivilStatusType.Single && Gender == GenderType.Female && property == "MaidenName")
            {
                if(String.IsNullOrWhiteSpace(MaidenName))
                    return "required";
                if (MaidenName.Length < 2 || MaidenName.Length > 20)
                    return "must be less that 20 chars.";
            }

            if (!isValid)
                return result.First().ErrorMessage;
            return null;
        }
        #endregion
    }
}
