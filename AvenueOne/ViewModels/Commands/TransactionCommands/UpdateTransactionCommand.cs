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
        private IGenericUnitOfWork<Booking> _genericUnitOfWorkBooking { get; set; }
        public UpdateTransactionCommand(IGenericUnitOfWork<Transaction> genericUnitOfWork, 
                                                                    IGenericUnitOfWork<Booking> genericUnitOfWorkBooking,
                                                                    IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
        {
            this._genericUnitOfWorkBooking = genericUnitOfWorkBooking;
        }

        protected override async Task<int> Update()
        {
            ITransactionViewModel viewModel = ViewModel as ITransactionViewModel;
            //if (viewModel.BookingViewModel.Model != null && viewModel.BookingViewModel.ModelSelected != null)
            //    viewModel.BookingViewModel.UpdateClassCommand.Execute(null);
            Booking modelBooking = await Task.Run(() => _genericUnitOfWorkBooking.Repository.GetAsync(viewModel.BookingViewModel.Model.Id));
            if (modelBooking == null)
                throw new InvalidOperationException("Invalid, model does not exist.");
            viewModel.BookingViewModel.ModelSelected.Id = modelBooking.Id;
            viewModel.BookingViewModel.ModelSelected.DateModified = DateTime.Now;
            viewModel.BookingViewModel.ModelSelected.DateAdded = modelBooking.DateAdded;
            viewModel.BookingViewModel.ModelSelected.DeepCopyTo(modelBooking);
            int n = await Task.Run(()=> _genericUnitOfWorkBooking.CompleteAsync());

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
                ViewModel.ModelSelected.DeepCopyTo(model);
            }

            return await Task.Run(() => _genericUnitOfWork.CompleteAsync());
        }
    }
}
