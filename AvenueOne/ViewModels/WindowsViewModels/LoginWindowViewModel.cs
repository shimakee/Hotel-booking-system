﻿using AvenueOne.Interfaces;
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

        public void Login(string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException("Username cannot be null.");
            if (password == null)
                throw new ArgumentNullException("Password cannot be null.");

            User.Username = username;
            User.Password = password;

            bool isValidUsername = User.IsValidProperty("Username");
            bool isValidPassword = User.IsValidProperty("Password");

            if (!isValidPassword || !isValidUsername)
            {
                MessageBox.Show("Invalid Input on username or password.");
            }
            else {
                bool isValidLogin = _loginService.Login(username, password);

                if (!isValidLogin)
                {
                    MessageBox.Show("Invalid Input on username or password.");
                }
                else {
                    MessageBox.Show($"Welcome {username}");

                    //show main window
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    //close source window
                    this.Window.Close();
                }
            }
        }
    }
}
