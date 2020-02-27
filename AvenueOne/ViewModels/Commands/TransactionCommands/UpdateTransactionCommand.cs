using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.TransactionCommands
{
    public class UpdateTransactionCommand :  UpdateClassCommand<Transaction>, IBaseClassCommand<Transaction>
    {
        public UpdateTransactionCommand(IGenericUnitOfWork<Transaction> genericUnitOfWork, 
                                                                    IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
        {
        }

        protected override async Task<int> Update()
        {
            Transaction model = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(Transaction)].Get(ViewModel.Model.Id));
            if (model == null)
                throw new InvalidOperationException("Invalid, model does not exist.");
            ViewModel.ModelSelected.Id = model.Id;
            ViewModel.ModelSelected.DateAdded = model.DateAdded;

            //so that no floating transaction exist without booking.
            if (ViewModel.ModelSelected.Bookings == null || ViewModel.ModelSelected.Bookings.Count <= 0)
            {
                _genericUnitOfWork.Repositories[typeof(Transaction)].Remove(model);
            }
            else
            {
                //foreach (var booking in ViewModel.ModelSelected.Bookings)=
                //{
                    //foreach (var item in model.Bookings)
                    //{
                        //if (booking.Id == item.Id && !booking.Equals(item)){
                        //    booking.DateModified = DateTime.Now;
                        //    booking.DateAdded = item.DateAdded;
                        //    booking.DeepCopyTo(item);
                        //}
                    //}

                        //Booking book = await Task.Run(()=> _genericUnitOfWorkBooking.Repositories[typeof(Booking)].GetAsync(booking.Id));
                        //booking.DateModified = DateTime.Now;
                        //booking.DateAdded = book.DateAdded;
                        //booking.DeepCopyTo(book);
                //}
                ViewModel.ModelSelected.DeepCopyTo(model);
            }

            //int n = await Task.Run(() => _genericUnitOfWorkBooking.CompleteAsync());
            //if (n <= 0)
            //    throw new InvalidOperationException("Could not update model.");

            return await Task.Run(() => _genericUnitOfWork.CompleteAsync());
        }
    }
}
