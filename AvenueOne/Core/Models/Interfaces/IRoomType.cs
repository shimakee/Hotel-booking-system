using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{

    public enum RateType
    {
        Seconds,
        Minutes,
        Hourly,
        Daily,
        Monthly,
        Yearly
    }
    public interface IRoomType : IBaseObservableModel<RoomType>
    {
        string Name { get; set; }
        string Details { get; set; }
        decimal Rate { get; set; }
        RateType RateType { get; set; }
        IList<Amenities> Amenities { get; set; }
    }
}
