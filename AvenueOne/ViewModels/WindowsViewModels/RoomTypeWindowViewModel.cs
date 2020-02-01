using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
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
    public class RoomTypeWindowViewModel : WindowViewModel, IRoomTypeWindowViewModel
    {
        public AddRoomTypeCommand AddRoomTypeCommand { get; set; }
        public IRoomType RoomType { get; set; }

        public RoomTypeWindowViewModel(Window window, BaseWindowCommand closeWindowCommand, IRoomType roomType, AddRoomTypeCommand addRoomTypeCommand)
            :base(window, closeWindowCommand)
        {
            this.RoomType = roomType;
            this.AddRoomTypeCommand = addRoomTypeCommand;
            this.AddRoomTypeCommand.ViewModel = this;
        }
    }
}
