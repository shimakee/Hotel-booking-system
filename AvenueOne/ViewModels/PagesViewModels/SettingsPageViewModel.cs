using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class SettingsPageViewModel : WindowViewModel, IProfileEditViewModel, INotifyPropertyChanged
    {
        #region Ctor
        SettingsPageViewModel(Window window)
            : base(window)
        {
        }

        public SettingsPageViewModel(Window window,
                                                        EditProfileCommand editProfileCommand,
                                                        IUser user)
            : this(window)
        {

            //commented out because it causes an error when there is no user in the database
            //and a temporary null user is granted access to enable to create a user account.
            //if (user.Person == null)
            //    throw new ArgumentNullException("Person object inside user of type IUser cannot be null.");

            this.User = user;
            this.EditProfileCommand = editProfileCommand;
            this.EditProfileCommand.ViewModel = this;
        }
        #endregion

        #region Properties

        public EditProfileCommand EditProfileCommand { get; set; }
        private IUser _user;
        public IUser Account { get; set; }
        public IPerson Profile { get; set; }
        public IUser User
        {
            get { return _user; }
            set
            {
                _user = value;
                //to separate editing... conserve the original values.
                if (value != null)
                {
                    Profile = value.Person.CopyPropertyValues();
                    Account = value.CopyPropertyValues();
                }
                else
                {
                    Account = null;
                    Profile = null;
                }
                OnPropertyChanged();
                OnPropertyChanged("Account");
                OnPropertyChanged("Profile");
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
