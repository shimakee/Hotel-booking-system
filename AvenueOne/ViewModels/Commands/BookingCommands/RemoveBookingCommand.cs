using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.BookingCommands
{
    public class RemoveBookingCommand : BaseClassCommand<Transaction>
    {
        public RemoveBookingCommand(IGenericUnitOfWork<Transaction> genericUnitOfWork, IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            try
            {
                ITransactionViewModel viewModel = ViewModel as ITransactionViewModel;

                if (ViewModel.Model == null || ViewModel.ModelSelected == null)
                    throw new InvalidOperationException("Transaction must be selected.");
                if (viewModel.BookingViewModel.ModelSelected == null || viewModel.Bookings == null)
                    throw new InvalidOperationException("There must ba a booking entry.");
                if (!viewModel.BookingViewModel.ModelSelected.IsValid)
                    throw new ValidationException("Booking must have a valid entry.");
                string bookingModelId = viewModel.BookingViewModel.Model.Id;
                //viewModel.Bookings.Remove(viewModel.BookingViewModel.Model);
                if(viewModel.Model.Bookings.Count <= 1)
                {
                    _genericUnitOfWork.Repository.Remove(viewModel.Model);
                }
                else
                {
                    Transaction transaction = _genericUnitOfWork.Repository.Get(ViewModel.Model.Id);
                    //check if it contains the booking
                    if (!transaction.Bookings.Contains(viewModel.BookingViewModel.Model))
                        throw new InvalidOperationException("Could not remove booking, it did not exist in the selected transaction.");
                    transaction.Bookings.Remove(viewModel.BookingViewModel.Model);
                }

                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

                //reassign model - since it will be turned to null when removing it form booking.
                viewModel.BookingViewModel.ModelSelected = viewModel.BookingViewModel.ModelList.Where(b => b.Id == bookingModelId).FirstOrDefault();
                //remove booking reference made to room.
                viewModel.BookingViewModel.Model.Room.Bookings.Remove(viewModel.BookingViewModel.Model);
                //delete booking
                if (viewModel.BookingViewModel.ModelSelected != null && viewModel.BookingViewModel.Model != null)
                    viewModel.BookingViewModel.DeleteClassCommand.Execute(null);

                viewModel.ClearClassCommand.Execute(null);
                viewModel.BookingViewModel.RoomsAvailable = viewModel.GetAvailableRooms();
            }
            catch (ValidationException valEx)
            {
                _displayService.MessageDisplay(valEx.Message, "Validation exception.");
            }
            catch (InvalidOperationException inEx)
            {
                _displayService.MessageDisplay(inEx.Message, "Invalid operation exception.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
