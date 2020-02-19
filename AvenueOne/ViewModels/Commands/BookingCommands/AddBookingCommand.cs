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
    public class AddBookingCommand : ICommand
    {
        public ITransactionViewModel ViewModel { get; set; }
        private IDisplayService _displayService;
        public AddBookingCommand(IDisplayService displayService)
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
                if(ViewModel.BookingViewModel.ModelSelected.Room == null)
                    throw new ValidationException("Room must be selected.");

                foreach (var item in ViewModel.BookingViewModel.ModelList)
                {
                    if (ViewModel.BookingViewModel.ModelSelected.IsBookingConflict(item))
                        throw new InvalidOperationException("There is a conflict in booking");
                }

                foreach (var item in ViewModel.Bookings)
                {
                    if(ViewModel.BookingViewModel.ModelSelected.IsBookingConflict(item))
                        throw new InvalidOperationException("There is a conflict in booking");
                }


                ViewModel.Bookings.Add(ViewModel.BookingViewModel.ModelSelected);

                ViewModel.BookingViewModel.ClearClassCommand.Execute(null);
                ViewModel.BookingViewModel.RoomsAvailable = ViewModel.GetAvailableRooms();
            }
            catch(ValidationException valEx)
            {
                _displayService.MessageDisplay(valEx.Message, "Validation exception.");
            }
            catch(InvalidOperationException inEx)
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
