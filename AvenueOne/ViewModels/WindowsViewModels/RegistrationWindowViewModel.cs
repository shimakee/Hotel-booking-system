using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    class RegistrationWindowViewModel : IRegistrationViewModel
    {
        private IUserModel _userAccount;
        //private IUserModel _userModel;
        //private IPersonModel _personModel;
        //private string _passwordConfirm;
        public ICommand AddUserCommand { get; private set; }
        private IUserModelValidator _userModelValidator;
        private IAddUserProcessor _addUserProcessor;

        public RegistrationWindowViewModel()
        {
            this.AddUserCommand = new AddUserCommand(this);
            //TODO: userAccount info should be gotten somewhere else, application resources perhaps or in settings
            //this is to access if account is admin or not to be able to execute addusercommand
            this._userAccount = new UserModel("sample user", "sample password", true);
        }

        //public RegistrationWindowViewModel(IUserModel user, IPersonModel person, IUserModelValidator userModelValidator)
        public RegistrationWindowViewModel(IUserModelValidator userModelValidator, IAddUserProcessor addUserProcessor)
            : this()
        {
            //this._userModel = user;
            //this._personModel = person;
            this._userModelValidator = userModelValidator;
            this._addUserProcessor = addUserProcessor;
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
        }

        public bool AccountIsAdmin
        {
            get { return _userAccount.IsAdmin; }
        }

        //public bool IsAdmin
        //{
        //    get { return _userModel.IsAdmin; }
        //    set { _userModel.IsAdmin = value; }
        //}

        //public string Username
        //{
        //    get { return _userModel.Username; }
        //    set { _userModel.Username = value; }
        //}

        //public string Password
        //{
        //    private get { return _userModel.Password; }
        //    set { _userModel.Password = value; }
        //}

        //public string PasswordConfirm
        //{
        //    private get { return _passwordConfirm;  }
        //    set { _passwordConfirm = value;  }
        //}

        //public IUserModel AddUser(IUserModel userModel)
        //{
        //    MessageBox.Show($"Registered {userModel.Username}");

        //    return userModel;
        //}

        public void AddUser(Window sourceWindow, IUserModel userModel, string passwordConfirm)
        {
            if (userModel == null)
                throw new ArgumentNullException("Username cannot be null.");
            if (sourceWindow == null)
                throw new ArgumentNullException("SourceWindow cannot be null.");
            if (passwordConfirm == null)
                throw new ArgumentNullException("PasswordConfirm cannot be null.");

            //validate user
           bool isValidUserModel = _userModelValidator.ValidateUserModel(userModel);
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

            //process adding user here
            IUserModel addedUser = _addUserProcessor.AddUser(userModel);

            //failed message
            if (addedUser == null)
                MessageBox.Show($"Failed to register {userModel.Username}");

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
