using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class AmenitiesListWindowViewModel : WindowViewModel, IAmenitiesListWindowViewModel
    {
        public ObservableCollection<Amenities> AmenitiesList { get; set; }
        public LinkAmenityToRoomTypeCommand LinkAmenityToRoomTypeCommand { get; set; }
        public IRoomType RoomType { get; set; }
        public IAmenities AmenitiesSelected { get; set; }
        public AmenitiesListWindowViewModel(Window window, ObservableCollection<Amenities> amenitiesList,
                                                                            IRoomType roomType,
                                                                            LinkAmenityToRoomTypeCommand linkAmenityToRoomTypeCommand)
            :base(window)
        {
            this.AmenitiesList = amenitiesList;
            this.RoomType = roomType;
            this.LinkAmenityToRoomTypeCommand = linkAmenityToRoomTypeCommand;
            this.LinkAmenityToRoomTypeCommand.ViewModel = this;
        }
    }
}
