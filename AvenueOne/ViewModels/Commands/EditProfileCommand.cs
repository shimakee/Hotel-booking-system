using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
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
                return ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public async void Execute(object parameter)
        {
            if (ViewModel == null)
                throw new NullReferenceException("View model must not be null in order to execute command.");

            //User user = this.ViewModel.User.User as User;
            if (ViewModel != null)
            {
                User user = await Task.Run(() => _unitOfWork.Users.GetAsync(ViewModel.Account.Id));
                //User user = await Task.Run(()=> _unitOfWork.Users.Find(u => u.Id == ViewModel.Account.Id).FirstOrDefault());
                if (user == null)
                    throw new NullReferenceException("Account does not exist.");

                string password = user.Password; //conserve password;
                user = ViewModel.User.User as User;
                user.Password = password;
                user.Person = ViewModel.Person.Person as Person;
                //just to be sure that id is not changed - for redundancy,
                user.Id = ViewModel.Account.Id;
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
    }
}
