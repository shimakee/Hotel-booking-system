using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public enum TransactionStatus : byte
    {
        open,
        closed,
        cancelled,
        voided
    }
    public interface ITransaction : IBaseObservableModel<Transaction>
    {
        //List<Room> Rooms { get; set; } // rooms being occupied
        List<Booking> Bookings { get; set; } // or put it in booking as well
        Customer Customer { get; set; }
        User Employee { get; set; }
        TransactionStatus Status { get; set; }
    }
}
