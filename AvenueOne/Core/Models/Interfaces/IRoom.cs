using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public enum RoomStatus
    {
        booked, //reserved
        occupied, // checkedin
        vacant //free
    }
    public interface IRoom : IBaseObservableModel<Room>
    {
        string Name { get; set; }
        int Floor { get; set; }
        int MaxOccupants { get; set; }
        //string Details { get; set; }
        RoomType RoomType { get; set; }
        RoomStatus GetRoomStatus(DateTime date);
        //RoomStatus GetRoomStatus(DateTime date, ICollection<Booking> bookings);
        //bool IsRoomAvailableOnDate(DateTime date);
        List<RoomStatus> GetAvailabilityForMonth(int year, int month);
    }
}
