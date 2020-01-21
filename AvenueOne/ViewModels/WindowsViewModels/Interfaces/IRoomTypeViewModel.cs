﻿using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands.UserCommands;
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

        OpenRoomTypeWindowCommand OpenRoomTypeWindowCommand { get; set; }
        IRoomType RoomType { get; set; }
        IRoomType RoomTypeSelected { get; set; }
        ObservableCollection<RoomType> RoomTypesList { get; set; }
    }
}