using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public interface IRoom : IBaseObservableModel
    {

        string Id { get; set; }
        string Name { get; set; }
        int Floor { get; set; }
        int MaxOccupants { get; set; }
        RoomType RoomType { get; set; }
        IRoom CopyPropertyValues();
        IRoom CopyPropertyValuesTo(IRoom room);
    }
}
