using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class AddUserCommand : ICommand
    {
        private IRegistrationViewModel _viewModel;
        public AddUserCommand(IRegistrationViewModel viewModel)
        {
            this._viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //get user access, if admin allow, if not deny
            if(_viewModel.UserAccount.IsAdmin)
                return _viewModel.UserAccount.IsAdmin;

            MessageBox.Show("User is not allowed, only accounts with admin access are able to execute command. ");
            return false;

        }

        public void Execute(object parameter)
        {
            if (parameter == null)
            throw new NullReferenceException("The object to add cannot be null. Need source window, is admin, username, password, and password confirm as argument.");

            try
            {
                //get parameters
                object[] values = (object[])parameter;
                Window sourceWindow = (Window)values[0];

                CheckBox isAdminCheckBox = (CheckBox)values[1];
                bool isAdmin = (bool)isAdminCheckBox.IsChecked;

                TextBox usernameTextBox = (TextBox)values[2];
                string username = usernameTextBox.Text;

                PasswordBox passwordPasswordBox = (PasswordBox)values[3];
                string password = passwordPasswordBox.Password;

                PasswordBox passwordConfirmPasswordBox = (PasswordBox)values[4];
                string passwordConfirm = passwordConfirmPasswordBox.Password;

                IUser user = new UserModel(username, password, isAdmin);

                this._viewModel.AddUser(sourceWindow, user, passwordConfirm);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
