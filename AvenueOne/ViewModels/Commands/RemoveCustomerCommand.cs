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

    public class RemoveCustomerCommand : ICommand
    {

        #region Properties
        public ICustomerViewModel ViewModel { get; set; }
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;
        #endregion

        public RemoveCustomerCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
        {

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
            try
            {
                if (ViewModel.Customer == null || ViewModel.CustomerProfile == null)
                    throw new NullReferenceException("No item selected to remove.");

                if (ViewModel != null && ViewModel.Customer != null)
                {
                    bool confirm = _displayService.MessagePrompt($"Confirm delete on customer {ViewModel.CustomerProfile.FullName}?", "Delete");
                    if (confirm)
                    {
                        Customer customer = _unitOfWork.Customers.Get(ViewModel.Customer.Id);
                        //TODO : improve customer Person relationship view Fluent api
                        //TODO : implement a cascade delte
                        _unitOfWork.Customers.Remove(customer);
                        int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                        if (n == 0)
                        {
                            _displayService.MessageDisplay("Could not remove customer.");
                        }
                        else
                        {
                            //ViewModel.User = new User(); //replace instance deleted with clear slate
                            _displayService.MessageDisplay($"Successfully removed {ViewModel.CustomerProfile.FullName}\nRows affected: {n}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error:");

                throw;
            }
        }
    }
}
