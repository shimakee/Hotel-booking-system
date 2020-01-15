using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class RemoveUserCommand : ICommand
    {
        public IUserCrudViewModel ViewModel { get; set; }
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;

        public RemoveUserCommand(IUnitOfWork unitOfWork ,IDisplayService displayService)
        {
            if (unitOfWork == null || displayService == null)
                throw new ArgumentNullException("Arguements cannot be null.");
            _unitOfWork = unitOfWork;
            _displayService = displayService;

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
            try
            {
                if (ViewModel.User == null)
                    throw new NullReferenceException("No item selected to remove.");

                if(ViewModel != null && ViewModel.User != null)
                    {
                    bool confirm = _displayService.MessagePrompt($"Confirm delete on account {ViewModel.User.Username}?", "Delete");
                    if (confirm)
                    {
                        //use person to implement cascade delete view fluent api
                        //Person person = _unitOfWork.People.Get(ViewModel.User.Person.Id);
                        //_unitOfWork.People.Remove(person);

                        /*
                            I used non cascading delete to retain data integrity, so that all info that is link to users transaction will still remain even if user account is deleted.
                         */
                        //use user to implement set null on delete
                        User user = _unitOfWork.Users.Get(ViewModel.User.Id);
                        _unitOfWork.Users.Remove(user);
                        int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                        if(n == 0)
                        {
                            _displayService.MessageDisplay("Could not remove account.");
                        }
                        else
                        {
                            //ViewModel.User = new User(); //replace instance deleted with clear slate
                            _displayService.MessageDisplay($"Successfully removed {ViewModel.User.Username}\nRows affected: {n}.");
                        }
                    }   
                }
            }
            catch(Exception ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error:");

                throw;
            }
        }
    }
}
