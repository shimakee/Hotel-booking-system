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

namespace AvenueOne.ViewModels.Commands.CustomerCommands
{
    public class AddCustomerCommand : ICommand
    {

        #region Properties
            public ICustomerWindowViewModel ViewModel { get; set; }
            private IUnitOfWork _unitOfWork;
            private IDisplayService _displayService;
        #endregion

        #region Constructors
        public AddCustomerCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
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

                //ICustomer customer = await Task.Run(()=>_unitOfWork.Customers.GetAsync(ViewModel.Customer.Id));

                if (ViewModel.CustomerProfile.IsValid)
                {
                    Customer customer = ViewModel.Customer as Customer;
                    customer.Person = ViewModel.CustomerProfile as Person;
                    _unitOfWork.Customers.Add(customer);
                    int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                    if (n != 0)
                    {
                        _displayService.MessageDisplay($"Added customer:\nFull name: {customer.Person.FullName}\nAffected rows:{n}.",
                                                        "Customer added.");
                    }
                    else
                    {
                        _displayService.MessageDisplay($"Info: Could not add {customer.Person.FullName}.",
                                                        "Error on insert.");
                    }

                    if (ViewModel != null)
                        ViewModel.Window.Close();
                }
                else
                {
                    _displayService.ErrorDisplay("Invalid profile entry.", "Customer eror!");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
