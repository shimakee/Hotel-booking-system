using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.ModelViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public IPerson Person { get; private set; }
        public byte[] GenderValues { get { return (byte[])Enum.GetValues(typeof(GenderType));  } }
        public byte[] CivilStatusValues { get { return (byte[])Enum.GetValues(typeof(CivilStatusType)); } }

        public PersonViewModel(IPerson person)
        {
            this.Person = person;
        }

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
            set { Person.FirstName = value;
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

        public bool IsMaiden
        {
            get { return Person.Gender == GenderType.Female && Person.CivilStatus != CivilStatusType.Single; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
