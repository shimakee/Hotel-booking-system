using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.ModelViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public IUser User { get; private set; }

        public UserViewModel(IUser user)
        {
            User = user;
        }

        public string Id
        {
            get { return User.Id; }
            set { User.Id = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return User.Username; }
            set { User.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return User.Password; }
            set { User.Password = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin
        {
            get { return User.IsAdmin; }
            set { User.IsAdmin = value;
                OnPropertyChanged();
            }
        }

        public void Save()
        {
            throw new NotImplementedException("There is no save method yet.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] String property = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
