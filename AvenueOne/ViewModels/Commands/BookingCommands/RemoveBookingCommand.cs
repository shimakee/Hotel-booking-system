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
    public class RemoveBookingCommand : ICommand
    {
        public ITransactionViewModel ViewModel { get; set; }
        private IDisplayService _displayService { get; set; }
        public RemoveBookingCommand(IDisplayService displayService)
        {
            this._displayService = displayService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (ViewModel.BookingViewModel.ModelSelected == null || ViewModel.Bookings == null)
                    throw new InvalidOperationException("There must ba a booking entry.");
                if (!ViewModel.BookingViewModel.ModelSelected.IsValid)
                    throw new ValidationException("Booking must have a valid entry.");
                string id = ViewModel.BookingViewModel.Model.Id;
                ViewModel.Bookings.Remove(ViewModel.BookingViewModel.Model);

                //reassign model - since it will be turned to null when removing it form booking.
                ViewModel.BookingViewModel.Model = ViewModel.BookingViewModel.ModelList.Where(b => b.Id == id).FirstOrDefault();
                //remove booking reference made to room.
                ViewModel.BookingViewModel.Model.Room.Bookings.Remove(ViewModel.BookingViewModel.Model);
                //Delete Booking, 
                //no need to Check if booking belongs to other transaction - since booking should only belong to one transaction
                //only check when transfering booking. to maintain consistency of relationship
                if (ViewModel.BookingViewModel.ModelSelected != null && ViewModel.BookingViewModel.Model != null)
                    ViewModel.BookingViewModel.DeleteClassCommand.Execute(null);


                ViewModel.BookingViewModel.ClearClassCommand.Execute(null);
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
