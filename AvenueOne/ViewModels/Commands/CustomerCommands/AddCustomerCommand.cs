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
    public class AddCustomerCommand : BaseClassCommand<Customer>, IBaseClassCommand<Customer>
    {

        #region Properties
        //public ICustomerViewModel ViewModel { get; set; }
            //private IGenericUnitOfWork<Customer> _genericUnitOfWork;
            //private IDisplayService _displayService;

        #endregion

        #region Constructors
        public AddCustomerCommand(IGenericUnitOfWork<Customer> genericUnitOfWork, IDisplayService displayService)
            : base(genericUnitOfWork, displayService)
        {
            //this._genericUnitOfWork = genericUnitOfWork;
            //this._displayService = displayService;
        }
        #endregion

        //public event EventHandler CanExecuteChanged;

        //public bool CanExecute(object parameter)
        //{
        //    if (ViewModel != null)
        //        return ViewModel.UserAccount.IsAdmin;
        //    return false;
        //}
        public override async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new NullReferenceException("View model must not be null in order to execute command.");
                if (ViewModel.Model == null)
                    throw new NullReferenceException("No model to update.");
                if (ViewModel.ModelSelected == null)
                    throw new NullReferenceException("No model selected to update.");
                if (ViewModel.ModelSelected.Person == null)
                    throw new NullReferenceException("No profile to update.");

                //ICustomer customer = await Task.Run(()=>_unitOfWork.Customers.GetAsync(ViewModel.Customer.Id));

                if (!ViewModel.ModelSelected.Person.IsValid || !ViewModel.ModelSelected.IsValid)
                    throw new InvalidOperationException("Invalid profile entry.");

                Customer customer = await Task.Run(()=> _genericUnitOfWork.Repositories[typeof(Customer)].Find(c => c.Id == ViewModel.ModelSelected.Id || c.Person.FullName == ViewModel.ModelSelected.Person.FullName).FirstOrDefault());
                if (customer != null)
                    throw new InvalidOperationException("Profile information and customer id already exist.");

                //ViewModel.ModelSelected.Id = ViewModel.Model.Id;
                //ViewModel.ModelSelected.Person = ViewModel.Profile as Person;
                ViewModel.ModelSelected.Person.Customer = ViewModel.ModelSelected; // to correct conflicting references
                _genericUnitOfWork.Repositories[typeof(Customer)].Add(ViewModel.ModelSelected);
                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

                if (n == 0)
                    throw new InvalidOperationException($"Info: Could not add {ViewModel.ModelSelected.Person.FullName}.");

                _displayService.MessageDisplay($"Added customer:\nFull name: {ViewModel.ModelSelected.Person.FullName}\nAffected rows:{n}.",
                                                        "Customer added.");
            }
            catch(InvalidOperationException inEx)
            {
                _displayService.ErrorDisplay(inEx.Message, "Invalid Operation Error");
                //throw;
            }
            catch(NullReferenceException nullEx)
            {
                _displayService.ErrorDisplay(nullEx.Message, "Null reference error.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
