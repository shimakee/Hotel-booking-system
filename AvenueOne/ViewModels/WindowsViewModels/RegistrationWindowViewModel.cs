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
    class RegistrationWindowViewModel : IRegistrationViewModel
    {
        public IUser UserAccount { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        private IUnitOfWork _unitOfWork;
        public IUserViewModel User { get; private set; }
        public IPersonViewModel Person { get; private set; }

        public RegistrationWindowViewModel()
        {
            this.AddUserCommand = new AddUserCommand(this);
            this.UserAccount = Settings.Default["UserAccount"] as IUser;
            this.User = new UserViewModel(new UserModel());
            this.Person = new PersonViewModel(new PersonModel());
        }

        public RegistrationWindowViewModel(IUnitOfWork unitOfWork)
            : this()
        {
            this._unitOfWork = unitOfWork;
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
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
                MessageBox.Show($"Username: {User.Username} is a {Person.CivilStatus} {Person.Gender} with Fullname {Person.FullName} born on {Person.BirthDate}");
            }

            //            //validate username
            //           bool isValidUsername = _userModelValidator.ValidateUsername(User.Username);
            //            if (!isValidUsername)
            //            {
            //                MessageBox.Show("Invalid input on username.");
            //                return;
            //            }

            //            //validate password & password confirmation
            //            bool isValidPassword = _userModelValidator.ValidatePassword(password) && _userModelValidator.ValidatePassword(passwordConfirm);
            //            if(!isValidPassword || passwordConfirm != password)
            //            {
            //;                MessageBox.Show("Invalid input or missmatched password  or password confirmation.");
            //                return;
            //            }
            //            else
            //            {
            //                User.Password = password;
            //            }

            //            //does user exist
            //            IUser userExist = _unitOfWork.Users.Find(user => user.Equals(User));// TODO: should be registrationProcessor with method add account?...

            //            //failed message
            //            if(userExist != null)
            //            {
            //                MessageBox.Show($"Failed to register {User.Username}, username already exist");
            //            }
            //            else
            //            {
            //                //add user
            //                if (userExist == null)
            //                    _unitOfWork.Users.Add(User.User);

            //                //success message
            //                StringBuilder message = new StringBuilder();
            //                message.Append("Registered ");

            //                if (User.IsAdmin)
            //                    message.Append("admin ");
            //                message .Append($"user {User.Username} with password {User.Password}");
            //                MessageBox.Show(message.ToString());

            //                //notify parent window
            //                OnUserAdded(User.User);

            //                //close source window
            //                sourceWindow.Close();
            //            }
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
