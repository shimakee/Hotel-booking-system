using AvenueOne.Interfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Persistence.Repositories;
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
        public IWindowViewModel ViewModel { get; set; }
        private PlutoContext _plutoContext;

        public RegisterUserCommand(PlutoContext plutoContext)
        {
            if (plutoContext == null)
                throw new ArgumentNullException("Pluto context cannot be null.");

            this._plutoContext = plutoContext;

        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (!_viewModel.UserAccount.IsAdmin)
            //    MessageBox.Show("Info: User is not an admin, not allowed to create account;");
            if(ViewModel != null)
                return ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public void Execute(object parameter)
        {
            Window registrationWindow = new RegistrationWindow(_plutoContext);

            if(ViewModel != null)
                registrationWindow.Owner = ViewModel.Window;
            registrationWindow.ShowDialog();
        }
    }
}
