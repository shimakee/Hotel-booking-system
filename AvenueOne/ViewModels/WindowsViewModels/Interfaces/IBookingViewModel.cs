using AvenueOne.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IBookingViewModel : IBaseObservableViewModel<Booking>
    {
        //IList<Room> RoomsBooked { get; set; }
        //IList<Booking> BookingsPrepared { get; set; }
        //bool IsBookingEntyValid { get; }
        ICommand GetAvailableRoomsCommand { get; set; }
        Room RoomSelected { get; set; }
        List<Room> RoomsAvailable { get; set; }
        ObservableCollection<Room> RoomList { get; set; }
        RoomType RoomTypeSelected { get; set; }
        ObservableCollection<RoomType> RoomTypeList { get; set; }

        List<Room> GetAvailableRooms();
        List<Room> GetAvailableRooms(List<Booking> bookingList);
        List<Room> GetAvailableRooms(List<Booking> bookingList, Booking currentBooking, List<Room> roomList, RoomType roomTypeSelected);
    }
}
