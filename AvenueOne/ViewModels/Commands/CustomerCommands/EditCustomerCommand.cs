using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.CustomerCommands
{
    public class EditCustomerCommand : ICommand
    {

        #region Properties
            public ICustomerTabViewModel ViewModel { get; set; }
            private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;
        #endregion

        #region Constructor
            public EditCustomerCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
            //:base(unitOfWork, displayService)
            {
            this._unitOfWork = unitOfWork;
            this._displayService = displayService;
            }
        #endregion


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new NullReferenceException("View model must not be null in order to execute command.");
                if (ViewModel.Customer == null)
                    throw new NullReferenceException("No item selected to update.");
                if (ViewModel.CustomerProfile == null)
                    throw new NullReferenceException("No profile to update.");

                ICustomer customer = await Task.Run(() => _unitOfWork.Customers.GetAsync(ViewModel.Customer.Id));

                if (customer == null)
                    throw new NullReferenceException("Customer does not exist.");
                ViewModel.CustomerProfile.DeepCopyTo(customer.Person);

                int n = await Task.Run(() => _unitOfWork.CompleteAsync());
                if (n == 0)
                {
                    _displayService.MessageDisplay("Could not edit customer.", "Customer edit");
                }
                else
                {
                    _displayService.MessageDisplay($"Edited customer: {customer.Person.FullName}.\nAffected rows: {n}.");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
