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
    public interface IRoomTypeViewModel : IAccountViewModel
    {
        IRoomType RoomType { get; set; }
        ObservableCollection<RoomType> RoomTypesList { get; set; }
    }
}
