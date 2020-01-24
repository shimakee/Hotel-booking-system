using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands.RoomCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IRoomViewModel : IAccountViewModel, INotifyPropertyChanged
    {
        IRoom Room { get; set; }
        IRoom RoomSelected { get; set; }
        ObservableCollection<Room> RoomsList { get; set; }
        RemoveRoomCommand RemoveRoomCommand { get; set; }
    }
}
