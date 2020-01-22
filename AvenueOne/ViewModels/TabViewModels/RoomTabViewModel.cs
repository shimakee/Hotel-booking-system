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
        private IAmenitiesViewModel _amenitiesViewModel;

        public IAmenitiesViewModel AmenitiesViewModel
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
        #endregion

        #region Constructors
            public RoomTabViewModel(IAmenitiesViewModel amenitiesViewModel,
                                                    IRoomTypeViewModel roomTypeViewModel)
        {
                this.RoomTypeViewModel = roomTypeViewModel;
                this.AmenitiesViewModel = amenitiesViewModel;
            }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
