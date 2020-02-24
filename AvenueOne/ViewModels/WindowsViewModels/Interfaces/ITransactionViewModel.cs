using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
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
        DateTime CurrentDateViewed { get; set; }
        //List<DateTime> DatesInAMonth { get; set; }
        //Tuple<List<Room>, Dictionary<string, List<bool>>> OccupancyList { get; }
        //Dictionary<Room, List<bool>> OccupancyList { get; }
        Dictionary<Room, List<Occupancy>> OccupancyList { get; }
        ICommand GetAvailableRoomsInTransactionCommand { get; set; }
        ICommand OpenCustomerWindowCommand { get; set; }
        ICommand AddBookingCommand { get; set; }
        ICommand RemoveBookingCommand { get; set; }
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
