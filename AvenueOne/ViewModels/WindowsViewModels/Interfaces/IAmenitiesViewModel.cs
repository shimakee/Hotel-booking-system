using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
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
    public interface IAmenitiesViewModel : IAccountViewModel
    {
        OpenAmenitiesWindowCommand OpenAmenitiesWindowCommand { get; set; }
        RemoveAmenitiesCommand RemoveAmenitiesCommand { get; }
        EditAmenitiesCommand EditAmenitiesCommand { get; }
        IAmenities Amenities { get; set; }
        IAmenities AmenitiesSelected { get; set; }
        ObservableCollection<Amenities> AmenitiesList { get; set; }
    }
}
