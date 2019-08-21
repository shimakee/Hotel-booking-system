using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.ModelViewModel
{
    public class PersonViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public IPerson Person { get; private set; }
        public byte[] GenderValues { get { return (byte[])Enum.GetValues(typeof(GenderType));  } }
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
        public string Id
        {
            get { return Person.Id; }
            set { Person.Id = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return Person.FirstName; }
            set {
                Person.FirstName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        public string MiddleName
        {
            get { return Person.MiddleName; }
            set { Person.MiddleName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        public string LastName
        {
            get { return Person.LastName; }
            set { Person.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        public string MaidenName
        {
            get { return Person.MaidenName; }
            set { Person.MaidenName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

        public string FullName
        {
            get { return Person.FullName; }
            set
            {
                if(value != Person.FullName)
                    OnPropertyChanged();
            }
        }

        public GenderType Gender
        {
            get { return Person.Gender; }
            set {
                Person.Gender = value; 
                OnPropertyChanged();
                OnPropertyChanged("IsMaiden");
            }
        }

        public CivilStatusType CivilStatus
        {
            get { return Person.CivilStatus; }
            set { Person.CivilStatus = value;
                OnPropertyChanged();
                OnPropertyChanged("IsMaiden");
            }
        }

        public string Nationality
        {
            get { return Person.Nationality; }
            set { Person.Nationality = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get { return Person.BirthDate; }
            set { Person.BirthDate = value;
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
            string error = null;

            switch (property)
            {
                case "FirstName":
                    if (!String.IsNullOrWhiteSpace(FirstName))
                    {
                        Match match = Regex.Match(FirstName, @"^\w{4}\d{6,7}$");
                        if (!match.Success)
                            error = "invalid first name format";
                    }
                    else
                    {
                        error = "Invalid first name.";
                    }
                    break;

                default:
                    break;
            }

            return error;
        }
        #endregion
    }
}
