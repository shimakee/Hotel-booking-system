using AvenueOne.Interfaces;
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
        private ILoginViewModel _loginWindowViewModel;

        public LoginCommand(ILoginViewModel loginWindowViewModel)
        {
            _loginWindowViewModel = loginWindowViewModel;
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

                _loginWindowViewModel.Login(username, password);
               
            }
            catch (Exception)
            {
                //perhaps log something here.
                throw;
            }
        }
    }
}
