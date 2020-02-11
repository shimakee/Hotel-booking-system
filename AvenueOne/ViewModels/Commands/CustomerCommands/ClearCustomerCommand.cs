using AvenueOne.Core;
using AvenueOne.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands.ClassCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.CustomerCommands
{
    public class ClearCustomerCommand : ClearClassCommand<Customer>
    {
        public ClearCustomerCommand()
            :base()
        //public ClearCustomerCommand(IGenericUnitOfWork<Customer> genericUnitOfWork, IDisplayService displayService)
        //: base(genericUnitOfWork, displayService)
        {

        }

        //public override void Execute(object parameter)
        //{
        //    try
        //    {
        //        if (this.ViewModel == null)
        //            throw new NullReferenceException("Viewmodel cannot be null.");
        //        //if (this.ViewModel.Model == null || this.ViewModel.ModelSelected == null)
        //        //    throw new NullReferenceException("Model or Selection cannot be null.");
                
        //    }
        //    catch (Exception exception)
        //    {
        //        //TODO: create logger
        //        throw;
        //    }
        //}

        protected override void Clear()
        {
            Customer customer = new Customer();
            customer.Person = new Person();
            ViewModel.ModelSelected = customer;
        }
    }
}
