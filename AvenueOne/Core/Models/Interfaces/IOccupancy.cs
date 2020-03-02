using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public interface IOccupancy : INotifyPropertyChanged
    {
        DateTime Date { get; set; }
        Room Room { get; set; }
        Booking Booking { get; set; }
        RoomStatus RoomStatus { get;}
    }
}
