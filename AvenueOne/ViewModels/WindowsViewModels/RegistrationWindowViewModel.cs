using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using AvenueOne.Properties;
using System.ComponentModel;
using AvenueOne.ViewModels.ModelViewModel;
using System.Windows.Controls;
using AvenueOne.Interfaces.ViewModelInterfaces;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class RegistrationWindowViewModel : WindowViewModel, IRegistrationViewModel
    {
        private IUnitOfWork _unitOfWork;
        public ICommand AddUserCommand { get; private set; }
        public IUserViewModel User { get; private set; }
        public IPersonViewModel Person { get; private set; }

        RegistrationWindowViewModel(Window window)
            :base (window)
        {
            this.AddUserCommand = new AddUserCommand(this);
        }

        public RegistrationWindowViewModel(Window registrationWindow, IUnitOfWork unitOfWork, IUserViewModel userViewModel, IPersonViewModel personViewModel)
            : this(registrationWindow)
        {
            this._unitOfWork = unitOfWork;
            this.User = userViewModel;
            this.Person = personViewModel;
        }

        public void AddUser(Window sourceWindow, string password, string passwordConfirm)
        {
            if (password == null)
                throw new ArgumentNullException("Password cannot be null.");
            if (sourceWindow == null)
                throw new ArgumentNullException("SourceWindow cannot be null.");
            if (passwordConfirm == null)
                throw new ArgumentNullException("PasswordConfirm cannot be null.");

            if (!Person.IsValid)
            {
                MessageBox.Show("Invalid Please try again.");
            }
            else
            {
                // add user here
                MessageBox.Show($"Username: {User.Username} is a {Person.CivilStatus} {Person.Gender} with Fullname {Person.FullName} born on {Person.BirthDate}");
            }
        }

        public event EventHandler<UserEventArgs> UserAdded;

        public  void OnUserAdded(IUser user)
        {
            if (UserAdded != null)
                UserAdded(this, new UserEventArgs(user));
        }
    }

    public class UserEventArgs : EventArgs
    {
        public IUser User { get; set; }

        public UserEventArgs(IUser user)
        {
            User = user;
        }
    }
}
