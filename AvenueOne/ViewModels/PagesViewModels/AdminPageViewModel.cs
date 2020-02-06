using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.WindowCommands;
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
            public ObservableCollection<User> UsersList { get; set; } 
            private IPerson _profile;
            public IPerson Profile
            {
                get { return _profile; }
                set
                {
                    _profile = value;
                    OnPropertyChanged();
                }
            }
        private IUser _account;
        public IUser Account
        {
            get { return _account; }
            set { _account = value;
                OnPropertyChanged();
            }
        }

        public IBaseObservableViewModel<Customer> CustomerTab { get; set; }
        public IRoomTabViewModel RoomTab { get; set; }

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
                    //to separate editing... conserve the original values.
                    if(value != null)
                    {
                        _user = value;
                        Profile = value.Person.DeepCopy();
                        Account = value.DeepCopy();
                    }
                    OnPropertyChanged();
                }
            }
        #endregion


        #region Constructor

            AdminPageViewModel(Window window, BaseWindowCommand closeWindowCommand)
                :base(window, closeWindowCommand)
            {
                Account = new User();
                Profile = new Person();
            }

            public AdminPageViewModel(Window window, 
                                                            BaseWindowCommand closeWindowCommand, 
                                                            RegisterUserCommand registerUserCommand, 
                                                            EditUserProfileCommand editProfileCommand, 
                                                            RemoveUserCommand removeUserCommand, 
                                                            IUser user, 
                                                            ObservableCollection<User> usersList,
                                                            //ICustomerTabViewModel customerTab,
                                                            IBaseObservableViewModel<Customer> customerTab,
                                                            IRoomTabViewModel roomTab)
                : this(window, closeWindowCommand)
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
                this.RoomTab = roomTab;

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
