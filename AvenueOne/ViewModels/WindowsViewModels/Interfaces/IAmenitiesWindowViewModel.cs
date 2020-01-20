using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IAmenitiesWindowViewModel : IWindowViewModel
    {
        AddAmenitiesCommand AddAmenitiesCommand { get; }
        IAmenities Amenities { get; set; }
        //add amenities command
    }
}
