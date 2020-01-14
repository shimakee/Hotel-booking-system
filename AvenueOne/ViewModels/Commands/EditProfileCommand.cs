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
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class EditProfileCommand : ICommand
    {
        public IProfileEditViewModel ViewModel { get; set; }
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;

        public EditProfileCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
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

                if (ViewModel.User == null)
                    throw new NullReferenceException("No item selected to update.");

                //User user = this.ViewModel.User.User as User;
                if (ViewModel != null && ViewModel.User != null)
                {

                    IUser user = await Task.Run(() => _unitOfWork.Users.GetAsync(ViewModel.User.Id));
                    //User user = await Task.Run(()=> _unitOfWork.Users.Find(u => u.Id == ViewModel.Account.Id).FirstOrDefault());
                    if (user == null)
                        throw new NullReferenceException("Account does not exist.");

                    string password = user.Password; //conserve hashed password;
                    user = ViewModel.Account.CopyPropertyValues(user);
                    IPerson person = user.Person;
                    person = ViewModel.Profile.CopyPropertyValuesTo(person);
                    user.Password = password;
                    user.PasswordConfirm = password;
                    int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                    if (n == 0)
                    {
                        _displayService.MessageDisplay("Could not edit accout.", "Account edit");
                    }
                    else
                    {
                        _displayService.MessageDisplay($"Edited account: {user.Username}.\nAffected rows: {n}.");
                    }
                }
            }
            catch(Exception ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error");
            }
        }
    }
}
