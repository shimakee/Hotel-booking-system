using AvenueOne.Interfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
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
    public class PersonViewModel : BaseObservableModel, IPersonViewModel //TODO : ADd User property; - also in settings
    {
        private IPerson _person;
        public byte[] GenderValues { get { return (byte[])Enum.GetValues(typeof(GenderType)); } }
        public byte[] CivilStatusValues { get { return (byte[])Enum.GetValues(typeof(CivilStatusType)); } }

        public PersonViewModel(IPerson person)
        {
            _person = person;
        }

        #region Properties
        public IPerson Person
        {
            get { return _person; }
            set
            {
                _person = value;

                OnPropertyChanged();
                OnPropertyChanged("Id");
                OnPropertyChanged("FirstName");
                OnPropertyChanged("MiddleName");
                OnPropertyChanged("LastName");
                OnPropertyChanged("Suffix");
                OnPropertyChanged("MaidenName");
                OnPropertyChanged("IsNotMaiden");
                OnPropertyChanged("Fullname");
                OnPropertyChanged("Gender");
                OnPropertyChanged("CivilStatus");
                OnPropertyChanged("Nationality");
                OnPropertyChanged("BirthDate");
            }
        }

        //public IUser User
        //{
        //    get { return _person.User; }
        //    set
        //    {
        //        _person.User = value;
        //        OnPropertyChanged();
        //    }
        //}

        public string Id
        {
            get { return _person.Id; }
            set
            {
                _person.Id = value;
                OnPropertyChanged();
            }
        }
        
        [Required(ErrorMessage = "required")]
        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "alphanumeric only.")]
        public string FirstName
        {
            get { return _person.FirstName; }
            set
            {
                //ValidateProperty(value, "FirstName");
                _person.FirstName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }


        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid format.")]
        public string MiddleName
        {
            get { return _person.MiddleName; }
            set
            {
                _person.MiddleName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }


        [Required(ErrorMessage = "required")]
        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid format.")]
        public string LastName
        {
            get { return _person.LastName; }
            set
            {
                _person.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid format.")]
        public string Suffix
        {
            get { return _person.Suffix; }
            set
            {
                _person.Suffix = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        //Person.CivilStatus != CivilStatusType.Single && Person.Gender == GenderType.Female; 
        [RequiredIf("IsNotMaiden", ErrorMessage ="requireds.")]
        [StringLength(30, ErrorMessage = "30 chars max.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid format.")]
        public string MaidenName
        {
            get { return _person.MaidenName; }
            set
            {
                _person.MaidenName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        public bool IsNotMaiden
        {
            get
            {
                return _person.CivilStatus != CivilStatusType.Single && _person.Gender == GenderType.Female;
            }
        }
        
        public string FullName
        {
            get { return _person.FullName; }
            set
            {
                if (value != _person.FullName)
                    OnPropertyChanged();
            }
        }
        
        [RequiredEnum(ErrorMessage = "required")]
        [EnumDataType(typeof(GenderType))]
        public GenderType Gender
        {
            get { return _person.Gender; }
            set
            {
                _person.Gender = value;
                OnPropertyChanged();
                OnPropertyChanged("IsNotMaiden");
                OnPropertyChanged("MaidenName");
            }
        }

        [RequiredEnum(ErrorMessage = "required")]
        [EnumDataType(typeof(CivilStatusType))]
        public CivilStatusType CivilStatus
        {
            get { return _person.CivilStatus; }
            set
            {
                _person.CivilStatus = value;
                OnPropertyChanged();
                OnPropertyChanged("IsNotMaiden");
                OnPropertyChanged("MaidenName");
            }
        }

        [StringLength(30, ErrorMessage = "must be between 5 to 30 characters in length.")]
        [RegularExpression(@"^([A-z ])*$", ErrorMessage = "invalid username format.")]
        public string Nationality
        {
            get { return _person.Nationality; }
            set
            {
                _person.Nationality = value;
                OnPropertyChanged();
            }
        }
        
        [BeforeToday(ErrorMessage ="before today.")]
        [DataType(DataType.Date, ErrorMessage ="must be date.")]
        [Required(ErrorMessage ="required.")]
        public DateTime? BirthDate
        {
            get { return _person.BirthDate; }
            set
            {
                _person.BirthDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
