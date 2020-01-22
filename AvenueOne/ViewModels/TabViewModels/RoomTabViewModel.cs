using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class RoomTabViewModel : AccountViewModel, IRoomTabViewModel, INotifyPropertyChanged
    {

        #region Properties
        public ObservableCollection<RoomType> RoomTypesList { get; set; }
        public ObservableCollection<Amenities> AmenitiesList { get; set; }
        public OpenRoomTypeWindowCommand OpenRoomTypeWindowCommand { get; set; }
        public RemoveRoomTypeCommand RemoveRoomTypeCommand { get; set; }
        public EditRoomTypeCommand EditRoomTypeCommand { get; set; }
        public OpenAmenitiesWindowCommand OpenAmenitiesWindowCommand { get; set; }
        public RemoveAmenitiesCommand RemoveAmenitiesCommand { get; set; }
        public EditAmenitiesCommand EditAmenitiesCommand { get; set; }
        private IRoomType _roomTypeSelected;
        public IRoomType RoomTypeSelected
        {
            get { return _roomTypeSelected; }
            set { _roomTypeSelected = value;
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
                    if (value != null)
                {
                    //Debug.WriteLine($"RoomType: {value.Name.ToString()}");
                    RoomTypeSelected = value.CopyPropertyValues();
                }
                     OnPropertyChanged();
                    OnPropertyChanged("RoomTypeSelected");
            }
            }

        private IAmenities _amenitiesSelected;
        public IAmenities AmenitiesSelected
        {
            get { return _amenitiesSelected; }
            set {
                _amenitiesSelected = value;
                OnPropertyChanged();
            }
        }

        private IAmenities _amenities;
            public IAmenities Amenities
            {
                get { return _amenities; }
                set
                {

                _amenities = value;
                    if (value != null)
                {
                    //Debug.WriteLine($"Amenities: {value.Name.ToString()}");
                    AmenitiesSelected = value.CopyPropertyValues();
                }
                    OnPropertyChanged();
                    OnPropertyChanged("AmenitiesSelected");
            }
            }
        #endregion

        #region Constructors

            public RoomTabViewModel(IAmenities amenities, IAmenities amenitiesSelected, IRoomType roomType, IRoomType roomTypeSelected, 
                ObservableCollection<Amenities> amenitiesList, ObservableCollection<RoomType> roomTypesList,
                OpenAmenitiesWindowCommand openAmenitiesWindowCommand,
                EditAmenitiesCommand editAmenitiesCommand, RemoveAmenitiesCommand removeAmenitiesCommand,
                OpenRoomTypeWindowCommand openRoomTypeWindowCommand,
                EditRoomTypeCommand editRoomTypeCommand, RemoveRoomTypeCommand removeRoomTypeCommand)
            {
                this.Amenities = amenities;
                this.AmenitiesSelected = amenitiesSelected;
                this.RoomType = roomType;
                this.RoomTypeSelected = roomTypeSelected;
                this.AmenitiesList = amenitiesList;
                this.RoomTypesList = roomTypesList;
                this.OpenAmenitiesWindowCommand = openAmenitiesWindowCommand;
                this.OpenAmenitiesWindowCommand.ViewModel = this;
                this.EditAmenitiesCommand = editAmenitiesCommand;
                this.EditAmenitiesCommand.ViewModel = this;
                this.RemoveAmenitiesCommand = removeAmenitiesCommand;
                this.RemoveAmenitiesCommand.ViewModel = this;
                this.OpenRoomTypeWindowCommand = openRoomTypeWindowCommand;
                this.OpenRoomTypeWindowCommand.ViewModel = this;
                this.EditRoomTypeCommand = editRoomTypeCommand;
                this.EditRoomTypeCommand.ViewModel = this;
                this.RemoveRoomTypeCommand = removeRoomTypeCommand;
                this.RemoveRoomTypeCommand.ViewModel = this;
            }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
