using AvenueOne.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class Occupancy
    {
        public DateTime Date { get; set; }
        public RoomStatus RoomStatus { get; set; }
        public Occupancy(DateTime date, RoomStatus roomStatus)
        {
            this.Date = date;
            this.RoomStatus = roomStatus;
        }
    }
}
