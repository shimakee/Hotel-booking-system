using AvenueOne.Interfaces;
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
                throw new ArgumentNullException("Need Window object as parameter.");
            try
            {
                //get parameters
                object[] values = (object[])parameter;

                //get source window
                Window sourceWindow = (Window)values[0];
                //get username
                TextBlock usernameTextBlock = (TextBlock)values[1];
                string username = usernameTextBlock.Text;
                //get password
                PasswordBox passwordBox = (PasswordBox)values[2];
                string password = passwordBox.Password;

                //process password
                _loginWindowViewModel.Login(sourceWindow, username, password);
            }
            catch (Exception)
            {
                //perhaps log something here.
                throw;
            }
        }
    }
}
