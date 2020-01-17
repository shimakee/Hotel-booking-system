using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
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
        IAmenities Amenities { get; set; }
        ObservableCollection<Amenities> AmenitiesList { get; set; }
    }
}
