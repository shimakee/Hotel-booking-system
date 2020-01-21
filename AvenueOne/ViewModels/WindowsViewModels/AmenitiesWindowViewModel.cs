using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class AmenitiesWindowViewModel : WindowViewModel, IAmenitiesWindowViewModel
    {
        public AddAmenitiesCommand AddAmenitiesCommand { get; private set; }
        public IAmenities Amenities { get; set; }

        public AmenitiesWindowViewModel(Window window, IAmenities amenities, AddAmenitiesCommand addAmenitiesCommand)
            :base(window)
        {
            this.Amenities = amenities;
            this.AddAmenitiesCommand = addAmenitiesCommand;
            this.AddAmenitiesCommand.ViewModel = this;
        }
    }
}
