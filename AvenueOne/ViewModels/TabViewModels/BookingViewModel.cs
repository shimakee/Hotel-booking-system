using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.BookingCommands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class BookingViewModel : BaseObservableViewModel<Booking>,  IBookingViewModel
    {

        public ICommand GetAvailableRoomsCommand { get; set; }
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
        private ObservableCollection<Room> _roomList;
        public ObservableCollection<Room> RoomList
        {
            get {
                return _roomList;
            }
            set { _roomList = value;
                if (value != null)
                {
                    this.RoomsAvailable = GetAvailableRooms();
                }
                OnPropertyChanged();
            }
        }
        private List<Room> _roomsAvailable;
        public List<Room> RoomsAvailable
        {
            get
            {
                return _roomsAvailable;
            }
            set
            {
                _roomsAvailable = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<RoomType> _roomTypeList;
        public ObservableCollection<RoomType> RoomTypeList

        {
            get { return _roomTypeList; }
            set
            {
                _roomTypeList = value;
                OnPropertyChanged();
            }
        }

        public Room RoomSelected
        {
            get { return _modelSelected.Room; }
            set { _modelSelected.Room = value;
                OnPropertyChanged();
            }
        }

        private RoomType _roomTypeSelected;
        public RoomType RoomTypeSelected
        {
            get { return _roomTypeSelected; }
            set { _roomTypeSelected = value;
                if (value != null)
                {
                    this.RoomsAvailable = GetAvailableRooms();
                }
                OnPropertyChanged();
            }
        }


        public BookingViewModel(Booking booking, ObservableCollection<Booking> bookingsList,
                                                    ObservableCollection<Room> roomList,
                                                    ObservableCollection<RoomType> roomTypeList,
                                                    BaseClassCommand<Booking> createClassCommand,
                                                    BaseClassCommand<Booking> updateClassCommand,
                                                    BaseClassCommand<Booking> deleteClassCommand,
                                                    ClearClassCommand<Booking> clearClassCommand,
                                                    GetAvailableRoomsCommand getAvailableRooms)
            : base(booking, bookingsList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.RoomList = roomList;
            this.RoomTypeList = roomTypeList;
            if (RoomSelected == null && roomList.Count > 0)
                this.RoomSelected = roomList[0];
            if (RoomTypeSelected == null && RoomTypeList.Count > 0)
                this.RoomTypeSelected = RoomTypeList[0];

            getAvailableRooms.ViewModel = this;
            this.GetAvailableRoomsCommand = getAvailableRooms;
            //this.RoomsAvailable = GetAvaliableRooms(this.ModelList.ToList(), this.ModelSelected, roomList.ToList(), RoomTypeSelected);
            this.RoomsAvailable = GetAvailableRooms();

        }

        public List<Room> GetAvailableRooms()
        {
            return GetAvailableRooms(this.ModelList.ToList(), this.ModelSelected, this.RoomList.ToList(), this.RoomTypeSelected);
        }
        public List<Room> GetAvailableRooms(List<Booking> bookingList, Booking currentBooking, List<Room> roomList, RoomType roomTypeSelected)
        {
            if (roomList == null)
                return new List<Room>();
            if (bookingList == null)
                return roomList;
            if (bookingList.Count <= 0)
                return roomList;
            if (currentBooking == null)
                return roomList;

            //be mindful of booking status.
            List<Booking> bookingWithConflict = bookingList.Where(b => b.IsBookingDateConflict(currentBooking) && (b.Status == BookingStatus.reserved || b.Status == BookingStatus.checkedin)).ToList();
            List<Room> roomsToReturn = roomList;

            if(roomTypeSelected != null)
            {
                roomsToReturn = roomList.Where(r => r.RoomType == roomTypeSelected).ToList();
            }

                    if (bookingWithConflict.Count > 0)
                    {
                        //rooms available
                        foreach (var item in bookingWithConflict)
                        {
                            Room room =  roomsToReturn.Find(r => r.Id == item.Room.Id);
                                 roomsToReturn.Remove(room);
                        }
                    }

            return roomsToReturn;
        }
    }
}
