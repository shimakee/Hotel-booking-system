using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Windows;
using System;
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
            if(ViewModel != null)
                return ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public void Execute(object parameter)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(_plutoContext);


            if(ViewModel != null)
            {
                registrationWindow.Owner = ViewModel.Window;
            }

            registrationWindow.ShowDialog();
        }
    }
}
