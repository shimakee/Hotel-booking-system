using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class RoomViewModel : BaseObservableViewModel<Room>, IRoomViewModel
    {
        public ObservableCollection<RoomType> RoomTypeList { get; set; }

        #region Constructors
        public RoomViewModel(Room room, ObservableCollection<Room> roomList, ObservableCollection<RoomType> roomTypeList,
                                                BaseClassCommand<Room> createClassCommand,
                                                BaseClassCommand<Room> updateClassCommand,
                                                BaseClassCommand<Room> deleteClassCommand,
                                                ClearClassCommand<Room> clearClassCommand)
            :base(room, roomList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.RoomTypeList = roomTypeList;
        }
        #endregion
    }
}
