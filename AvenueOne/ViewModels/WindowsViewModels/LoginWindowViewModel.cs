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
    public class LoginWindowViewModel: WindowViewModel, ILoginViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public IUserViewModel User { get;  private set; }
        private ILoginService _loginService;

        #region Ctor
        LoginWindowViewModel(Window window)
            :base(window)
        {
            LoginCommand = new LoginCommand(this); //how to decouple? - also pass as depndency injection?
        }

        public LoginWindowViewModel(Window loginWindow, ILoginService loginService, IUserViewModel userViewModel)
            : this(loginWindow)
        {
            this._loginService = loginService;
            this.User = userViewModel;
        }
        #endregion

        #region methods
        public void Login(string username, string password)
        {
            if (username == null || password == null)
                throw new ArgumentNullException("Username or password cannot be null.");

            User.Username = username;
            User.Password = password;

            bool isValidInput = User.IsValidProperty("Username") && User.IsValidProperty("Password");

            if (!isValidInput)
            {
                MessageBox.Show("Invalid Input on username or password.");
            }
            else {
                bool isValidLogin = _loginService.Login(username, password);

                if (!isValidLogin)
                {
                    MessageBox.Show("Account does not exist.");
                }
                else {
                    MessageBox.Show($"Welcome {username}");

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    this.Window.Close();
                }
            }
        }
        #endregion
    }
}
