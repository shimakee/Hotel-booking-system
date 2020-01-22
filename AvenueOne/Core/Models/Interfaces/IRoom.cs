using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public enum rateType
    {
        Seconds,
        Minutes,
        Hourly,
        Daily,
        Monthly,
        Yearly
    }
    public interface IRoom : IBaseObservableModel
    {

        string Id { get; set; }
        decimal Rate { get; set; }
        rateType RateType { get; set; }
        string Name { get; set; }
        int Floor { get; set; }
        int MaxOccupants { get; set; }
        RoomType RoomType { get; set; }
    }
}
