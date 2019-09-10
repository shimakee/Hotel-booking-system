using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
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
            //get user access based on account
            if (!_viewModel.UserAccount.IsAdmin)
                MessageBox.Show("User is not allowed, only accounts with admin access are able to execute command.");
            return _viewModel.UserAccount.IsAdmin;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (parameter == null)
                throw new NullReferenceException("The object to add cannot be null. Need source window, is admin, username, password, and password confirm as argument.");

                    //get parameters
                    object[] values = (object[])parameter;

                    PasswordBox passwordPasswordBox = (PasswordBox)values[0];
                    string password = passwordPasswordBox.Password;

                    PasswordBox passwordConfirmPasswordBox = (PasswordBox)values[1];
                    string passwordConfirm = passwordConfirmPasswordBox.Password;

                    this._viewModel.AddUser(password, passwordConfirm);

            }
            catch (Exception ex)
            {
                //throw;
                //logg
                MessageBox.Show(ex.Message, "Something went wrong",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
