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
    public class DeleteTransactionCommand : DeleteClassCommand<Transaction>, IBaseClassCommand<Transaction>
    {
        private IGenericUnitOfWork<Booking> _genericUnitOfWorkBooking { get; set; }
        public DeleteTransactionCommand(IGenericUnitOfWork<Transaction> genericUnitOfWork, IGenericUnitOfWork<Booking> genericUnitOfWorkBooking, IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
        {
            this._genericUnitOfWorkBooking = genericUnitOfWorkBooking;
        }

        protected override async Task<int> Delete()
        {
            Transaction model = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(Transaction)].Get(ViewModel.Model.Id));
            if (model == null)
                throw new InvalidOperationException("Invalid, model does not exist.");

            //to remove floating booking when transaction is deleted, also delete the booking.
            List<Booking> bookingList = model.Bookings.ToList();
            foreach (var item in bookingList)
            {
                Booking booking = _genericUnitOfWorkBooking.Repositories[typeof(Booking)].Get(item.Id);
                _genericUnitOfWorkBooking.Repositories[typeof(Booking)].Remove(booking);
            }
            await Task.Run(() => _genericUnitOfWorkBooking.CompleteAsync());

            _genericUnitOfWork.Repositories[typeof(Transaction)].Remove(model);
            int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());


            return n;
        }
    }
}
