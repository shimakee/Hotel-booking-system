using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
using AvenueOne.Utilities;
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
    public class LoginWindowViewModel: ILoginViewModel
    {
        public IUser UserAccount { get; private set; } //TODO should be on settongs or app resource
        //private IUser _userModel; 
        public ICommand LoginCommand { get; private set; }
        private ILoginProcessor _loginProcessor;
        private IUserValidator _userModelValidator;

        public LoginWindowViewModel()
        {
            LoginCommand = new LoginCommand(this); //  how to decouple?
        }

        public LoginWindowViewModel(IUser user, ILoginProcessor loginProcessor, IUserValidator userModelValidator)
            :this()
        {
            this.UserAccount = user;
            this._loginProcessor = loginProcessor;
            this._userModelValidator = userModelValidator;
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
        }

        //user this for whatever features, but not login as username is passed as a parameter
        public string Username
        {
            get { return UserAccount.Username; }
            set { UserAccount.Username = value; }
        }

        public void Login(Window sourceWindow, string username, string password)
        {
            //validate Input here
            bool isValidUsername = _userModelValidator.ValidateUsername(username);
            bool isValidPassword = _userModelValidator.ValidatePassword(password);
            
            if (!isValidPassword && !isValidUsername)
            {
                MessageBox.Show("The username and password are using invalid characters.");
            }
            else if (!isValidUsername)
            {
                MessageBox.Show("The username is using invalid characters.");
            }
            else if(!isValidPassword)
            {
                MessageBox.Show("The password is using invalid characters.");
            }
            else //process login using login class
            {
                bool isValidLogin = _loginProcessor.Login(username, password);

                if (isValidLogin)
                {
                    MessageBox.Show($"Welcome {username}");

                    //show main window
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    //close source window
                    sourceWindow.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password or account does not exist");
                }
            }
        }

    }
}
