using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.BookingCommands
{
    public class GetAvailableRoomsInTransactionCommand : ICommand
    {
        public ITransactionViewModel ViewModel { get; set; }
        private IDisplayService _displayService;
        public GetAvailableRoomsInTransactionCommand(IDisplayService displayService)
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
            if (ViewModel != null && ViewModel.BookingViewModel != null)
            {
                ViewModel.BookingViewModel.RoomsAvailable = ViewModel.GetAvailableRooms();
            }
        }
    }
}
