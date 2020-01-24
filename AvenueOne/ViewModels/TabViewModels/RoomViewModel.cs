using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
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
    public class RoomViewModel : AccountViewModel, IRoomViewModel
    {
        public RemoveRoomCommand RemoveRoomCommand { get; set; }
        public ObservableCollection<Room> RoomsList { get; set; }
        private IRoom _room;
        public IRoom Room
        {
            get { return _room; }
            set { _room = value;
                OnPropertyChanged();
            }
        }

        private IRoom _roomSelected;
        public IRoom RoomSelected
        {
            get { return _roomSelected; }
            set {
                Room = value;
                if(value != null)
                    _roomSelected = value.CopyPropertyValues();
                OnPropertyChanged();
            }
        }

        #region Constructors
        public RoomViewModel(IRoom room, ObservableCollection<Room> roomsList,
                                                RemoveRoomCommand removeRoomCommand)
        {
            this.RoomsList = roomsList;
            this.Room = room;
            this.RemoveRoomCommand = removeRoomCommand;
            this.RemoveRoomCommand.ViewModel = this;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
