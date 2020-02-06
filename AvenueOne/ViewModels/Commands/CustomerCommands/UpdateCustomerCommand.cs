using AvenueOne.Core;
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
    public class UpdateCustomerCommand : BaseClassCommand<Customer>, IBaseClassCommand<Customer>
    {

        #region Properties
        //    public ICustomerTabViewModel ViewModel { get; set; }
        //    private IUnitOfWork _unitOfWork;
        //private IDisplayService _displayService;
        #endregion

        #region Constructor
            public UpdateCustomerCommand(IGenericUnitOfWork<Customer> genericUnitOfWork, IDisplayService displayService)
            : base(genericUnitOfWork, displayService)
        {
            //this._unitOfWork = unitOfWork;
            //this._displayService = displayService;
            }
        #endregion


        //public event EventHandler CanExecuteChanged;

        //public bool CanExecute(object parameter)
        //{
        //    return true;
        //}

        public override async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new NullReferenceException("View model must not be null in order to execute command.");
                if (ViewModel.Model == null || ViewModel.ModelSelected == null)
                    throw new NullReferenceException("No item selected to update.");
                if (ViewModel.ModelSelected.Person == null)
                    throw new NullReferenceException("No profile to update.");

                Customer customer = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(Customer)].GetAsync(ViewModel.Model.Id)) ?? throw new InvalidOperationException("Customer does not exist.");
                ViewModel.ModelSelected.Person.Id = customer.Person.Id; // to retain id
                ViewModel.ModelSelected.Person.Customer = customer; //establish correct relationship
                ViewModel.ModelSelected.Person.DeepCopyTo(customer.Person);

                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());
                if (n <= 0)
                    throw new InvalidOperationException("Could not edit customer.");

                _displayService.MessageDisplay($"Edited customer: {customer.Person.FullName}.\nAffected rows: {n}.");
            }
            catch(NullReferenceException nullEx)
            {
                _displayService.ErrorDisplay(nullEx.Message, "Null reference exception.");
            }
            catch (InvalidOperationException inEx)
            {
                _displayService.ErrorDisplay(inEx.Message, "Invalid Operation exception.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
