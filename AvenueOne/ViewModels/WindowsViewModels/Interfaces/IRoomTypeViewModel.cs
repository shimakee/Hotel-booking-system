using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IRoomTypeViewModel : IBaseObservableViewModel<RoomType>
    {
        //BaseClassCommand<RoomType> EditRoomTypeCommand { get; set; }
        //BaseClassCommand<RoomType> RemoveRoomTypeCommand { get; set; }
        BaseWindowCommand OpenRoomTypeWindowCommand { get; set; }
        BaseWindowCommand OpenAmenitiesListWindowCommand {get; set;}
        DetachAmenityCommand DetachAmenityCommand { get; set; }
        //IRoomType RoomType { get; set; }
        //IRoomType RoomTypeSelected { get; set; }
        IAmenities AmenitiesSelected { get; set; }
        ObservableCollection<RoomType> RoomTypesList { get; set; }
    }
}
