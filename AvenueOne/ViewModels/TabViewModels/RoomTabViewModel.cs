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
        private IBaseObservableViewModel<Amenities> _amenitiesViewModel;
        public IBaseObservableViewModel<Amenities> AmenitiesViewModel
        {
            get { return _amenitiesViewModel; }
            set { _amenitiesViewModel = value;
                OnPropertyChanged();
            }
        }

        private IRoomTypeViewModel _roomTypeViewModel;
        public IRoomTypeViewModel RoomTypeViewModel
        {
            get { return _roomTypeViewModel; }
            set { _roomTypeViewModel = value;
                OnPropertyChanged();
            }
        }

        private IBaseObservableViewModel<Room> _roomViewModel;

        public IBaseObservableViewModel<Room> RoomViewModel
        {
            get { return _roomViewModel; }
            set { _roomViewModel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors
        public RoomTabViewModel(IBaseObservableViewModel<Amenities> amenitiesViewModel,
                                                    IRoomTypeViewModel roomTypeViewModel,
                                                    IBaseObservableViewModel<Room> roomViewModel)
        {
            this.RoomTypeViewModel = roomTypeViewModel;
            this.AmenitiesViewModel = amenitiesViewModel;
            this.RoomViewModel = roomViewModel;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
