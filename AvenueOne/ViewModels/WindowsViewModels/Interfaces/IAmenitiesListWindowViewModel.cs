using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands.RoomCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IAmenitiesListWindowViewModel : IWindowViewModel
    {
        ObservableCollection<Amenities> AmenitiesList { get; set; }
        LinkAmenityToRoomTypeCommand LinkAmenityToRoomTypeCommand { get; set; }
        IRoomType RoomType { get; }
        IAmenities AmenitiesSelected { get; set; }
    }
}
