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

        public void AddUser(string password, string passwordConfirm)
        {
            if (password == null || passwordConfirm == null)
                throw new ArgumentNullException("Password and PasswordConfirm cannot be null.");

            User.Password = password;
            User.PasswordConfirm = passwordConfirm;

            if (!Person.IsValid || !User.IsValid || !User.IsValidProperty("Password") || !User.IsValidProperty("PasswordConfirm"))
            {
                MessageBox.Show("Invalid entry please try again.");
            }
            else
            {
                User user = User.User as User;
                Person person = Person.Person as Person;
                user.Person = person;
                _unitOfWork.Users.Add(user);
                int n = _unitOfWork.Complete();

                // add user here
                MessageBox.Show($"Added {n} Account with Username: {user.Username} belonging to {person.FullName}.",
                                                "User added.", 
                                                MessageBoxButton.OK, 
                                                MessageBoxImage.Information);

                Window.Close();
            }
        }

        //public event EventHandler<UserEventArgs> UserAdded;

        //public  void OnUserAdded(IUser user)
        //{
        //    if (UserAdded != null)
        //        UserAdded(this, new UserEventArgs(user));
        //}
    }

    //public class UserEventArgs : EventArgs
    //{
    //    public IUser User { get; set; }

    //    public UserEventArgs(IUser user)
    //    {
    //        User = user;
    //    }
    //}
}
