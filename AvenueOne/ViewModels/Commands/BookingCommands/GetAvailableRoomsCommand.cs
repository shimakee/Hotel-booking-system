using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.BookingCommands
{
    public class GetAvailableRoomsCommand : ICommand
    {
        public IBookingViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (ViewModel != null)
            //    return ViewModel.UserAccount.IsAdmin;
            //return false;
            return true;
        }

        public void Execute(object parameter)
        {
            if(ViewModel != null)
            {
                ViewModel.RoomsAvailable = ViewModel.GetAvailableRooms();
            }
        }
    }
}
