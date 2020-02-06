using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.RoomCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IRoomTabViewModel
    {
        //openamenitieswindowcommand
        Amenities AmenitiesSelected { get; set; }
        IRoomTypeViewModel RoomTypeViewModel { get; set; }
        IBaseObservableViewModel<Amenities> AmenitiesViewModel { get; set; }
        IBaseObservableViewModel<Room> RoomViewModel { get; set; }

    }
}
