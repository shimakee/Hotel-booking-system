using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public interface ITransaction : IBaseObservableModel<Transaction>
    {
        List<Booking> Bookings { get; set; } // or put it in booking as well
        Customer Customer { get; set; }
        User Employee { get; set; }
    }
}
