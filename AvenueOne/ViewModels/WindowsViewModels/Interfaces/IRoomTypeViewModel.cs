using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IRoomTypeViewModel : IAccountViewModel
    {
        EditRoomTypeCommand EditRoomTypeCommand { get; set; }
        RemoveRoomTypeCommand RemoveRoomTypeCommand { get; set; }
        OpenRoomTypeWindowCommand OpenRoomTypeWindowCommand { get; set; }
        DetachAmenityCommand DetachAmenityCommand { get; set; }
        OpenAmenitiesListWindowCommand OpenAmenitiesListWindowCommand {get; set;}
        IRoomType RoomType { get; set; }
        IRoomType RoomTypeSelected { get; set; }
        IAmenities AmenitiesSelected { get; set; }
        ObservableCollection<RoomType> RoomTypesList { get; set; }
    }
}
