using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public enum BookingStatus : byte
    {
        reserved,
        cancelled,
        voided,
        checkedin,
        checkedout
    }
    public interface IBooking : IBaseObservableModel<Booking>
    {
        DateTime DateCheckin { get; set; }
        DateTime DateCheckout { get; set; }
        TimeSpan LengthOfStay { get; }
        // we put amount total so that even when you changed the roomtype rate, 
        //the initial rate used for computation when the booking was made will still prevail.
        //unless otherwise edited or changed.
        decimal AmountTotal { get; set; } 
        int Occupants { get; set; }
        BookingStatus Status { get; set; }
        Room Room { get; set; }
        bool IsBookingConflict(Booking booking);
        bool IsBookingConflict(Booking booking, Room room);
        bool IsBookingDateConflict(Booking booking);
        Transaction Transaction { get; set; }

    }
}
