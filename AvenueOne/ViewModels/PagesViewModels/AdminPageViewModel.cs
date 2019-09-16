using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Properties;
using AvenueOne.Utilities;
using AvenueOne.Utilities.Tools;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class AdminPageViewModel : WindowViewModel, IRegistrationParent, IProfileEditViewModel
    {
        //private ObservableCollection<User> _usersList;
        public ObservableCollection<User> UsersList { get; set; }
        //{
        //    get { return _usersList; }
        //    set
        //    {
        //        _usersList = value;
        //    }
        //}
        public RegisterUserCommand RegisterUserCommand { get; private set; }
        public EditProfileCommand EditProfileCommand { get; set; }
        public IPersonViewModel Person { get;  set; }
        public IUserViewModel User { get; set; }
        private User _account;

        public User Account
        {
            get
            {
                return _account;
            }
            set {
                _account = value;
                //for editing purposes.
                User.User = value;
                Person.Person = value.Person;
            }
        }

        AdminPageViewModel(Window window)
            :base(window)
        {
        }

        public AdminPageViewModel(Window window, RegisterUserCommand registerUserCommand, EditProfileCommand editProfileCommand, IUserViewModel userViewModel, IPersonViewModel personViewModel, ObservableCollection<User> usersList)
            : this(window)
        {
            this.User = userViewModel;
            this.Person = personViewModel;
            //registerUserCommand.ViewModel = this;
            this.RegisterUserCommand = registerUserCommand;
            this.RegisterUserCommand.ViewModel = this;
            this.UsersList = usersList;
            this.EditProfileCommand = editProfileCommand;
            this.EditProfileCommand.ViewModel = this;

        }

        public void OnUserAdded(object source, UserEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("Event args must not be null.");

            if(UsersList != null)
                UsersList.Add(e.User);
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged([CallerMemberName] String property = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        //}
    }
}
