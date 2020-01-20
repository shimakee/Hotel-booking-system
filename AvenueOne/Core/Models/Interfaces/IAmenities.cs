using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public interface IAmenities : IBaseObservableModel
    {
        string Id { get; set; }
        string Name { get; set; }
        string Details { get; set; }
        IList<RoomType> RoomTypes { get; set; }
        IAmenities CopyPropertyValues();
        IAmenities CopyPropertyValuesTo(IAmenities amenities);
    }
}
