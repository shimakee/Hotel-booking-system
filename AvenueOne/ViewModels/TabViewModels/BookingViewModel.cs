using AvenueOne.Core.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class BookingViewModel : BaseObservableViewModel<Booking>,  IBookingViewModel
    {
        private Booking _modelSelected;
        public override Booking ModelSelected
        {
            get { return _modelSelected; }
            set
            {
                Model = value;
                if(value != null)
                {
                    _modelSelected = value.Copy();
                    RoomSelected = value.Room;
                }
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Room> RoomList { get; set; }

        public Room RoomSelected
        {
            get { return _modelSelected.Room; }
            set { _modelSelected.Room = value;
                OnPropertyChanged();
            }
        }


        public BookingViewModel(Booking booking, ObservableCollection<Booking> bookingsList,
                                                    ObservableCollection<Room> roomList,
                                                    BaseClassCommand<Booking> createClassCommand,
                                                    BaseClassCommand<Booking> updateClassCommand,
                                                    BaseClassCommand<Booking> deleteClassCommand,
                                                    ClearClassCommand<Booking> clearClassCommand)
            : base(booking, bookingsList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.RoomList = roomList;
        }
    }
}
