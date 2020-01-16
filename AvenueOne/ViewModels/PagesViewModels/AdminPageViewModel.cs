using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class AdminPageViewModel : WindowViewModel, IUserProfileEditViewModel, INotifyPropertyChanged
    {

        #region Commands
            public RegisterUserCommand RegisterUserCommand { get; private set; }
            public EditUserProfileCommand EditProfileCommand { get; set; }
            public RemoveUserCommand RemoveUserCommand { get; set; }

        #endregion

        #region Properties
            private IUser _user;
            public ObservableCollection<User> UsersList { get; set; } //TODO check to see if you can use interface
            //private ICustomerTabViewModel _customerTab;
            public IPerson Profile { get; set; }
            public IUser Account { get; set; }
            public ICustomerTabViewModel CustomerTab { get; set; }

            private bool _isPasswordIncluded;
            public bool IsPasswordIncluded
            {
                get { return _isPasswordIncluded; }
                set { _isPasswordIncluded = value;
                    OnPropertyChanged();
                }
            }

            public IUser User
            {
                get { return _user; }
                set {
                        _user = value;
                    //to separate editing... conserve the original values.
                    if(value != null)
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


        #region Constructor

            AdminPageViewModel(Window window)
                :base(window)
            {
                Account = new User();
                Profile = new Person();
            }

            public AdminPageViewModel(Window window, 
                                                            RegisterUserCommand registerUserCommand, 
                                                            EditUserProfileCommand editProfileCommand, 
                                                            RemoveUserCommand removeUserCommand, 
                                                            IUser user, 
                                                            ObservableCollection<User> usersList,
                                                            ICustomerTabViewModel customerTab)
                : this(window)
            {

                if (user.Person == null)
                    throw new ArgumentNullException("Person object inside user of type IUser cannot be null.");

                this.User = user;
                this.UsersList = usersList;
                this.RegisterUserCommand = registerUserCommand;
                this.RegisterUserCommand.ViewModel = this;
                this.EditProfileCommand = editProfileCommand;
                this.EditProfileCommand.ViewModel = this;
                this.RemoveUserCommand = removeUserCommand;
                this.RemoveUserCommand.ViewModel = this;
                this.CustomerTab = customerTab;

        }
        #endregion



        #region Utilities

            public void OnUserAdded(object source, UserEventArgs e)
            {
                if (e == null)
                {
                    throw new ArgumentNullException("Event args must not be null.");
                }
                else
                {
                    UsersList.Add(e.User);
                }

            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        #endregion
    }
}
