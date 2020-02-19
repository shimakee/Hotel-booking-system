using AvenueOne.Core.Models;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface ITransactionViewModel : IBaseObservableViewModel<Transaction>
    {
        ICommand GetAvailableRoomsInTransactionCommand { get; set; }
        IBookingViewModel BookingViewModel { get; set; }
        ObservableCollection<Booking> Bookings { get; set; }
        Customer CustomerSelected { get; set; }
        ObservableCollection<Customer> CustomerList { get; set; }
        User EmployeeSelected { get; set; }
        ObservableCollection<User> EmployeeList { get; set; }

        List<Room> GetAvailableRooms();
        List<Room> GetAvailableRooms(List<Booking> bookingList, Booking currentBooking, List<Room> roomList, RoomType roomTypeSelected);
    }
}
