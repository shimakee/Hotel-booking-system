using AvenueOne.Interfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private IWindowViewModel _viewModel;

        public RegisterUserCommand(IWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (!_viewModel.UserAccount.IsAdmin)
            //    MessageBox.Show("Info: User is not an admin, not allowed to create account;");
            return _viewModel.UserAccount.IsAdmin;
        }

        public void Execute(object parameter)
        {
            Window registrationWindow = new RegistrationWindow();
            registrationWindow.Owner = _viewModel.Window;
            registrationWindow.ShowDialog();
        }
    }
}
