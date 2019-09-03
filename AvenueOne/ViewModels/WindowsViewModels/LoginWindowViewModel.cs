using AvenueOne.Interfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
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
        public IUserViewModel User { get;  private set; }
        public ICommand LoginCommand { get; private set; }
        private ILoginProcessor _loginProcessor;

        LoginWindowViewModel()
        {
            LoginCommand = new LoginCommand(this); //  how to decouple?
        }
        
        public LoginWindowViewModel(IUser user, ILoginProcessor loginProcessor, IUserViewModel userViewModel)
            : this()
        {
            this.UserAccount = user;
            this._loginProcessor = loginProcessor;
            this.User = userViewModel;
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
        }

        public void Login(Window sourceWindow, string username, string password)
        {
            if (sourceWindow == null)
                throw new ArgumentNullException("Source window cannot be null.");
            //if (String.IsNullOrWhiteSpace(username))
            if (username == null)
                    throw new ArgumentNullException("Username cannot be null.");
            //if (String.IsNullOrWhiteSpace(password))
            if (password == null)
                    throw new ArgumentNullException("Password cannot be null.");

            User.Username = username;//no need to assign since using binding, but just to be sure.
            User.Password = password;
            bool isValidUsername = User.IsValidProperty("Username"); //just redundancy.
            bool isValidPassword = User.IsValidProperty("Password");

            if (User.IsValid && isValidPassword && isValidUsername)
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
            else
            {
                MessageBox.Show("Invalid Input on username or password.");
            }
        }

    }
}
