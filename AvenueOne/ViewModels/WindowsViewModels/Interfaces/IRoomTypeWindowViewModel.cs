using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IRoomTypeWindowViewModel : IWindowViewModel
    {
        AddRoomTypeCommand AddRoomTypeCommand { get; }
        IRoomType RoomType { get; set; }
    }
}
