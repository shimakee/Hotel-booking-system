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

namespace AvenueOne.ViewModels.WindowsViewModels
{
    class RegistrationWindowViewModel : IRegistrationViewModel
    {
        private IUser _userAccount;
        public ICommand AddUserCommand { get; private set; }
        private IUserValidator _userModelValidator;
        private IUnitOfWork _unitOfWork; 


        public RegistrationWindowViewModel()
        {
            this.AddUserCommand = new AddUserCommand(this);
            //TODO: userAccount info should be gotten somewhere else, application resources perhaps or in settings
            //this is to access if account is admin or not to be able to execute addusercommand
            this._userAccount = new UserModel("sample user", "sample password", true);
        }

        public RegistrationWindowViewModel(IUserValidator userModelValidator, IUnitOfWork unitOfWork)
            : this()
        {
            this._userModelValidator = userModelValidator;
            this._unitOfWork = unitOfWork;
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
        }

        public bool AccountIsAdmin
        {
            get { return _userAccount.IsAdmin; }
        }

        public void AddUser(Window sourceWindow, IUser userModel, string passwordConfirm)
        {
            if (userModel == null)
                throw new ArgumentNullException("User cannot be null.");
            if (sourceWindow == null)
                throw new ArgumentNullException("SourceWindow cannot be null.");
            if (passwordConfirm == null)
                throw new ArgumentNullException("PasswordConfirm cannot be null.");

            //validate user
           bool isValidUserModel = _userModelValidator.Validate(userModel);
            if (!isValidUserModel)
            {
                MessageBox.Show("Invalid input on Username or Password.");
                return;
            }

            //validate password confirmation
            bool isValidPassword = _userModelValidator.ValidatePassword(passwordConfirm);
            if(!isValidPassword || passwordConfirm != userModel.Password)
            {
;                MessageBox.Show("Invalid input on Password confirmation.");
                return;
            }

            //does user exist
            IUser userExist = _unitOfWork.Users.Find(user => user.Equals(userModel));// TODO: should be registrationProcessor with method add account?...
            
            //failed message
            if(userExist != null)
            {
                MessageBox.Show($"Failed to register {userModel.Username}, username already exist");
            }
            else
            {
                //add user
                if (userExist == null)
                    _unitOfWork.Users.Add(userModel);

                //success message
                StringBuilder message = new StringBuilder();
                message.Append("Registered ");

                if (userModel.IsAdmin)
                    message.Append("admin ");
                message .Append($"user {userModel.Username} with password {userModel.Password}");
                MessageBox.Show(message.ToString());

                //close source window
                sourceWindow.Close();
            }


        }
    }
}
