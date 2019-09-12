using AvenueOne.Interfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class LoginCommand : ICommand
    {
        public ILoginViewModel ViewModel { get; set; }
        public IUserViewModel User { get; set; }
        private ILoginService _loginService;
        private IDisplayService _displayService;

        //public LoginCommand(ILoginViewModel loginWindowViewModel)
        public LoginCommand(ILoginService loginService, IDisplayService displayService)
        {
            //_viewModel = loginWindowViewModel;
            //_user = user;
            _loginService = loginService;
            _displayService = displayService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("Need object as parameter with values username and password.");
            try
            {
                //get parameters
                object[] values = (object[])parameter;

                //get username
                TextBox usernameTextBlock = (TextBox)values[0];
                string username = usernameTextBlock.Text;
                //get password
                PasswordBox passwordBox = (PasswordBox)values[1];
                string password = passwordBox.Password;

                //_viewModel.Login(username, password);
                if (User == null)
                    throw new NullReferenceException("User cannot be null");

                User.Username = username;
                User.Password = password;

                if(!User.IsValidProperty("Password") || !User.IsValidProperty("Username"))
                {
                    _displayService.MessageDisplay("Invalid input");
                }
                else
                {
                    User userLogin = _loginService.Login(User);

                    if (userLogin == null)
                    {
                        _displayService.MessageDisplay("Invalid login");
                    }
                    else
                    {

                        userLogin.Password = null;
                        Settings.Default.UserAccount = userLogin;
                        Settings.Default.UserProfile = userLogin.Person;
                        Settings.Default.Save();
                        _displayService.MessageDisplay($"Welcome {userLogin.Person.FullName} using account {userLogin.Username}.");

                        Window mainWindow = _displayService.CreateMainWindow();
                        mainWindow.Show();

                        if(ViewModel != null)
                            ViewModel.Window.Close();
                    }
                }

            }
            catch (Exception)
            {
                //perhaps log something here.
                throw;
            }
        }
    }
}
