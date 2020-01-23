using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
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
    public class RoomTypeViewModel : AccountViewModel, IRoomTypeViewModel, INotifyPropertyChanged
    {
        public EditRoomTypeCommand EditRoomTypeCommand { get; set; }
        public RemoveRoomTypeCommand RemoveRoomTypeCommand { get; set; }
        public OpenRoomTypeWindowCommand OpenRoomTypeWindowCommand { get; set; }
        public DetachAmenityCommand DetachAmenityCommand { get; set; }
        public OpenAmenitiesListWindowCommand OpenAmenitiesListWindowCommand { get; set; }
        public ObservableCollection<RoomType> RoomTypesList { get; set; }
        private IRoomType _roomTypeSelected;
        public IRoomType RoomTypeSelected
        {
            get { return _roomTypeSelected; }
            set
            {
                RoomType = value;
                if (value != null)
                    _roomTypeSelected = value.CopyPropertyValues();
                OnPropertyChanged();
            }
        }

        private IRoomType _roomType;
        public IRoomType RoomType
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                OnPropertyChanged();
            }
        }

        private IAmenities _amenitiesSelected;
        public IAmenities AmenitiesSelected
        {
            get { return _amenitiesSelected; }
            set { _amenitiesSelected = value;
                OnPropertyChanged();
            }
        }


        public RoomTypeViewModel(IRoomType roomType,
                                                    OpenRoomTypeWindowCommand openRoomTypeWindowCommand, 
                                                    EditRoomTypeCommand editRoomTypeCommand, 
                                                    RemoveRoomTypeCommand removeRoomTypeCommand,
                                                    DetachAmenityCommand detachAmenityCommand,
                                                    OpenAmenitiesListWindowCommand openAmenitiesListWindowCommand,
                                                    ObservableCollection<RoomType> roomTypesList)
            : base()
        {
            //this.RoomTypeSelected = roomTypeSelected;
            this.RoomType = roomType;
            this.RoomTypesList = roomTypesList;
            this.OpenRoomTypeWindowCommand = openRoomTypeWindowCommand;
            this.EditRoomTypeCommand = editRoomTypeCommand;
            this.RemoveRoomTypeCommand = removeRoomTypeCommand;
            this.DetachAmenityCommand = detachAmenityCommand;
            this.OpenAmenitiesListWindowCommand = openAmenitiesListWindowCommand;
            this.OpenRoomTypeWindowCommand.ViewModel = this;
            this.EditRoomTypeCommand.ViewModel = this;
            this.RemoveRoomTypeCommand.ViewModel = this;
            this.DetachAmenityCommand.ViewModel = this;
            this.OpenAmenitiesListWindowCommand.ViewModel = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
