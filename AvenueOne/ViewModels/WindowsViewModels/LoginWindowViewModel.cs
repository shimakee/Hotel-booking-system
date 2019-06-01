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
    public class LoginWindowViewModel: ILoginViewModel
    {
        private UserModel _user;  // need to interface this
        public ICommand LoginCommand { get; private set; }

        //user this for whatever features, but not login as username is passed as a parameter
        public string Username
        {
            get { return _user.Username; }
            set { _user.Username = value; }
        }

        public LoginWindowViewModel()
        {
            LoginCommand = new LoginCommand(this); //  how to decouple?
        }

        public LoginWindowViewModel(UserModel user)
            :this()
        {
            this._user = user;
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
        }

        public void Login(Window sourceWindow, string username, string password)
        {
            MessageBox.Show($"username:{username}, password:{password}");

            //validate Input here? or at the command level?

            //process Login here
            bool isValidPassword = (String.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password)) ? false : true;

            if (isValidPassword)
            {
                //show main window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                //close source window
                sourceWindow.Close();
            }
        }

    }
}
