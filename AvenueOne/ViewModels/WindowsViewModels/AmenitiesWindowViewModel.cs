using AvenueOne.Core.Models.Interfaces;
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
        public IAmenities Amenities { get; set; }
        public AmenitiesWindowViewModel(Window window, IAmenities amenities)
            :base(window)
        {
            this.Amenities = amenities;
        }
    }
}
