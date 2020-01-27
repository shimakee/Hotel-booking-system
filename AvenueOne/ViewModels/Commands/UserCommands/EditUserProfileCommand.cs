using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.PagesViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AvenueOne.ViewModels.Commands
{
    public class EditUserProfileCommand : ICommand
    {
        public IUserProfileEditViewModel ViewModel { get; set; }
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;

        public EditUserProfileCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
        {
            if (unitOfWork == null || displayService == null)
                throw new ArgumentNullException("Unit of work and display service cannot be null.");
            //this.ViewModel = viewModel;
            this._unitOfWork = unitOfWork;
            this._displayService = displayService;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (ViewModel != null)
            {

                return ViewModel.UserAccount.IsAdmin || ViewModel.Account.Id == ViewModel.UserAccount.Id;
            }

            return false;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new NullReferenceException("View model must not be null in order to execute command.");
                if (ViewModel.User == null || ViewModel.Profile == null)
                    throw new NullReferenceException("No item selected to update.");


                if (!ViewModel.Account.IsValidProperty("Username"))
                    throw new ArgumentException("Invalid entry in username.");
                IUser user = await Task.Run(() => _unitOfWork.Users.GetAsync(ViewModel.User.Id));
                if (user == null)
                    throw new NullReferenceException("Account does not exist.");

                // to retain original password
                string password = user.Password; 
                ViewModel.Account.DeepCopyTo(user as User);
                user.Password = password;
                user.PasswordConfirm = password;

                if (!ViewModel.Profile.IsValid)
                    throw new ArgumentException("Invalid entry on profile");
                ViewModel.Profile.DeepCopyTo(user.Person);

                if (ViewModel.IsPasswordIncluded)
                {
                    //get parameters
                    object[] values = (object[])parameter ?? throw new ArgumentNullException("parameter cannot be null, you need to pass password and password confirmbox");
                    PasswordBox passwordBox = (PasswordBox)values[0];
                    PasswordBox passwordConfirmBox = (PasswordBox)values[1];

                    //get password
                    ViewModel.Account.Password = passwordBox.Password;
                    ViewModel.Account.PasswordConfirm = passwordConfirmBox.Password;

                    if (!ViewModel.Account.IsValidProperty("Password") || !ViewModel.Account.IsValidProperty("PasswordConfirm"))
                        throw new ArgumentException("Invalid entry on password or password confirmation does not match.");

                    user.Password = HashService.Hash(ViewModel.Account.Password);
                    user.PasswordConfirm = user.Password;
                }


                int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                if (n == 0)
                    throw new Exception("Could not edit accout.");
                _displayService.MessageDisplay($"Edited account: {user.Username}.\nAffected rows: {n}.");
            }
            catch(Exception ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error");
            }
        }
    }
}
