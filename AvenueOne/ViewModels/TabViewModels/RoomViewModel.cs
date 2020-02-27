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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class RoomViewModel : BaseObservableViewModel<Room>, IRoomViewModel
    {
        private DateTime _currentDateViewed;
        public DateTime CurrentDateViewed
        {
            get { return _currentDateViewed; }
            set
            {
                _currentDateViewed = value;
                if (value != null)
                {
                    if (value.Year != _currentDateViewed.Year || value.Month != _currentDateViewed.Month)
                    {
                        GenerateOccupancyList();
                    }
                }

                OnPropertyChanged();
            }
        }

        private List<Occupancy> _occupancyList;
        public List<Occupancy> OccupancyList
        {
            get
            {
                return _occupancyList;
            }
            set
            {
                _occupancyList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RoomType> _roomTypeList;
        public ObservableCollection<RoomType> RoomTypeList
        {
            get { return _roomTypeList; }
            set { _roomTypeList = value;
                OnPropertyChanged();
            }
        }

        #region Constructors
        public RoomViewModel(Room room, ObservableCollection<Room> roomList, ObservableCollection<RoomType> roomTypeList,
                                                BaseClassCommand<Room> createClassCommand,
                                                BaseClassCommand<Room> updateClassCommand,
                                                BaseClassCommand<Room> deleteClassCommand,
                                                ClearClassCommand<Room> clearClassCommand)
            :base(room, roomList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.CurrentDateViewed = DateTime.Now;
            this.RoomTypeList = roomTypeList;
            this.ModelList.CollectionChanged += OnCollectionChanged;
        }
        #endregion

        #region Methods

        private void GenerateOccupancyList()
        {
            var rooms = this.ModelList;
            OccupancyList = Occupancy.GenerateOccupancyList(rooms, CurrentDateViewed);
        }

        public override void OnPropertyChanged([CallerMemberName] string property = "")
        {
            base.OnPropertyChanged(property);

            if (property == nameof(CurrentDateViewed))
            {
                GenerateOccupancyList();
            }
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GenerateOccupancyList();
        }

        #endregion
    }
}
