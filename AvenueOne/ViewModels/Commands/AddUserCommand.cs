using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class AddUserCommand : ICommand
    {
        private IDisplayService _displayService;
        private IUnitOfWork _unitOfWork;
        public IRegistrationViewModel ViewModel { get; set; }
        public IUserViewModel User { get; set; }
        public IPersonViewModel Person { get; set; }

        public AddUserCommand(IDisplayService displayService, IUnitOfWork unitOfWork)
        {
            this._displayService = displayService;
            this._unitOfWork = unitOfWork;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //get user access based on account
            if (!ViewModel.UserAccount.IsAdmin)
                _displayService.ErrorDisplay("User is not allowed, only accounts with admin access are able to execute command.", "Access information");
            return ViewModel.UserAccount.IsAdmin;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (parameter == null)
                throw new NullReferenceException("The object to add cannot be null. Need source window, is admin, username, password, and password confirm as argument.");

                    //get parameters
                    object[] values = (object[])parameter;

                    PasswordBox passwordPasswordBox = (PasswordBox)values[0];
                    string password = passwordPasswordBox.Password;

                    PasswordBox passwordConfirmPasswordBox = (PasswordBox)values[1];
                    string passwordConfirm = passwordConfirmPasswordBox.Password;


                if (password == null || passwordConfirm == null)
                    throw new ArgumentNullException("Password and PasswordConfirm cannot be null.");
                if (User == null || Person == null)
                    throw new ArgumentNullException("User and person view model cannot be null, must assign valid view model to the properties.");

                User.Password = password;
                User.PasswordConfirm = passwordConfirm;

                int n = await AddUser(User, Person);

                if (n != 0)
                {
                    _displayService.MessageDisplay($"Added accoun:\n\nUsername: {User.Username}\nName:{Person.FullName}\n\nAffected rows:{n}.",
                                                    "Account added.");
                }
                else
                {
                    _displayService.MessageDisplay($"Info: Could not add {User.Username} belongin to {Person.FullName}.",
                                                    "Error on insert.");
                }

                if (ViewModel != null)
                    ViewModel.Window.Close();

            }
            catch (Exception ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error");
            }
        }

        private async Task<int> AddUser(IUserViewModel User, IPersonViewModel Person)
        {
            if (User == null || Person == null)
                throw new ArgumentNullException("User and person view model cannot be null.");
            if (!Person.IsValid)
                throw new ArgumentException("Invalid profile detais.");
            if (!User.IsValid)
                throw new ArgumentException("Invalid user details.");
            if (!User.IsValidProperty("Password") || !User.IsValidProperty("PasswordConfirm"))
                throw new ArgumentException("Invalid password or password confirmation.");

                User user = User.User as User;
                Person person = Person.Person as Person;
                user.Person = person;
                _unitOfWork.Users.Add(user);
                int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                if (n != 0)
                    OnUserAdded(user);

                return n;
                //return await _unitOfWork.CompleteAsync();
        }

        public event EventHandler<UserEventArgs> UserAdded;
        protected virtual void OnUserAdded(User user)
        {
            UserAdded?.Invoke(this, new UserEventArgs(user));
        }
    }
}
